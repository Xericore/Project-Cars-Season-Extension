using System;
using NUnit.Framework;
using ProjectCarsSeasonExtension.Models;
using ProjectCarsSeasonExtension.Models.Player;
using ProjectCarsSeasonExtension.Serialization;
using ProjectCarsSeasonExtension.ViewModels;
using ProjectCarsSeasonExtension.Views;

namespace ProjectCarsSeasonExtensionTests
{
    [TestFixture]
    public class PlayersTest
    {
        private static DataView _dataView;
        private static AllChallengeStandings _allChallengeStandings;
        private static PlayerController _playerController;
        private static PlayerSelection _playerSelection;

        [SetUp]
        public static void ReadSeasonData()
        {
            ISeasonReader seasonReader = new DummySeasonReader();
            _dataView = new DataView(seasonReader);

            _allChallengeStandings = new AllChallengeStandings(_dataView);
            _playerController = new PlayerController(_dataView);
            _playerSelection = new PlayerSelection(_playerController);
        }

        [Test]
        public void Given_SlowestChallengeResult_IsPlayerAtSamePosition()
        {
            var challengeResult = new ChallengeResult
            {
                CarName = "Formula C",
                LastValidLapTime = new TimeSpan(0,0,0,45,000),
                TrackLocationAndVariant = "Circuit de Barcelona-Catalunya Club"
            };
            var player = new Player
            {
                AvatarFileName = "",
                Group = AuthenticationGroup.User,
                Id = 0,
                Name = "Mario"
            };

            int playerPosition = _allChallengeStandings.GetPlayerPosition(challengeResult, player);

            Assert.That(0, Is.EqualTo(playerPosition));
        }
    }
}
