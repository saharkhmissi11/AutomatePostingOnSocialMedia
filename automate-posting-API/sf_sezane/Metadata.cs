using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Metadata
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public string? MetaName { get; set; }

    public string? MetaValue { get; set; }

    public int MediaId { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual Media Media { get; set; } = null!;
}
