using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Filecategory
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public string? Title { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual ICollection<File> Files { get; set; } = new List<File>();
}
