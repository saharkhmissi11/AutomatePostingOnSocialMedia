using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Fileversion
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public string? VersionName { get; set; }

    public int FileId { get; set; }

    public string? Thumbnail { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual File File { get; set; } = null!;

    public virtual ICollection<Fileversioncomment> Fileversioncomments { get; set; } = new List<Fileversioncomment>();
}
