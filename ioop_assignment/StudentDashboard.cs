using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ioop_assignment
{
    public partial class StudentDashboard : Form
    {
        public static string userID;
        public static string username;
        public static string name;
        public static string role;
        public StudentDashboard()
        {
            InitializeComponent();
        }

        public StudentDashboard(string id, string uname, string n, string r)
        {
            InitializeComponent();
            userID = id;
            username = uname;
            name = n;
            role = r;
        }

        private void admin_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            lbl_identity.Text = "Welcome back, " + name;
            DateTime loggedInDate = DateTime.Now;
            lbl_loggedintime.Text = "Logged in on: \n" + loggedInDate.ToString();
            lbl_role.Text = "Role: " + role;
            InitialLoad();
            MouseCursorChanged();
            LoadViewScheduleDGV();
            LoadEnrollRequestComboBox();
            LoadRequestIDComboBox();
            LoadViewInvoiceDGV();
        }

        private void InitialLoad()
        {
            panel_updateprofile.Visible = false;
            panel_schedule.Visible = false;
            panel_send.Visible = false;
            panel_delete.Visible = false;
            panel_payment.Visible = false;
            panel_home.Visible = true;
        }

        private void MouseCursorChanged()
        {
            //Base functions
            lbl_home.Cursor = Cursors.Hand;
            lbl_updateprofile.Cursor = Cursors.Hand;
            //
            //student functions
            lbl_viewschedule.Cursor = Cursors.Hand;
            lbl_sendrequest.Cursor = Cursors.Hand;
            lbl_delrequest.Cursor = Cursors.Hand;
            lbl_invpayment.Cursor = Cursors.Hand;
            //
        }

        private void lbl_updateprofile_Click(object sender, EventArgs e)
        {
            panel_updateprofile.Visible = true;
            panel_schedule.Visible = false;
            panel_send.Visible = false;
            panel_delete.Visible = false;
            panel_payment.Visible = false;

            //load viewProfile
            Users obj1 = new Users(username);
            Users.viewProfile(obj1);

            txtbox_name.Text = obj1.Name;
            txtbox_phone.Text = obj1.Phone;
            txtbox_email.Text = obj1.Email;
            //
        }

        private void btn_updateprofile_Click(object sender, EventArgs e)
        {
            Users obj1 = new Users(username);
            MessageBox.Show(obj1.updateProfile(txtbox_name.Text, txtbox_phone.Text, txtbox_email.Text));
        }

        private void lbl_home_Click(object sender, EventArgs e)
        {
            panel_updateprofile.Visible = false;
            panel_schedule.Visible = false;
            panel_send.Visible = false;
            panel_delete.Visible = false;
            panel_payment.Visible = false;
            panel_home.Visible = true;
        }

        private void lbl_viewschedule_Click(object sender, EventArgs e)
        {
            panel_schedule.Visible = true;
            panel_updateprofile.Visible = false;
            panel_send.Visible = false;
            panel_delete.Visible = false;
            panel_payment.Visible = false;
            panel_home.Visible = false;
        }

        private void lbl_sendrequest_Click(object sender, EventArgs e)
        {
            panel_updateprofile.Visible = false;
            panel_schedule.Visible = false;
            panel_send.Visible = true;
            panel_delete.Visible = false;
            panel_payment.Visible = false;
            panel_home.Visible = false;
        }

        private void lbl_delrequest_Click(object sender, EventArgs e)
        {
            panel_updateprofile.Visible = false;
            panel_schedule.Visible = false;
            panel_send.Visible = false;
            panel_delete.Visible = true;
            panel_payment.Visible = false;
            panel_home.Visible = false;
        }

        private void lbl_invpayment_Click(object sender, EventArgs e)
        {
            panel_updateprofile.Visible = false;
            panel_schedule.Visible = false;
            panel_send.Visible = false;
            panel_delete.Visible = false;
            panel_payment.Visible = true;
            panel_home.Visible = false;
        }

        private void LoadViewScheduleDGV()
        {
            Enrollrequest obj1 = new Enrollrequest();
            obj1.Username = username;
            obj1.Loadschedule(dgv_vi_schedule);
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            if (cbox_sendenroll_module.SelectedItem != null && cbox_sendenroll_level.SelectedItem != null)
            {
                string cbox_selectedmodulename = cbox_sendenroll_module.SelectedItem.ToString();
                string cbox_selectedlevelname = cbox_sendenroll_level.SelectedItem.ToString();

                Enrollrequest obj1 = new Enrollrequest(cbox_selectedmodulename);
                MessageBox.Show(obj1.sendRequest(cbox_selectedmodulename, cbox_selectedlevelname, name));
                ReloadRequestIDComboBox();

            }
        }

        private void LoadEnrollRequestComboBox()
        {
            ArrayList module = new ArrayList();

            module = Enrollrequest.viewModule();
            foreach (var item in module)
            {
                cbox_sendenroll_module.Items.Add(item);
            }
            ArrayList level = new ArrayList();
            level = Enrollrequest.viewLevels();
            foreach (var item in level)
            {
                cbox_sendenroll_level.Items.Add(item);
            }
        }

        private void LoadRequestIDComboBox()
        {
            ArrayList request = new ArrayList();
            Enrollrequest obj1 = new Enrollrequest();
            obj1.Username = username;
            request = obj1.viewRequestID();
            foreach (var item in request)
            {
                cbox_deleteenroll_module.Items.Add(item);
            }

        }

        private void ReloadRequestIDComboBox()
        {
            cbox_deleteenroll_module.Items.Clear();
            ArrayList request = new ArrayList();
            Enrollrequest obj1 = new Enrollrequest();
            obj1.Username = username;
            request = obj1.viewRequestID();
            foreach (var item in request)
            {
                cbox_deleteenroll_module.Items.Add(item);
            }
        }

        private void cbox_deleteenroll_module_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string modulename = lbl_module.Text;
            Enrollrequest obj1 = new Enrollrequest(modulename);
            string cbox_deleteenrollmodule = cbox_deleteenroll_module.SelectedItem.ToString();
            int cbox_deleteenroll = int.Parse(cbox_deleteenrollmodule);
            string module = obj1.GetModuleName(cbox_deleteenroll);
            lbl_module.Text = module;

            string levelname = lbl_level.Text;
            string level = obj1.GetLevelName(cbox_deleteenroll);
            lbl_level.Text = level;
        }

        private void LoadViewInvoiceDGV()
        {

            Enrollrequest obj1 = new Enrollrequest();
            obj1.Username = username;
            obj1.Loadinvoice(dgv_vi_invoice);
        }


        private void MakePayment()
        {
            Enrollrequest obj1 = new Enrollrequest();
            obj1.Username = username;
            obj1.UpdatePaymentstatus(dgv_vi_invoice);
            LoadViewInvoiceDGV();

        }


        private void btn_makepayment_Click_1(object sender, EventArgs e)
        {
            MakePayment();
        }

        private void btn_home_logout_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginPanel login = new LoginPanel();
            login.Show();
        }

        private void btn_delete_Click_1(object sender, EventArgs e)
        {
            if (cbox_deleteenroll_module.SelectedItem != null)
            {
                string deleteenrollmodule = cbox_deleteenroll_module.SelectedItem.ToString();
                int deleteenrollID = int.Parse(deleteenrollmodule);
                Enrollrequest obj1 = new Enrollrequest(deleteenrollmodule);
                MessageBox.Show(obj1.DeleteRequest(deleteenrollID));
                ReloadRequestIDComboBox();
                cbox_deleteenroll_module.SelectedIndex = -1;
                cbox_deleteenroll_module.Text = string.Empty;
                lbl_module.Text = null;
                lbl_level.Text = null;
            }
        }
    }
}
