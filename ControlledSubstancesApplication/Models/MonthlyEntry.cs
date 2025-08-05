using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlledSubstancesApplication.Models
{
    public class MonthlyEntry
    {
        public int id { get; set; }

        public DateTime entry_date { get; set; }

        public DateTime? given_date { get; set; }

        public int lot_id { get; set; }

        public string patient_mrn { get; set; }


        public string provider { get; set; }


        public decimal amt_given { get; set; }


        public decimal amt_wasted { get; set; }


        public string administered_by { get; set; }

        public string witnessed_by { get; set; }

        public string lot_number { get; set; }
        public int siteID { get; set; }

        public string DrugName { get; set; }
        public string PatientName { get; set; }
    }
}