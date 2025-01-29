using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TrelloCopy.Models;

namespace TrelloCopy.Data;
public class Context : DbContext
{
    public Context(DbContextOptions options) : base(options)
    {
    }

    // Users Management
    public DbSet<User> Users { get; set; }
    public DbSet<SprintItem> SprintItems { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Project> Projects { get; set; }
    
    public DbSet<UserAssignedProject> UserAssignedProjects { get; set; }
    public DbSet<UserSprintItem> UserSprintItems { get; set; }
    public DbSet<UserFeature> UserFeatures { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        SeedData(modelBuilder);
        modelBuilder.Entity<UserAssignedProject>()
            .HasOne(uap => uap.User)
            .WithMany(u => u.UserAssignedProjects)
            .HasForeignKey(u => u.UserID)
            .OnDelete(DeleteBehavior.NoAction);  // Avoid cascade delete

        // Project to UserAssignedProjects
        modelBuilder.Entity<UserAssignedProject>()
            .HasOne(uap => uap.Project)
            .WithMany(p => p.UserAssignedProjects)
            .HasForeignKey(uap => uap.ProjectID)
            .OnDelete(DeleteBehavior.NoAction);

        // User to CreatedProjects
        modelBuilder.Entity<Project>()
            .HasOne(p => p.Creator)
            .WithMany(u => u.CreatedProjects)
            .HasForeignKey(p => p.CreatorID)
            .OnDelete(DeleteBehavior.NoAction);  // Avoid cascade delete
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed roles
        modelBuilder.Entity<Role>().HasData(
            new Role { ID = 1, Name = "Admin" },
            new Role { ID = 2, Name = "User" }
        );

        // Seed admin user
        modelBuilder.Entity<User>().HasData(
            new User
            {
                ID = 1, // Make sure the ID is set explicitly, as auto-generation might conflict.
                Email = "upskillingfinalproject@gmail.com",
                Password = "Admin123", // Ensure to hash passwords properly in your real app!
                Name = "Admin User",
                PhoneNo = "1234567890",
                Country = "CountryName",
                IsActive = true,
                TwoFactorAuthEnabled = false,
                IsEmailConfirmed = true,
                RoleID = 1 // Admin role
            }
        );
    }
}
