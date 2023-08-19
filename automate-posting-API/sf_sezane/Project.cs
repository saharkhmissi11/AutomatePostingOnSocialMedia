using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Project
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int ShootingType { get; set; }

    public string? OwnerId { get; set; }

    public DateTime StartShootingDate { get; set; }

    public int Status { get; set; }

    public string? Thumbnail { get; set; }

    public DateTime LastSyncDate { get; set; }

    public string TenantId { get; set; } = null!;

    public string? RawMediasUrl { get; set; }

    public bool NeedToBeSynchronized { get; set; }

    public string? FolderCursor { get; set; }

    public virtual ICollection<Callsheetitem> Callsheetitems { get; set; } = new List<Callsheetitem>();

    public virtual ICollection<Contributorsinproject> Contributorsinprojects { get; set; } = new List<Contributorsinproject>();

    public virtual ICollection<File> Files { get; set; } = new List<File>();

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();

    public virtual ICollection<Placecategory> Placecategories { get; set; } = new List<Placecategory>();

    public virtual ICollection<Productinproject> Productinprojects { get; set; } = new List<Productinproject>();

    public virtual ICollection<Projectmember> Projectmembers { get; set; } = new List<Projectmember>();

    public virtual ICollection<Projectsetting> Projectsettings { get; set; } = new List<Projectsetting>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
