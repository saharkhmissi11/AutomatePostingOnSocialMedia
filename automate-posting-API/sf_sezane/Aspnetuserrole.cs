using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Aspnetuserrole
{
    public string UserId { get; set; } = null!;

    public string RoleId { get; set; } = null!;

    public string TenantId { get; set; } = null!;

    public virtual Aspnetrole Role { get; set; } = null!;

    public virtual Aspnetuser User { get; set; } = null!;
}
