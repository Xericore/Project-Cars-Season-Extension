using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Serialization;
using ProjectCarsSeasonExtension.ViewModels;
using ProjectCarsSeasonExtension.Views;

namespace ProjectCarsSeasonExtensionTests
{
    [TestClass]
    public class ChampionshipTest
    {
        [TestMethod]
        public void ChampionshipViewTest()
        {
            ISeasonReader seasonReader = new SeasonReaderTest();
            var dataView = new DataView(seasonReader);

            var allChallengeStandings = new AllChallengeStandings(dataView);

            var championshipView = new ChampionshipView(dataView, allChallengeStandings);

            //var mario = championshipView.ChampionshipStandings.FirstOrDefault(p => p.Player.Name == "Mario");
            Assert.Fail("Can't test without referencing PresentationFramework. Please refactor classes to decouple the logic from the UI.");
        }
    }
}
