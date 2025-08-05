namespace ControlledSubstancesApplication
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Entry
    {
        public int id { get; set; }

        public DateTime entry_date { get; set; }
        [Required(ErrorMessage = "Date is Required")]
        [DataType(DataType.DateTime)]
        public DateTime? given_date { get; set; }

        public int lot_id { get; set; }

        [Required(ErrorMessage = "MRN is Required")]
        [StringLength(20)]
        public string patient_mrn { get; set; }

        [Required(ErrorMessage = "Provider is Required")]
        [StringLength(255)]
        public string provider { get; set; }

        [Required(ErrorMessage ="Amt. Given is Required")]
        [Range(typeof(decimal),"0", "1000", ErrorMessage = "Amount Given must be a positive number")]
        public decimal amt_given { get; set; }

        [Range(typeof(decimal), "0", "1000", ErrorMessage = "Amount Wasted must be a positive number")]
        public decimal amt_wasted { get; set; }

        [Required(ErrorMessage = "Administered By is Required")]
        [StringLength(255)]
        public string administered_by { get; set; }

        [Required(ErrorMessage = "Witness is Required")]
        [StringLength(255)]
        public string witnessed_by { get; set; }

        public bool is_error { get; set; }
        
        public virtual Lot Lot { get; set; }
    }
}
