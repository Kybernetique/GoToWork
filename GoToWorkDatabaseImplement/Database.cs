using GoToWorkDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace GoToWorkDatabaseImplement
{
    public class Database : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-8RJEJL3;Initial Catalog=Database;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Engineer> Engineers { set; get; }
        public virtual DbSet<Boss> Bosses { set; get; }
        public virtual DbSet<Worker> Workers { set; get; }
        public virtual DbSet<Machine> Machines { set; get; }
        public virtual DbSet<Shift> Shifts { set; get; }
        public virtual DbSet<Part> Parts { set; get; }
        public virtual DbSet<Product> Products { set; get; }
        public virtual DbSet<Certificate> Certificates { set; get; }
        public virtual DbSet<CertificateProduct> CertificateProduct { set; get; }
        public virtual DbSet<MachinePart> MachineParts { set; get; }
        public virtual DbSet<MachineWorker> MachineWorkers { set; get; }
        public virtual DbSet<ProductPart> ProductParts { set; get; }
        public virtual DbSet<ProductWorker> ProductWorkers { set; get; }
        public virtual DbSet<ShiftWorker> ShiftWorkers { set; get; }
    }
}
