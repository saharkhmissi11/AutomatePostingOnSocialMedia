using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Placecategory
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public string? Title { get; set; }

    public int ProjectId { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual ICollection<Place> Places { get; set; } = new List<Place>();

    public virtual Project Project { get; set; } = null!;
}
