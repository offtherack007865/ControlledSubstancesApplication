using ControlledSubstancesApplication.Models;
using ControlledSubstancesApplication.Models.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControlledSubstancesApplication.Data
{
    public class TsqlQuery
    {
        private readonly string _connectionString;

        public TsqlQuery()
        {
            _connectionString = ConfigurationManager.AppSettings["mssqlConnection"];
        }

        /// <summary>
        /// Gets patient information from Data warehouse.
        /// </summary>
        /// <param name="mrn"></param>
        /// <returns></returns>
        public PatientInformation GetPatientByID(string mrn)
        {
            PatientInformation result = new PatientInformation();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = string.Format(@"SELECT FirstName, LastName, DOB FROM [AHODS].[dbo].[PatientDemo] WHERE PatientID = {0}", mrn);

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandTimeout = 0;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.FirstName = reader["FirstName"].ToString().Trim();
                            result.LastName = reader["LastName"].ToString().Trim();
                            result.DOB = reader["DOB"].ToString().Trim();
                        }
                    }
                }
            }

            return result;
        }

        public List<MonthlyEntry> getEntriesForSite(int site,DateTime date)
        {
            List<MonthlyEntry> entries = new List<MonthlyEntry>();
            string connection = "Data Source = smg-sql01; Initial Catalog = ControlledSubstanceLog; Persist Security Info=True; User ID = AppUser; Password=SoMm1t!@";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string sql = $@"select e.entry_date,
	e.patient_mrn,
	l.lot_number,
	l.site_number,
	l.entry_code,
	e.provider,
	e.amt_given,
	e.amt_wasted,
	e.administered_by,
	e.witnessed_by,
	--e.amt_to_count,
	e.given_date,
	m.EntryName
	--l.note
from [dbo].[Entries] e
inner join [dbo].[Lots] l on l.id = e.lot_id
inner join [dbo].[Medications] m on m.EntryCode  = l.entry_code
where l.site_number = '{site}'
	and Month(entry_date) = MONTH('{date}') and YEAR(entry_date) = YEAR('{date}')
ORDER BY entry_date";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandTimeout = 0;
                   
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MonthlyEntry e = new MonthlyEntry();
                            e.entry_date = Convert.ToDateTime(reader["entry_date"]);
                            e.patient_mrn = reader["patient_mrn"].ToString();
                            e.provider = reader["provider"].ToString();
                            e.amt_given = Convert.ToDecimal(reader["amt_given"]);
                            e.amt_wasted = Convert.ToDecimal(reader["amt_wasted"]);
                            e.administered_by = reader["administered_by"].ToString();
                            e.witnessed_by = reader["witnessed_by"].ToString();
                            e.lot_number = reader["lot_number"].ToString();
                            e.siteID = Convert.ToInt32(reader["site_number"]);
                            e.DrugName = reader["EntryName"].ToString();

                            entries.Add(e);
                        }
                    }
                }
            }


            return entries;
        }

        public string GetPatientName(string MRN)
        {
            string patientName = "";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = string.Format($@"SELECT FirstName, LastName FROM [AHODS].[dbo].[PatientDemo] WHERE PatientID = '{MRN}'");

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandTimeout = 0;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            patientName = reader["FirstName"].ToString().Trim() + " " + reader["LastName"].ToString().Trim();

                        }
                    }
                }
                conn.Close();  
            }

            return patientName;
        }
    }
}