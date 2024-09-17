using System.Text;

namespace TinyAdventure;

public static class GlobalSettings
{
    public static float DefaultFrameDurationMsFloor = 1000f / 60f;
    public static bool ShowDebugMask = false;
    public static bool IsDebugMode = false;
    public static StringBuilder DebugLogBuffer = new StringBuilder();
    public static int WindowHeight = 1920;
    public static int WindowWidth = 1080;
    public static string SaveLevelLocation = "level.json";
    public static int SaveFileVersionCount = 10;
    public static bool SaveFilesInFormattedJson = true;
    public static float GamepadDeadZone = 0.5f;

    public static void Init()
    {

    }

    public static void Cleanup()
    {

    }
}
