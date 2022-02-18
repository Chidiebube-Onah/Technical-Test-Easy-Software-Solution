using Microsoft.EntityFrameworkCore;
using TechnicalTest.DAL.Entities;

namespace TechnicalTest.DAL
{
    public class TechTestContext : DbContext
    {
        public TechTestContext(DbContextOptions<TechTestContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Patient>()
                .HasIndex(u => u.CardNo)
                .IsUnique();
        }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }
    }
}