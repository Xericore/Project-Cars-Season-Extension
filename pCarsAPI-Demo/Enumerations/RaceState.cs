using System.ComponentModel;

namespace pCarsAPI_Demo
{
    public enum RaceState
    {
        [Description("Invalid")]
        RacestateInvalid = 0,
        [Description("Not started")]
        RacestateNotStarted,
        [Description("Racing")]
        RacestateRacing,
        [Description("Finished")]
        RacestateFinished,
        [Description("Disqualified")]
        RacestateDisqualified,
        [Description("Retired")]
        RacestateRetired,
        [Description("DNF")]
        RacestateDnf,
        RacestateMax
    }
}