using System.ComponentModel;

namespace pCarsAPI_Demo
{
    public enum FlagReason
    {
        [Description("No Reason")]
        FlagReasonNone = 0,
        [Description("Solo Crash")]
        FlagReasonSoloCrash,
        [Description("Vehicle Crash")]
        FlagReasonVehicleCrash,
        [Description("Vehicle Obstruction")]
        FlagReasonVehicleObstruction,
        FlagReasonMax
    }
}