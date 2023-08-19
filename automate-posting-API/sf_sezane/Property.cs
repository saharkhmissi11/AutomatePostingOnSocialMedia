using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Property
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public string? PropertyName { get; set; }

    public string TenantId { get; set; } = null!;

    public bool Displayable { get; set; }

    public bool Searchable { get; set; }

    public virtual ICollection<Productproperty> Productproperties { get; set; } = new List<Productproperty>();
}
