using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<BoardModel> Boards { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<ThreadModel> Threads { get; set; }
        public DbSet<FileModel> Files { get; set; }

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

            builder.Entity<ThreadModel>(thread =>
            {
                thread.Property(t => t.Name).IsRequired().HasMaxLength(100);
                thread.Property(t => t.Text).IsRequired().HasMaxLength(500);
                thread.Property(t => t.DateCreated).IsRequired();

                thread.HasOne(t => t.Board)
                    .WithMany(b => b.Threads)
                    .HasForeignKey(t => t.BoardId);
            });

            builder.Entity<FileModel>(file =>
            {
                file.Property(f => f.Name).IsRequired().HasMaxLength(30);
                file.Property(f => f.Path).IsRequired().HasMaxLength(100);
                file.Property(f => f.Extension).IsRequired().HasMaxLength(5);
                file.Property(f => f.DateCreated).IsRequired();

                file.HasOne(f => f.Thread)
                    .WithMany(t => t.Files)
                    .HasForeignKey(f => f.ThreadId);
            });
        }
    }
}