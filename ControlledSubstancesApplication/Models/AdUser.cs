namespace ControlledSubstancesApplication
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AdUser
    {
        public int id { get; set; }

        [StringLength(255)]
        public string user_name { get; set; }

        public bool? is_provider { get; set; }
    }
}
