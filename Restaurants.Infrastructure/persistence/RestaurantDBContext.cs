using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Resturants.Domain.Entities;
using Resturants.Domainntities;

namespace Restaurants.Infrastructure.persistence;
public class RestaurantDBContext(DbContextOptions<RestaurantDBContext> dbContextOptions) : IdentityDbContext<User>(dbContextOptions)
{
    internal DbSet<Restaurant> Restaurants { get; set; }
    internal DbSet<Dish> Dishes { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurant>(builder =>
        {
            builder.OwnsOne(r => r.address, address =>
            {   
                address.Property(a => a.City).HasColumnName("City").HasMaxLength(100);
                address.Property(a => a.Street).HasColumnName("Street").HasMaxLength(200);
                address.Property(a => a.PostalCode).HasColumnName("PostalCode").HasMaxLength(15);
            });
        });
        modelBuilder.Entity<Restaurant>()
            .HasMany(d => d.Dishes)
            .WithOne()
            .HasForeignKey(i => i.RestaurantId);

        modelBuilder.Entity<User>()
            .HasMany(o=>o.OwnedRestaurant).WithOne(r => r.Owner)
            .HasForeignKey(o => o.OwnerId);

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Categories");

            entity.HasKey(c => c.Id);

            entity.Property(c => c.Name)
                  .HasColumnType("VARCHAR(200)");

            entity.Property(c => c.Description)
                  .HasColumnType("VARCHAR(MAX)");

            entity.HasOne(c => c.Restaurant)
                  .WithMany(r => r.Categories)
                  .HasForeignKey(c => c.RestaurantId)
                  .IsRequired();
        });
        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(x => x.UserId);

            entity.HasIndex(x => new { x.UserId, x.RestaurantId })
                  .IsUnique(); // منع التكرار

            entity.HasOne(x => x.Restaurant)
                  .WithMany(r => r.StaffMembers)
                  .HasForeignKey(x => x.RestaurantId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.User)
                  .WithMany()
                  .HasForeignKey(x => x.UserId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.Property(x => x.Role)
                  .HasConversion<string>();
        });

    }
}
