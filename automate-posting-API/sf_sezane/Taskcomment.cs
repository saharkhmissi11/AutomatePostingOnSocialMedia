using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Taskcomment
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public string? Comment { get; set; }

    public int AuthorId { get; set; }

    public int TaskId { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual Member Author { get; set; } = null!;

    public virtual Task Task { get; set; } = null!;
}
