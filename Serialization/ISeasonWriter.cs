using System.Collections.Generic;
using ProjectCarsSeasonExtension.Models.Player;

namespace ProjectCarsSeasonExtension.Serialization
{
    public interface ISeasonWriter
    {
        void SavePlayers(IEnumerable<Player> players);
    }
}
