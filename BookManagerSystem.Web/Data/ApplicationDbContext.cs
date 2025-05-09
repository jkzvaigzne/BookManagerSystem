using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookManagerSystem.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    builder.Entity<Book>(entity =>
        //    {
        //        entity.HasKey(b => b.Id);

        //        entity.Property(b => b.Title)
        //              .IsRequired()
        //              .HasMaxLength(150)
        //              .HasColumnType("nvarchar(150)");

        //        entity.Property(b => b.Author)
        //              .IsRequired()
        //              .HasMaxLength(150)
        //              .HasColumnType("nvarchar(150)");

        //        entity.Property(b => b.PublishedDate)
        //              .HasColumnType("datetime");
        //    });
        //}
    }
}
