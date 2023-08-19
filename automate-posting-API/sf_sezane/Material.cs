using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Material
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public int Status { get; set; }

    public string? Title { get; set; }

    public double Price { get; set; }

    public string? Origin { get; set; }

    public DateTime CollectDate { get; set; }

    public int AssignedToId { get; set; }

    public int ProjectId { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual Member AssignedTo { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
