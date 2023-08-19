using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Membernotificationsetting
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public int MemberId { get; set; }

    public int NotificationTypeId { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;

    public virtual Notificationtype NotificationType { get; set; } = null!;
}
