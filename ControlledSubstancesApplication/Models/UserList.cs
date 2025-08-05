using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlledSubstancesApplication
{
    public class UserList
    {
        public List<AdUser> Users { get; set; } = new List<AdUser>();
        public List<string> strUsers { get; set; } = new List<string>();
       
        public UserList()
        {
            using (Db db = new Db())
            {
                Users =
                    (from u in db.AdUsers
                     orderby u.user_name ascending
                     select u).ToList();
                foreach (AdUser user in Users)
                    strUsers.Add(user.user_name);
            }

        }
    }
}