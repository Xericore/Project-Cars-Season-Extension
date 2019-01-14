using NUnit.Framework;
using pCarsAPI_Demo;
using ProjectCarsSeasonExtension.ChallengeResultSender;

namespace ProjectCarsSeasonExtensionTests
{
    [TestFixture]
    public class GameStateTest
    {
        private ChallengeResultSender _challengeResultSender;

        [SetUp]
        public void SetUp()
        {
            _challengeResultSender = new ChallengeResultSender();
        }
        
        [Test]
        public void Given_GameExited_IsEventMissing()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 60, lapInvalidated: false,
                gameState: GameState.GameExited,
                sessionState: SessionState.SessionTimeAttack,
                raceState: RaceState.RacestateRacing
                );

            bool eventWasFired = false;

            _challengeResultSender.ChallengeResultEvent += result => { eventWasFired = true; };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(!eventWasFired);
        }
        
        [Test]
        public void Given_GameMax_IsEventMissing()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 60, lapInvalidated: false,
                gameState: GameState.GameMax,
                sessionState: SessionState.SessionTimeAttack,
                raceState: RaceState.RacestateRacing
                );

            bool eventWasFired = false;

            _challengeResultSender.ChallengeResultEvent += result => { eventWasFired = true; };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(!eventWasFired);
        }
        
        [Test]
        public void Given_GameFrontEnd_IsEventMissing()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 60, lapInvalidated: false,
                gameState: GameState.GameFrontEnd,
                sessionState: SessionState.SessionTimeAttack,
                raceState: RaceState.RacestateRacing
                );

            bool eventWasFired = false;

            _challengeResultSender.ChallengeResultEvent += result => { eventWasFired = true; };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(!eventWasFired);
        }
        
        [Test]
        public void Given_GameIngamePaused_IsEventMissing()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 60, lapInvalidated: false,
                gameState: GameState.GameIngamePaused,
                sessionState: SessionState.SessionTimeAttack,
                raceState: RaceState.RacestateRacing
                );

            bool eventWasFired = false;

            _challengeResultSender.ChallengeResultEvent += result => { eventWasFired = true; };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(!eventWasFired);
        }
        
        [Test]
        public void Given_GameIngamePaused_IsEventFired()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 60, lapInvalidated: false,
                gameState: GameState.GameIngamePlaying,
                sessionState: SessionState.SessionTimeAttack,
                raceState: RaceState.RacestateRacing
                );

            bool eventWasFired = false;

            _challengeResultSender.ChallengeResultEvent += result => { eventWasFired = true; };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(eventWasFired);
        }
    }
}
