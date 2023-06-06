using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;
using WebApiAdvance.Entities;
using WebApiAdvance.Entities.Auth;

namespace WebApiAdvance.DAL.EfCore;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Product>()
        //            .Property(p => p.Price)
        //            .IsRequired()
        //            .HasColumnType(SqlDbType.Float.ToString())
        //            .HasDefaultValue(0);
        //modelBuilder.Entity<Product>()
        //            .Property(p => p.Name) 
        //            .HasMaxLength(100)
        //            .HasColumnType(SqlDbType.NVarChar.ToString())
        //            .HasDefaultValue("PPPP");
        //modelBuilder.Entity<Brand>()
        //    .Property(b => b.Name)
        //    .HasMaxLength(100)
        //    .HasColumnType(SqlDbType.NText.ToString())
        //.HasDefaultValue("XXXX");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Product>? Products { get; set; }
    public DbSet<Brand>? Brands { get; set; }
}
