using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ioop_assignment
{
    public partial class AdminDashboard : Form
    {
        public static string userID;
        public static string username;
        public static string name;
        public static string role;
        public AdminDashboard()
        {
            InitializeComponent();
        }

        public AdminDashboard(string id, string uname, string n, string r)
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

        private void FormInitial()
        {
            lbl_identity.Text = "Welcome back, " + name;
            DateTime loggedInDate = DateTime.Now;
            lbl_loggedintime.Text = "Logged in on: \n" + loggedInDate.ToString();
            lbl_role.Text = "Role: " + role;
            txtbox_rt_password.UseSystemPasswordChar = true;
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            FormInitial();
            InitialVisibleItem();
            MouseCursorChanged();
            Loadlistbox();
            LoadAssignTrainerCombobox();
            LoadViewIncomeComboBox();
            LoadViewIncomeDGV();
            LoadViewFeedbackDGV();
        }



        private void InitialVisibleItem()
        {
            panel_home.Visible = true;
            panel_registertrainer.Visible = false;
            panel_updateprofile.Visible = false;
            panel_assigntrainer.Visible = false;
            panel_viewincome.Visible = false;
            panel_viewfeedback.Visible = false;
        }

        private void Loadlistbox()
        {
            ArrayList name = new ArrayList();

            name = Trainer.viewAll();
            foreach (var item in name)
            {
                lstbox_rt_view.Items.Add(item);
            }
        }

        private void Reloadlistbox()
        {
            lstbox_rt_view.Items.Clear();
            Loadlistbox();
        }

        private void LoadAssignTrainerCombobox()
        {
            ArrayList trainername = new ArrayList();

            trainername = Trainer.viewAll();
            foreach (var item in trainername)
            {
                cbox_at_trainer.Items.Add(item);
            }
            ArrayList module = new ArrayList();

            module = Trainer.viewModule();
            foreach (var item in module)
            {
                cbox_at_module.Items.Add(item);
            }
            ArrayList level = new ArrayList();

            level = Trainer.viewLevel();
            foreach (var item in level)
            {
                cbox_at_level.Items.Add(item);
            }
        }

        private void ReloadAssignTrainerCombobox()
        {
            cbox_at_level.Items.Clear();
            cbox_at_module.Items.Clear();
            cbox_at_trainer.Items.Clear();
            LoadAssignTrainerCombobox();
        }

        private void LoadViewIncomeComboBox()
        {
            ArrayList trainername = new ArrayList();

            trainername = Trainer.viewAll();
            foreach (var item in trainername)
            {
                cbox_vi_trainer.Items.Add(item);
            }
            ArrayList module = new ArrayList();

            module = Trainer.viewModule();
            foreach (var item in module)
            {
                cbox_vi_module.Items.Add(item);
            }
            ArrayList level = new ArrayList();

            level = Trainer.viewLevel();
            foreach (var item in level)
            {
                cbox_vi_level.Items.Add(item);
            }
        }

        private void ReloadViewIncomeComboBox()
        {
            cbox_vi_trainer.Items.Clear();
            cbox_vi_module.Items.Clear();
            cbox_vi_level.Items.Clear();
            LoadViewIncomeComboBox();
        }


        private void LoadViewIncomeDGV()
        {
            DataTable dataTable = Trainer.Loadincome();
            dgv_vi_income.DataSource = dataTable;
        }

        private void SearchDGV()
        {
            string trainerName = cbox_vi_trainer.SelectedItem?.ToString();
            string moduleName = cbox_vi_module.SelectedItem?.ToString();
            string levelName = cbox_vi_level.SelectedItem?.ToString();

            List<TrainerDGV> filteredIncome = TrainerDGV.SearchIncomeByFilters(trainerName, moduleName, levelName);

            cbox_vi_trainer.SelectedItem = null;
            cbox_vi_module.SelectedItem = null;
            cbox_vi_level.SelectedItem = null;

            dgv_vi_income.DataSource = filteredIncome;
        }
        private void LoadViewFeedbackDGV()
        {
            DataTable feedbackdataTable = Trainer.LoadFeedback();
            dgv_vf_feedback.DataSource = feedbackdataTable;
        }


        private void viewProfile()
        {
            Users obj1 = new Users(username);
            Users.viewProfile(obj1);

            txtbox_name.Text = obj1.Name;
            txtbox_phone.Text = obj1.Phone;
            txtbox_email.Text = obj1.Email;
        }

        private void updateProfile()
        {
            Users obj1 = new Users(username);
            MessageBox.Show(obj1.updateProfile(txtbox_name.Text, txtbox_phone.Text, txtbox_email.Text));

        }

        private void Registertrainer()
        {
            if (!string.IsNullOrEmpty(txtbox_rt_username.Text) && !string.IsNullOrEmpty(txtbox_rt_password.Text))
            {
                Trainer obj1 = new Trainer(txtbox_rt_username.Text, txtbox_rt_password.Text, txtbox_rt_name.Text, txtbox_rt_phone.Text, txtbox_rt_email.Text);
                MessageBox.Show(obj1.addTrainer());
            }
            else
                MessageBox.Show("Please insert data");

            txtbox_rt_username.Text = null;
            txtbox_rt_password.Text = null;
            txtbox_rt_name.Text = null;
            txtbox_rt_phone.Text = null;
            txtbox_rt_email.Text = null;
        }

        private void Deletetrainer()
        {
            if (lstbox_rt_view.SelectedItem != null)
            {
                string selectedTrainerName = lstbox_rt_view.SelectedItem.ToString();

                Trainer obj1 = new Trainer(selectedTrainerName);
                MessageBox.Show(obj1.deleteTrainer(selectedTrainerName));

                lstbox_rt_view.Items.Remove(lstbox_rt_view.SelectedItem);
            }
            else
                MessageBox.Show("Please select Trainers first");
        }

        private void AssignTrainer()
        {
            if (cbox_at_trainer.SelectedItem != null && cbox_at_module.SelectedItem != null && cbox_at_level.SelectedItem != null)
            {
                string cbox_selectedTrainerName = cbox_at_trainer.SelectedItem.ToString();
                string cbox_selectedModuleName = cbox_at_module.SelectedItem.ToString();
                string cbox_selectedLevelName = cbox_at_level.SelectedItem.ToString();

                Trainer obj1 = new Trainer(cbox_selectedTrainerName, cbox_selectedModuleName, cbox_selectedLevelName);
                MessageBox.Show(obj1.assignTrainer(cbox_selectedTrainerName, cbox_selectedModuleName, cbox_selectedLevelName));

                cbox_at_level.SelectedItem = null;
                cbox_at_module.SelectedItem = null;
                cbox_at_trainer.SelectedItem = null;
            }
            else
            {
                MessageBox.Show("Data not found, Please try again.");
            }
        }

        private void MouseCursorChanged()
        {
            //Base functions
            lbl_home.Cursor = Cursors.Hand;
            lbl_updateprofile.Cursor = Cursors.Hand;
            //
            //admin functions
            lbl_assigntrainer.Cursor = Cursors.Hand;
            lbl_regtrainer.Cursor = Cursors.Hand;
            lbl_viewincome.Cursor = Cursors.Hand;
            lbl_viewfeedback.Cursor = Cursors.Hand;
            //
        }
        private void lbl_regtrainer_Click(object sender, EventArgs e)
        {
            panel_updateprofile.Visible = false;
            panel_registertrainer.Visible = true;
            panel_assigntrainer.Visible = false;
            panel_viewincome.Visible = false;
            panel_viewfeedback.Visible = false;
            panel_home.Visible = false;
        }
        private void lbl_updateprofile_Click(object sender, EventArgs e)
        {

            panel_updateprofile.Visible = true;
            panel_registertrainer.Visible = false;
            panel_assigntrainer.Visible = false;
            panel_viewincome.Visible = false;
            panel_viewfeedback.Visible = false;
            panel_home.Visible = false;
            viewProfile();
        }

        private void btn_updateprofile_Click(object sender, EventArgs e)
        {
            updateProfile();
        }

        private void lbl_home_Click(object sender, EventArgs e)
        {
            panel_updateprofile.Visible = false;
            panel_registertrainer.Visible = false;
            panel_assigntrainer.Visible = false;
            panel_viewincome.Visible = false;
            panel_viewfeedback.Visible = false;
            panel_home.Visible = true;
        }

        private void btn_rt_register_Click(object sender, EventArgs e)
        {
            Registertrainer();
            Reloadlistbox();
            ReloadAssignTrainerCombobox();
        }

        private void btn_rt_deletetrainer_Click(object sender, EventArgs e)
        {
            Deletetrainer();
            ReloadAssignTrainerCombobox();
            ReloadViewIncomeComboBox();
            LoadViewIncomeDGV();
        }

        private void lbl_assigntrainer_Click(object sender, EventArgs e)
        {
            panel_updateprofile.Visible = false;
            panel_registertrainer.Visible = false;
            panel_assigntrainer.Visible = true;
            panel_viewincome.Visible = false;
            panel_viewfeedback.Visible = false;
            panel_home.Visible = false;
        }

        private void btn_at_assign_Click(object sender, EventArgs e)
        {
            AssignTrainer();
            LoadViewIncomeDGV();
        }

        private void lbl_viewincome_Click(object sender, EventArgs e)
        {
            panel_viewincome.Visible = true;
            panel_updateprofile.Visible = false;
            panel_registertrainer.Visible = false;
            panel_assigntrainer.Visible = false;
            panel_viewfeedback.Visible = false;
            panel_home.Visible = false;
            ReloadViewIncomeComboBox();
        }

        private void btn_vi_search_Click(object sender, EventArgs e)
        {
            SearchDGV();
        }

        private void lbl_viewfeedback_Click(object sender, EventArgs e)
        {
            panel_viewincome.Visible = false;
            panel_updateprofile.Visible = false;
            panel_registertrainer.Visible = false;
            panel_assigntrainer.Visible = false;
            panel_viewfeedback.Visible = true;
            panel_home.Visible = false;
        }

        private void btn_home_logout_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginPanel login = new LoginPanel();
            login.Show();
        }

        private void dgv_vf_feedback_SelectionChanged(object sender, EventArgs e)
        {
            Trainer obj1 = new Trainer();
            obj1.Username = username;
            if (dgv_vf_feedback.SelectedRows.Count > 0)
            {
                int rowIndex = dgv_vf_feedback.SelectedRows[0].Index;
                DataGridViewRow selectedRow = dgv_vf_feedback.Rows[rowIndex];

                obj1.DisplayFeedback(selectedRow, lbl_vf_feedbackcontent);
            }
        }
    }
}

    
