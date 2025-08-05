using ControlledSubstancesApplication.Models.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;

namespace ControlledSubstancesApplication.APIClient
{
    public class APIService
    {
        private readonly string _AuthURL;
        private readonly string _RootURL;
        private readonly string _User;
        private readonly string _Password;

        public APIService()
        {
            _AuthURL = ConfigurationManager.AppSettings["AuthUrl"];
            _RootURL = ConfigurationManager.AppSettings["RootUrl"];
            _User = ConfigurationManager.AppSettings["APIUser"];
            _Password = ConfigurationManager.AppSettings["APIPassword"];
        }

        public PatientByMRN GetPatientByMRN(string mrn)
        {
            string token = Authentication();
            if (!string.IsNullOrEmpty(token))
            {
                RestClient client = new RestClient(string.Format(@"{0}/{1}", _RootURL, "api/Athena/GetPatientbyMRN")) { CookieContainer = new CookieContainer() };
                client.Authenticator = new JwtAuthenticator(token);
                RestRequest request = new RestRequest(Method.GET);
                request.RequestFormat = DataFormat.Json;
                request.Parameters.Clear();
                request.AddParameter("mrn", mrn);
                try
                {
                    IRestResponse response = client.Execute(request);
                    PatientByMRN results = JsonConvert.DeserializeObject<PatientByMRN>(response.Content);
                    return results;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format(@"Cannot connect properly to API call. See errors. Stacktrace: {0} \r\n Message: {1}", ex.StackTrace, ex.Message));
                    throw;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Does authentication to connect to api.
        /// </summary>
        /// <returns></returns>
        private string Authentication()
        {
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback =
                ((sender, certificate, chain, sslPolicyErrors) => true);
                RestClient client = new RestClient(_AuthURL) { CookieContainer = new CookieContainer() };
                RestRequest request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.Parameters.Clear();
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("grant_type", "password");
                request.AddParameter("username", _User);
                request.AddParameter("password", _Password);

                IRestResponse response = client.Execute(request);
                string result = string.Empty;
                result = JsonConvert.DeserializeObject<ApiAuthentication>(response.Content).access_token;

                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format(@"Cannot connect properly to API call. See errors. Stacktrace: {0} \r\n Message: {1}", ex.StackTrace, ex.Message));
                throw;
            }
        }
    }
}