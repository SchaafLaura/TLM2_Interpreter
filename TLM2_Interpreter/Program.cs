using SadConsole.Configuration;
using TLM2_Interpreter;
Settings.WindowTitle = "My SadConsole Game";

Parser.Parse("./Programs/program.tlm");

var gameStartup = new Builder()
        .SetScreenSize(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT)
        .SetStartingScreen<TLM2_Interpreter.Scenes.RootScreen>()
        .IsStartingScreenFocused(true)
        .ConfigureFonts(true);
Game.Create(gameStartup);
Game.Instance.Run();
Game.Instance.Dispose();