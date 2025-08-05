using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlledSubstancesApplication.Unity
{
    public class GetPatientByMRN
    {
        public string WorkPhone { get; set; }
        public string ID { get; set; }
        public string AddressLine1 { get; set; }
        public string ZipCode { get; set; }
        public string dateofbirth { get; set; }
        public string MRN { get; set; }
        public string State { get; set; }
        public string HomePhone { get; set; }
        public string age { get; set; }
        public string PhoneNumber { get; set; }
        public string gender { get; set; }
        public string ssn { get; set; }
        public string City { get; set; }
        public string OtherNumber { get; set; }
        public string lastname { get; set; }
        public string AddressLine2 { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
    }

    public class RootObject
    {
        public List<GetPatientByMRN> getpatientbymrninfo { get; set; }
    }
}

