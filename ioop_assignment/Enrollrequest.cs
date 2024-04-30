using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ioop_assignment
{
    internal class Enrollrequest
    {
        private string modulename;
        private string levelname;
        private string name;
        private string requestID;
        private string username;
        private string studentID;
        private string invoiceID;
        private static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ToString());


        public string Modulename { get { return modulename; } set { modulename = value; } }
        public string Levelname { get { return levelname; } set { levelname = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string RequestID { get { return requestID; } set { requestID = value; } }
        public string Username { get { return username; } set { username = value; } }
        public string StudentID { get { return studentID; } set { studentID = value; } }

        public string InvoiceID { get { return invoiceID; } set { invoiceID = value; } }
        public Enrollrequest()
        {

        }
        public Enrollrequest(string modulename)
        {
            this.modulename = modulename;
        }
        public Enrollrequest(string modulename, string levelname, string name)
        {
            this.modulename = modulename;
            this.levelname = levelname;
            this.name = name;
        }

        public string sendRequest(string modulename, string levelname, string name)
        {
            string status;
            int studentID = GetStudentID(name);
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO request (studentID, moduleID, status) SELECT @studentID, Modules.moduleID, 'pending' FROM Modules JOIN Levels ON Modules.levelID = Levels.levelID WHERE Modules.modulename = @modulename AND Levels.levelname = @levelname", con);
            cmd.Parameters.AddWithValue("@studentID", studentID);
            cmd.Parameters.AddWithValue("@modulename", modulename);
            cmd.Parameters.AddWithValue("@levelname", levelname);
            int rowsAffected = cmd.ExecuteNonQuery();
            con.Close();

            if (rowsAffected > 0)
                status = "Request sent";
            else
                status = "Unable to send request";

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

        private int GetStudentID(string name)
        {
            int studentID = -1;

            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT studentID FROM Students WHERE name= @name", con);
            cmd.Parameters.AddWithValue("@name", name);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                studentID = reader.GetInt32(0);
            }

            reader.Close();
            con.Close();

            return studentID;
        }

        public static ArrayList viewLevels()
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

        public ArrayList viewRequestID()
        {
            int studentID = getStudentID(username);
            ArrayList nm = new ArrayList();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT requestID FROM Request WHERE studentID = @studentID AND status = 'pending'", con);
            cmd.Parameters.AddWithValue("@studentID", studentID);
            SqlDataReader rd = cmd.ExecuteReader();


            while (rd.Read())
            {
                nm.Add(rd.GetInt32(0));
            }
            con.Close();
            return nm;
        }


        public string DeleteRequest(int requestId)
        {
            string status;
            con.Open();

            SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Request WHERE requestId = @RequestId", con);
            checkCmd.Parameters.AddWithValue("@RequestId", requestId);
            int requestCount = (int)checkCmd.ExecuteScalar();

            if (requestCount == 0)
            {
                con.Close();
                return "Request not found";
            }

            SqlCommand deleteCmd = new SqlCommand("DELETE FROM Request WHERE requestId = @RequestId", con);


            deleteCmd.Parameters.AddWithValue("@RequestId", requestId);

            deleteCmd.ExecuteNonQuery();

            status = "Request deleted successfully";

            con.Close();
            return status;
        }

        public string GetModuleName(int requestID)
        {
            string moduleName = string.Empty;

            con.Open();

            SqlCommand cmd = new SqlCommand($"SELECT modulename FROM Modules WHERE (moduleID = (SELECT moduleID FROM Request WHERE requestID = '{requestID}'))", con);
            /* cmd.Parameters.AddWithValue("@RequestId", requestId);*/

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                moduleName = reader["modulename"].ToString();
            }

            reader.Close();
            con.Close();

            return moduleName;

        }

        public string GetLevelName(int requestID)
        {
            string levelName = string.Empty;

            con.Open();

            SqlCommand cmd = new SqlCommand($"SELECT levelname FROM Levels WHERE levelID = (SELECT levelID FROM Modules WHERE (moduleID = (SELECT moduleID FROM Request WHERE requestID = '{requestID}')))", con);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                levelName = reader["levelname"].ToString();
            }

            reader.Close();
            con.Close();

            return levelName;

        }

        /*   View Invoice*/
        public void Loadinvoice(DataGridView dgv)
        {
            int studentID = getStudentID(username);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT i.invoiceID, s.name AS studentName, t.name AS trainerName, m.modulename, i.amount, p.paymentstatus FROM Invoice AS i JOIN Students AS s ON i.studentID = s.studentID JOIN Trainers AS tr ON i.trainerID = tr.trainerID JOIN Users AS t ON tr.userID = t.userID JOIN Modules AS m ON i.moduleID = m.moduleID JOIN Payment AS p ON i.paymentID = p.paymentID WHERE i.studentID = @studentID", con);
            cmd.Parameters.AddWithValue("@studentID", studentID);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgv.DataSource = dt;
            con.Close();

        }

        public void UpdatePaymentstatus(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                int rowIndex = dgv.SelectedRows[0].Index;
                int invoiceID = Convert.ToInt32(dgv.Rows[rowIndex].Cells["invoiceID"].Value);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Invoice SET paymentID = 1 WHERE invoiceID = @invoiceID", con);
                cmd.Parameters.AddWithValue("@invoiceID", invoiceID);
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();



                if (rowsAffected > 0)
                {
                    dgv.Rows.RemoveAt(rowIndex);
                    MessageBox.Show("Invoice Paid");
                }
                else
                {
                    MessageBox.Show("No matching row found in the database.");
                }
            }
            else
            {
                MessageBox.Show("No row selected");
            }
            Loadinvoice(dgv);
        }



        private int getStudentID(string username)
        {
            int studentID = 0;

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT studentID FROM Students WHERE username = @username", con);
            cmd.Parameters.AddWithValue("@username", username);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                studentID = Convert.ToInt32(reader["studentID"]);
            }

            reader.Close();
            con.Close();

            return studentID;
        }

        public ArrayList viewInvoiceID()
        {
            int studentID = getStudentID(username);
            ArrayList nm = new ArrayList();
            con.Open();
            SqlCommand cmd = new SqlCommand("Select invoiceID from Invoice WHERE studentID=@studentID", con);
            cmd.Parameters.AddWithValue("@studentID", studentID);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                nm.Add(rd.GetInt32(0));
            }
            con.Close();
            return nm;
        }

        /*ViewSchedule*/
        public void Loadschedule(DataGridView dgv)
        {
            int studentID = getStudentID(username);
            ArrayList moduleIDs = getModuleIDEnrolled();
            if (moduleIDs.Count == 0)
            {
                dgv.Columns.Clear();
                dgv.Rows.Clear();
                dgv.Columns.Add("MessageColumn", "Message");
                dgv.Rows.Add("No classes found.");
                return;
            }
            con.Open();

            string query = "SELECT c.classID, m.modulename AS Modulename, l.levelname AS Levelname, u.name AS TrainerName, c.schedule FROM Class AS c " +
                           "JOIN Modules AS m ON c.moduleID = m.moduleID " +
                           "JOIN Trainers AS t ON c.trainerID = t.trainerID " +
                           "JOIN Users AS u ON t.userID = u.userID " +
                           "JOIN Levels AS l ON m.levelID = l.levelID " +
                           "WHERE c.moduleID IN (" + string.Join(",", moduleIDs.ToArray()) + ")";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@studentID", studentID);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dgv.DataSource = dt;

            con.Close();
        }

        private ArrayList getModuleIDEnrolled()
        {
            int studentID = getStudentID(username);
            ArrayList nm = new ArrayList();
            con.Open();
            SqlCommand cmd = new SqlCommand("Select moduleID from Enrollment WHERE studentID=@studentID", con);
            cmd.Parameters.AddWithValue("@studentID", studentID);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                nm.Add(rd.GetInt32(0));
            }
            con.Close();
            return nm;
        }
    }
}
