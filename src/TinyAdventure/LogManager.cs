namespace TinyAdventure;

public static class LogManager
{

    /// <summary>
    ///  Format the message for output and then write a console message
    /// </summary>
    private static void WriteMessage(string level, string message, params object?[] args)
    {
        string formattedMessage = String.Format(message, args);
        Console.WriteLine("{0}: {1}", level, formattedMessage);
    }

    /// <summary>
    /// Level 1 messages meant to help track the very small details of what is happening in the game
    /// </summary>
    public static void Trace(string message, params object?[] args)
    {
        WriteMessage("TRACE", message, args);
    }

    /// <summary>
    /// Level 2 messages intended for debugging purposes to tell useful information when debugging
    /// </summary>
    public static void Debug(string message, params object?[] args)
    {
        WriteMessage("DEBUG", message, args);
    }

    /// <summary>
    /// Level 3 messages intended to communicate useful information, like game saved, game loaded, etc.
    /// </summary>
    public static void Info(string message, params object?[] args)
    {
        WriteMessage("INFO", message, args);
    }

    /// <summary>
    /// Level 4 messages intended to let developers know that there may have been a non-critical unexpected state or event
    /// </summary>
    public static void Warn(string message, params object?[] args)
    {
        WriteMessage("WARN", message, args);
    }

    /// <summary>
    /// Level 5 messages intended to let developers know that there was an error
    /// </summary>
    public static void Error(string message, Exception? ex, params object?[] args)
    {
        WriteMessage("ERROR", message, args);
        if (ex != null)
        {
            WriteMessage("ERROR EXCEPTION", "{0}", ex);
        }
    }

    /// <summary>
    /// Level 6 message intended to let developers know that there was a fatal
    /// </summary>
    public static void Fatal(string message, Exception? ex, params object?[] args)
    {
        WriteMessage("FATAL", message, args);
        if (ex != null)
        {
            WriteMessage("FATAL EXCEPTION", "{0}", ex);
        }
    }

}
