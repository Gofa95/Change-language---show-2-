using System.IO;

namespace KeyboardLayoutToast;

internal static class Log
{
    private static readonly object Gate = new();
    private static string PathName => Path.Combine(AppContext.BaseDirectory, "app.log");
    public static void Write(string message, Exception? ex = null)
    {
        try
        {
            lock (Gate)
                File.AppendAllText(PathName, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {message}{(ex is null ? "" : ": " + ex)}{Environment.NewLine}");
        }
        catch { /* logging must never take down the tray process */ }
    }
}
