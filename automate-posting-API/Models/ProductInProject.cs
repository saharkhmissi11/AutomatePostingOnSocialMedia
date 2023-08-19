using System.ComponentModel.DataAnnotations.Schema;

namespace PostingOnSociallMedia.Models
{
    public class ProductInProject : ProjectRelatedBaseModel
    {
        public bool Shooted { get; set; }
        public string StoragePath { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

    }
}