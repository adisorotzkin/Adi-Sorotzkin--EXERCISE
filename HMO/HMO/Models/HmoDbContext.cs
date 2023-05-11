using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace HMO.Models;

public partial class HmoDbContext : DbContext
{
    public HmoDbContext()
    {
    }

    public HmoDbContext(DbContextOptions<HmoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientVaccination> PatientVaccinations { get; set; }

    public virtual DbSet<Vaccination> Vaccinations { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
       // => optionsBuilder.UseSqlServer("Server=LAPTOP-T3VUSPAU;Database=HMO_DB;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true;");
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.Property(e => e.PatientId).ValueGeneratedNever();
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MobilePhone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PositiveResultDate).HasColumnType("date");
            entity.Property(e => e.RecoveryDate).HasColumnType("date");
            entity.Property(e => e.Street)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PatientVaccination>(entity =>
        {
            entity.HasKey(e => new { e.PatientId, e.VaccinationId });

            entity.ToTable("PatientVaccination");

            entity.Property(e => e.Vdate)
                .HasColumnType("date")
                .HasColumnName("VDate");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientVaccinations)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientVaccination_Patients");

            entity.HasOne(d => d.Vaccination).WithMany(p => p.PatientVaccinations)
                .HasForeignKey(d => d.VaccinationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientVaccination_Vaccinations");
        });

        modelBuilder.Entity<Vaccination>(entity =>
        {
            entity.Property(e => e.VaccinationId).ValueGeneratedNever();
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Vname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("VName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
