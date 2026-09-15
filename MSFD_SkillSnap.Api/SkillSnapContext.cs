using Microsoft.EntityFrameworkCore;
using MSFD_SkillSnap.Api.Models;

public class SkillSnapContext : DbContext
{
    public SkillSnapContext(DbContextOptions<SkillSnapContext> options) : base(options) { }
    public DbSet<PortfolioUser> PortfolioUsers { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Skill> Skills { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // One PortfolioUser has many Projects
        modelBuilder.Entity<PortfolioUser>()
            .HasMany(u => u.Projects)
            .WithOne(p => p.PortfolioUser)
            .HasForeignKey(p => p.PortfolioUserId)
            .OnDelete(DeleteBehavior.Cascade);

        // One PortfolioUser has many Skills
        modelBuilder.Entity<PortfolioUser>()
            .HasMany(u => u.Skills)
            .WithOne(s => s.PortfolioUser)
            .HasForeignKey(s => s.PortfolioUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}