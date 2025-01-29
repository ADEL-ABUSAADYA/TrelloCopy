using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TrelloCopy.Models;

namespace TrelloCopy.Data;
public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
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



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source =desktop-vp7hi13; Initial Catalog = TrelloCopy; Integrated Security = True; Connect Timeout = 30; Encrypt=True;Trust Server Certificate=True;Application Intent = ReadWrite; Multi Subnet Failover=False")
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
            .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User to UserAssignedProjects
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


        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.ClrType != typeof(User) && typeof(BaseModel).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property("CreatedBy")
                    .IsRequired();

                modelBuilder.Entity(entityType.ClrType)
                    .Property("UpdatedBy")
                    .IsRequired();
            }
        }


       
    }
}
//Data Source =.; Initial Catalog = HotelReservationSystem; Integrated Security = True; Connect Timeout = 30; Encrypt=True;Trust Server Certificate=True;Application Intent = ReadWrite; Multi Subnet Failover=False