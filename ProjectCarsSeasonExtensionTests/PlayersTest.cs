using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Serialization;
using ProjectCarsSeasonExtension.ViewModels;
using ProjectCarsSeasonExtension.Views;

namespace ProjectCarsSeasonExtensionTests
{
    [TestClass]
    public class PlayersTest
    {
        private DataView _dataView;

        [TestMethod]
        [ClassInitialize]
        public void ReadSeasonData()
        {
            ISeasonReader seasonReader = new SeasonReaderTest();
            _dataView = new DataView(seasonReader);

            var allChallengeStandings = new AllChallengeStandings(_dataView);

            var playerSelection = new PlayerSelection(_dataView.Players);

            Assert.Fail("Unfinished test.");
        }
    }
}
