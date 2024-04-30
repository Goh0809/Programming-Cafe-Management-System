using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ioop_assignment
{
    internal class Users
    {
        private string username;
        private string name;
        private string phone;
        private string email;
        private static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ToString());

        public string Username { get { return username; } set { username = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Phone { get { return phone; } set { phone = value; } }
        public string Email { get { return email; } set { email = value; } }


        public Users(string username)
        {
            this.username = username;
        }

        public static void viewProfile(Users o1)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Users where username ='" + o1.username + "'", con);
            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                o1.name = rd.GetString(4);
                o1.phone = rd.GetString(5);
                o1.email = rd.GetString(6);
            }
            con.Close();
        }
        public string updateProfile(string n, string num, string em)
        {
            string status;
            con.Open();

            name = n;
            phone = num;
            email = em;

            SqlCommand cmd = new SqlCommand("update Users set name ='" + name + "',phone='" + phone + "',email='" + email + "' where username ='" + username + "'", con);
            int i = cmd.ExecuteNonQuery();
            if (i != 0)
                status = "Update Successfully.";
            else
                status = "Unable to update.";
            con.Close();

            return status;

        }
    }
}
