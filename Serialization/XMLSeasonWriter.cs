using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using ProjectCarsSeasonExtension.Models.Player;

namespace ProjectCarsSeasonExtension.Serialization
{
    public class XmlSeasonWriter : ISeasonWriter
    {
        public void SavePlayers(IEnumerable<Player> players)
        {
            string fileName = FileLocations.PlayerFileUri;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Player>));

            using (var writer = new StreamWriter(fileName))
            {
                xmlSerializer.Serialize(writer, players.ToList());
                
                writer.Flush();
            }
        }

        public void SavePlayerResults(IEnumerable<PlayerResult> playerResults)
        {
            string fileName = FileLocations.PlayerResultFileUri;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<PlayerResult>));

            using (var writer = new StreamWriter(fileName))
            {
                xmlSerializer.Serialize(writer, playerResults.ToList());

                writer.Flush();
            }

        }
    }
}
