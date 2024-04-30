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
    public partial class LecturerDashboard : Form
    {
        public static string userID;
        public static string username;
        public static string name;
        public static string role;
        public LecturerDashboard()
        {
            InitializeComponent();
        }

        public LecturerDashboard(string id, string uname, string n, string r)
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
            MouseCursorChanged();
            InitialLoad();
            loadallComboBox();
            ShowEnrollmentDGV();
            ShowRequestDGV();
            showDeleteList();
        }

        private void InitialLoad()
        {
            panel_updateprofile.Visible = false;
            panel_regenrollstudent.Visible = false;
            panel_updateEnrollment.Visible = false;
            panel_approverequest.Visible = false;
            panel_deletestudent.Visible = false;
            panel_home.Visible = true;
        }

        private void MouseCursorChanged()
        {
            //Base functions
            lbl_home.Cursor = Cursors.Hand;
            lbl_updateprofile.Cursor = Cursors.Hand;
            //
            //lecturer functions
            lbl_regenrollstudent.Cursor = Cursors.Hand;
            lbl_updateenroll.Cursor = Cursors.Hand;
            lbl_approve.Cursor = Cursors.Hand;
            lbl_delete.Cursor = Cursors.Hand;
            //

        }

        private void lbl_updateprofile_Click(object sender, EventArgs e)
        {
            panel_updateprofile.Visible = true;
            panel_regenrollstudent.Visible = false;
            panel_updateEnrollment.Visible = false;
            panel_approverequest.Visible = false;
            panel_deletestudent.Visible = false;
            panel_home.Visible = false;
            viewProfile();
        }

        private void viewProfile()
        {
            Users obj1 = new Users(username);
            Users.viewProfile(obj1);

            txtbox_name.Text = obj1.Name;
            txtbox_phone.Text = obj1.Phone;
            txtbox_email.Text = obj1.Email;
        }

        private void btn_updateprofile_Click(object sender, EventArgs e)
        {
            Users obj1 = new Users(username);
            MessageBox.Show(obj1.updateProfile(txtbox_name.Text, txtbox_phone.Text, txtbox_email.Text));
        }

        private void lbl_home_Click(object sender, EventArgs e)
        {
            panel_updateprofile.Visible = false;
            panel_regenrollstudent.Visible = false;
            panel_updateEnrollment.Visible = false;
            panel_approverequest.Visible = false;
            panel_deletestudent.Visible = false;
            panel_home.Visible = true;
        }

        private void lbl_regenrollstudent_Click(object sender, EventArgs e)
        {
            panel_updateprofile.Visible = false;
            panel_regenrollstudent.Visible = true;
            panel_updateEnrollment.Visible = false;
            panel_approverequest.Visible = false;
            panel_deletestudent.Visible = false;
            panel_home.Visible = false;
        }

        private void lbl_updateenroll_Click(object sender, EventArgs e)
        {
            panel_updateprofile.Visible = false;
            panel_regenrollstudent.Visible = false;
            panel_updateEnrollment.Visible = true;
            panel_approverequest.Visible = false;
            panel_deletestudent.Visible = false;
            panel_home.Visible = false;
            ShowEnrollmentDGV();
        }

        private void lbl_approve_Click(object sender, EventArgs e)
        {
            panel_updateprofile.Visible = false;
            panel_regenrollstudent.Visible = false;
            panel_updateEnrollment.Visible = false;
            panel_approverequest.Visible = true;
            panel_deletestudent.Visible = false;
            panel_home.Visible = false;
        }

        private void lbl_delete_Click(object sender, EventArgs e)
        {
            panel_updateprofile.Visible = false;
            panel_regenrollstudent.Visible = false;
            panel_updateEnrollment.Visible = false;
            panel_approverequest.Visible = false;
            panel_deletestudent.Visible = true;
            panel_home.Visible = false;
            showDeleteList();
        }

        private void RegisterStudent()
        {
            if (!string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtTPnum.Text) && !string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(txtContact.Text) && !string.IsNullOrEmpty(txtAddress.Text))
            {
                Student obj1 = new Student(txtUsername.Text, txtName.Text, txtTPnum.Text, txtContact.Text, txtEmail.Text, txtAddress.Text);
                MessageBox.Show(obj1.registerStudent());
                reloadList();
            }
            else
                MessageBox.Show("Please insert data");

            txtUsername.Text = null;
            txtName.Text = null;
            txtTPnum.Text = null;
            txtEmail.Text = null;
            txtContact.Text = null;
            txtAddress.Text = null;
        }

        private void showList()
        {
            ArrayList name = new ArrayList();

            name = Student.viewStudent();
            foreach (var item in name)
            {
                listStudent.Items.Add(item);
            }
        }

        private void reloadList()
        {
            listStudent.Items.Clear();
            ArrayList name = new ArrayList();

            name = Student.viewStudent();
            foreach (var item in name)
            {
                listStudent.Items.Add(item);
            }
        }
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

        private string GetSelectedString(ListBox listBox)
        {
            if (listBox.SelectedIndex != -1)
            {
                return listBox.SelectedItem.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        private void loadallComboBox()
        {
            ArrayList levelid = new ArrayList();
            levelid = Student.viewlevelName();
            foreach (var item in levelid)
            {
                cmbBox_level.Items.Add(item);
                cmbBox_Update_Level.Items.Add(item);
            }

            ArrayList coursename = new ArrayList();
            coursename = Student.viewmoduleName();
            foreach (var item in coursename)
            {
                cmbBox_module.Items.Add(item);
                cmbBox_UpdateModule.Items.Add(item);
            }

            ArrayList requestid = new ArrayList();
            requestid = Student.viewRequestID();
            foreach (var item in requestid)
            {
                cmbBoxRequest.Items.Add(item);
            }
        }

        private void reloadRequestComboBox()
        {
            cmbBoxRequest.Items.Clear();
            ArrayList requestid = new ArrayList();
            requestid = Student.viewRequestID();
            foreach (var item in requestid)
            {
                cmbBoxRequest.Items.Add(item);
            }
        }
        private void EnrollStudent()
        {
            string student_name = GetSelectedString(listStudent);
            if (string.IsNullOrWhiteSpace(student_name))
            {
                MessageBox.Show("Please select a student to enroll.");
                return;
            }
            if (!string.IsNullOrEmpty(student_name) && cmbBox_level.SelectedItem != null && cmbBox_module.SelectedItem != null && !string.IsNullOrEmpty(txtmonthofEnrollment.Text) && !string.IsNullOrWhiteSpace(student_name))
            {
                Student obj1 = new Student();
                MessageBox.Show(obj1.enrollStudent(student_name, GetSelectedComboBoxItem(cmbBox_level), GetSelectedComboBoxItem(cmbBox_module), txtmonthofEnrollment.Text));

                txtmonthofEnrollment.Text = null;
                cmbBox_level.SelectedIndex = -1;
                cmbBox_module.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Please fill in all the required fields.");
            }
        }

        private void UpdateEnrollment()
        {
            if (dGV_updateEnrollment.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a student to enroll.");
                return;
            }
            if (dGV_updateEnrollment.SelectedRows.Count > 0 & cmbBox_UpdateModule.SelectedItem != null & cmbBox_Update_Level.SelectedItem != null)
            {
                Student obj1 = new Student();
                Student obj2 = new Student();
                obj1.updateEnrollment(dGV_updateEnrollment, GetSelectedComboBoxItem(cmbBox_Update_Level), GetSelectedComboBoxItem(cmbBox_UpdateModule));
                obj2.getOriginModuleID(dGV_updateEnrollment);
            }
            else
            {
                MessageBox.Show("Please select all the required fields");
            }
        }

        private void UpdateRequest()
        {
            if (cmbBoxRequest.SelectedItem != null)
            {
                Student obj1 = new Student();
                int requestID = int.Parse(cmbBoxRequest.SelectedItem.ToString());
                MessageBox.Show(obj1.updateRequest(requestID));
                cmbBoxRequest.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Please select required requestID.");
            }
        }

        private void DeclineRequest()
        {
            if (cmbBoxRequest.SelectedItem != null)
            {
                Student obj1 = new Student();
                int requestID = int.Parse(cmbBoxRequest.SelectedItem.ToString());
                MessageBox.Show(obj1.deleteRequest(requestID));
                cmbBoxRequest.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Please select required requestID.");
            }
        }

        private void DeleteStudent()
        {
            if (lstBox_DeleteStudent.SelectedItem != null)
            {
                string selectedStudentName = lstBox_DeleteStudent.SelectedItem.ToString();

                Student obj1 = new Student(selectedStudentName);
                MessageBox.Show(obj1.deleteStudent(selectedStudentName));

                lstBox_DeleteStudent.Items.Remove(lstBox_DeleteStudent.SelectedItem);
            }
            else
            {
                MessageBox.Show("Please select a student.");
            }
        }
        private void ShowEnrollmentDGV()
        {
            DataTable dt = Student.showEnrollment();
            dGV_updateEnrollment.DataSource = dt;
            dGV_updateEnrollment.Refresh();
        }

        private void ShowRequestDGV()
        {
            DataTable dt = Student.showRequest();
            dgv_RequestApproval.DataSource = dt;
            dgv_RequestApproval.Refresh();
        }

        private void showDeleteList()
        {

            ArrayList name = new ArrayList();

            name = Student.viewStudent();
            lstBox_DeleteStudent.Items.Clear();
            foreach (var item in name)
            {
                lstBox_DeleteStudent.Items.Add(item);
            }
        }


        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterStudent();

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            showList();
            reloadList();
        }

        private void btnEnroll_Click(object sender, EventArgs e)
        {
            EnrollStudent();
            ShowEnrollmentDGV();
        }

        private void btn_UpdateEnrollment_Click(object sender, EventArgs e)
        {
            UpdateEnrollment();
            ShowEnrollmentDGV();
        }

        private void btn_EnrollRequest_Accept_Click(object sender, EventArgs e)
        {
            UpdateRequest();
            ShowRequestDGV();
            reloadRequestComboBox();
        }

        private void btn_EnrollRequest_Decline_Click(object sender, EventArgs e)
        {
            DeclineRequest();
            ShowRequestDGV();
            reloadRequestComboBox();
        }

        private void btn_DeleteStudent_Click(object sender, EventArgs e)
        {
            DeleteStudent();
            reloadList();
        }

        private void btn_home_logout_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginPanel login = new LoginPanel();
            login.Show();
        }

    }
}
