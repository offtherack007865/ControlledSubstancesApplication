namespace ControlledSubstancesApplication
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Db : DbContext
    {
        public Db()
            : base("name=Db")
        {
        }

        public virtual DbSet<AdUser> AdUsers { get; set; }
        public virtual DbSet<Entry> Entries { get; set; }
        public virtual DbSet<Lot> Lots { get; set; }
        public virtual DbSet<Site> Sites { get; set; }
        public virtual DbSet<Medication> Medications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entry>()
                .Property(e => e.patient_mrn)
                .IsUnicode(false);

            modelBuilder.Entity<Entry>()
                .Property(e => e.provider)
                .IsUnicode(false);

            modelBuilder.Entity<Entry>()
                .Property(e => e.amt_given)
                .HasPrecision(8, 4);

            modelBuilder.Entity<Entry>()
                .Property(e => e.amt_wasted)
                .HasPrecision(8, 4);

            modelBuilder.Entity<Entry>()
                .Property(e => e.administered_by)
                .IsUnicode(false);

            modelBuilder.Entity<Entry>()
                .Property(e => e.witnessed_by)
                .IsUnicode(false);

            modelBuilder.Entity<Lot>()
                .Property(e => e.lot_number)
                .IsUnicode(false);

            modelBuilder.Entity<Lot>()
                .Property(e => e.entry_code)
                .IsUnicode(false);

            modelBuilder.Entity<Lot>()
                .HasMany(e => e.Entries)
                .WithRequired(e => e.Lot)
                .HasForeignKey(e => e.lot_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Site>()
                .Property(e => e.site_name)
                .IsUnicode(false);

            modelBuilder.Entity<Site>()
                .HasMany(e => e.Lots)
                .WithRequired(e => e.Site)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Medication>()
                .Property(e => e.ID)
                .HasPrecision(8, 0);

            modelBuilder.Entity<Medication>()
                .Property(e => e.EnterpriseEntryID)
                .HasPrecision(16, 0);

            modelBuilder.Entity<Medication>()
                .Property(e => e.Entry)
                .HasPrecision(8, 0);

            modelBuilder.Entity<Medication>()
                .Property(e => e.EntryCode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Medication>()
                .Property(e => e.EntryName)
                .IsUnicode(false);

            modelBuilder.Entity<Medication>()
                .Property(e => e.form)
                .IsUnicode(false);

            modelBuilder.Entity<Medication>()
                .Property(e => e.RouteOfAdmin)
                .IsUnicode(false);
        }
    }
}
