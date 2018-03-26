using System.Collections.ObjectModel;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.Serialization
{
    public interface ISeasonReader
    {
        SeasonModel GetCurrentSeason();
        ObservableCollection<PlayerResult> GetPlayerResults();
    }
}
