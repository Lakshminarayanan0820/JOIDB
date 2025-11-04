//using System;
//using JOIEnergy.Domain;
//using JOIEnergy.Services;

////using System.Data.SQLite;
////using System.Data.SqlClient;
//using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore;

//namespace JOIEnergy.Database;
//public class JoiDBContext : DbContext
//{
//    public JoiDBContext(DbContextOptions<JoiDBContext> options) : base(options) { }
//    public DbSet<PlanDetailsDB> PlanDetails { get; set; }
//    public DbSet<Readings> Readings { get; set; }
//    public DbSet<SmartMeterIDDetailsDB> SmartMeterIDDetails { get; set; }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        base.OnModelCreating(modelBuilder);
//        modelBuilder.Entity<PlanDetailsDB>(entity =>
//        {
//            entity.ToTable("PlanDetails");
//            entity.HasKey(e => e.PlanName); // Define primary key
//        });

//        modelBuilder.Entity<Readings>(entity =>
//        {
//            entity.ToTable("Readings");
//            entity.HasKey(e => e.SmartMeterId); // Define primary key
//        });

//        modelBuilder.Entity<SmartMeterIDDetailsDB>(entity =>
//        {
//            entity.ToTable("SmartMeterIDDetails");
//            entity.HasKey(e => e.SmartMeterId); // Define primary key
//        });
//    }


//}

using JOIEnergy.Domain;
using Microsoft.EntityFrameworkCore;

namespace JOIEnergy.Database
{
    public class JoiDBContext : DbContext
    {
        public JoiDBContext(DbContextOptions<JoiDBContext> options) : base(options) { }

        public DbSet<PlanDetailsDB> Plans { get; set; }
        public DbSet<SmartMeterIDDetailsDB> SmartMeters { get; set; }
        public DbSet<Reading> Readings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- Table configurations ---

            // PlanDetails table
            modelBuilder.Entity<PlanDetailsDB>(entity =>
            {
                entity.ToTable("Plans");
                entity.HasKey(e => e.PlanId);

                entity.Property(e => e.PlanName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.EnergySupplier)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UnitRate)
                    .IsRequired()
                    .HasPrecision(10, 2);
            });

            // SmartMeter table
            modelBuilder.Entity<SmartMeterIDDetailsDB>(entity =>
            {
                entity.ToTable("SmartMeters");
                entity.HasKey(e => e.SmartMeterId);

                entity.Property(e => e.SmartMeterId)
                    .IsRequired()
                    .HasMaxLength(50);

                // Relationship: Plan (1) → SmartMeter (many)
                entity.HasOne(e => e.PlanDetails)
                      .WithMany(p => p.SmartMeters)
                      .HasForeignKey(e => e.PlanId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Reading table
            modelBuilder.Entity<Reading>(entity =>
            {
                entity.ToTable("Readings");
                entity.HasKey(e => e.ReadingId);

                entity.Property(e => e.SmartMeterId)
                    .IsRequired()
                    .HasMaxLength(50);

                // Relationship: SmartMeter (1) → Readings (many)
                entity.HasOne(e => e.SmartMeter)
                      .WithMany(s => s.Readings)
                      .HasForeignKey(e => e.SmartMeterId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
