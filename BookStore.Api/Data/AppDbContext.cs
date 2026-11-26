// Data/AppDbContext.cs
using BookStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Publisher> Publishers => Set<Publisher>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Book>(b =>
        {
            b.Property(x => x.Title).IsRequired().HasMaxLength(200);
            b.Property(x => x.Author).IsRequired().HasMaxLength(150);
            b.Property(x => x.Description).HasMaxLength(2000);
            b.Property(x => x.Price).HasPrecision(18, 2);
            b.Property(x => x.DiscountPercent).HasPrecision(5, 2);
            b.Property(x => x.DiscountCode).HasMaxLength(50);

            b.HasOne(x => x.Category).WithMany(c => c.Books)
                .HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.Publisher).WithMany(p => p.Books)
                .HasForeignKey(x => x.PublisherId).OnDelete(DeleteBehavior.SetNull);
        });

        builder.Entity<Category>(c =>
        {
            c.Property(x => x.Name).IsRequired().HasMaxLength(100);
            c.HasIndex(x => x.Name).IsUnique();
        });

        builder.Entity<Publisher>(p =>
        {
            p.Property(x => x.Name).IsRequired().HasMaxLength(100);
            p.HasIndex(x => x.Name).IsUnique();
        });
    }
}