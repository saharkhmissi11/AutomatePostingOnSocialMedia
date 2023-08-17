using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PostingOnSociallMedia.Models
{
    public class ImageUrl
    {
        [Key]
        public string Platform { get; set; }
        public string? Url { get; set; }
        
    }
}
