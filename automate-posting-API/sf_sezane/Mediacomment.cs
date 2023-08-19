using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Mediacomment
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public string? Comment { get; set; }

    public bool Resolved { get; set; }

    public string? Position { get; set; }

    public int AuthorId { get; set; }

    public int MediaId { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual Member Author { get; set; } = null!;

    public virtual Media Media { get; set; } = null!;
}
