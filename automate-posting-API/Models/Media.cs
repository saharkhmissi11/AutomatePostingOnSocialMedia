using System.ComponentModel.DataAnnotations.Schema;

namespace PostingOnSociallMedia.Models
{
    public class Media : ProjectRelatedBaseModel
    {
        public string URL { get; set; }
        public string PublicURL { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public string CurrentVersion { get; set; }
        public bool IsValid { get; set; }
        public bool IsSelected { get; set; }
        public string ExternalId { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
