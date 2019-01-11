using System;
using pCarsAPI_Demo;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.ChallengeResultSender
{
    public interface IChallengeResultSender
    {
        event Action<ChallengeResult> ChallengeResultEvent;

        void CheckProjectCarsData(pCarsAPIStruct pCarsApiData);
    }
}
