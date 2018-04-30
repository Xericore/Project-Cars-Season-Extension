using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Models.Player;

namespace ProjectCarsSeasonExtension.Serialization
{
    public class XmlSeasonWriter : ISeasonWriter
    {
        public void SavePlayers(IEnumerable<Player> players)
        {
            SerializeList(players, FileLocations.PlayerFileUri);
        }

        public void SaveChallenges(IEnumerable<Challenge> challenges)
        {
            SerializeList(challenges, FileLocations.ChallangeFileUri);
        }

        public void SaveSeason(SeasonModel season)
        {
            var xmlSerializer = new XmlSerializer(typeof(SeasonModel));

            using (var writer = new StreamWriter(FileLocations.SeasonFileUri))
            {
                xmlSerializer.Serialize(writer, season);
                writer.Flush();
            }
        }

        public void SavePlayerResults(IEnumerable<PlayerResult> playerResults)
        {
            SerializeList(playerResults, FileLocations.PlayerResultFileUri);
        }

        public void SaveHandicaps(IEnumerable<PlayerHandicap> handicaps)
        {
            SerializeList(handicaps, FileLocations.HandicapsFileUri);
        }

        private static void SerializeList<T>(IEnumerable<T> enumerableToSerialize, string fileName)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<T>));

            CreateDirectoryIfMissing(fileName);

            using (var writer = new StreamWriter(fileName))
            {
                xmlSerializer.Serialize(writer, enumerableToSerialize.ToList());
                writer.Flush();
            }
        }

        private static void CreateDirectoryIfMissing(string fileName)
        {
            var directoryName = Path.GetDirectoryName(fileName);

            if (!string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
        }
    }
}
