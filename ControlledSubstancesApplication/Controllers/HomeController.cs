using ControlledSubstancesApplication.APIClient;
using ControlledSubstancesApplication.Data;
using ControlledSubstancesApplication.Models.DTO;
using ControlledSubstancesApplication.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using ControlledSubstancesApplication.Models;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Security.Policy;

namespace ControlledSubstancesApplication.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
           
            return View();
        }
        public ActionResult Log()
        {
            SitesList Sites = new SitesList();
            return View(Sites);
        }
        [HttpPost]
        public ActionResult Entries(FormCollection form)
        {
            EntryLogic lEntry = new EntryLogic(form);
            //if the LotId is 0, the lot/site combo does not exist, so go
            //back to the Log screen with an error message
            if (lEntry.vmEntryScreen.LotId == 0)
            {
                TempData["LotNotFoundMsg"] = "Lot not found";
                return RedirectToAction("Log");
            }
            else
                return View(lEntry.vmEntryScreen);
        }     

        public ActionResult Entries()
        {
            return RedirectToAction("Log");
        }
        [HttpGet]        
        public ActionResult Entries(int? page, string lot, DateTime? start, DateTime? end, int site = 0)
        {
            if (site == 0)
                return RedirectToAction("Log");
            int pageNumber = page ?? 1;
            Filter filter = new Filter(start, end);
            EntryLogic lEntry = new EntryLogic(site, lot, filter, pageNumber);
            return View(lEntry.vmEntryScreen);
        }
        [HttpPost]
        public ActionResult Filter(vmEntryScreen entryscreen)
        {
            entryscreen.FilterStartDate = entryscreen.FilterStartDate.Value.Date;
            entryscreen.FilterEndDate = entryscreen.FilterEndDate.Value.Date;
            EntryLogic lEntry = new EntryLogic(entryscreen);
            return View("Entries", lEntry.vmEntryScreen);
        }
        public ActionResult AddLot()
        {
            LotLogic lLot = new LotLogic();
            return View(lLot.vmLotScreen);
        }
        [HttpPost]
        public ActionResult AddLot(vmLotScreen model)
        {
            ModelState.Clear();
            LotLogic lLot = new LotLogic(model, this);
            return View(lLot.vmLotScreen);
        }
        [HttpPost]
        public ActionResult AddEntry(vmEntryScreen entryscreen)
        {
            EntryLogic lEntry = new EntryLogic(entryscreen);
            lEntry.Save();
            //TempData["entryscreen"] = lEntry.vmEntryScreen;
            return RedirectToAction("Entries", new { page = 1, site = lEntry.vmEntryScreen.Site.site_number, lot = lEntry.vmEntryScreen.LotNumber,
                            start = lEntry.vmEntryScreen.FilterStartDate, end = lEntry.vmEntryScreen.FilterEndDate });
        }
        [HttpPost]
        public ActionResult UpdateEntry(vmEntryScreen entryscreen)
        {            
            EntryLogic.MarkErrors(entryscreen);
            TempData["EntriesUpdated"] = entryscreen;
            return RedirectToAction("Entries", new { page = entryscreen.CurrentPage, site = entryscreen.Site.site_number, lot = entryscreen.LotNumber,
                            start = entryscreen.FilterStartDate, end = entryscreen.FilterEndDate });
        }
        public ActionResult GetAllscriptsInfo(vmEntryScreen entryscreen)
        {
            APIService service = new APIService();
            TsqlQuery query = new TsqlQuery();

            PatientInformation patInfo = query.GetPatientByID(entryscreen.Entry.patient_mrn);
            if(!string.IsNullOrEmpty(patInfo.FirstName) && !string.IsNullOrEmpty(patInfo.LastName))
            {
                entryscreen.PatientName = string.Format(@"{0}, {1}", patInfo.LastName.Trim(), patInfo.FirstName.Trim());
                entryscreen.PatientDob = patInfo.DOB.Replace(@"/", "-").Trim();
            }
            else
            {
                //PatientByMRN results = service.GetPatientByMRN(entryscreen.Entry.patient_mrn);
                //entryscreen.PatientName = string.Format(@"{0}, {1}", results.Lastname.Trim(), results.Firstname.Trim());
                //entryscreen.PatientDob = results.Dob.Replace(@"/", "-").Trim();
            }

            return Json(entryscreen);
        }
        [HttpGet]
        public ActionResult Print(string LotId, string SiteNumber, DateTime? StartDate, DateTime? EndDate)
        {
            EntryLogic lEntry = new EntryLogic(Convert.ToInt32(SiteNumber), LotId, null, 1);
            lEntry.vmEntryScreen.FilterStartDate = StartDate;
            lEntry.vmEntryScreen.FilterEndDate = EndDate;
            lEntry.PopulateValues();
            lEntry.CreatePagedList();
            return View(lEntry.vmEntryScreen);
        }
        [HttpGet]
        public ActionResult GeneratePdf(string LotId, string SiteNumber, DateTime? StartDate, DateTime? EndDate)
        {
            return new Rotativa.ActionAsPdf("Print", new { LotId = LotId, SiteNumber = SiteNumber, StartDate = StartDate, EndDate = EndDate })
            {                
                PageOrientation = Rotativa.Options.Orientation.Landscape,
                FileName = string.Format("Report - {0}.pdf", DateTime.Now.ToString("MM/dd/yyyy"))
            };
        }

        public ActionResult MonthlyReport()
        {
            if (Request.Params["date"] != null)
            {
                TsqlQuery query = new TsqlQuery();
                int siteid = Convert.ToInt32(Request.Params["siteID"]);
                DateTime date = Convert.ToDateTime(Request.Params["date"]);


                List<MonthlyEntry> entries = query.getEntriesForSite(siteid, date);
                
            }


            SitesList Sites = new SitesList();
            return View(Sites);
        }
        [HttpPost]
        public ActionResult Month(FormCollection form)
        {
            TsqlQuery query = new TsqlQuery();
            int site = Convert.ToInt32(form["rzrSiteList"]);
            DateTime time = Convert.ToDateTime(form["date"]);
            List<MonthlyEntry> entries = query.getEntriesForSite(site,time);
           vmMonthlyReport vm = new vmMonthlyReport();
            vm.entries = entries;
            vm.siteID = site;
            vm.date = time;

            for(int i = 0; i < entries.Count();i++)
            {
                entries[i].PatientName = query.GetPatientName(entries[i].patient_mrn);
            }

            return View(vm);
        }
       
    }
}