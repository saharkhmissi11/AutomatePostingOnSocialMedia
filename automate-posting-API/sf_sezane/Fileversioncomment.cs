using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Fileversioncomment
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public string? Comment { get; set; }

    public bool Resolved { get; set; }

    public string? Position { get; set; }

    public int AuthorId { get; set; }

    public int FileVersionId { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual Member Author { get; set; } = null!;

    public virtual Fileversion FileVersion { get; set; } = null!;
}
