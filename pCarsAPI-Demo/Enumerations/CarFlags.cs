using System;
using System.ComponentModel;

namespace pCarsAPI_Demo
{
    [Flags]
    public enum CarFlags
    {
        [Description("None")]
        None = 0,
        [Description("Headlight")]
        CarHeadlight = 1,
        [Description("Engine Active")]
        CarEngineActive = 2,
        [Description("Engine Warning")]
        CarEngineWarning = 4,
        [Description("Speed Limiter")]
        CarSpeedLimiter = 8,
        [Description("ABS")]
        CarAbs = 16,
        [Description("Handbrake")]
        CarHandbrake = 32
    }
}