using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Member
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public string? Name { get; set; }

    public string? ProfilePicture { get; set; }

    public string? UserId { get; set; }

    public string? Email { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual ICollection<Calltimemember> Calltimemembers { get; set; } = new List<Calltimemember>();

    public virtual ICollection<File> Files { get; set; } = new List<File>();

    public virtual ICollection<Fileversioncomment> Fileversioncomments { get; set; } = new List<Fileversioncomment>();

    public virtual ICollection<Inventoryhistory> Inventoryhistories { get; set; } = new List<Inventoryhistory>();

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();

    public virtual ICollection<Mediacomment> Mediacomments { get; set; } = new List<Mediacomment>();

    public virtual ICollection<Membernotificationsetting> Membernotificationsettings { get; set; } = new List<Membernotificationsetting>();

    public virtual ICollection<Notificationmember> Notificationmembers { get; set; } = new List<Notificationmember>();

    public virtual ICollection<Projectmember> Projectmembers { get; set; } = new List<Projectmember>();

    public virtual ICollection<Taskcomment> Taskcomments { get; set; } = new List<Taskcomment>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
