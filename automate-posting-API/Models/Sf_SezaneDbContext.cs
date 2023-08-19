
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PostingOnSociallMedia.Models;

namespace PostingOnSocialMedia.Models
{
    public class Sf_SezaneDbContext : DbContext
    {
        public Sf_SezaneDbContext(DbContextOptions<Sf_SezaneDbContext> options)
        : base(options)
        { }
        public virtual DbSet<Media> Medias { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Project> Projets { get; set; }
        public virtual DbSet<VisibleProduct> VisibleProducts { get; set; }
        public virtual DbSet<ProductInProject> ProductInProjects { get; set; }
       
    }

}

