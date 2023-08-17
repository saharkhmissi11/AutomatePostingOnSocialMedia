﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PostingOnSocialMedia.Models;

#nullable disable

namespace PostingOnSocialMedia.Migrations
{
    [DbContext(typeof(SocialMediaDbContext))]
    partial class SocialMediaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PostingOnSocialMedia.Models.Image", b =>
                {
                    b.Property<string>("ImageId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DisplayPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimaryProduct")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondaryProducts")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("PostingOnSocialMedia.Models.Product", b =>
                {
                    b.Property<string>("ProductReference")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductReference");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("PostingOnSociallMedia.Models.ImageUrl", b =>
                {
                    b.Property<string>("Platform")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Platform");

                    b.ToTable("ImageUrls");

                    b.HasData(
                        new
                        {
                            Platform = "Facebook"
                        },
                        new
                        {
                            Platform = "Instagram"
                        },
                        new
                        {
                            Platform = "Twitter"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}