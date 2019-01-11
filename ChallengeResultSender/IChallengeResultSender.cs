using System;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.ChallengeResultSender
{
    public interface IChallengeResultSender
    {
        event Action<ChallengeResult> ChallengeResultEvent;

        void CheckProjectCarsStateData(ProjectCarsStateData projectCarsState);
    }
}
