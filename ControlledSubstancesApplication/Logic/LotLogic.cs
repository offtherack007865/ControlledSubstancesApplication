using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlledSubstancesApplication
{
    public class LotLogic
    {
        public vmLotScreen vmLotScreen { get; set; }

        
        public LotLogic()
        {
            vmLotScreen = new vmLotScreen()
            {
                SiteList = new SitesList(),
                MedList = GetMedList()
            }; 
        }
        public LotLogic(vmLotScreen model, Controller c)
        {
            vmLotScreen = model;
            vmLotScreen.SiteList = new SitesList();
            vmLotScreen.MedList = GetMedList();
            using (Db db = new Db())
            {
                model.Lot.date_entered = DateTime.Now;
                db.Lots.Add(model.Lot);
                var errors = db.GetValidationErrors();
                try
                {
                    if (errors.Count() == 0)
                    {
                        db.SaveChanges();
                        vmLotScreen.LotAddSuccessful = true;
                    }
                    else
                    {                        
                        foreach(var error in errors)
                        {
                            foreach(var _error in error.ValidationErrors)
                            {
                                c.ModelState.AddModelError(_error.ErrorMessage, _error.ErrorMessage);
                            }
                        }                        
                    }
                }
                catch(Exception e)
                {
                    //TODO
                }
                model.Lot = null;
            }
        }
        private List<Medication> GetMedList()
        {
            using (Db db = new Db())
            {
                return  (from m in db.Medications
                         orderby m.EntryName ascending
                         select m).ToList();
            }
        }
    }
}