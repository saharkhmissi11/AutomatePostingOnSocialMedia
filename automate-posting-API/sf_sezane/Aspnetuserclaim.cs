﻿using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Aspnetuserclaim
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual Aspnetuser User { get; set; } = null!;
}