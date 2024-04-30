using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ioop_assignment
{
    internal class Trainer
    {
        private string userID;
        private string username;
        private string password;
        private string name;
        private string phone;
        private string email;
        private string modulename;
        private string levelname;
        private string amount;
        private static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ToString());


        public string UserID { get { return userID; } set { userID = value; } }
        public string Username { get { return username; } set { username = value; } }
        public string Password { get { return password; } set { password = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Phone { get { return phone; } set { phone = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Modulename { get { return modulename; } set { modulename = value; } }
        public string Levelname { get { return levelname; } set { levelname = value; } }
        public string Amount { get { return amount; } set { amount = value; } }


        public Trainer(string username, string password, string name, string phone, string email)
        {
            this.username = username;
            this.password = password;
            this.name = name;
            this.phone = phone;
            this.email = email;
        }

        public Trainer(string name)
        {
            this.name = name;
        }

        public Trainer(string name, string modulename, string levelname)
        {
            this.name = name;
            this.modulename = modulename;
            this.levelname = levelname;
        }
        public Trainer(string name, string modulename, string levelname, string amount)
        {
            this.name = name;
            this.modulename = modulename;
            this.levelname = levelname;
            this.amount = amount;
        }

        public Trainer()
        {

        }

        public string addTrainer()
        {
            string status;
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into Users(username,password,role,name,phone,email) values(@username,@password,'trainer',@name,@phone,@email); SELECT SCOPE_IDENTITY()", con);
            SqlCommand cmd2 = new SqlCommand("Insert into Trainers(userid) values(@userID) ", con);

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@email", email);


            int UserID = Convert.ToInt32(cmd.ExecuteScalar());
            cmd2.Parameters.AddWithValue("@userID", UserID);
            int i = cmd2.ExecuteNonQuery();
            if (i != 0)
                status = "Trainer Registered";
            else
                status = "Unable to Register Trainer";
            con.Close();
            return status;
        }

        public static ArrayList viewAll()
        {
            ArrayList nm = new ArrayList();
            con.Open();
            SqlCommand cmd = new SqlCommand("Select name from Users where role = 'trainer'", con);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                nm.Add(rd.GetString(0));
            }
            con.Close();
            return nm;
        }

        public string deleteTrainer(string name)
        {
            string status;
            con.Open();

            SqlCommand cmd = new SqlCommand("Delete from Class WHERE trainerID= (SELECT trainerID FROM Trainers WHERE userID IN (SELECT userID FROM Users WHERE name = @TrainerName))", con);
            SqlCommand cmd2 = new SqlCommand("Delete from Feedback WHERE trainerID= (SELECT trainerID FROM Trainers WHERE userID IN (SELECT userID FROM Users WHERE name = @TrainerName))", con);
            SqlCommand cmd3 = new SqlCommand("DELETE FROM Invoice WHERE trainerID = (SELECT trainerID FROM Trainers WHERE userID IN (SELECT userID FROM Users WHERE name = @TrainerName))", con);
            SqlCommand cmd4 = new SqlCommand("DELETE FROM TrainerModules WHERE trainerID IN (SELECT trainerID FROM Trainers WHERE userID IN (SELECT userID FROM Users WHERE name = @TrainerName));", con);
            SqlCommand cmd5 = new SqlCommand("delete FROM Trainers WHERE userID IN (SELECT userID FROM Users WHERE name = @TrainerName)", con);
            SqlCommand cmd6 = new SqlCommand("delete FROM Users WHERE name = @TrainerName", con);


            cmd.Parameters.AddWithValue("@TrainerName", name);
            cmd2.Parameters.AddWithValue("@TrainerName", name);
            cmd3.Parameters.AddWithValue("@TrainerName", name);
            cmd4.Parameters.AddWithValue("@TrainerName", name);
            cmd5.Parameters.AddWithValue("@TrainerName", name);
            cmd6.Parameters.AddWithValue("@TrainerName", name);
            cmd.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            cmd4.ExecuteNonQuery();
            cmd5.ExecuteNonQuery();

            int i = cmd6.ExecuteNonQuery();
            if (i != 0)
                status = "Trainer Deleted";
            else
                status = "Unable to Delete Trainer";
            con.Close();
            return status;
        }

        public static ArrayList viewModule()
        {
            ArrayList nm = new ArrayList();
            con.Open();
            SqlCommand cmd = new SqlCommand("Select distinct modulename FROM Modules", con);
            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                nm.Add(rd.GetString(0));
            }
            con.Close();
            return nm;
        }

        public static ArrayList viewLevel()
        {
            ArrayList nm = new ArrayList();
            con.Open();
            SqlCommand cmd = new SqlCommand("Select distinct levelname FROM Levels", con);
            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                nm.Add(rd.GetString(0));
            }
            con.Close();
            return nm;
        }

        public string assignTrainer(string name, string modulename, string levelname)
        {
            string status;
            int trainerID = GetTrainerID(name);

            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO TrainerModules(trainerID, moduleID) SELECT @trainerID, m.moduleID FROM Modules m JOIN Levels l ON m.levelID = l.levelID WHERE m.moduleName = @ModuleName AND l.levelName = @LevelName", con);

            cmd.Parameters.AddWithValue("@trainerID", trainerID);
            cmd.Parameters.AddWithValue("@levelName", levelname);
            cmd.Parameters.AddWithValue("@ModuleName", modulename);
            int rowsAffected = cmd.ExecuteNonQuery();
            con.Close();

            if (rowsAffected > 0)
                status = "Trainer Assigned";
            else
                status = "Unable to Assign Trainer";

            return status;
        }

        private int GetTrainerID(string name)
        {
            int trainerID = -1;

            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT trainerID FROM Trainers WHERE userID IN (SELECT userID FROM Users WHERE name = @TrainerName)", con);
            cmd.Parameters.AddWithValue("@TrainerName", name);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                trainerID = reader.GetInt32(0);
            }

            reader.Close();
            con.Close();

            return trainerID;
        }

        public static DataTable Loadincome()
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Invoice", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            con.Close();

            return dataTable;

        }

        public static DataTable LoadFeedback()
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT Feedback.feedbackID, Users.name AS Name, Feedback.feedbackContent FROM Feedback JOIN Trainers ON Feedback.trainerID = Trainers.trainerID JOIN Users ON Trainers.userID = Users.userID", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable feedbackdataTable = new DataTable();
            adapter.Fill(feedbackdataTable);
            con.Close();

            return feedbackdataTable;

        }
        public void DisplayFeedback(DataGridViewRow selectedRow,Label feedbacklabel)
        {
            if (selectedRow != null)
            {
                string feedback = selectedRow.Cells["feedbackContent"].Value?.ToString() ?? string.Empty;
                feedbacklabel.Text = feedback;
            }
        }
    }
}

