using Raylib_cs;
using TinyAdventure.Globals;

namespace TinyAdventure
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                const int screenWidth = 1920;
                const int screenHeight = 1080;
                const string gameTitle = "Texture Atlas Test";
                const int targetFps = 60;
                Raylib.InitWindow(screenWidth, screenHeight, gameTitle);
                Raylib.SetTargetFPS(targetFps);

                using Game game = new Game();

                game.Init();

                while (!Raylib.WindowShouldClose())
                {
                    GlobalSettings.DebugLogBuffer.Clear();


                    game.Update();
                    game.Draw();

                    if (GlobalSettings.IsDebugMode) {
                        Raylib.DrawFPS(Raylib.GetScreenWidth() - 100, 20);
                    }
                }

                game.Cleanup();

                Raylib.CloseWindow();
            }
            catch (Exception e)
            {
                LogManager.Fatal("An error escaped and was caught in the main Program loop.", e);
            }
        }
    }
}
