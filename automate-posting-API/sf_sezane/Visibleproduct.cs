using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Visibleproduct
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public int MediaId { get; set; }

    public int ProductId { get; set; }

    public string TenantId { get; set; } = null!;

    public int ProjectId { get; set; }

    public virtual Media Media { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
