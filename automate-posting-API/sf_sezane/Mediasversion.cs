using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Mediasversion
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public string? VersionName { get; set; }

    public string? Thumbnail { get; set; }

    public int MediaId { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual Media Media { get; set; } = null!;
}
