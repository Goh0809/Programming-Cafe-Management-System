using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ioop_assignment
{
    internal class Student
    {
        private string username;
        private string name;
        private string tpnumber;
        private string email;
        private string phone;
        private string address;
        private string level_name;
        private string modulename;
        private string monthofenrollment;
        private int levelid;
        private int moduleid;
        private int studentid;
        private static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ToString());

        public string Username { get { return username; } set { username = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string TPNumber { get { return tpnumber; } set { tpnumber = value; } }
        public string Phone { get { return phone; } set { phone = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Address { get { return address; } set { address = value; } }
        public string LevelName { get { return level_name; } set { level_name = value; } }
        public string Modulename { get { return modulename; } set { modulename = value; } }
        public string MonthofEnrollment { get { return monthofenrollment; } set { monthofenrollment = value; } }
        public int LevelID { get { return levelid; } set { levelid = value; } }
        public int ModuleID { get { return moduleid; } set { moduleid = value; } }
        public int StudentID { get { return studentid; } set { studentid = value; } }

        public Student(string username, string name, string tpnumber, string phone, string email, string address)
        {
            this.username = username;
            this.name = name;
            this.tpnumber = tpnumber;
            this.phone = phone;
            this.email = email;
            this.address = address;
        }

        public Student() { }

        public Student(string selectedStudentName)
        {
            this.name = selectedStudentName;
        }

        public string registerStudent()
        {
            string status;
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into Students(username,name,tpnumber,phone,email,address) values(@username, @name, @tpnumber, @phone, @email,@address)", con);
            SqlCommand cmd2 = new SqlCommand("Insert into Users(username,password,role,name,phone,email) values(@username,'123', 'student', @name, @phone, @email)", con);

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@tpnumber", tpnumber);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@address", address);

            cmd2.Parameters.AddWithValue("@username", username);
            cmd2.Parameters.AddWithValue("@name", name);
            cmd2.Parameters.AddWithValue("@phone", phone);
            cmd2.Parameters.AddWithValue("@email", email);

            cmd.ExecuteNonQuery();
            int i = cmd2.ExecuteNonQuery();
            if (i != 0)
                status = "Student Registered";
            else
                status = "Unable to Register Student";
            con.Close();
            return status;
        }
        public static ArrayList viewStudent()
        {
            ArrayList studentnm = new ArrayList();
            con.Open();
            SqlCommand cmd = new SqlCommand("Select DISTINCT name from Students", con);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                studentnm.Add(rd.GetString(0));
            }
            con.Close();
            return studentnm;
        }

        public static ArrayList viewlevelName()
        {
            ArrayList nm = new ArrayList();
            con.Open();
            SqlCommand cmd = new SqlCommand("Select DISTINCT levelname FROM Levels", con);
            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                string levelName = rd.GetString(0);
                nm.Add(levelName);
            }
            con.Close();
            return nm;
        }

        public static ArrayList viewmoduleName()
        {
            ArrayList nm = new ArrayList();
            con.Open();
            SqlCommand cmd = new SqlCommand("Select DISTINCT modulename from Modules", con);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                nm.Add(rd.GetString(0));
            }
            con.Close();
            return nm;
        }

        public string enrollStudent(string studentName, string level_name, string module_name, string monthofenrollment)
        {
            string status;
            int studentid = GetstudentID(studentName);
            int level_id = getLevelIDFromLevel(level_name);
            int module_id = getModuleID(module_name, level_id);
            addToInvoice(studentid, module_id);
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Enrollment (studentID, moduleID, levelID, MonthofEnroll) VALUES(@studentID, @moduleID, @levelID, @monthofenrollment)", con);
            cmd.Parameters.AddWithValue("@studentID", studentid);
            cmd.Parameters.AddWithValue("@moduleID", module_id);
            cmd.Parameters.AddWithValue("@levelID", level_id);
            cmd.Parameters.AddWithValue("@monthofenrollment", monthofenrollment);
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
                status = "Student Enrolled";
            else
                status = "Unable to Enroll Student";
            con.Close();
            return status;
        }

        private int getTrainerID(int module_id)
        {
            int trainerID = 0;
            con.Open();

            SqlCommand cmd = new SqlCommand($"SELECT trainerID FROM TrainerModules WHERE moduleID = '{module_id}'", con);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                trainerID = reader.GetInt32(0);
            }

            reader.Close();
            con.Close();

            return trainerID;
        }

        private int getModuleCharges(int module_id)
        {
            int moduleCharges = 0;
            con.Open();

            SqlCommand cmd = new SqlCommand($"SELECT charges FROM Modules WHERE moduleID = '{module_id}'", con);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                string chargesStr = reader.GetString(0);
                if (int.TryParse(chargesStr, out int chargesValue))
                {
                    moduleCharges = chargesValue;
                }
                else
                {

                }
            }

            reader.Close();
            con.Close();

            return moduleCharges;
        }

        public string addToInvoice(int studentid, int module_id)
        {
            string status;
            int trainerID = getTrainerID(module_id);
            int modulecharges = getModuleCharges(module_id);
            int paymentID = 2;
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Invoice (studentID, trainerID, moduleID, amount, paymentID) VALUES (@studentID, @trainerID, @moduleID, @amount, @paymentID)", con);
            cmd.Parameters.AddWithValue("@studentID", studentid);
            cmd.Parameters.AddWithValue("@trainerID", trainerID);
            cmd.Parameters.AddWithValue("@moduleID", module_id);
            cmd.Parameters.AddWithValue("@amount", modulecharges);
            cmd.Parameters.AddWithValue("@paymentID", paymentID);
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
                status = "Student Enrolled";
            else
                status = "Unable to Enroll Student";
            con.Close();
            return status;
        }

        private int GetstudentID(string studentName)
        {
            int studentID = 0;

            con.Open();
            SqlCommand cmd = new SqlCommand($"SELECT studentID FROM Students WHERE name = '{studentName}' ", con);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                studentID = reader.GetInt32(0);
            }

            reader.Close();
            con.Close();

            return studentID;
        }

        private int getLevelIDFromLevel(string level)
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

        private int getModuleID(string module_name, int levelID)
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


        public static DataTable showEnrollment()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT e.enrollID, s.name AS studentName, m.moduleName, l.levelName FROM Enrollment e JOIN Students s ON e.studentID = s.studentID JOIN Modules m ON e.moduleID = m.moduleID JOIN Levels l ON e.levelID = l.levelID", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            adapter.Fill(dt);
            con.Close();
            return dt;
        }

        public string updateInvoice(int module_id, DataGridView dgv)
        {
            string status;
            int modulecharges = getModuleCharges(module_id);
            int moduleid = getOriginModuleID(dgv);
            string studentName = dgv.SelectedRows[0].Cells["studentName"].Value.ToString();
            int studentid = GetstudentID(studentName);

            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Invoice SET moduleID = @newModuleID, amount = @newAmount WHERE studentID = @studentID AND moduleID = @moduleID;", con);

            cmd.Parameters.AddWithValue("@newModuleID", module_id);
            cmd.Parameters.AddWithValue("@newAmount", modulecharges);
            cmd.Parameters.AddWithValue("@studentID", studentid);
            cmd.Parameters.AddWithValue("@moduleID", moduleid);
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
                status = "Student Enrolled";
            else
                status = "Unable to Enroll Student";
            con.Close();
            return status;
        }


        public int getOriginModuleID(DataGridView dgv)
        {
            int moduleID = 0;

            if (dgv.SelectedRows.Count > 0)
            {
                string moduleName = dgv.SelectedRows[0].Cells["modulename"].Value.ToString();
                string levelName = dgv.SelectedRows[0].Cells["levelname"].Value.ToString();

                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT moduleID FROM Modules WHERE modulename = @moduleName AND levelID = (SELECT levelID FROM Levels WHERE levelname = @levelName)", con);
                cmd.Parameters.AddWithValue("@moduleName", moduleName);
                cmd.Parameters.AddWithValue("@levelName", levelName);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    moduleID = reader.GetInt32(0);
                }

                reader.Close();
                con.Close();
            }

            return moduleID;
        }


        public void updateEnrollment(DataGridView dgv, string level, string module_name)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                int enrollID = Convert.ToInt32(dgv.SelectedRows[0].Cells["enrollID"].Value);
                string studentName = dgv.SelectedRows[0].Cells["studentName"].Value.ToString();
                int levelID = getLevelIDFromLevel(level);
                int moduleID = getModuleID(module_name, levelID);
                int studentid = GetstudentID(studentName);

                updateInvoice(moduleID, dgv);

                if (string.IsNullOrEmpty(module_name) && string.IsNullOrEmpty(level))
                {
                    MessageBox.Show("Please select what you want to update.");
                    return;
                }

                con.Open();
                string updateQuery = "UPDATE Enrollment SET ";

                if ((!string.IsNullOrEmpty(module_name)) && (!string.IsNullOrEmpty(level)))
                {
                    updateQuery += "moduleID = @moduleID, levelID = @levelID ";
                }

                updateQuery += "WHERE enrollID = @enrollID";

                SqlCommand cmd = new SqlCommand(updateQuery, con);
                cmd.Parameters.AddWithValue("@enrollID", enrollID);
                cmd.Parameters.AddWithValue("@moduleID", moduleID);
                cmd.Parameters.AddWithValue("@levelID", levelID);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Enrollment updated successfully.");
                con.Close();

                showEnrollment();
            }
            else
            {
                MessageBox.Show("Please select a row to update.");
            }
        }

        public static DataTable showRequest()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT r.requestID, s.name AS studentName, m.moduleName, l.levelname AS levelname, r.status FROM Request r JOIN Students s ON r.studentID = s.studentID JOIN Modules m ON r.moduleID = m.moduleID JOIN Levels l ON m.levelID = l.levelID WHERE r.status = 'pending'", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            adapter.Fill(dt);
            con.Close();
            return dt;
        }

        public static ArrayList viewRequestID()
        {
            ArrayList nm = new ArrayList();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT requestID FROM Request WHERE status = 'pending'", con);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                nm.Add(rd.GetInt32(0));
            }
            con.Close();
            return nm;
        }


        public string updateRequest(int requestID)
        {
            string status;

            acceptEnrollment(requestID);
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Request SET status = 'approved' WHERE requestID = @requestID", con);
            cmd.Parameters.AddWithValue("@requestID", requestID);
            int updated = cmd.ExecuteNonQuery();
            con.Close();

            if (updated > 0)
                status = "Request Approved";
            else
                status = "Failed. Please try again.";

            return status;
        }


        public void acceptEnrollment(int requestID)
        {
            int studentID = GetStudentIDFromRequest(requestID);
            int moduleID = GetModuleIDFromRequest(requestID);
            int levelID = getLevelID(moduleID);

            addToInvoice(studentID, moduleID);

            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Enrollment (studentID, moduleID, levelID) VALUES (@studentID, @moduleID, @levelID)", con);
            cmd.Parameters.AddWithValue("@studentID", studentID);
            cmd.Parameters.AddWithValue("@moduleID", moduleID);
            cmd.Parameters.AddWithValue("@levelID", levelID);

            cmd.ExecuteNonQuery();
            con.Close();
        }

        private int GetStudentIDFromRequest(int requestID)
        {
            int studentID = 0;
            con.Open();
            SqlCommand cmd = new SqlCommand($"SELECT studentID FROM Request WHERE requestID = '{requestID}' ", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                studentID = reader.GetInt32(0);
            }

            reader.Close();
            con.Close();
            return studentID;
        }

        private int GetModuleIDFromRequest(int requestID)
        {
            int moduleID = 0;
            con.Open();
            SqlCommand cmd = new SqlCommand($"SELECT moduleID FROM Request WHERE requestID = '{requestID}' ", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                moduleID = reader.GetInt32(0);
            }

            reader.Close();
            con.Close();
            return moduleID;
        }

        private int getLevelID(int moduleID)
        {
            int levelID = 0;
            con.Open();
            SqlCommand cmd = new SqlCommand($"SELECT levelID FROM Modules WHERE moduleID = '{moduleID}' ", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                levelID = reader.GetInt32(0);
            }

            reader.Close();
            con.Close();
            return levelID;
        }

        public string deleteRequest(int requestID)
        {
            string status;
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Request SET status = 'declined' WHERE requestID = @requestID", con);
            cmd.Parameters.AddWithValue("@requestID", requestID);
            int deleted = cmd.ExecuteNonQuery();
            con.Close();

            if (deleted > 0)
                status = "Request Declined";
            else
                status = "Failed. Please try again.";
            return status;
        }

        public string deleteStudent(string name)
        {
            string status;
            con.Open();

            SqlCommand getStudentIDCmd = new SqlCommand("SELECT studentID FROM Students WHERE name = @name", con);
            getStudentIDCmd.Parameters.AddWithValue("@name", name);
            int studentID = (int)getStudentIDCmd.ExecuteScalar();

            SqlCommand deleteEnrollmentCmd = new SqlCommand("DELETE FROM Enrollment WHERE studentID = @studentID", con);
            deleteEnrollmentCmd.Parameters.AddWithValue("@studentID", studentID);
            int affectedRowsEnrollment = deleteEnrollmentCmd.ExecuteNonQuery();

            SqlCommand deleteRequestCmd = new SqlCommand("DELETE FROM Request WHERE studentID = @studentID", con);
            deleteRequestCmd.Parameters.AddWithValue("@studentID", studentID);
            int affectedRowsRequest = deleteRequestCmd.ExecuteNonQuery();

            SqlCommand deleteInvoicesCmd = new SqlCommand("DELETE FROM Invoice Where studentID = @studentID", con);
            deleteInvoicesCmd.Parameters.AddWithValue("@studentID", studentID);
            int affectedRowsInvoices = deleteInvoicesCmd.ExecuteNonQuery();

            SqlCommand deleteStudentsCmd = new SqlCommand("DELETE FROM Students WHERE name = @name", con);
            deleteStudentsCmd.Parameters.AddWithValue("@name", name);
            int affectedRowsStudents = deleteStudentsCmd.ExecuteNonQuery();


            SqlCommand deleteUsersCmd = new SqlCommand("DELETE FROM Users WHERE name = @name", con);
            deleteUsersCmd.Parameters.AddWithValue("@name", name);
            int affectedRowsUsers = deleteUsersCmd.ExecuteNonQuery();


            con.Close();

            if (affectedRowsEnrollment > 0 || affectedRowsRequest > 0 || affectedRowsStudents > 0 || affectedRowsUsers > 0 || affectedRowsInvoices > 0)
                status = "Student Deleted";
            else
                status = "Unable to Delete Student";

            return status;
        }
    }
}