using ControlledSubstancesApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlledSubstancesApplication
{
    public class vmMonthlyReport
    {
        public List<MonthlyEntry> entries { get; set; }
        public int siteID { get; set; }
        public DateTime date { get; set; }

    }
}