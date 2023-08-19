using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Place
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public int Status { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    public DateTime ReservationDate { get; set; }

    public string? Weather { get; set; }

    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public string? Icon { get; set; }

    public int PlaceCategoryId { get; set; }

    public string TenantId { get; set; } = null!;

    public virtual Placecategory PlaceCategory { get; set; } = null!;
}
