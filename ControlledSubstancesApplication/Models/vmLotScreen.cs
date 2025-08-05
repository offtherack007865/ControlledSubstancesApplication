using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlledSubstancesApplication
{
    public class vmLotScreen
    {
        public Lot Lot { get; set; }
        public Site Site { get; set; }
        public SitesList SiteList { get; set; }
        public List<Medication> MedList { get; set; }
        public bool LotAlreadyExists { get; set; }
        public bool LotAddSuccessful { get; set; }

    }
}