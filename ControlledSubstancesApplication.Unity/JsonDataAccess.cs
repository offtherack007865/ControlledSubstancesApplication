using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace ControlledSubstancesApplication.Unity
{
    public class JsonDataAccess
    {
        public string JSONURL
        {
            get;
            set;
        }

        //public string SvcUsername
        //{
        //    get;
        //    set;
        //}

        //public string svcUserPwd
        //{
        //    get;
        //    set;

        //}


        public string GetToken(string sSvcName, string sPwd)
        {
            string token = "";

            string TokenBody = string.Format("{{'Username':'{0}',", sSvcName);
            TokenBody += string.Format("'Password':'{0}'}}", sPwd);
            TokenBody = TokenBody.Replace("'", "\"");

            byte[] tokendata = ASCIIEncoding.ASCII.GetBytes(TokenBody);
            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("Content-Type", "application/json");
                byte[] bDataToken = wc.UploadData(JSONURL + "/GetToken", "POST", tokendata);

                using (MemoryStream ms = new MemoryStream(bDataToken))
                {
                    token = ASCIIEncoding.ASCII.GetString(ms.ToArray());

                }
            }
            return token;
        }
        public string Magic(string Action, string UserID, string Appname, string PatientID, string Token, string Parameter1,
                            string Parameter2, string Parameter3, string Parameter4, string Parameter5, string Parameter6, byte[] unuseddata)
        {
            //data parameter not used, need to base64encode data and pass it in param6.
            try
            {
                string sJson = "";

                string reqBody = string.Format("{{'Action':'{0}',", Action);
                reqBody += string.Format("'Appname':'{0}',", Appname);
                reqBody += string.Format("'AppUserID':'{0}',", UserID);
                reqBody += string.Format("'PatientID':'{0}',", PatientID);
                reqBody += string.Format("'Token':'{0}',", Token);
                reqBody += string.Format("'Parameter1':'{0}',", Parameter1);
                reqBody += string.Format("'Parameter2':'{0}',", Parameter2);
                reqBody += string.Format("'Parameter3':'{0}',", Parameter3);
                reqBody += string.Format("'Parameter4':'{0}',", Parameter4);
                reqBody += string.Format("'Parameter5':'{0}',", Parameter5);
                reqBody += string.Format("'Parameter6':'{0}',", Parameter6);

                reqBody += "'Data':''}";

                reqBody = reqBody.Replace("'", "\"");

                byte[] data = ASCIIEncoding.ASCII.GetBytes(reqBody);

                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add("Content-Type", "application/json");
                    byte[] bData = wc.UploadData(JSONURL + "/MagicJson", "POST", data);

                    using (MemoryStream ms = new MemoryStream(bData))
                    {
                        sJson = ASCIIEncoding.ASCII.GetString(ms.ToArray());
                    }
                }
                return sJson;
            }
            catch (Exception ex)
            {
                Console.WriteLine("jason magic call error :" + ex.Message);
                return "";
            }
        }

    }
}