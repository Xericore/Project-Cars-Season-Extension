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
    }
}
