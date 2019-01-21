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

        [Test]
        public void Given_CheckingTwice_IsEventFiredOnlyOnce()
        {
            ProjectCarsStateData projectCarsStateData = new ProjectCarsStateData(
                carName: "Formula A", trackLocation: "Barcelona", trackVariant: "Club",
                lastLapTime: 100, lapInvalidated: false,
                gameState: GameState.GameIngamePlaying, 
                sessionState: SessionState.SessionTimeAttack, 
                raceState: RaceState.RacestateRacing
                );

            uint eventFiredCount = 0;

            _challengeResultSender.ChallengeResultEvent += result => { eventFiredCount++; };

            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);
            _challengeResultSender.CheckProjectCarsStateData(projectCarsStateData);

            Assert.That(eventFiredCount, Is.EqualTo(1));
        }

        [Test]
        public void Given_0false_1false_IsEventFiredOnlyOnce()
        {
            uint eventFiredCount = 0;

            _challengeResultSender.ChallengeResultEvent += result => { eventFiredCount++; };

            _challengeResultSender.CheckProjectCarsStateData(new TestStateData(lastLapTime: 0f, lapInvalidated: false));
            _challengeResultSender.CheckProjectCarsStateData(new TestStateData(lastLapTime: 1f, lapInvalidated: false));

            Assert.That(eventFiredCount, Is.EqualTo(1));
        }

        [Test]
        public void Given_0false_0true_1false_IsEventMissing()
        {
            uint eventFiredCount = 0;

            _challengeResultSender.ChallengeResultEvent += result => { eventFiredCount++; };

            _challengeResultSender.CheckProjectCarsStateData(new TestStateData(lastLapTime: 0f, lapInvalidated: false));
            _challengeResultSender.CheckProjectCarsStateData(new TestStateData(lastLapTime: 0f, lapInvalidated: true));
            _challengeResultSender.CheckProjectCarsStateData(new TestStateData(lastLapTime: 1f, lapInvalidated: false));

            Assert.That(eventFiredCount, Is.EqualTo(0));
        }

        [Test]
        public void Given_0f_0f_0f_1f_1f_1f_IsEventFiredOnlyOnce()
        {
            uint eventFiredCount = 0;

            _challengeResultSender.ChallengeResultEvent += result => { eventFiredCount++; };

            _challengeResultSender.CheckProjectCarsStateData(new TestStateData(lastLapTime: 0f, lapInvalidated: false));
            _challengeResultSender.CheckProjectCarsStateData(new TestStateData(lastLapTime: 0f, lapInvalidated: false));
            _challengeResultSender.CheckProjectCarsStateData(new TestStateData(lastLapTime: 0f, lapInvalidated: false));
            
            _challengeResultSender.CheckProjectCarsStateData(new TestStateData(lastLapTime: 1f, lapInvalidated: false));
            _challengeResultSender.CheckProjectCarsStateData(new TestStateData(lastLapTime: 1f, lapInvalidated: false));
            _challengeResultSender.CheckProjectCarsStateData(new TestStateData(lastLapTime: 1f, lapInvalidated: false));

            Assert.That(eventFiredCount, Is.EqualTo(1));
        }

        [Test]
        public void Given_0f_0f_0f_1f_1t_1f_IsEventFiredOnlyOnce()
        {
            uint eventFiredCount = 0;

            _challengeResultSender.ChallengeResultEvent += result => { eventFiredCount++; };

            _challengeResultSender.CheckProjectCarsStateData(new TestStateData(lastLapTime: 0f, lapInvalidated: false));
            _challengeResultSender.CheckProjectCarsStateData(new TestStateData(lastLapTime: 0f, lapInvalidated: false));
            _challengeResultSender.CheckProjectCarsStateData(new TestStateData(lastLapTime: 0f, lapInvalidated: false));
            
            _challengeResultSender.CheckProjectCarsStateData(new TestStateData(lastLapTime: 1f, lapInvalidated: false));
            _challengeResultSender.CheckProjectCarsStateData(new TestStateData(lastLapTime: 1f, lapInvalidated: true));
            _challengeResultSender.CheckProjectCarsStateData(new TestStateData(lastLapTime: 1f, lapInvalidated: false));

            Assert.That(eventFiredCount, Is.EqualTo(1));
        }

        [Test]
        public void Given_0f_1f_2t_3t_4f_IsEventFiredTwice()
        {
            uint eventFiredCount = 0;

            _challengeResultSender.ChallengeResultEvent += result =>
            {
                eventFiredCount++;
            };

            _challengeResultSender.CheckProjectCarsStateData(new TestStateData(lastLapTime: 0f, lapInvalidated: false));
            _challengeResultSender.CheckProjectCarsStateData(new TestStateData(lastLapTime: 1f, lapInvalidated: false));
            _challengeResultSender.CheckProjectCarsStateData(new TestStateData(lastLapTime: 2f, lapInvalidated: true));
            _challengeResultSender.CheckProjectCarsStateData(new TestStateData(lastLapTime: 3f, lapInvalidated: true));
            _challengeResultSender.CheckProjectCarsStateData(new TestStateData(lastLapTime: 4f, lapInvalidated: false));

            Assert.That(eventFiredCount, Is.EqualTo(2));
        }
    }
}
