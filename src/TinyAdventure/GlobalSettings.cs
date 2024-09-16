using System.Text;

namespace TinyAdventure;

public static class GlobalSettings
{
    public static bool IsDebugMode { get; set; } = true;

    public static float DefaultFrameDurationMsFloor { get; set; } = 1000f / 60f;
    public static bool ShowDebugMask { get; set; } = false;

    public static StringBuilder DebugLogBuffer = new StringBuilder();
}
