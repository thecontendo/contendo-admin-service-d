using ContendoAdmin.Extensions.EntityConfiguration;
using ContendoAdmin.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace ContendoAdmin.Db.Context;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyEntityConfigurations();
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    // public DbSet<User> TodoItems => Set<User>();
}