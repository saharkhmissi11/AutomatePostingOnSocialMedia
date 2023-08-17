using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostingOnSocialMedia.Models
{
    public class Image
    {
        public string ImageId { get; set; }
        public string? ImagePath { get; set; }
        public string? DisplayPath { get; set; }
        public string? ImageTitle { get; set; }
        public string? PrimaryProduct { get; set; }
        public string? SecondaryProducts { get; set; }
    }
}
