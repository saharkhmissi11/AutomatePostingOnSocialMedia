namespace PostingOnSociallMedia.Models
{
    public class Product : BaseModel
    {
        public string Reference { get; set; }
        public string Title { get; set; }
        public string? Thumbnail { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public double Price { get; set; }
        public DateTime? AvailabilityDate { get; set; }
        public string? Color { get; set; } = string.Empty;
        public string? Size { get; set; } = string.Empty;
        public string? Comment { get; set; } = string.Empty;
        public int InventoryQty { get; set; }
        public virtual IEnumerable<VisibleProduct> VisibleIn { get; set; }
    }
}
