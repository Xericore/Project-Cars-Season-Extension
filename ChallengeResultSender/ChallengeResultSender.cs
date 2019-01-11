using System;
using pCarsAPI_Demo;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.ChallengeResultSender
{
    public class ChallengeResultSender : IChallengeResultSender
    {
        public event Action<ChallengeResult> ChallengeResultEvent;

        public void CheckProjectCarsData(pCarsAPIStruct pCarsApiData)
        {
            

        }
    }
}
