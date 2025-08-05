using ControlledSubstancesApplication.Unity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControlledSubstancesApplication.Unity
{
    public class AllscriptsQuery
    {
        //private static readonly NameValueCollection collection = ConfigurationManager.AppSettings;
        private static readonly string endpoint = "http://touchworks/unity/unityservice.svc/json";
        private static readonly string svcusername = "Summi-ad67-SummitSmar-test";
        private static readonly string svcpassword = "S^0M#tS6@Rt3H%1LthT7sT@pP2213f";
        private static readonly string ehrusername = "EPRESLEY";
        private static readonly string appname = "SummitSmarteHealth.TestApp";

        private JsonDataAccess jda = new JsonDataAccess();
        private string token;
        private string _patientmrn;


        public AllscriptsQuery(string mrn)
        {
            this._patientmrn = mrn;
            jda.JSONURL = endpoint;
            //this.token = jda.GetToken(svcusername, svcpassword);
        }

        private RootObject GetAllscriptsJsonResults()
        {
            string JsonResult = jda.Magic("GetPatientByMRN",
                                            ehrusername,
                                            appname,
                                            "",
                                            this.token,
                                            this._patientmrn,
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            null);
            var Json = JsonConvert.DeserializeObject<List<RootObject>>(JsonResult)[0];
            return Json;
        }

        public string GetPatientFullNameLastNameFirst()
        {
            //RootObject Json = new RootObject();
            //Json = GetAllscriptsJsonResults();
            //if (Json.getpatientbymrninfo.Count == 0)

            //stop gap fix while unity is down
            string name = "";
            string datasource = "Data Source=SM-Report1; Initial Catalog=Works; Integrated Security=False; User ID = devusr; Password = Summit99$";
            string sql = string.Format("SELECT TOP 1 t1.LastName, t1.FirstName FROM Person AS t1 JOIN Patient_Iorg AS t2 ON t1.ID = t2.PersonID WHERE t2.OrganizationMrn = '{0}'", _patientmrn);
            using (SqlConnection db = new SqlConnection(datasource))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = db;
                cmd.CommandText = sql;
                cmd.CommandTimeout = 0;
                cmd.CommandType = System.Data.CommandType.Text;

                db.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Check is the reader has any rows at all before starting to read.
                    if (reader.HasRows)
                    {

                        // Read advances to the next row.
                        while (reader.Read())
                        {
                            name = string.Format("{0}, {1}", reader.GetString(1), reader.GetString(0));
                        }
                    }
                }
            }
            if (name == "")
                    return null;
                else
                    return name;          
        }
        public string GetPatientDateOfBirth()
        {
            string dob = "";
            string datasource = "Data Source=SM-Report1;Initial Catalog=Works; Integrated Security=False; User ID = devusr; Password = Summit99$";
            string sql = string.Format("SELECT TOP 1 t1.DateOfBirth FROM Person AS t1 JOIN Patient_Iorg AS t2 ON t1.ID = t2.PersonID WHERE t2.OrganizationMrn = '{0}'", _patientmrn);
            using (SqlConnection db = new SqlConnection(datasource))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = db;
                cmd.CommandText = sql;
                cmd.CommandTimeout = 0;
                cmd.CommandType = System.Data.CommandType.Text;

                db.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Check is the reader has any rows at all before starting to read.
                    if (reader.HasRows)
                    {
                        // Read advances to the next row.
                        while (reader.Read())
                        {
                            dob = reader.GetDateTime(0).ToString();
                        }
                    }
                }
            }
            if (dob == "")
                return null;
            else
            {
                string[] s = dob.Split(' ');
                return s[0];
            }



            //RootObject Json = new RootObject();
            //Json = GetAllscriptsJsonResults();
            //if (Json.getpatientbymrninfo.Count == 0)
            //    return null;
            //else
            //    return Json.getpatientbymrninfo[0].dateofbirth;
        }
    }
}