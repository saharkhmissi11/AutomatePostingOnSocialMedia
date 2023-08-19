using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PostingOnSociallMedia.sf_sezane;

public partial class SfSezaneContext : DbContext
{
    public SfSezaneContext()
    {
    }

    public SfSezaneContext(DbContextOptions<SfSezaneContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aspnetrole> Aspnetroles { get; set; }

    public virtual DbSet<Aspnetroleclaim> Aspnetroleclaims { get; set; }

    public virtual DbSet<Aspnetuser> Aspnetusers { get; set; }

    public virtual DbSet<Aspnetuserclaim> Aspnetuserclaims { get; set; }

    public virtual DbSet<Aspnetuserlogin> Aspnetuserlogins { get; set; }

    public virtual DbSet<Aspnetuserrole> Aspnetuserroles { get; set; }

    public virtual DbSet<Aspnetusertoken> Aspnetusertokens { get; set; }

    public virtual DbSet<Callsheetitem> Callsheetitems { get; set; }

    public virtual DbSet<Calltime> Calltimes { get; set; }

    public virtual DbSet<Calltimemember> Calltimemembers { get; set; }

    public virtual DbSet<Checklistitem> Checklistitems { get; set; }

    public virtual DbSet<Contributor> Contributors { get; set; }

    public virtual DbSet<Contributorsinproject> Contributorsinprojects { get; set; }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    public virtual DbSet<File> Files { get; set; }

    public virtual DbSet<Filecategory> Filecategories { get; set; }

    public virtual DbSet<Fileversion> Fileversions { get; set; }

    public virtual DbSet<Fileversioncomment> Fileversioncomments { get; set; }

    public virtual DbSet<Inventoryhistory> Inventoryhistories { get; set; }

    public virtual DbSet<Linkedproduct> Linkedproducts { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Media> Medias { get; set; }

    public virtual DbSet<Mediacomment> Mediacomments { get; set; }

    public virtual DbSet<Mediasversion> Mediasversions { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Membernotificationsetting> Membernotificationsettings { get; set; }

    public virtual DbSet<Metadata> Metadatas { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Notificationmember> Notificationmembers { get; set; }

    public virtual DbSet<Notificationtype> Notificationtypes { get; set; }

    public virtual DbSet<Place> Places { get; set; }

    public virtual DbSet<Placecategory> Placecategories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Productinproject> Productinprojects { get; set; }

    public virtual DbSet<Productproperty> Productproperties { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Projectmember> Projectmembers { get; set; }

    public virtual DbSet<Projectsetting> Projectsettings { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<Taskcomment> Taskcomments { get; set; }

    public virtual DbSet<Visibleproduct> Visibleproducts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;port=3306;database=sf_sezane;user=root;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aspnetrole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetroles");

            entity.HasIndex(e => new { e.NormalizedName, e.TenantId }, "RoleNameIndex").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
            entity.Property(e => e.TenantId).HasMaxLength(64);
        });

        modelBuilder.Entity<Aspnetroleclaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetroleclaims");

            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.Property(e => e.TenantId).HasMaxLength(64);

            entity.HasOne(d => d.Role).WithMany(p => p.Aspnetroleclaims)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_AspNetRoleClaims_AspNetRoles_RoleId");
        });

        modelBuilder.Entity<Aspnetuser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetusers");

            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => new { e.NormalizedUserName, e.TenantId }, "UserNameIndex").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.LockoutEnd).HasMaxLength(6);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.TenantId).HasMaxLength(64);
            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        modelBuilder.Entity<Aspnetuserclaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetuserclaims");

            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.Property(e => e.TenantId).HasMaxLength(64);

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetuserclaims)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserClaims_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Aspnetuserlogin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetuserlogins");

            entity.HasIndex(e => new { e.LoginProvider, e.ProviderKey, e.TenantId }, "IX_AspNetUserLogins_LoginProvider_ProviderKey_TenantId").IsUnique();

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.TenantId).HasMaxLength(64);

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetuserlogins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserLogins_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Aspnetuserrole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId }).HasName("PRIMARY");

            entity.ToTable("aspnetuserroles");

            entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

            entity.Property(e => e.TenantId).HasMaxLength(64);

            entity.HasOne(d => d.Role).WithMany(p => p.Aspnetuserroles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_AspNetUserRoles_AspNetRoles_RoleId");

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetuserroles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserRoles_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Aspnetusertoken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name }).HasName("PRIMARY");

            entity.ToTable("aspnetusertokens");

            entity.Property(e => e.TenantId).HasMaxLength(64);

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetusertokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserTokens_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Callsheetitem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("callsheetitems");

            entity.HasIndex(e => e.ProjectId, "IX_CallSheetItems_ProjectId");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.Date).HasMaxLength(6);
            entity.Property(e => e.TenantId)
                .HasMaxLength(64)
                .HasDefaultValueSql("''");

            entity.HasOne(d => d.Project).WithMany(p => p.Callsheetitems)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_CallSheetItems_Projects_ProjectId");
        });

        modelBuilder.Entity<Calltime>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("calltimes");

            entity.HasIndex(e => e.CallSheetItemId, "IX_CallTimes_CallSheetItemId");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);
            entity.Property(e => e.Time).HasMaxLength(6);

            entity.HasOne(d => d.CallSheetItem).WithMany(p => p.Calltimes)
                .HasForeignKey(d => d.CallSheetItemId)
                .HasConstraintName("FK_CallTimes_CallSheetItems_CallSheetItemId");
        });

        modelBuilder.Entity<Calltimemember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("calltimemembers");

            entity.HasIndex(e => e.CallSheetItemId, "IX_CallTimeMembers_CallSheetItemId");

            entity.HasIndex(e => e.ContributorId, "IX_CallTimeMembers_ContributorId");

            entity.HasIndex(e => e.MemberId, "IX_CallTimeMembers_MemberId");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);

            entity.HasOne(d => d.CallSheetItem).WithMany(p => p.Calltimemembers)
                .HasForeignKey(d => d.CallSheetItemId)
                .HasConstraintName("FK_CallTimeMembers_CallSheetItems_CallSheetItemId");

            entity.HasOne(d => d.Contributor).WithMany(p => p.Calltimemembers)
                .HasForeignKey(d => d.ContributorId)
                .HasConstraintName("FK_CallTimeMembers_Contributors_ContributorId");

            entity.HasOne(d => d.Member).WithMany(p => p.Calltimemembers)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_CallTimeMembers_Members_MemberId");
        });

        modelBuilder.Entity<Checklistitem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("checklistitems");

            entity.HasIndex(e => e.TaskId, "IX_CheckListItems_TaskId");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);

            entity.HasOne(d => d.Task).WithMany(p => p.Checklistitems)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK_CheckListItems_Tasks_TaskId");
        });

        modelBuilder.Entity<Contributor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("contributors");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);
        });

        modelBuilder.Entity<Contributorsinproject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("contributorsinproject");

            entity.HasIndex(e => e.ContributorId, "IX_ContributorsInProject_ContributorId");

            entity.HasIndex(e => e.ProjectId, "IX_ContributorsInProject_ProjectId");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.EndDate).HasMaxLength(6);
            entity.Property(e => e.StartDate).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);

            entity.HasOne(d => d.Contributor).WithMany(p => p.Contributorsinprojects)
                .HasForeignKey(d => d.ContributorId)
                .HasConstraintName("FK_ContributorsInProject_Contributors_ContributorId");

            entity.HasOne(d => d.Project).WithMany(p => p.Contributorsinprojects)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_ContributorsInProject_Projects_ProjectId");
        });

        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__efmigrationshistory");

            entity.Property(e => e.MigrationId).HasMaxLength(95);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<File>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("files");

            entity.HasIndex(e => e.AuthorId, "IX_Files_AuthorId");

            entity.HasIndex(e => e.FileCategoryId, "IX_Files_FileCategoryId");

            entity.HasIndex(e => e.ProjectId, "IX_Files_ProjectId");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);

            entity.HasOne(d => d.Author).WithMany(p => p.Files)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_Files_Members_AuthorId");

            entity.HasOne(d => d.FileCategory).WithMany(p => p.Files)
                .HasForeignKey(d => d.FileCategoryId)
                .HasConstraintName("FK_Files_FileCategories_FileCategoryId");

            entity.HasOne(d => d.Project).WithMany(p => p.Files)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_Files_Projects_ProjectId");
        });

        modelBuilder.Entity<Filecategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("filecategories");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);
        });

        modelBuilder.Entity<Fileversion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("fileversions");

            entity.HasIndex(e => e.FileId, "IX_FileVersions_FileId");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId)
                .HasMaxLength(64)
                .HasDefaultValueSql("''");

            entity.HasOne(d => d.File).WithMany(p => p.Fileversions)
                .HasForeignKey(d => d.FileId)
                .HasConstraintName("FK_FileVersions_Files_FileId");
        });

        modelBuilder.Entity<Fileversioncomment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("fileversioncomments");

            entity.HasIndex(e => e.AuthorId, "IX_FileVersionComments_AuthorId");

            entity.HasIndex(e => e.FileVersionId, "IX_FileVersionComments_FileVersionId");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);

            entity.HasOne(d => d.Author).WithMany(p => p.Fileversioncomments)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_FileVersionComments_Members_AuthorId");

            entity.HasOne(d => d.FileVersion).WithMany(p => p.Fileversioncomments)
                .HasForeignKey(d => d.FileVersionId)
                .HasConstraintName("FK_FileVersionComments_FileVersions_FileVersionId");
        });

        modelBuilder.Entity<Inventoryhistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("inventoryhistory");

            entity.HasIndex(e => e.AuthorId, "IX_InventoryHistory_AuthorId");

            entity.HasIndex(e => e.ProductId, "IX_InventoryHistory_ProductId");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);

            entity.HasOne(d => d.Author).WithMany(p => p.Inventoryhistories)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_InventoryHistory_Members_AuthorId");

            entity.HasOne(d => d.Product).WithMany(p => p.Inventoryhistories)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_InventoryHistory_Products_ProductId");
        });

        modelBuilder.Entity<Linkedproduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("linkedproducts");

            entity.HasIndex(e => e.ProductId, "IX_LinkedProducts_ProductId");

            entity.HasIndex(e => e.ProjectId, "IX_LinkedProducts_ProjectId");

            entity.HasIndex(e => e.ReferencedProductId, "IX_LinkedProducts_ReferencedProductId");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);

            entity.HasOne(d => d.Product).WithMany(p => p.LinkedproductProducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_LinkedProducts_Products_ProductId");

            entity.HasOne(d => d.ReferencedProduct).WithMany(p => p.LinkedproductReferencedProducts)
                .HasForeignKey(d => d.ReferencedProductId)
                .HasConstraintName("FK_LinkedProducts_Products_ReferencedProductId");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("materials");

            entity.HasIndex(e => e.AssignedToId, "IX_Materials_AssignedToId");

            entity.HasIndex(e => e.ProjectId, "IX_Materials_ProjectId");

            entity.Property(e => e.CollectDate).HasMaxLength(6);
            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);

            entity.HasOne(d => d.AssignedTo).WithMany(p => p.Materials)
                .HasForeignKey(d => d.AssignedToId)
                .HasConstraintName("FK_Materials_Members_AssignedToId");

            entity.HasOne(d => d.Project).WithMany(p => p.Materials)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_Materials_Projects_ProjectId");
        });

        modelBuilder.Entity<Media>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("medias");

            entity.HasIndex(e => e.ProductId, "IX_Medias_ProductId");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.PublicUrl).HasColumnName("PublicURL");
            entity.Property(e => e.TenantId).HasMaxLength(64);
            entity.Property(e => e.Url).HasColumnName("URL");

            entity.HasOne(d => d.Product).WithMany(p => p.Media)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Medias_Products_ProductId");
        });

        modelBuilder.Entity<Mediacomment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("mediacomments");

            entity.HasIndex(e => e.AuthorId, "IX_MediaComments_AuthorId");

            entity.HasIndex(e => e.MediaId, "IX_MediaComments_MediaId");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);

            entity.HasOne(d => d.Author).WithMany(p => p.Mediacomments)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_MediaComments_Members_AuthorId");

            entity.HasOne(d => d.Media).WithMany(p => p.Mediacomments)
                .HasForeignKey(d => d.MediaId)
                .HasConstraintName("FK_MediaComments_Medias_MediaId");
        });

        modelBuilder.Entity<Mediasversion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("mediasversions");

            entity.HasIndex(e => e.MediaId, "IX_MediasVersions_MediaId");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);

            entity.HasOne(d => d.Media).WithMany(p => p.Mediasversions)
                .HasForeignKey(d => d.MediaId)
                .HasConstraintName("FK_MediasVersions_Medias_MediaId");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("members");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);
        });

        modelBuilder.Entity<Membernotificationsetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("membernotificationsettings");

            entity.HasIndex(e => e.MemberId, "IX_MemberNotificationSettings_MemberId");

            entity.HasIndex(e => e.NotificationTypeId, "IX_MemberNotificationSettings_NotificationTypeId");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);

            entity.HasOne(d => d.Member).WithMany(p => p.Membernotificationsettings)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_MemberNotificationSettings_Members_MemberId");

            entity.HasOne(d => d.NotificationType).WithMany(p => p.Membernotificationsettings)
                .HasForeignKey(d => d.NotificationTypeId)
                .HasConstraintName("FK_MemberNotificationSettings_NotificationTypeId");
        });

        modelBuilder.Entity<Metadata>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("metadatas");

            entity.HasIndex(e => e.MediaId, "IX_MetaDatas_MediaId");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);

            entity.HasOne(d => d.Media).WithMany(p => p.Metadata)
                .HasForeignKey(d => d.MediaId)
                .HasConstraintName("FK_MetaDatas_Medias_MediaId");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("notifications");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);
        });

        modelBuilder.Entity<Notificationmember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("notificationmembers");

            entity.HasIndex(e => e.MemberId, "IX_NotificationMembers_MemberId");

            entity.HasIndex(e => e.NotificationId, "IX_NotificationMembers_NotificationId");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);

            entity.HasOne(d => d.Member).WithMany(p => p.Notificationmembers)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_NotificationMembers_Members_MemberId");

            entity.HasOne(d => d.Notification).WithMany(p => p.Notificationmembers)
                .HasForeignKey(d => d.NotificationId)
                .HasConstraintName("FK_NotificationMembers_Notifications_NotificationId");
        });

        modelBuilder.Entity<Notificationtype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("notificationtypes");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);
        });

        modelBuilder.Entity<Place>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("places");

            entity.HasIndex(e => e.PlaceCategoryId, "IX_Places_PlaceCategoryId");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.ReservationDate).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);

            entity.HasOne(d => d.PlaceCategory).WithMany(p => p.Places)
                .HasForeignKey(d => d.PlaceCategoryId)
                .HasConstraintName("FK_Places_PlaceCategories_PlaceCategoryId");
        });

        modelBuilder.Entity<Placecategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("placecategories");

            entity.HasIndex(e => e.ProjectId, "IX_PlaceCategories_ProjectId");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);

            entity.HasOne(d => d.Project).WithMany(p => p.Placecategories)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_PlaceCategories_Projects_ProjectId");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("products");

            entity.Property(e => e.AvailabilityDate).HasMaxLength(6);
            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);
        });

        modelBuilder.Entity<Productinproject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("productinprojects");

            entity.HasIndex(e => e.ProductId, "IX_ProductInProjects_ProductId");

            entity.HasIndex(e => e.ProjectId, "IX_ProductInProjects_ProjectId");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);

            entity.HasOne(d => d.Product).WithMany(p => p.Productinprojects)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductInProjects_Products_ProductId");

            entity.HasOne(d => d.Project).WithMany(p => p.Productinprojects)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_ProductInProjects_Projects_ProjectId");
        });

        modelBuilder.Entity<Productproperty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("productproperties");

            entity.HasIndex(e => e.ProductId, "IX_ProductProperties_ProductId");

            entity.HasIndex(e => e.PropertyId, "IX_ProductProperties_PropertyId");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);

            entity.HasOne(d => d.Product).WithMany(p => p.Productproperties)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductProperties_Products_ProductId");

            entity.HasOne(d => d.Property).WithMany(p => p.Productproperties)
                .HasForeignKey(d => d.PropertyId)
                .HasConstraintName("FK_ProductProperties_Properties_PropertyId");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("projects");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.LastSyncDate).HasMaxLength(6);
            entity.Property(e => e.StartShootingDate).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);
        });

        modelBuilder.Entity<Projectmember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("projectmembers");

            entity.HasIndex(e => e.MemberId, "IX_ProjectMembers_MemberId");

            entity.HasIndex(e => e.ProjectId, "IX_ProjectMembers_ProjectId");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);

            entity.HasOne(d => d.Member).WithMany(p => p.Projectmembers)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_ProjectMembers_Members_MemberId");

            entity.HasOne(d => d.Project).WithMany(p => p.Projectmembers)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_ProjectMembers_Projects_ProjectId");
        });

        modelBuilder.Entity<Projectsetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("projectsettings");

            entity.HasIndex(e => e.ProjectId, "IX_ProjectSettings_ProjectId");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);

            entity.HasOne(d => d.Project).WithMany(p => p.Projectsettings)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_ProjectSettings_Projects_ProjectId");
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("properties");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("settings");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tasks");

            entity.HasIndex(e => e.MemberId, "IX_Tasks_MemberId");

            entity.HasIndex(e => e.ProjectId, "IX_Tasks_ProjectId");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId).HasMaxLength(64);

            entity.HasOne(d => d.Member).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_Tasks_Members_MemberId");

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_Tasks_Projects_ProjectId");
        });

        modelBuilder.Entity<Taskcomment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("taskcomments");

            entity.HasIndex(e => e.AuthorId, "IX_TaskComments_AuthorId");

            entity.HasIndex(e => e.TaskId, "IX_TaskComments_TaskId");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId)
                .HasMaxLength(64)
                .HasDefaultValueSql("''");

            entity.HasOne(d => d.Author).WithMany(p => p.Taskcomments)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_TaskComments_Members_AuthorId");

            entity.HasOne(d => d.Task).WithMany(p => p.Taskcomments)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK_TaskComments_Tasks_TaskId");
        });

        modelBuilder.Entity<Visibleproduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("visibleproduct");

            entity.HasIndex(e => e.MediaId, "IX_VisibleProduct_MediaId");

            entity.HasIndex(e => e.ProductId, "IX_VisibleProduct_ProductId");

            entity.HasIndex(e => e.ProjectId, "IX_VisibleProduct_ProjectId");

            entity.Property(e => e.Created).HasMaxLength(6);
            entity.Property(e => e.TenantId)
                .HasMaxLength(64)
                .HasDefaultValueSql("''");

            entity.HasOne(d => d.Media).WithMany(p => p.Visibleproducts)
                .HasForeignKey(d => d.MediaId)
                .HasConstraintName("FK_VisibleProduct_Medias_MediaId");

            entity.HasOne(d => d.Product).WithMany(p => p.Visibleproducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_VisibleProduct_Products_ProductId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
