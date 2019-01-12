using NUnit.Framework;
using pCarsAPI_Demo;
using ProjectCarsSeasonExtension.ChallengeResultSender;

namespace ProjectCarsSeasonExtensionTests
{
    [TestFixture]
    public class ChallengeResultSenderTest
    {
        private ChallengeResultSender _challengeResultSender;

        [SetUp]
        public void SetUp()
        {
            _challengeResultSender = new ChallengeResultSender();
        }

        [Test]
        public void DoesEventFire()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 60, lapInvalidated: false, lapsInEvent: 3,
                gameState: GameState.GameIngamePlaying, 
                sessionState: SessionState.SessionTimeAttack, 
                raceState: RaceState.RacestateRacing
                );

            bool eventWasCalled = false;
            _challengeResultSender.ChallengeResultEvent += result => { eventWasCalled = true; };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(eventWasCalled);
        }
    }
}
