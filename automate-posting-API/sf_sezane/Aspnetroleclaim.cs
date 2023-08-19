using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Aspnetroleclaim
{
    public int Id { get; set; }

    public string RoleId { get; set; } = null!;

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual Aspnetrole Role { get; set; } = null!;
}
