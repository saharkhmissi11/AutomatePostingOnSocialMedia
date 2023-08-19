using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Inventoryhistory
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public int Status { get; set; }

    public int ProductId { get; set; }

    public int AuthorId { get; set; }

    public string TenantId { get; set; } = null!;

    public string? Description { get; set; }

    public virtual Member Author { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
