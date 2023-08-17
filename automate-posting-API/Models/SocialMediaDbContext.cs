using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PostingOnSociallMedia.Models;

namespace PostingOnSocialMedia.Models
{
    public class SocialMediaDbContext:DbContext
    {
        public SocialMediaDbContext(DbContextOptions<SocialMediaDbContext> options)
        : base(options)
        {
        }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ImageUrl> ImageUrls { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImageUrl>().HasData(new ImageUrl { Platform = "Facebook"});
            modelBuilder.Entity<ImageUrl>().HasData(new ImageUrl { Platform = "Instagram" });
            modelBuilder.Entity<ImageUrl>().HasData(new ImageUrl { Platform = "Twitter" });
        }


    }
}
