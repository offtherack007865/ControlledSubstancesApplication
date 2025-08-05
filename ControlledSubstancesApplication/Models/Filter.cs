using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlledSubstancesApplication
{
    public class Filter
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        //public Filter()
        //{
        //    int today = DateTime.Now.Day;
        //    StartDate = DateTime.Now.AddDays(-(today - 1));
        //    EndDate = DateTime.Now;
        //}
        public Filter(DateTime? start_date, DateTime? end_date)
        {
            if (end_date == null)
                EndDate = DateTime.Now;
            else
                EndDate = end_date;

            if (start_date == null)
            {
                int today = DateTime.Now.Day;
                StartDate = DateTime.Now.AddDays(-(today - 1));
            }
            else
                StartDate = start_date;
        }
    }
}