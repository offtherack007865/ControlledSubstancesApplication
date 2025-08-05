namespace ControlledSubstancesApplication
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [LotUnique]
    public partial class Lot
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lot()
        {
            Entries = new HashSet<Entry>();
        }

        public int id { get; set; }

        public DateTime date_entered { get; set; }
        [Required(ErrorMessage = "Site is required")]
        public int site_number { get; set; }

        [Required(ErrorMessage = "Lot Number is required")]
        [StringLength(50)]
        public string lot_number { get; set; }

        [Required(ErrorMessage = "Drug is required")]
        [StringLength(50)]
        public string entry_code { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Entry> Entries { get; set; }

        public virtual Site Site { get; set; }       
    }
}
