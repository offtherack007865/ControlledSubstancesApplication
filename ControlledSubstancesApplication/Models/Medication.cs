namespace ControlledSubstancesApplication
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Medication
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal ID { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal EnterpriseEntryID { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal Entry { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string EntryCode { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(255)]
        public string EntryName { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(4)]
        public string form { get; set; }

        [StringLength(255)]
        public string RouteOfAdmin { get; set; }
    }
}
