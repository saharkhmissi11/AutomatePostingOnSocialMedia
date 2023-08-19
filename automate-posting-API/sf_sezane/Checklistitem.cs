using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Checklistitem
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public bool Resolved { get; set; }

    public string? Description { get; set; }

    public int TaskId { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual Task Task { get; set; } = null!;
}
