using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ioop_assignment
{
    public partial class TrainerDashboard : Form
    {
        public static string userID;
        public static string username;
        public static string name;
        public static string role;
        
        public TrainerDashboard()
        {
            InitializeComponent();
        }

        public TrainerDashboard(string id, string uname, string n, string r)
        {
            InitializeComponent();
            userID = id;
            username = uname;
            name = n;
            role = r;
            panel_updateCoachingClass.Visible = false;
            panel_viewStudentEnr.Visible = false;
            panel_sendFeedback.Visible = false;
            panel_addCoachingClass.Visible = false;
            panel_updateprofile.Visible = false;
            panel_home.Visible = true;
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
        }


        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            FormInitial();
            MouseCursorChanged();
            loadComboBox();
            loadDataGridView();
        }

        private void MouseCursorChanged()
        {
            //Base functions
            lbl_home.Cursor = Cursors.Hand;
            lbl_updateprofile.Cursor = Cursors.Hand;
            //
            //trainer functions
            lbl_addcoachclass.Cursor = Cursors.Hand;
            lbl_updatecoachclass.Cursor = Cursors.Hand;
            lbl_viewenroll.Cursor = Cursors.Hand;
            lbl_sendfeedback.Cursor = Cursors.Hand;
            //
        }

       

        private void lbl_updateprofile_Click(object sender, EventArgs e)
        {
            panel_updateCoachingClass.Visible = false;
            panel_viewStudentEnr.Visible = false;
            panel_sendFeedback.Visible = false;
            panel_addCoachingClass.Visible = false;
            panel_updateprofile.Visible = true;
            panel_home.Visible = false;
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
            panel_addCoachingClass.Visible = false;
            panel_updateCoachingClass.Visible = false;
            panel_viewStudentEnr.Visible = false;
            panel_sendFeedback.Visible = false;
            panel_home.Visible = true;
        }

        private void lbl_addcoachclass_Click(object sender, EventArgs e)
        {
            panel_updateprofile.Visible = false;
            panel_updateCoachingClass.Visible = false;
            panel_viewStudentEnr.Visible = false;
            panel_sendFeedback.Visible = false;
            panel_addCoachingClass.Visible = true;
            panel_home.Visible = false;
        }

        private void lbl_updatecoachclass_Click(object sender, EventArgs e)
        {
            panel_updateprofile.Visible = false;
            panel_viewStudentEnr.Visible = false;
            panel_sendFeedback.Visible = false;
            panel_addCoachingClass.Visible = false;
            panel_updateCoachingClass.Visible = true;
            panel_home.Visible = false;
        }

        private void lbl_viewenroll_Click(object sender, EventArgs e)
        {
            panel_updateprofile.Visible = false;
            panel_sendFeedback.Visible = false;
            panel_addCoachingClass.Visible = false;
            panel_updateCoachingClass.Visible = false;
            panel_viewStudentEnr.Visible = true;
            panel_home.Visible = false;
        }

        private void lbl_sendfeedback_Click(object sender, EventArgs e)
        {
            panel_updateprofile.Visible = false;
            panel_addCoachingClass.Visible = false;
            panel_updateCoachingClass.Visible = false;
            panel_viewStudentEnr.Visible = false;
            panel_sendFeedback.Visible = true;
            panel_home.Visible = false;
        }

        private void loadComboBox()
        {
            ArrayList moduleName = new ArrayList();
            Coaching obj1 = new Coaching();
            obj1.Username = username;
            moduleName = obj1.getTrainerModuleNames(username);
            foreach (var item in moduleName)
            {
                cmbBox_module1.Items.Add(item);
                cmbBox_module2.Items.Add(item);
                cmbBox_module3.Items.Add(item);
            }


        }

        // Return the item selected in combobox if no selected item return empty string
        private string GetSelectedComboBoxItem(ComboBox comboBox)
        {
            if (comboBox.SelectedIndex != -1)
            {
                return comboBox.SelectedItem.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        // display Correspond Level
        private void displayCorrespondLevel()
        {
            cmbBox_level1.SelectedIndex = -1;
            cmbBox_level1.Items.Clear();
            ArrayList level = new ArrayList();
            Coaching obj1 = new Coaching();
            obj1.Username = username;
            level = obj1.displaysSpecificLevel(GetSelectedComboBoxItem(cmbBox_module1));
            foreach (var item in level)
            {
                cmbBox_level1.Items.Add(item);
            }
        }

        private void displayCorrespondLeve2()
        {
            cmbBox_level2.SelectedIndex = -1;
            cmbBox_level2.Items.Clear();
            ArrayList level = new ArrayList();
            Coaching obj1 = new Coaching();
            obj1.Username = username;
            level = obj1.displaysSpecificLevel(GetSelectedComboBoxItem(cmbBox_module2));
            foreach (var item in level)
            {
                cmbBox_level2.Items.Add(item);
            }
        }

        private void displayCorrespondLeve3()
        {
            cmbBox_level3.SelectedIndex = -1;
            cmbBox_level3.Items.Clear();
            ArrayList level = new ArrayList();
            Coaching obj1 = new Coaching();
            obj1.Username = username;
            level = obj1.displaysSpecificLevel(GetSelectedComboBoxItem(cmbBox_module3));
            foreach (var item in level)
            {
                cmbBox_level3.Items.Add(item);
            }
        }

        private void cmbBox_module1_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayCorrespondLevel();
        }

        private void addCoachingClass()
        {
            if (cmbBox_module1.SelectedIndex != -1 && !string.IsNullOrEmpty(txtBox_Schedule.Text))
            {
                Coaching obj1 = new Coaching(GetSelectedComboBoxItem(cmbBox_module1), GetSelectedComboBoxItem(cmbBox_level1), txtBox_Schedule.Text);
                obj1.Username = username;
                MessageBox.Show(obj1.addCoachingClass());
            }
            else
                MessageBox.Show("Please insert data");

            cmbBox_module1.SelectedIndex = -1;
            cmbBox_level1.SelectedIndex = -1;
            txtBox_Schedule.Text = String.Empty;
            loadDataGridView();
        }

        private void loadDataGridView()
        {
            Coaching obj1 = new Coaching();
            obj1.Username = username;
            obj1.loadClassTable(dgv_coachClass);
        }

        private void searchClass()
        {
            Coaching obj1 = new Coaching();
            string status = obj1.searchCoachingClass(dgv_coachClass, GetSelectedComboBoxItem(cmbBox_module2), GetSelectedComboBoxItem(cmbBox_level2));
            if (status == "")
            {
                txtBox_schedule2.Text = string.Empty;
            }
            else if (status == "module")
            {
                txtBox_schedule2.Text = string.Empty;
                cmbBox_level2.SelectedIndex = -1;
            }
        }

        private void deleteClass()
        {
            Coaching obj1 = new Coaching();
            obj1.Username = username;
            obj1.deleteCoachingClass(dgv_coachClass);
        }

        private void updateClass()
        {
            Coaching obj1 = new Coaching();
            obj1.Username = username;
            obj1.updateCoachingClass(dgv_coachClass, GetSelectedComboBoxItem(cmbBox_module2), GetSelectedComboBoxItem(cmbBox_level2), txtBox_schedule2.Text);
        }

        private void refreshClass()
        {
            loadDataGridView();
            cmbBox_level2.SelectedIndex = -1;
            cmbBox_module2.SelectedIndex = -1;
            txtBox_schedule2.Text = String.Empty;
        }

        private void sendFeedback()
        {
            TrainerFunction obj1 = new TrainerFunction(username);
            obj1.sendFeedback(richTxtBox_Feedback.Text);
        }

        private void viewStudentEnroll()
        {
            lstBox_student.Items.Clear();
            ArrayList students = new ArrayList();
            TrainerFunction obj1 = new TrainerFunction(username);
            students = obj1.viewStudentEnrolled(GetSelectedComboBoxItem(cmbBox_module3), GetSelectedComboBoxItem(cmbBox_level3));
            foreach (var item in students)
            {
                lstBox_student.Items.Add(item);
            }
        }

        private void viewPaid()
        {
            lstBox_student.Items.Clear();
            ArrayList students = new ArrayList();
            TrainerFunction obj1 = new TrainerFunction(username);
            students = obj1.viewStudentPaid(GetSelectedComboBoxItem(cmbBox_module3), GetSelectedComboBoxItem(cmbBox_level3));
            foreach (var item in students)
            {
                lstBox_student.Items.Add(item);
            }

        }

        private void viewUnpaid()
        {
            lstBox_student.Items.Clear();
            ArrayList students = new ArrayList();
            TrainerFunction obj1 = new TrainerFunction(username);
            students = obj1.viewStudentUnPaid(GetSelectedComboBoxItem(cmbBox_module3), GetSelectedComboBoxItem(cmbBox_level3));
            foreach (var item in students)
            {
                lstBox_student.Items.Add(item);
            }
        }

        private void btn_addCoachingClass_Click(object sender, EventArgs e)
        {
            addCoachingClass();
        }

        private void cmbBox_module2_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayCorrespondLeve2();
        }

        private void cmbBox_module3_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayCorrespondLeve3();
        }


        private void btn_searchClass_Click(object sender, EventArgs e)
        {
            searchClass();
        }

        private void btn_deleteClass_Click(object sender, EventArgs e)
        {
            deleteClass();
        }

        private void btn_updateClass_Click(object sender, EventArgs e)
        {
            updateClass();
        }

        private void btn_refreshClass_Click(object sender, EventArgs e)
        {
            refreshClass(); 
        }

        private void dgv_coachClass_SelectionChanged(object sender, EventArgs e)
        {
            Coaching obj1 = new Coaching();
            obj1.Username = username;
            if (dgv_coachClass.SelectedRows.Count > 0)
            {
                int rowIndex = dgv_coachClass.SelectedRows[0].Index;
                DataGridViewRow selectedRow = dgv_coachClass.Rows[rowIndex];

                // Call the method to update controls
                obj1.UpdateControlsFromSelectedRow(selectedRow, cmbBox_module2, cmbBox_level2, txtBox_schedule2);
            }
        }

        private void btn_sndFeedback_Click(object sender, EventArgs e)
        {
            sendFeedback();
        }

        private void btn_viewStudent_Click(object sender, EventArgs e)
        {
            viewStudentEnroll();
        }

        private void btn_viewPaid_Click(object sender, EventArgs e)
        {
            viewPaid();
        }

        private void btn_viewUnpaid_Click(object sender, EventArgs e)
        {
            viewUnpaid();
        }

        private void btn_home_logout_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginPanel login = new LoginPanel();
            login.Show();
        }

    }
}
