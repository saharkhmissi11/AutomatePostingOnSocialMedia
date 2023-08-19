using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Aspnetuserlogin
{
    public string Id { get; set; } = null!;

    public string LoginProvider { get; set; } = null!;

    public string ProviderKey { get; set; } = null!;

    public string? ProviderDisplayName { get; set; }

    public string UserId { get; set; } = null!;

    public string TenantId { get; set; } = null!;

    public virtual Aspnetuser User { get; set; } = null!;
}
