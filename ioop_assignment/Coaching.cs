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
    public class Coaching
    {
        private string module_name;
        private string level;
        private double charges;
        private string schedule;
        private string username;

        private static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ToString());

        public string Username { get { return username; } set { username = value; } }
        public string Module_Name { get { return module_name; } set { module_name = value; } }

        public string Level { get { return level; } set { level = value; } }
        public double Charges { get { return charges; } set { charges = value; } }

        public string Schedule { get { return schedule; } set { schedule = value; } }

        public Coaching()
        {

        }

        public Coaching(string module_Selected, string level_selected, string schedule)
        {
            module_name = module_Selected;
            level = level_selected;
            this.schedule = schedule;
        }

        // insert information into table
        public string addCoachingClass()
        {
            string status;
            int trainerId = GetTrainerID(username);
            int levelId = GetLevelID(level);
            int moduleId = GetModuleID(module_name, levelId);
            con.Open();
            SqlCommand cmd = new SqlCommand($"INSERT INTO Class (moduleID,trainerID,schedule) VALUES ('{moduleId}','{trainerId}','{schedule}')", con);
            int i = cmd.ExecuteNonQuery();
            if (i != 0)
                status = "Class Added Successfully";
            else
                status = "Unable to add class";
            con.Close();
            return status;
        }

        // search the coaching class
        public string searchCoachingClass(DataGridView dgv, string module_name, string level_name)
        {
            string status = string.Empty;
            if (string.IsNullOrEmpty(module_name) && string.IsNullOrEmpty(level_name))
            {
                MessageBox.Show("Please select the module and level to search.");
                return status;
            }

            int levelID = GetLevelID(level_name);
            int moduleID = GetModuleID(module_name, levelID);
            dgv.ClearSelection();
            bool moduleFound = false;
            bool moduleAndLevelFound = false;

            if (string.IsNullOrEmpty(level_name))
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    var cellValue = row.Cells["modulename"].Value;
                    if (cellValue != null && cellValue != DBNull.Value)
                    {
                        string rowModuleName = cellValue.ToString();

                        if (rowModuleName == module_name)
                        {
                            row.Selected = true;
                            moduleFound = true;
                        }
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    var moduleCellValue = row.Cells["modulename"].Value;
                    var levelCellValue = row.Cells["levelname"].Value;

                    if (moduleCellValue != null && moduleCellValue != DBNull.Value && levelCellValue != null && levelCellValue != DBNull.Value)
                    {
                        string rowModuleName = moduleCellValue.ToString();
                        string rowLevelName = levelCellValue.ToString();

                        if (rowModuleName == module_name && rowLevelName == level_name)
                        {
                            row.Selected = true;
                            moduleAndLevelFound = true;
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(level_name))
            {
                if (!moduleFound)
                {
                    string message = $"The module '{module_name}' does not exist in the class.";
                    MessageBox.Show(message);
                }
                else
                {
                    status = "module";
                }
            }
            else
            {
                if (!moduleAndLevelFound)
                {
                    string message = $"The module '{module_name}' with level '{level_name}' was not found.";
                    MessageBox.Show(message);
                }
            }

            return status;
        }


        // update coaching class
        public void updateCoachingClass(DataGridView dgv, string module_name, string level, string schedule)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                int classID = Convert.ToInt32(dgv.SelectedRows[0].Cells["classID"].Value);
                int trainerID = GetTrainerID(username);
                int levelID = GetLevelID(level);
                int moduleID = GetModuleID(module_name, levelID);
                if (string.IsNullOrEmpty(module_name) && string.IsNullOrEmpty(level) && string.IsNullOrEmpty(schedule))
                {
                    MessageBox.Show("Please select what you want to update.");
                    return;
                }
                con.Open();
                string updateQuery = "UPDATE Class SET ";

                if ((!string.IsNullOrEmpty(module_name)) && (!string.IsNullOrEmpty(level)))
                {
                    updateQuery += "moduleID = @moduleID, ";
                }

                if (!string.IsNullOrEmpty(schedule))
                {
                    updateQuery += "schedule = @schedule, ";
                }

                updateQuery += "trainerID = @trainerID WHERE classID = @classID";
                SqlCommand cmd = new SqlCommand(updateQuery, con);
                cmd.Parameters.AddWithValue("@moduleID", moduleID);
                cmd.Parameters.AddWithValue("@trainerID", trainerID);
                cmd.Parameters.AddWithValue("@schedule", schedule);
                cmd.Parameters.AddWithValue("@classID", classID);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Row updated successfully.");
                con.Close();
                loadClassTable(dgv);
            }
            else
            {
                MessageBox.Show("Please select a row to update.");
            }

        }


        // delete coaching class
        public void deleteCoachingClass(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                int rowIndex = dgv.SelectedRows[0].Index;
                int classID = Convert.ToInt32(dgv.Rows[rowIndex].Cells["classID"].Value);

                con.Open();
                SqlCommand deleteCmd = new SqlCommand("DELETE FROM Class WHERE classID = @classID", con);
                deleteCmd.Parameters.AddWithValue("@classID", classID);
                int rowsAffected = deleteCmd.ExecuteNonQuery();
                con.Close();

                if (rowsAffected > 0)
                {
                    dgv.Rows.RemoveAt(rowIndex);
                    MessageBox.Show("Row deleted successfully.");
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
            loadClassTable(dgv);
        }

        public void UpdateControlsFromSelectedRow(DataGridViewRow selectedRow, ComboBox moduleComboBox, ComboBox levelComboBox, TextBox scheduleTextBox)
        {
            if (selectedRow != null)
            {
                string schedule = selectedRow.Cells["schedule"].Value?.ToString() ?? string.Empty;
                scheduleTextBox.Text = schedule;

                if (selectedRow.Cells["modulename"].Value == DBNull.Value || selectedRow.Cells["schedule"].Value == DBNull.Value)
                {
                    moduleComboBox.SelectedIndex = -1;
                    levelComboBox.SelectedIndex = -1;
                }
                else
                {
                    string moduleName = selectedRow.Cells["modulename"].Value.ToString();
                    string levelName = selectedRow.Cells["levelname"].Value.ToString();

                    moduleComboBox.SelectedItem = moduleName;
                    levelComboBox.SelectedItem = levelName;
                }
            }
        }


        // view class table in dta grid view
        public void loadClassTable(DataGridView dgv)
        {
            int trainerId = GetTrainerID(username);
            con.Open();
            SqlCommand cmd = new SqlCommand($"SELECT C.classID, M.modulename, L.levelname, U.name AS trainerName, C.schedule FROM Class AS C JOIN Modules AS M ON C.moduleID = M.moduleID JOIN Levels AS L ON M.levelID = L.levelID JOIN Trainers AS T ON C.trainerID = T.trainerID JOIN Users AS U ON T.userID = U.userID WHERE C.trainerID = '{trainerId}'", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgv.DataSource = dt;
            con.Close();
        }

        // get trainer's module's moduleID
        private ArrayList getTrainerModuleID(string username)
        {
            int trainerID = GetTrainerID(username);
            ArrayList nm = new ArrayList();
            con.Open();
            SqlCommand cmd = new SqlCommand($"Select moduleID FROM TrainerModules WHERE trainerID = '{trainerID}'", con);
            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                int moduleID = rd.GetInt32(0);
                nm.Add(moduleID);
            }
            con.Close();
            return nm;
        }

        // get the trainer module name
        public ArrayList getTrainerModuleNames(string username)
        {
            ArrayList moduleNames = new ArrayList();
            ArrayList moduleIDs = getTrainerModuleID(username);

            con.Open();
            foreach (int moduleID in moduleIDs)
            {
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT moduleName FROM Modules WHERE moduleID = @moduleID", con);
                cmd.Parameters.AddWithValue("@moduleID", moduleID);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string moduleName = reader.GetString(0);
                    moduleNames.Add(moduleName);
                }

                reader.Close();
            }
            con.Close();

            ArrayList uniqueModuleNames = RemoveDuplicateModuleNames(moduleNames);

            return uniqueModuleNames;
        }

        // remove duplicate arraylist 
        public ArrayList RemoveDuplicateModuleNames(ArrayList moduleNames)
        {
            ArrayList uniqueModuleNames = new ArrayList();

            foreach (string moduleName in moduleNames)
            {
                if (!uniqueModuleNames.Contains(moduleName))
                {
                    uniqueModuleNames.Add(moduleName);
                }
            }

            return uniqueModuleNames;
        }

        // loading Item From Database Modules Table to ComboBox
        public static ArrayList viewModuleName()
        {
            ArrayList nm = new ArrayList();
            con.Open();
            SqlCommand cmd = new SqlCommand("Select DISTINCT moduleName from Modules", con);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                nm.Add(rd.GetString(0));
            }
            con.Close();
            return nm;
        }


        // loading Item From Database level Table to ComboBox
        public ArrayList viewLevel()
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

        // GET LevelID
        private int GetLevelID(string level)
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

        // GET Trainer Specific Module Level ID
        private ArrayList GetSpecificLevelID(string moduleName)
        {
            ArrayList levelIDs = new ArrayList();
            ArrayList moduleIDs = getTrainerModuleID(username);
            con.Open();
            foreach (int moduleID in moduleIDs)
            {
                SqlCommand cmd = new SqlCommand("SELECT levelID FROM Modules WHERE moduleID = @moduleID AND moduleName = @moduleName", con);
                cmd.Parameters.AddWithValue("@moduleID", moduleID);
                cmd.Parameters.AddWithValue("@moduleName", moduleName);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int levelID = reader.GetInt32(0);
                    levelIDs.Add(levelID);
                }

                reader.Close();
            }
            con.Close();

            return levelIDs;
        }

        // display specific level ID
        public ArrayList displaysSpecificLevel(string module_name)
        {
            ArrayList arrlist = GetSpecificLevelID(module_name);
            ArrayList nm = new ArrayList();

            foreach (var item in arrlist)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"Select levelname FROM Levels WHERE levelID = '{item}' ", con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    nm.Add(rd.GetString(0));
                }
                con.Close();
            }
            return nm;
        }

        // Get Trainer ID
        private int GetTrainerID(string username)
        {
            int trainerID = 0;
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT trainerID FROM Trainers WHERE userID = (SELECT userID FROM Users WHERE username = @username)", con);
            cmd.Parameters.AddWithValue("@username", username);

            object result = cmd.ExecuteScalar();

            if (result != null && int.TryParse(result.ToString(), out int id))
            {
                trainerID = id;
            }
            con.Close();
            return trainerID;
        }


        // GET Module ID
        private int GetModuleID(string module_name, int levelID)
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


    }
}
