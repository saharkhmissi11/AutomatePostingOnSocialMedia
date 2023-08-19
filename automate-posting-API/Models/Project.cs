namespace PostingOnSociallMedia.Models
{
    public class Project : BaseModel
    {
        public string Title { get; set; }
        public DateTime LastSyncDate { get; set; }
        public string? Thumbnail { get; set; }
        public bool NeedToBeSynchronized { get; set; }
        public int ShootingType { get; set; }


    }
}
