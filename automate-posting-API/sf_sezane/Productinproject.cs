using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Productinproject
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public int ProductId { get; set; }

    public int ProjectId { get; set; }

    public string TenantId { get; set; } = null!;

    public bool Shooted { get; set; }

    public string? StoragePath { get; set; }

    public string? Comment { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
