using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class File
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public int Status { get; set; }

    public string? FilePath { get; set; }

    public string? FileName { get; set; }

    public string? FileType { get; set; }

    public double FileSize { get; set; }

    public string? CurrentVersionName { get; set; }

    public string? FileExternalId { get; set; }

    public int FileCategoryId { get; set; }

    public int ProjectId { get; set; }

    public int AuthorId { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual Member Author { get; set; } = null!;

    public virtual Filecategory FileCategory { get; set; } = null!;

    public virtual ICollection<Fileversion> Fileversions { get; set; } = new List<Fileversion>();

    public virtual Project Project { get; set; } = null!;
}
