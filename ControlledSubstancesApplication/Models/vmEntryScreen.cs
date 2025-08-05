using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace ControlledSubstancesApplication
{
    public class vmEntryScreen
    {
        private DateTime? _filterstartdate;
        private DateTime? _filterenddate;

        public string LotNumber { get; set; } = null;
        public int LotId { get; set; }
        public string DrugName { get; set; } = null;
        public bool MrnIsValid { get; set; }
        public string PatientName { get; set; } = null;
        public string PatientDob { get; set; } = null;
        public Site Site { get; set; } = new Site();
        public Entry Entry { get; set; }
        public List<Entry> AllEntriesList { get; set; }
        public IPagedList<Entry> PagedEntriesList { get; set; }
        public List<Entry> EntriesForDisplay { get; set; }
        public int PagingUpperLimit { get; set; }
        public int PagingLowerLimit { get; set; }
        //public DateTime? FilterStartDate { get; set; }
        //public DateTime? FilterEndDate { get; set; }
        public UserList Users { get; set; } = new UserList();
        public int CurrentPage { get; set; } = 1;
        public int EntriesPerPage { get; set; } = 20;


        //for date comparisons, we have to grab the date and make end date 11:59:59 pm and 
        //start date 00:00:01
        public DateTime? FilterStartDate
        {
            get { return this._filterstartdate; }
            set
            {
                TimeSpan span = new TimeSpan(0, 0, 01);
                DateTime? dt = new DateTime();
                dt = (DateTime)value.Value.Date;
                dt = dt.Value.Add(span);
                this._filterstartdate = dt;
            }
        }
        public DateTime? FilterEndDate
        {
            get { return this._filterenddate; }
            set
            {
                TimeSpan span = new TimeSpan(23, 59, 59);
                DateTime? dt = new DateTime();
                dt = (DateTime)value.Value.Date;
                dt = dt.Value.Add(span);
                this._filterenddate = dt;
            }
        }
    }
}