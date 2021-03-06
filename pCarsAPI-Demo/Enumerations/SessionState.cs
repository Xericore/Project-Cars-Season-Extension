using System.ComponentModel;

namespace pCarsAPI_Demo
{
    public enum SessionState
    {
        [Description("No Session")]
        SessionInvalid = 0,
        [Description("Practise")]
        SessionPractice,
        [Description("Testing")]
        SessionTest,
        [Description("Qualifying")]
        SessionQualify,
        [Description("Formation Lap")]
        SessionFormationlap,
        [Description("Racing")]
        SessionRace,
        [Description("Time Trial")]
        SessionTimeAttack,
        SessionMax
    }
}