using System.ComponentModel;

namespace pCarsAPI_Demo
{
    public enum CurrentSector
    {
        [Description("Invalid Sector")]
        SectorInvalid = 0,
        [Description("Sector Start")]
        SectorStart,
        [Description("Sector 1")]
        SectorSector1,
        [Description("Sector 2")]
        SectorSector2,
        [Description("Sector 3")]
        SectorFinish,
        [Description("Sector Stop??")]
        SectorStop,
        SectorMax
    }
}