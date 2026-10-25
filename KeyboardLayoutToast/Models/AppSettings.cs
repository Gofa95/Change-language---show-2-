using System.Text.Json;
using System.IO;

namespace KeyboardLayoutToast;

public sealed class AppSettings
{
    public int DisplayDurationMs { get; set; } = 1000;
    public double IndicatorFontSize { get; set; } = 48;
    public double BackgroundOpacity { get; set; } = 0.80;
    public string Position { get; set; } = "CenterLower";
    public int CenterOffsetMultiplier { get; set; } = 2;
    public string ToastSize { get; set; } = "Linux";
    public string TrayIconStyle { get; set; } = "Plasma";
    public bool UseBackdropBlur { get; set; } = true;

    public static string FilePath => Path.Combine(AppContext.BaseDirectory, "settings.json");

    public static AppSettings Load()
    {
        try
        {
            if (File.Exists(FilePath))
                return JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(FilePath)) ?? new();
            var value = new AppSettings();
            File.WriteAllText(FilePath, JsonSerializer.Serialize(value, new JsonSerializerOptions { WriteIndented = true }));
            return value;
        }
        catch (Exception ex)
        {
            Log.Write("Не удалось прочитать настройки; используются значения по умолчанию", ex);
            return new();
        }
    }

    public void Normalize()
    {
        DisplayDurationMs = Math.Clamp(DisplayDurationMs, 300, 5000);
        IndicatorFontSize = Math.Clamp(IndicatorFontSize, 32, 96);
        CenterOffsetMultiplier = Math.Clamp(CenterOffsetMultiplier, 0, 5);
        BackgroundOpacity = Math.Clamp(BackgroundOpacity, 0.45, 1.0);
        if (Position is not ("Center" or "CenterLower" or "TopLeft" or "TopCenter" or "TopRight" or "CenterLeft" or "CenterRight" or "BottomLeft" or "BottomCenter" or "BottomRight")) Position = "CenterLower";
        if (ToastSize is not ("Linux" or "Compact" or "Small" or "Medium")) ToastSize = "Linux";
        if (TrayIconStyle is not ("Plasma" or "Minimal")) TrayIconStyle = "Plasma";
    }

    public void Save()
    {
        try
        {
            File.WriteAllText(FilePath, JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true }));
        }
        catch (Exception ex) { Log.Write("Не удалось сохранить настройки", ex); }
    }
}
