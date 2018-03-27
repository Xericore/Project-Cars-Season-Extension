namespace ProjectCarsSeasonExtension.Models
{
    public class Challenge : BaseModel
    {
        public int Id { get; set; }
        public string TrackName { get; set; }
        public string CarName { get; set; }
        public string Description { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
