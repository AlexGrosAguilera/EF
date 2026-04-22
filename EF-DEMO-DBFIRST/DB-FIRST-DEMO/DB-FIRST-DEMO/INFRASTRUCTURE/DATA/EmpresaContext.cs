using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace DB_FIRST_DEMO.INFRASTRUCTURE.DATA;

public partial class EmpresaContext : DbContext
{
    public EmpresaContext()
    {
    }

    public EmpresaContext(DbContextOptions<EmpresaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobGrade> JobGrades { get; set; }

    public virtual DbSet<JobHistory> JobHistories { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;userid=appuser;password=1234;database=EMPRESA;allowuservariables=True", Microsoft.EntityFrameworkCore.ServerVersion.Parse("11.8.3-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_uca1400_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PRIMARY");

            entity.ToTable("countries");

            entity.HasIndex(e => e.RegionId, "REGION_ID");

            entity.Property(e => e.CountryId)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("COUNTRY_ID");
            entity.Property(e => e.CountryName)
                .HasMaxLength(40)
                .HasColumnName("COUNTRY_NAME");
            entity.Property(e => e.RegionId)
                .HasColumnType("int(11)")
                .HasColumnName("REGION_ID");

            entity.HasOne(d => d.Region).WithMany(p => p.Countries)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("countries_ibfk_1");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PRIMARY");

            entity.ToTable("departments");

            entity.HasIndex(e => e.ManagerId, "FK_DEPARTMENTS_EMPLOYEES");

            entity.HasIndex(e => e.LocationId, "LOCATION_ID");

            entity.Property(e => e.DepartmentId)
                .ValueGeneratedNever()
                .HasColumnType("int(4)")
                .HasColumnName("DEPARTMENT_ID");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(30)
                .HasColumnName("DEPARTMENT_NAME");
            entity.Property(e => e.LocationId)
                .HasColumnType("int(4)")
                .HasColumnName("LOCATION_ID");
            entity.Property(e => e.ManagerId)
                .HasColumnType("int(6)")
                .HasColumnName("MANAGER_ID");

            entity.HasOne(d => d.Location).WithMany(p => p.Departments)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("departments_ibfk_1");

            entity.HasOne(d => d.Manager).WithMany(p => p.Departments)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK_DEPARTMENTS_EMPLOYEES");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PRIMARY");

            entity.ToTable("employees");

            entity.HasIndex(e => e.DepartmentId, "DEPARTMENT_ID");

            entity.HasIndex(e => e.Email, "EMP_EMAIL_UK").IsUnique();

            entity.HasIndex(e => e.JobId, "JOB_ID");

            entity.HasIndex(e => e.ManagerId, "MANAGER_ID");

            entity.Property(e => e.EmployeeId)
                .ValueGeneratedNever()
                .HasColumnType("int(6)")
                .HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.CommissionPct)
                .HasPrecision(2, 2)
                .HasColumnName("COMMISSION_PCT");
            entity.Property(e => e.DepartmentId)
                .HasColumnType("int(4)")
                .HasColumnName("DEPARTMENT_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(25)
                .HasColumnName("EMAIL");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.HireDate).HasColumnName("HIRE_DATE");
            entity.Property(e => e.JobId)
                .HasMaxLength(10)
                .HasColumnName("JOB_ID");
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.ManagerId)
                .HasColumnType("int(6)")
                .HasColumnName("MANAGER_ID");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("PHONE_NUMBER");
            entity.Property(e => e.Salary)
                .HasPrecision(8, 2)
                .HasColumnName("SALARY");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("employees_ibfk_3");

            entity.HasOne(d => d.Job).WithMany(p => p.Employees)
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employees_ibfk_1");

            entity.HasOne(d => d.Manager).WithMany(p => p.InverseManager)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("employees_ibfk_2");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PRIMARY");

            entity.ToTable("jobs");

            entity.Property(e => e.JobId)
                .HasMaxLength(10)
                .HasColumnName("JOB_ID");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(35)
                .HasColumnName("JOB_TITLE");
            entity.Property(e => e.MaxSalary)
                .HasColumnType("int(6)")
                .HasColumnName("MAX_SALARY");
            entity.Property(e => e.MinSalary)
                .HasColumnType("int(6)")
                .HasColumnName("MIN_SALARY");
        });

        modelBuilder.Entity<JobGrade>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("job_grades");

            entity.Property(e => e.GradeLevel)
                .HasMaxLength(3)
                .HasColumnName("GRADE_LEVEL");
            entity.Property(e => e.HighestSal)
                .HasColumnType("int(11)")
                .HasColumnName("HIGHEST_SAL");
            entity.Property(e => e.LowestSal)
                .HasColumnType("int(11)")
                .HasColumnName("LOWEST_SAL");
        });

        modelBuilder.Entity<JobHistory>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.JobId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("job_history");

            entity.HasIndex(e => e.DepartmentId, "DEPARTMENT_ID");

            entity.HasIndex(e => e.JobId, "JOB_ID");

            entity.Property(e => e.EmployeeId)
                .HasColumnType("int(6)")
                .HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.JobId)
                .HasMaxLength(10)
                .HasColumnName("JOB_ID");
            entity.Property(e => e.DepartmentId)
                .HasColumnType("int(4)")
                .HasColumnName("DEPARTMENT_ID");
            entity.Property(e => e.EndDate).HasColumnName("END_DATE");
            entity.Property(e => e.StartDate).HasColumnName("START_DATE");

            entity.HasOne(d => d.Department).WithMany(p => p.JobHistories)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("job_history_ibfk_3");

            entity.HasOne(d => d.Employee).WithMany(p => p.JobHistories)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("job_history_ibfk_1");

            entity.HasOne(d => d.Job).WithMany(p => p.JobHistories)
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("job_history_ibfk_2");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PRIMARY");

            entity.ToTable("locations");

            entity.HasIndex(e => e.CountryId, "COUNTRY_ID");

            entity.Property(e => e.LocationId)
                .ValueGeneratedNever()
                .HasColumnType("int(4)")
                .HasColumnName("LOCATION_ID");
            entity.Property(e => e.City)
                .HasMaxLength(30)
                .HasColumnName("CITY");
            entity.Property(e => e.CountryId)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("COUNTRY_ID");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(12)
                .HasColumnName("POSTAL_CODE");
            entity.Property(e => e.StateProvince)
                .HasMaxLength(25)
                .HasColumnName("STATE_PROVINCE");
            entity.Property(e => e.StreetAddress)
                .HasMaxLength(40)
                .HasColumnName("STREET_ADDRESS");

            entity.HasOne(d => d.Country).WithMany(p => p.Locations)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("locations_ibfk_1");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.RegionId).HasName("PRIMARY");

            entity.ToTable("regions");

            entity.Property(e => e.RegionId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("REGION_ID");
            entity.Property(e => e.RegionName)
                .HasMaxLength(25)
                .HasColumnName("REGION_NAME");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
