using System;

namespace ProjectCarsSeasonExtension.Models.Player
{
    [Serializable]
    public class Player : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
