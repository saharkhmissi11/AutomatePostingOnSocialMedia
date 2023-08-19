using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Notificationmember
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public int NotificationId { get; set; }

    public int MemberId { get; set; }

    public bool IsRead { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;

    public virtual Notification Notification { get; set; } = null!;
}
