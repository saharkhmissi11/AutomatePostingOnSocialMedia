using System;
using System.Collections.Generic;

namespace PostingOnSociallMedia.sf_sezane;

public partial class Product
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public string? Reference { get; set; }

    public string? Title { get; set; }

    public int ImageCount { get; set; }

    public bool IsUrgent { get; set; }

    public string? Thumbnail { get; set; }

    public string? Barcode { get; set; }

    public double Price { get; set; }

    public DateTime? AvailabilityDate { get; set; }

    public int InventoryQty { get; set; }

    public string TenantId { get; set; } = null!;

    public string? Comment { get; set; }

    public virtual ICollection<Inventoryhistory> Inventoryhistories { get; set; } = new List<Inventoryhistory>();

    public virtual ICollection<Linkedproduct> LinkedproductProducts { get; set; } = new List<Linkedproduct>();

    public virtual ICollection<Linkedproduct> LinkedproductReferencedProducts { get; set; } = new List<Linkedproduct>();

    public virtual ICollection<Media> Media { get; set; } = new List<Media>();

    public virtual ICollection<Productinproject> Productinprojects { get; set; } = new List<Productinproject>();

    public virtual ICollection<Productproperty> Productproperties { get; set; } = new List<Productproperty>();

    public virtual ICollection<Visibleproduct> Visibleproducts { get; set; } = new List<Visibleproduct>();
}
