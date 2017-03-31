using System.ComponentModel;

namespace pCarsAPI_Demo
{
    public enum GameState
    {
        [Description("Waiting for game to start...")]
        GameExited = 0,
        [Description("In Menus")]
        GameFrontEnd,
        [Description("In Session")]
        GameIngamePlaying,
        [Description("Game Paused")]
        GameIngamePaused,
        GameMax
    }
}