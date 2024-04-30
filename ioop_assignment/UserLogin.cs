using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ioop_assignment
{
    internal class UserLogin
    {
        private string username;
        private string password;
        private static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ToString());

        public string Username { get { return username; } }
        public string Password { get { return password; } }

        public UserLogin(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        
        public string login(string un)
        {
            string status = null;
            con.Open();
            string sqlstring;

            sqlstring = "select count(*) from Users where username='" + username + "' and password = '" + password + "'";

            SqlCommand cmd = new SqlCommand(sqlstring,con);

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            if (count > 0)
            {
                SqlCommand cmd2 = new SqlCommand("select role from Users where username='" + username + "' and password = '" + password + "'", con);
                string userRole = cmd2.ExecuteScalar().ToString();
                SqlCommand cmd3 = new SqlCommand("select name from Users where username='" + username + "' and password = '" + password + "'", con);
                string name = cmd3.ExecuteScalar().ToString();
                SqlCommand cmd4 = new SqlCommand("select userID from Users where username='" + username + "' and password = '" + password + "'", con);
                string userID = cmd4.ExecuteScalar().ToString();

                if (userRole == "admin")
                {
                    AdminDashboard a = new AdminDashboard(userID, un, name, userRole);
                    a.Show();
                }
                else if (userRole == "student")
                {
                    StudentDashboard s = new StudentDashboard(userID, un, name, userRole);
                    s.Show();
                }
                else if (userRole == "trainer")
                {
                    TrainerDashboard t = new TrainerDashboard(userID, un, name, userRole);
                    t.Show();
                }
                else if (userRole == "lecturer")
                {
                    LecturerDashboard l = new LecturerDashboard(userID, un, name, userRole);
                    l.Show();
                }
            }
            else
                status = "Incorrect username or password.";
            con.Close();


            return status;
            }
        }
    }

