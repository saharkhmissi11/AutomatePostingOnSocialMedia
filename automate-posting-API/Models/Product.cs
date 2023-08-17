using System.ComponentModel.DataAnnotations;

namespace PostingOnSocialMedia.Models
{
    public class Product
    {
        [Key]
        public string? ProductReference { get; set; }
        public string? ProductName { get; set; }
        public string? ProductUrl { get; set; }

    }
}
