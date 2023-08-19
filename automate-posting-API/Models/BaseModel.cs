using System.ComponentModel.DataAnnotations.Schema;

namespace PostingOnSociallMedia.Models
{
    [NotMapped]
    public class BaseModel
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }
    }
}
