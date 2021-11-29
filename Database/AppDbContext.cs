using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<BoardModel> Boards { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BoardModel>(board =>
            {
                board.Property(b => b.Name).IsRequired().HasMaxLength(100);
                board.Property(b => b.Description).HasMaxLength(400);
                board.Property(b => b.DateCreated).IsRequired();

                board.HasOne(b => b.Category)
                    .WithMany(c => c.Boards)
                    .HasForeignKey(b => b.CategoryId);
            });

            builder.Entity<CategoryModel>(category =>
            {
                category.Property(c => c.Name).IsRequired().HasMaxLength(100);
                category.Property(c => c.DateCreated).IsRequired();
            });
        }
    }
}