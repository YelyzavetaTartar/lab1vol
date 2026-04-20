using System;
using System.Collections.Generic;
using LibraryDomain.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryVolunteer;

public partial class DbLibraryContext : DbContext
{
    public DbLibraryContext()
    {
    }

    public DbLibraryContext(DbContextOptions<DbLibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<GeneralStaff> GeneralStaffs { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Medic> Medics { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<Translator> Translators { get; set; }

    public virtual DbSet<Volunteer> Volunteers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=volunteersdb;Username= yelyzavetatartar;Password=postgres;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryCode).HasName("Countries_pkey");

            entity.Property(e => e.CountryCode).HasColumnName("country_code")
                .ValueGeneratedNever();
            entity.Property(e => e.CountryName)
                .HasMaxLength(30)
                .HasColumnName("country_name");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Drivers_pkey");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID_volunteer");
            entity.Property(e => e.DrivingLicenseCategories)
                .HasColumnType("character varying")
                .HasColumnName("driving_license_categories");
            entity.Property(e => e.DrivingLicenseId)
                .HasColumnType("character varying")
                .HasColumnName("driving_license_id");

            entity.HasOne(d => d.IdVolunteerNavigation).WithOne(p => p.Driver)
                .HasForeignKey<Driver>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_drivers_volunteer");
        });

        modelBuilder.Entity<GeneralStaff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("General_Staff_pkey");

            entity.ToTable("General_Staff");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID_volunteer");
            entity.Property(e => e.Experience)
                .HasMaxLength(10)
                .HasColumnName("experience");
            entity.Property(e => e.TypeOfWork)
                .HasMaxLength(150)
                .HasColumnName("type_of_work");

            entity.HasOne(d => d.IdVolunteerNavigation).WithOne(p => p.GeneralStaff)
                .HasForeignKey<GeneralStaff>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_general_staff_volunteer");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("Languages_pkey");

            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.LanguageLevel)
                .HasColumnType("character varying")
                .HasColumnName("language_level");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Locations_pkey");

            entity.Property(e => e.Id).HasColumnName("location_id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Medic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Medics_pkey");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID_volunteer");
            entity.Property(e => e.DiplomaNumber)
                .HasMaxLength(10)
                .HasColumnName("diploma_number");
            entity.Property(e => e.Specialization)
                .HasMaxLength(50)
                .HasColumnName("specialization");

            entity.HasOne(d => d.IdVolunteerNavigation).WithOne(p => p.Medic)
                .HasForeignKey<Medic>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_medics_volunteer");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Shifts_pkey");

            entity.Property(e => e.Id).HasColumnName("shift_id");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.ShiftDate).HasColumnName("shift_date");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
        });

        modelBuilder.Entity<Translator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Translators_pkey");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID_volunteer");
            entity.Property(e => e.LanguageLevel)
                .HasMaxLength(100)
                .HasColumnName("language_level");
            entity.Property(e => e.LanguagePair)
                .HasMaxLength(150)
                .HasColumnName("language_pair");

            entity.HasOne(d => d.IdVolunteerNavigation).WithOne(p => p.Translator)
                .HasForeignKey<Translator>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_translators_volunteer");
        });

        modelBuilder.Entity<Volunteer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Volunteers_pkey");

            entity.Property(e => e.Id).HasColumnName("ID_volunteer");
            entity.Property(e => e.CountryCode)
                .ValueGeneratedOnAdd()
                .HasColumnName("country_code");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(250)
                .HasColumnName("full_name");

            entity.HasOne(d => d.CountryCodeNavigation).WithMany(p => p.Volunteers)
                .HasForeignKey(d => d.CountryCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_volunteer_countries");

            entity.HasMany(d => d.Languages).WithMany(p => p.Volunteers)
                .UsingEntity<Dictionary<string, object>>(
                    "VolunteerLanguage",
                    r => r.HasOne<Language>().WithMany()
                        .HasForeignKey("Language")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_language"),
                    l => l.HasOne<Volunteer>().WithMany()
                        .HasForeignKey("Volunteer")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_volunteer"),
                    j =>
                    {
                        j.HasKey("Volunteer", "Language").HasName("VolunteerLanguages_pkey");
                        j.ToTable("VolunteerLanguages");
                        j.IndexerProperty<int>("Volunteer").HasColumnName("volunteer");
                        j.IndexerProperty<int>("Language").HasColumnName("language");
                    });

            entity.HasMany(d => d.Locations).WithMany(p => p.Volunteers)
                .UsingEntity<Dictionary<string, object>>(
                    "VolunteerLocation",
                    r => r.HasOne<Location>().WithMany()
                        .HasForeignKey("Location")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_location"),
                    l => l.HasOne<Volunteer>().WithMany()
                        .HasForeignKey("Volunteer")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_volunteer"),
                    j =>
                    {
                        j.HasKey("Volunteer", "Location").HasName("VolunteerLocation_pkey");
                        j.ToTable("VolunteerLocation");
                        j.IndexerProperty<int>("Volunteer").HasColumnName("volunteer");
                        j.IndexerProperty<int>("Location").HasColumnName("location");
                    });

            entity.HasMany(d => d.Shifts).WithMany(p => p.Volunteers)
                .UsingEntity<Dictionary<string, object>>(
                    "Schedule",
                    r => r.HasOne<Shift>().WithMany()
                        .HasForeignKey("Shift")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_schedule_shift"),
                    l => l.HasOne<Volunteer>().WithMany()
                        .HasForeignKey("Volunteer")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_schedule_volunteers"),
                    j =>
                    {
                        j.HasKey("Volunteer", "Shift").HasName("Schedule_pkey");
                        j.ToTable("Schedule");
                        j.IndexerProperty<int>("Volunteer").HasColumnName("volunteer");
                        j.IndexerProperty<int>("Shift").HasColumnName("shift");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
