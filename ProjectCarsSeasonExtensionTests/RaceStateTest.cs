using NUnit.Framework;
using pCarsAPI_Demo;
using ProjectCarsSeasonExtension.ChallengeResultSender;

namespace ProjectCarsSeasonExtensionTests
{
    [TestFixture]
    public class RaceStateTest
    {
        private ChallengeResultSender _challengeResultSender;

        [SetUp]
        public void SetUp()
        {
            _challengeResultSender = new ChallengeResultSender();
        }

        [Test]
        public void Given_RacestateRacing_IsEventFired()
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

        [Test]
        public void Given_RacestateDisqualified_IsEventMissing()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 60, lapInvalidated: false,
                gameState: GameState.GameIngamePlaying,
                sessionState: SessionState.SessionTimeAttack,
                raceState: RaceState.RacestateDisqualified
                );

            bool eventWasFired = false;

            _challengeResultSender.ChallengeResultEvent += result => { eventWasFired = true; };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(!eventWasFired);
        }

        [Test]
        public void Given_RacestateDnf_IsEventMissing()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 60, lapInvalidated: false,
                gameState: GameState.GameIngamePlaying,
                sessionState: SessionState.SessionTimeAttack,
                raceState: RaceState.RacestateDnf
                );

            bool eventWasFired = false;

            _challengeResultSender.ChallengeResultEvent += result => { eventWasFired = true; };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(!eventWasFired);
        }

        [Test]
        public void Given_RacestateFinished_IsEventMissing()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 60, lapInvalidated: false,
                gameState: GameState.GameIngamePlaying,
                sessionState: SessionState.SessionTimeAttack,
                raceState: RaceState.RacestateFinished
                );

            bool eventWasFired = false;

            _challengeResultSender.ChallengeResultEvent += result => { eventWasFired = true; };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(!eventWasFired);
        }

        [Test]
        public void Given_RacestateMax_IsEventMissing()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 60, lapInvalidated: false,
                gameState: GameState.GameIngamePlaying,
                sessionState: SessionState.SessionTimeAttack,
                raceState: RaceState.RacestateMax
                );

            bool eventWasFired = false;

            _challengeResultSender.ChallengeResultEvent += result => { eventWasFired = true; };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(!eventWasFired);
        }

        [Test]
        public void Given_RacestateInvalid_IsEventMissing()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 60, lapInvalidated: false,
                gameState: GameState.GameIngamePlaying,
                sessionState: SessionState.SessionTimeAttack,
                raceState: RaceState.RacestateInvalid
                );

            bool eventWasFired = false;

            _challengeResultSender.ChallengeResultEvent += result => { eventWasFired = true; };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(!eventWasFired);
        }

        [Test]
        public void Given_RacestateNotStarted_IsEventMissing()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 60, lapInvalidated: false,
                gameState: GameState.GameIngamePlaying,
                sessionState: SessionState.SessionTimeAttack,
                raceState: RaceState.RacestateNotStarted
                );

            bool eventWasFired = false;

            _challengeResultSender.ChallengeResultEvent += result => { eventWasFired = true; };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(!eventWasFired);
        }

        [Test]
        public void Given_RacestateRetired_IsEventMissing()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 60, lapInvalidated: false,
                gameState: GameState.GameIngamePlaying,
                sessionState: SessionState.SessionTimeAttack,
                raceState: RaceState.RacestateRetired
                );

            bool eventWasFired = false;

            _challengeResultSender.ChallengeResultEvent += result => { eventWasFired = true; };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(!eventWasFired);
        }
    }
}
