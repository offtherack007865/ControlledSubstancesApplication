using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlledSubstancesApplication
{
    public class SitesList
    {
        public List<Site> Sites { get; set; }
        public SitesList()
        {
            using (Db db = new Db())
            {
                Sites =
                    (from a in db.Sites
                     orderby a.site_number ascending
                     select a).ToList();
            }
        }

        public DateTime date { get; set; }
    }
}