using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Notificationtype
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public string? NotificationName { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual ICollection<Membernotificationsetting> Membernotificationsettings { get; set; } = new List<Membernotificationsetting>();
}
