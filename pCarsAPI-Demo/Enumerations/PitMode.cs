using System.ComponentModel;

namespace pCarsAPI_Demo
{
    public enum PitMode
    {
        [Description("None")]
        PitModeNone = 0,
        [Description("Pit Entry")]
        PitModeDrivingIntoPits,
        [Description("In Pits")]
        PitModeInPit,
        [Description("Pit Exit")]
        PitModeDrivingOutOfPits,
        [Description("Pit Garage")]
        PitModeInGarage,
        PitModeMax
    }
}