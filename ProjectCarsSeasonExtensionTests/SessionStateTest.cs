using NUnit.Framework;
using pCarsAPI_Demo;
using ProjectCarsSeasonExtension.ChallengeResultSender;

namespace ProjectCarsSeasonExtensionTests
{
    [TestFixture]
    public class SessionStateTest
    {
        private ChallengeResultSender _challengeResultSender;

        [SetUp]
        public void SetUp()
        {
            _challengeResultSender = new ChallengeResultSender();
        }

        [Test]
        public void Given_SessionTimeAttack_IsEventFired()
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
        public void Given_SessionFormationlap_IsEventMissing()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 60, lapInvalidated: false,
                gameState: GameState.GameIngamePlaying,
                sessionState: SessionState.SessionFormationlap,
                raceState: RaceState.RacestateRacing
                );

            bool eventWasFired = false;

            _challengeResultSender.ChallengeResultEvent += result => { eventWasFired = true; };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(!eventWasFired);
        }

        [Test]
        public void Given_SessionInvalid_IsEventMissing()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 60, lapInvalidated: false,
                gameState: GameState.GameIngamePlaying,
                sessionState: SessionState.SessionInvalid,
                raceState: RaceState.RacestateRacing
                );

            bool eventWasFired = false;

            _challengeResultSender.ChallengeResultEvent += result => { eventWasFired = true; };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(!eventWasFired);
        }

        [Test]
        public void Given_SessionMax_IsEventMissing()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 60, lapInvalidated: false,
                gameState: GameState.GameIngamePlaying,
                sessionState: SessionState.SessionMax,
                raceState: RaceState.RacestateRacing
                );

            bool eventWasFired = false;

            _challengeResultSender.ChallengeResultEvent += result => { eventWasFired = true; };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(!eventWasFired);
        }

        [Test]
        public void Given_SessionPractice_IsEventMissing()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 60, lapInvalidated: false,
                gameState: GameState.GameIngamePlaying,
                sessionState: SessionState.SessionPractice,
                raceState: RaceState.RacestateRacing
                );

            bool eventWasFired = false;

            _challengeResultSender.ChallengeResultEvent += result => { eventWasFired = true; };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(!eventWasFired);
        }

        [Test]
        public void Given_SessionQualify_IsEventMissing()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 60, lapInvalidated: false,
                gameState: GameState.GameIngamePlaying,
                sessionState: SessionState.SessionQualify,
                raceState: RaceState.RacestateRacing
                );

            bool eventWasFired = false;

            _challengeResultSender.ChallengeResultEvent += result => { eventWasFired = true; };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(!eventWasFired);
        }

        [Test]
        public void Given_SessionRace_IsEventMissing()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 60, lapInvalidated: false,
                gameState: GameState.GameIngamePlaying,
                sessionState: SessionState.SessionRace,
                raceState: RaceState.RacestateRacing
                );

            bool eventWasFired = false;

            _challengeResultSender.ChallengeResultEvent += result => { eventWasFired = true; };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(!eventWasFired);
        }

        [Test]
        public void Given_SessionTest_IsEventMissing()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 60, lapInvalidated: false,
                gameState: GameState.GameIngamePlaying,
                sessionState: SessionState.SessionTest,
                raceState: RaceState.RacestateRacing
                );

            bool eventWasFired = false;

            _challengeResultSender.ChallengeResultEvent += result => { eventWasFired = true; };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(!eventWasFired);
        }
        
    }
}
