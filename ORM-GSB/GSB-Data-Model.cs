using Model;
using System.Data.Entity;

namespace ORM_GSB
{
    public partial class GSB_Data_Model : DbContext
    {
        public GSB_Data_Model()
            : base("name=GSB_Data_Model")
        {
        }

        //départements
        public virtual DbSet<Departement> Departements { get; set; }

        //médecin
        public virtual DbSet<Medecin> Medecins { get; set; }

        //Users
        public virtual DbSet<user> Users { get; set; }


        //fluent api
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departement>()
                .Property(e => e.dep_region)
                .IsUnicode(false);

            modelBuilder.Entity<Departement>()
                .Property(e => e.dep_nom)
                .IsUnicode(false);

            modelBuilder.Entity<Departement>()
                .HasMany(e => e.Medecins)
                .WithRequired(e => e.Departement)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Medecin>()
                .Property(e => e.med_nom)
                .IsUnicode(false);

            modelBuilder.Entity<Medecin>()
                .Property(e => e.med_prenom)
                .IsUnicode(false);

            modelBuilder.Entity<Medecin>()
                .Property(e => e.med_addresse)
                .IsUnicode(false);

            modelBuilder.Entity<Medecin>()
                .Property(e => e.med_telephone)
                .IsUnicode(false);
        }
    }
}
