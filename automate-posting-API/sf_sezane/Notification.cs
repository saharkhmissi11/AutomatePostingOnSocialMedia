using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Notification
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public string? Message { get; set; }

    public string? ObjectType { get; set; }

    public int ObjectId { get; set; }

    public string TenantId { get; set; } = null!;

    public int NotificationCategory { get; set; }

    public virtual ICollection<Notificationmember> Notificationmembers { get; set; } = new List<Notificationmember>();
}
