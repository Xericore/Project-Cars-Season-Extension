using System;
using System.Collections.Generic;

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
        public List<int> RookieSeasons { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
