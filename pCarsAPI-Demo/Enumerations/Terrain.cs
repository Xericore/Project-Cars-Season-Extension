using System.ComponentModel;

namespace pCarsAPI_Demo
{
    public enum Terrain
    {
        [Description("TerrainRoad")]
        TerrainRoad = 0,
        [Description("TerrainLowGripRoad")]
        TerrainLowGripRoad,
        [Description("TerrainBumpyRoad1")]
        TerrainBumpyRoad1,
        [Description("TerrainBumpyRoad2")]
        TerrainBumpyRoad2,
        [Description("TerrainBumpyRoad3")]
        TerrainBumpyRoad3,
        [Description("TerrainMarbles")]
        TerrainMarbles,
        [Description("TerrainGrassyBerms")]
        TerrainGrassyBerms,
        [Description("TerrainGrass")]
        TerrainGrass,
        [Description("TerrainGravel")]
        TerrainGravel,
        [Description("TerrainBumpyGravel")]
        TerrainBumpyGravel,
        [Description("TerrainRumbleStrips")]
        TerrainRumbleStrips,
        [Description("TerrainDrains")]
        TerrainDrains,
        [Description("TerrainTyrewalls")]
        TerrainTyrewalls,
        [Description("TerrainCementwalls")]
        TerrainCementwalls,
        [Description("TerrainGuardrails")]
        TerrainGuardrails,
        [Description("TerrainSand")]
        TerrainSand,
        [Description("TerrainBumpySand")]
        TerrainBumpySand,
        [Description("TerrainDirt")]
        TerrainDirt,
        [Description("TerrainBumpyDirt")]
        TerrainBumpyDirt,
        [Description("TerrainDirtRoad")]
        TerrainDirtRoad,
        [Description("TerrainBumpyDirtRoad")]
        TerrainBumpyDirtRoad,
        [Description("TerrainPavement")]
        TerrainPavement,
        [Description("TerrainDirtBank")]
        TerrainDirtBank,
        [Description("TerrainWood")]
        TerrainWood,
        [Description("TerrainDryVerge")]
        TerrainDryVerge,
        [Description("TerrainExitRumbleStrips")]
        TerrainExitRumbleStrips,
        [Description("TerrainGrasscrete")]
        TerrainGrasscrete,
        [Description("TerrainLongGrass")]
        TerrainLongGrass,
        [Description("TerrainSlopeGrass")]
        TerrainSlopeGrass,
        [Description("TerrainCobbles")]
        TerrainCobbles,
        [Description("TerrainSandRoad")]
        TerrainSandRoad,
        [Description("TerrainBakedClay")]
        TerrainBakedClay,
        [Description("TerrainAstroturf")]
        TerrainAstroturf,
        [Description("TerrainSnowhalf")]
        TerrainSnowhalf,
        [Description("TerrainSnowfull")]
        TerrainSnowfull,
        [Description("TerrainMax")]
        TerrainMax
    }
}