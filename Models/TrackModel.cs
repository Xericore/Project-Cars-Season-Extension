using System.Collections.Generic;

namespace ProjectCarsSeasonExtension.Models
{
    public class TrackModel
    {
        public string TrackName { get; set; }
        public string Description { get; set; }
        public List<PlayerModel> PlayerList { get; set; } = new List<PlayerModel>();
    }
}
