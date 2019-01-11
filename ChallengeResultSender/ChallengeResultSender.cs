using System;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.ChallengeResultSender
{
    public class ChallengeResultSender : IChallengeResultSender
    {
        public event Action<ChallengeResult> ChallengeResultEvent;

        public void CheckProjectCarsStateData(ProjectCarsStateData projectCarsStateData)
        {
            

        }
    }
}
