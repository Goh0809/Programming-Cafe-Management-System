using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ioop_assignment
{
    internal class TrainerDGV
    {
        private int invoiceID;
        private string trainer;
        private string module;
        private string level;
        private string amount;
        private string paymentstatus;

        private static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ToString());

        public int InvoiceID { get { return invoiceID; } set { invoiceID = value;} }
        public string Trainer { get { return trainer; } set { trainer = value; } }
        public string Module { get { return module; } set { module = value; } }
        public string Level { get { return level; } set { level = value; } }
        public string Amount { get { return amount; } set { amount = value; } }
        public string Paymentstatus { get { return paymentstatus; } set { paymentstatus = value; } }



        public TrainerDGV(string trainer, string module, string level)
        {
            this.trainer = trainer;
            this.module = module;
            this.level = level;
        }
        public static List<TrainerDGV> SearchIncomeByFilters(string trainerName, string moduleName, string levelName)
        {
            List<TrainerDGV> incomeData = new List<TrainerDGV>();
            con.Open();

            string query = "SELECT invoiceID, Trainer, Module, Level, Amount, paymentstatus FROM vwIncome WHERE 1=1";
            if (!string.IsNullOrEmpty(trainerName))
            {
                query += " AND Trainer = @TrainerName";
            }

            if (!string.IsNullOrEmpty(moduleName))
            {
                query += " AND Module = @ModuleName";
            }

            if (!string.IsNullOrEmpty(levelName))
            {
                query += " AND Level = @LevelName";
            }

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                if (!string.IsNullOrEmpty(trainerName))
                {
                    cmd.Parameters.AddWithValue("@TrainerName", trainerName);
                }

                if (!string.IsNullOrEmpty(moduleName))
                {
                    cmd.Parameters.AddWithValue("@ModuleName", moduleName);
                }

                if (!string.IsNullOrEmpty(levelName))
                {
                    cmd.Parameters.AddWithValue("@LevelName", levelName);
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TrainerDGV income = new TrainerDGV(trainerName, moduleName, levelName)
                        {
                            invoiceID = reader.GetInt32(reader.GetOrdinal("invoiceID")),
                            Trainer = reader.GetString(reader.GetOrdinal("Trainer")),
                            Module = reader.GetString(reader.GetOrdinal("Module")),
                            Level = reader.GetString(reader.GetOrdinal("Level")),
                            Amount = reader.GetString(reader.GetOrdinal("Amount")),
                            Paymentstatus = reader.GetString(reader.GetOrdinal("paymentstatus"))
                        };

                        incomeData.Add(income);
                    }
                }
            }
            con.Close();

            return incomeData;
        }
    }
}
