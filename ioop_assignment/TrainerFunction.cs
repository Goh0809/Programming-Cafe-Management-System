using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ioop_assignment
{
    internal class TrainerFunction
    {
        private string module_name;
        private string level;
        private double charges;
        private string schedule;
        private string username;
        private static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ToString());

        public string Username { get { return username; } set { username = value; } }
        public string Module_Name { get { return module_name; } set { module_name = value; } }

        public string Level { get { return level; } set { level = value; } }
        public double Charges { get { return charges; } set { charges = value; } }

        public string Schedule { get { return schedule; } set { schedule = value; } }


        public TrainerFunction(string username)
        {
            this.username = username;
        }

        public void sendFeedback(string feedback_content)
        {
            if (string.IsNullOrEmpty(feedback_content))
            {
                MessageBox.Show("Please enter the feedback content.");
                return;
            }
            int trainerID = GetTrainerID(username);
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Feedback (trainerID, feedbackContent) VALUES (@trainerID, @feedbackContent)", con);
            cmd.Parameters.AddWithValue("@trainerID", trainerID);
            cmd.Parameters.AddWithValue("@feedbackContent", feedback_content);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Feedback sent successfully");
            con.Close();
        }

        public ArrayList viewStudentEnrolled(string module_name, string level_name)
        {
            ArrayList studentNames = new ArrayList();
            int levelID = GetLevelID(level_name);
            int moduleID = GetModuleID(module_name, levelID);
            ArrayList studentIDs = getStudentIdEnrolled(moduleID);

            con.Open();
            foreach (int studentID in studentIDs)
            {
                SqlCommand cmd = new SqlCommand("SELECT name FROM Students WHERE studentID = @studentID", con);
                cmd.Parameters.AddWithValue("@studentID", studentID);
                string studentName = cmd.ExecuteScalar()?.ToString();
                if (!string.IsNullOrEmpty(studentName))
                {
                    studentNames.Add(studentName);
                }
            }
            con.Close();
            return studentNames;
        }


        // get the studentID and returned them into 

        private ArrayList getStudentIdEnrolled(int moduleID)
        {
            ArrayList studentIDs = new ArrayList();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT studentID FROM Enrollment WHERE moduleID = @moduleID", con);
            cmd.Parameters.AddWithValue("@moduleID", moduleID);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int studentID = reader.GetInt32(0);
                studentIDs.Add(studentID);
            }
            con.Close();
            return studentIDs;
        }

        // get the students id which are paid for the module
        private ArrayList getStudentIDPaid(int moduleID)
        {
            ArrayList studentIDs = new ArrayList();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT studentID FROM Invoice WHERE (moduleID = @moduleID AND paymentID = 1)", con);
            cmd.Parameters.AddWithValue("@moduleID", moduleID);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int studentID = reader.GetInt32(0);
                studentIDs.Add(studentID);
            }
            con.Close();
            return studentIDs;
        }

        // get the students name which paid for the module
        public ArrayList viewStudentPaid(string module_name, string level_name)
        {
            ArrayList studentNames = new ArrayList();
            int levelID = GetLevelID(level_name);
            int moduleID = GetModuleID(module_name, levelID);
            ArrayList studentIDs = getStudentIDPaid(moduleID);

            con.Open();
            foreach (int studentID in studentIDs)
            {
                SqlCommand cmd = new SqlCommand("SELECT name FROM Students WHERE studentID = @studentID", con);
                cmd.Parameters.AddWithValue("@studentID", studentID);
                string studentName = cmd.ExecuteScalar()?.ToString();
                if (!string.IsNullOrEmpty(studentName))
                {
                    studentNames.Add(studentName);
                }
            }
            con.Close();
            return studentNames;
        }

        // get the students id which are unpaid for the module
        private ArrayList getStudentIDUnPaid(int moduleID)
        {
            ArrayList studentIDs = new ArrayList();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT studentID FROM Invoice WHERE (moduleID = @moduleID AND paymentID = 2)", con);
            cmd.Parameters.AddWithValue("@moduleID", moduleID);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int studentID = reader.GetInt32(0);
                studentIDs.Add(studentID);
            }
            con.Close();
            return studentIDs;
        }

        public ArrayList viewStudentUnPaid(string module_name, string level_name)
        {
            ArrayList studentNames = new ArrayList();
            int levelID = GetLevelID(level_name);
            int moduleID = GetModuleID(module_name, levelID);
            ArrayList studentIDs = getStudentIDUnPaid(moduleID);

            con.Open();
            foreach (int studentID in studentIDs)
            {
                SqlCommand cmd = new SqlCommand("SELECT name FROM Students WHERE studentID = @studentID", con);
                cmd.Parameters.AddWithValue("@studentID", studentID);
                string studentName = cmd.ExecuteScalar()?.ToString();
                if (!string.IsNullOrEmpty(studentName))
                {
                    studentNames.Add(studentName);
                }
            }
            con.Close();
            return studentNames;
        }


        // Get Trainer ID
        private int GetTrainerID(string username)
        {
            int trainerID = 0;
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT trainerID FROM Trainers WHERE userID = (SELECT userID FROM Users WHERE username = @username)", con);
            cmd.Parameters.AddWithValue("@username", username);

            object result = cmd.ExecuteScalar();

            if (result != null && int.TryParse(result.ToString(), out int id))
            {
                trainerID = id;
            }
            con.Close();
            return trainerID;
        }

        // GET LevelID
        private int GetLevelID(string level)
        {
            int levelID = 0;
            con.Open();
            SqlCommand cmd = new SqlCommand($"SELECT levelID FROM Levels WHERE levelname = '{level}'", con);

            object result = cmd.ExecuteScalar();
            if (result != null && int.TryParse(result.ToString(), out int id))
            {
                levelID = id;
            }
            con.Close();
            return levelID;
        }

        private int GetModuleID(string module_name, int levelID)
        {
            int moduleID = 0;
            con.Open();
            SqlCommand cmd = new SqlCommand($"SELECT moduleID FROM Modules WHERE (moduleName = '{module_name}' AND levelID = '{levelID}')", con);

            object result = cmd.ExecuteScalar();

            if (result != null && int.TryParse(result.ToString(), out int id))
            {
                moduleID = id;
            }
            con.Close();
            return moduleID;
        }
    }
}
