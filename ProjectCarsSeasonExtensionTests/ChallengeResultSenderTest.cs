using System;
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
        public void Given_DefaultInput_DoesEventFire()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 60, lapInvalidated: false,
                gameState: GameState.GameIngamePlaying, 
                sessionState: SessionState.SessionTimeAttack, 
                raceState: RaceState.RacestateRacing
                );

            bool eventWasCalled = false;
            _challengeResultSender.ChallengeResultEvent += result => { eventWasCalled = true; };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(eventWasCalled);
        }

        [Test]
        public void Given_DefaultInput_IsTimeCorrect()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 60, lapInvalidated: false,
                gameState: GameState.GameIngamePlaying, 
                sessionState: SessionState.SessionTimeAttack, 
                raceState: RaceState.RacestateRacing
                );

            TimeSpan lastValidLapTime = new TimeSpan(0);

            _challengeResultSender.ChallengeResultEvent += result =>
            {
                lastValidLapTime = result.LastValidLapTime;
            };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(lastValidLapTime.TotalSeconds, Is.EqualTo(60d));
        }

        [Test]
        public void Given_DefaultInput_IsCarNameEqual()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 60, lapInvalidated: false,
                gameState: GameState.GameIngamePlaying, 
                sessionState: SessionState.SessionTimeAttack, 
                raceState: RaceState.RacestateRacing
                );

            string carName = "";

            _challengeResultSender.ChallengeResultEvent += result =>
            {
                carName = result.CarName;
            };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(carName, Is.EqualTo("Formula A"));
        }

        [Test]
        public void Given_DefaultInput_IsTrackLocationAndVariantEqual()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 60, lapInvalidated: false,
                gameState: GameState.GameIngamePlaying, 
                sessionState: SessionState.SessionTimeAttack, 
                raceState: RaceState.RacestateRacing
                );

            string trackLocationAndVariant = "";

            _challengeResultSender.ChallengeResultEvent += result =>
            {
                trackLocationAndVariant = result.TrackLocationAndVariant;
            };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(trackLocationAndVariant, Is.EqualTo("Barcelona Club"));
        }

        [Test]
        public void Given_LapTimeZero_IsEventMissing()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 0, lapInvalidated: false,
                gameState: GameState.GameIngamePlaying, 
                sessionState: SessionState.SessionTimeAttack, 
                raceState: RaceState.RacestateRacing
                );

            bool eventWasFired = false;

            _challengeResultSender.ChallengeResultEvent += result => { eventWasFired = true; };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(!eventWasFired);
        }

        [Test]
        public void Given_InvalidLap_IsEventMissing()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 50, lapInvalidated: true,
                gameState: GameState.GameIngamePlaying, 
                sessionState: SessionState.SessionTimeAttack, 
                raceState: RaceState.RacestateRacing
                );

            bool eventWasFired = false;

            _challengeResultSender.ChallengeResultEvent += result => { eventWasFired = true; };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(!eventWasFired);
        }
    }
}
