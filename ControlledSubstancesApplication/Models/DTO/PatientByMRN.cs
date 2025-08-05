using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlledSubstancesApplication.Models.DTO
{
    public class PatientByMRN
    {
        public long Departmentid { get; set; }
        public string Homephone { get; set; }
        public string Guarantorstate { get; set; }
        public bool Portalaccessgiven { get; set; }
        public bool Driverslicense { get; set; }
        public bool Homebound { get; set; }
        public string Guarantordob { get; set; }
        public string Zip { get; set; }
        public bool Guarantoraddresssameaspatient { get; set; }
        public bool Portaltermsonfile { get; set; }
        public string Status { get; set; }
        public string Lastname { get; set; }
        public string Guarantorfirstname { get; set; }
        public string City { get; set; }
        public string Guarantorcity { get; set; }
        public string Guarantorzip { get; set; }
        public string Sex { get; set; }
        public bool Privacyinformationverified { get; set; }
        public List<PatientBalance> Balances { get; set; }
        public bool Emailexists { get; set; }
        public long Primaryproviderid { get; set; }
        public bool Patientphoto { get; set; }
        public bool Consenttocall { get; set; }
        public string Registrationdate { get; set; }
        public string Caresummarydeliverypreference { get; set; }
        public string Guarantorlastname { get; set; }
        public string Firstname { get; set; }
        public string Guarantorcountrycode { get; set; }
        public string State { get; set; }
        public long Patientid { get; set; }
        public string Dob { get; set; }
        public long Guarantorrelationshiptopatient { get; set; }
        public string Address1 { get; set; }
        public string Guarantorphone { get; set; }
        public string Countrycode { get; set; }
        public string Guarantoraddress1 { get; set; }
        public bool Consenttotext { get; set; }
        public string Countrycode3166 { get; set; }
        public string Guarantorcountrycode3166 { get; set; }
    }

    public class PatientBalance
    {
        public long BalanceBalance { get; set; }
        public string Departmentlist { get; set; }
        public long Providergroupid { get; set; }
        public bool Cleanbalance { get; set; }
    }
}