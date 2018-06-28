using System;

namespace ProjectCarsSeasonExtension.Models.Player
{
    [Serializable]
    public class Player : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string AvatarFileName { get; set; }
        public AuthenticationGroup Group { get; set; }
    }
}
