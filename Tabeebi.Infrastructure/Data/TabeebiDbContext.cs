using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabeebi.Core.Entities;

namespace Tabeebi.Infrastructure.Data
{

    /// <summary>
    /// Database context for Tabeebi Clinic Management System
    /// </summary>
    public class TabeebiDbContext : DbContext
    {
        public TabeebiDbContext(DbContextOptions<TabeebiDbContext> options) : base(options)
        {
        }

        // Medical History entities
        public DbSet<MedicalHistory> MedicalHistories => Set<MedicalHistory>();
        public DbSet<ChronicCondition> ChronicConditions => Set<ChronicCondition>();
        public DbSet<MedicationAllergy> MedicationAllergies => Set<MedicationAllergy>();
        public DbSet<FamilyMedicalHistory> FamilyMedicalHistories => Set<FamilyMedicalHistory>();
        public DbSet<SurgicalHistory> SurgicalHistories => Set<SurgicalHistory>();
        public DbSet<ImmunizationRecord> ImmunizationRecords => Set<ImmunizationRecord>();

        // User entities
        public DbSet<Tenant> Tenants => Set<Tenant>();
        public DbSet<IdentityUser> IdentityUsers => Set<IdentityUser>();
        public DbSet<UserProfile> UserProfiles => Set<UserProfile>();

        // Appointment entities
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<DoctorSchedule> DoctorSchedules => Set<DoctorSchedule>();
        public DbSet<TimeSlot> TimeSlots => Set<TimeSlot>();

        // Medical record entities
        public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();
        public DbSet<Prescription> Prescriptions => Set<Prescription>();

        // Payment entities
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Invoice> Invoices => Set<Invoice>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configure MedicalHistory entity
            modelBuilder.Entity<MedicalHistory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.PatientId);
                entity.HasIndex(e => e.DoctorId);
                entity.HasIndex(e => e.TenantId);

                entity.HasOne(e => e.Patient)
                    .WithMany()
                    .HasForeignKey(e => e.PatientId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Doctor)
                    .WithMany()
                    .HasForeignKey(e => e.DoctorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.ChronicConditionDetails)
                    .WithOne(c => c.MedicalHistory)
                    .HasForeignKey(c => c.MedicalHistoryId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.MedicationAllergies)
                    .WithOne(m => m.MedicalHistory)
                    .HasForeignKey(m => m.MedicalHistoryId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.FamilyHistory)
                    .WithOne(f => f.MedicalHistory)
                    .HasForeignKey(f => f.MedicalHistoryId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.SurgicalHistory)
                    .WithOne(s => s.MedicalHistory)
                    .HasForeignKey(s => s.MedicalHistoryId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.ImmunizationRecords)
                    .WithOne(i => i.MedicalHistory)
                    .HasForeignKey(i => i.MedicalHistoryId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure ChronicCondition entity
            modelBuilder.Entity<ChronicCondition>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.MedicalHistoryId);
                entity.Property(e => e.ConditionName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.IcdCode).HasMaxLength(20);
            });

            // Configure MedicationAllergy entity
            modelBuilder.Entity<MedicationAllergy>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.MedicalHistoryId);
                entity.Property(e => e.MedicationName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.GenericName).HasMaxLength(200);
                entity.Property(e => e.Reaction).IsRequired().HasMaxLength(500);
            });

            // Configure FamilyMedicalHistory entity
            modelBuilder.Entity<FamilyMedicalHistory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.MedicalHistoryId);
                entity.Property(e => e.Relationship).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Condition).IsRequired().HasMaxLength(200);
                entity.Property(e => e.IcdCode).HasMaxLength(20);
            });

            // Configure SurgicalHistory entity
            modelBuilder.Entity<SurgicalHistory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.MedicalHistoryId);
                entity.Property(e => e.ProcedureName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.ProcedureCode).HasMaxLength(50);
                entity.Property(e => e.Surgeon).HasMaxLength(200);
                entity.Property(e => e.Hospital).HasMaxLength(200);
            });

            // Configure ImmunizationRecord entity
            modelBuilder.Entity<ImmunizationRecord>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.MedicalHistoryId);
                entity.Property(e => e.VaccineName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.VaccineCode).HasMaxLength(50);
                entity.Property(e => e.LotNumber).HasMaxLength(50);
                entity.Property(e => e.Manufacturer).HasMaxLength(100);
            });

            // Configure UserProfile entity
            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.TenantId);
                entity.HasIndex(e => e.IdentityUserId);
                entity.HasIndex(e => e.Email);
                entity.HasIndex(e => e.PhoneNumber);

                entity.HasOne(e => e.IdentityUser)
                    .WithOne(i => i.UserProfile)
                    .HasForeignKey<UserProfile>(e => e.IdentityUserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);

            // Apply global query filter for soft delete
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(Core.Common.BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = System.Linq.Expressions.Expression.Parameter(entityType.ClrType, "e");
                    var property = System.Linq.Expressions.Expression.Property(parameter, "IsDeleted");
                    var filter = System.Linq.Expressions.Expression.Lambda(
                        System.Linq.Expressions.Expression.Equal(property, System.Linq.Expressions.Expression.Constant(false)),
                        parameter);

                    entityType.SetQueryFilter(filter);
                }
            }
        }

    }
}
