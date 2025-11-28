using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using project_assignments.Models;
using System.ComponentModel.DataAnnotations;

namespace project_assignments.Infrastructure
{
    public class RepositoryContext(DbContextOptions<RepositoryContext> options) : DbContext(options)
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ProjectAssignment> ProjectAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Department unique constraint
            modelBuilder.Entity<Department>()
                .HasIndex(d => d.DepartmentCode)
                .IsUnique();

            // Team composite unique constraint (if Index attribute doesn't work)
            modelBuilder.Entity<Team>()
                .HasIndex(t => new { t.DepartmentId, t.TeamName })
                .IsUnique();

            // Project unique constraint
            modelBuilder.Entity<Project>()
                .HasIndex(p => p.ProjectCode)
                .IsUnique();

            // Employee unique constraint
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.EmployeeCode)
                .IsUnique();

            // Configure relationships
            modelBuilder.Entity<Team>()
                .HasOne(t => t.Department)
                .WithMany(d => d.Teams)
                .HasForeignKey(t => t.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Projects)
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectAssignment>()
                .HasOne(pa => pa.Project)
                .WithMany(p => p.ProjectAssignments)
                .HasForeignKey(pa => pa.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectAssignment>()
                .HasOne(pa => pa.Employee)
                .WithMany(e => e.ProjectAssignments)
                .HasForeignKey(pa => pa.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            ValidateEntities();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            ValidateEntities();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Base &&
                       (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (Base)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.Now;
                }

                entity.UpdatedAt = DateTime.Now;
            }
        }

        private void ValidateEntities()
        {
            var entities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(e => e.Entity);

            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                var validationResults = new List<ValidationResult>();

                if (!Validator.TryValidateObject(entity, validationContext, validationResults, true))
                {
                    var errors = string.Join("; ", validationResults.Select(v => v.ErrorMessage));
                    throw new ValidationException($"Validation failed: {errors}");
                }
            }
        }
    }
}

