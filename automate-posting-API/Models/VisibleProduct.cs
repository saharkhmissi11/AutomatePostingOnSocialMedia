using System.ComponentModel.DataAnnotations.Schema;

namespace PostingOnSociallMedia.Models
{
    public class VisibleProduct : ProjectRelatedBaseModel
    {
        public int MediaId { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("MediaId")]
        public Media Media { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public bool IsVisible { get; set; }

    }
}
