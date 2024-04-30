using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ioop_assignment
{
    public partial class LoginPanel : Form
    {
        public LoginPanel()
        {
            InitializeComponent();
        }


            private void login_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void login_usernamebox_Click(object sender, EventArgs e)
        {
            login_usernamebox.BackColor = Color.White;
            login_username_panel.BackColor = Color.White;
            login_password_panel.BackColor = SystemColors.Control;
            login_passwordbox.BackColor = SystemColors.Control;

        }

        private void login_passwordbox_Click(object sender, EventArgs e)
        {
            login_usernamebox.BackColor = SystemColors.Control;
            login_username_panel.BackColor = SystemColors.Control;
            login_password_panel.BackColor = Color.White;
            login_passwordbox.BackColor = Color.White;
        }

        private void login_password_icon_MouseDown(object sender, MouseEventArgs e)
        {
            login_passwordbox.UseSystemPasswordChar = false;
        }

        private void login_password_icon_MouseUp(object sender, MouseEventArgs e)
        {
            login_passwordbox.UseSystemPasswordChar = true;
        }

        private void login_btnLogin_Click(object sender, EventArgs e)
        {
            string stat;
            UserLogin obj1 = new UserLogin(login_usernamebox.Text, login_passwordbox.Text);
            stat = obj1.login(login_usernamebox.Text);

            if (stat != null)
            {
                MessageBox.Show(stat);
            }

            login_usernamebox.Text = String.Empty;
            login_passwordbox.Text = String.Empty;

            this.Hide();
        }

        private void login_password_icon_Click(object sender, EventArgs e)
        {

        }
    }
}
