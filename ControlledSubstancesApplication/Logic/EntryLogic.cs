using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlledSubstancesApplication
{
    public class EntryLogic
    {
        public vmEntryScreen vmEntryScreen { get; set; }
        public EntryLogic() { }
        public EntryLogic(int site_number, string lot_number, Filter filter, int pageNumber = 1)
        {
            vmEntryScreen = new vmEntryScreen()
            {
                LotNumber = lot_number,
                CurrentPage = pageNumber,
            };
            if(filter != null)
            {
                vmEntryScreen.FilterStartDate = filter.StartDate;
                vmEntryScreen.FilterEndDate = filter.EndDate;
            }
            vmEntryScreen.Site.site_number = site_number;
            PopulateValues();
            CreatePagedList();
        }
        public EntryLogic(FormCollection form)
        {
            vmEntryScreen = new vmEntryScreen();
            vmEntryScreen.Site.site_number = Convert.ToInt32(form["rzrSiteList"]);
            vmEntryScreen.LotNumber = form["rzrLotNum"];
            PopulateValues();
            CreatePagedList();
        }
        public EntryLogic(vmEntryScreen screen)
        {
            vmEntryScreen = screen;
            PopulateValues();
            CreatePagedList();
        }
        
        public void PopulateValues()
        {
            //if no filter start date is given, go to the beginning of the current month
            if(vmEntryScreen.FilterStartDate == null)
            {
                int today = DateTime.Now.Day;
                vmEntryScreen.FilterStartDate =  DateTime.Now.AddDays(-(today - 1));
            }
            if (vmEntryScreen.FilterEndDate == null)
                vmEntryScreen.FilterEndDate = DateTime.Now.Date;


            Filter filter = new Filter(vmEntryScreen.FilterStartDate, vmEntryScreen.FilterEndDate);
            if(vmEntryScreen.FilterStartDate != null)


            using (Db db = new Db())
            {
                vmEntryScreen.Site.site_name =
                    (from s in db.Sites
                     where s.site_number == vmEntryScreen.Site.site_number
                     select s.site_name).SingleOrDefault();

                vmEntryScreen.LotId =
                    (from i in db.Lots
                     where i.site_number == vmEntryScreen.Site.site_number &&
                     i.lot_number == vmEntryScreen.LotNumber
                     select i.id).FirstOrDefault();

                vmEntryScreen.DrugName =
                    (from b in db.Lots
                     where b.lot_number == vmEntryScreen.LotNumber &&
                      b.site_number == vmEntryScreen.Site.site_number
                     join c in db.Medications on b.entry_code equals c.EntryCode
                     select c.EntryName).FirstOrDefault();

                    vmEntryScreen.AllEntriesList =
                        (from e in db.Entries
                            where e.lot_id == vmEntryScreen.LotId
                            && e.given_date > filter.StartDate
                            && e.given_date < filter.EndDate
                            orderby e.given_date descending
                            select e).ToList();
                }
        }
        public void Save()
        {
            using (Db db = new Db())
            {
                vmEntryScreen.Entry.entry_date = DateTime.Now;
                //string[] dateSplit = vmEntryScreen.Entry.given_date.ToString().Split(' ');
                //var date = Convert.ToDateTime(dateSplit[0]);
                //vmEntryScreen.Entry.given_date = date.Date;
                vmEntryScreen.Entry.given_date = vmEntryScreen.Entry.given_date.Value.Date + vmEntryScreen.Entry.entry_date.TimeOfDay;
                vmEntryScreen.Entry.lot_id = vmEntryScreen.LotId;
                db.Entries.Add(vmEntryScreen.Entry);
                //add this entry to the List<Entry> above so it will show up in the model, then set the individual Entry to null 
                //so that the view forms don't populate it again
                this.vmEntryScreen.AllEntriesList.Insert(0, vmEntryScreen.Entry);
                CreatePagedList();
                this.vmEntryScreen.Entry = null;
                db.SaveChanges();
            }
        }
        public static void MarkErrors(vmEntryScreen screen)
        {
            using (Db db = new Db())
            {
                foreach (Entry e in screen.EntriesForDisplay)
                {
                    if(e.is_error)
                    {
                        Entry n = (from x in db.Entries
                                   where x.id == e.id
                                   select x).First();
                        if(!n.is_error)
                        {
                            n.is_error = true;
                            db.SaveChanges();
                        }
                    }
                }
            }
        }
        public void CreatePagedList()
        {          
            var AllEntriesList_Queryable = vmEntryScreen.AllEntriesList.AsQueryable<Entry>();
            vmEntryScreen.PagedEntriesList = AllEntriesList_Queryable.ToPagedList(vmEntryScreen.CurrentPage, vmEntryScreen.EntriesPerPage);
            vmEntryScreen.EntriesForDisplay = vmEntryScreen.PagedEntriesList.ToList();          
        }
    }
}