using MeetupAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetupAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Meetup> Meetups { get; set; }
    public DbSet<User>Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // modelBuilder.Entity<User>()
        //     .HasMany(ur => ur.UserRoles)
        //     .WithOne(u => u.User)
        //     .HasForeignKey(ur => ur.UserId)
        //     .IsRequired();
        //
        // modelBuilder.Entity<Role>()
        //     .HasMany(ur => ur.UserRoles)
        //     .WithOne(u => u.Role)
        //     .HasForeignKey(ur => ur.RoleId)
        //     .IsRequired();
    }
}