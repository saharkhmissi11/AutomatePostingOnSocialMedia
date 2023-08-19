using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Media
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public int Status { get; set; }

    public string? Url { get; set; }

    public string? PublicUrl { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public string? Title { get; set; }

    public string? Thumbnail { get; set; }

    public string? CurrentVersion { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsValid { get; set; }

    public bool IsSelected { get; set; }

    public string? ExternalId { get; set; }

    public int ProductId { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual ICollection<Mediacomment> Mediacomments { get; set; } = new List<Mediacomment>();

    public virtual ICollection<Mediasversion> Mediasversions { get; set; } = new List<Mediasversion>();

    public virtual ICollection<Metadata> Metadata { get; set; } = new List<Metadata>();

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<Visibleproduct> Visibleproducts { get; set; } = new List<Visibleproduct>();
}
