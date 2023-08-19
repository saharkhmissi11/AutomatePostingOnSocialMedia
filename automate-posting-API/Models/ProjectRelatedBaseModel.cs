using System.ComponentModel.DataAnnotations.Schema;
namespace PostingOnSociallMedia.Models
{
    [NotMapped]
    public class ProjectRelatedBaseModel : BaseModel
    {
        public int ProjectId { get; set; }
    }
}
