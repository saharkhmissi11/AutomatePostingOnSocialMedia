using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Productproperty
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public string? Value { get; set; }

    public int ProductId { get; set; }

    public int PropertyId { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual Property Property { get; set; } = null!;
}
