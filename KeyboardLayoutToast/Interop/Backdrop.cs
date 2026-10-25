using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace KeyboardLayoutToast;

internal static class Backdrop
{
    private const int WcaAccentPolicy = 19;
    private const int AccentEnableAcrylicBlurBehind = 4;
    [StructLayout(LayoutKind.Sequential)] private struct AccentPolicy { public int State; public int Flags; public int GradientColor; public int AnimationId; }
    [StructLayout(LayoutKind.Sequential)] private struct WindowCompositionAttributeData { public int Attribute; public IntPtr Data; public int SizeOfData; }
    [DllImport("user32.dll", SetLastError = true)] private static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

    public static bool TryEnable(IntPtr hwnd, byte opacity)
    {
        IntPtr memory = IntPtr.Zero;
        try
        {
            var accent = new AccentPolicy { State = AccentEnableAcrylicBlurBehind, GradientColor = (opacity << 24) | 0x20202A };
            memory = Marshal.AllocHGlobal(Marshal.SizeOf<AccentPolicy>());
            Marshal.StructureToPtr(accent, memory, false);
            var data = new WindowCompositionAttributeData { Attribute = WcaAccentPolicy, Data = memory, SizeOfData = Marshal.SizeOf<AccentPolicy>() };
            return SetWindowCompositionAttribute(hwnd, ref data) != 0;
        }
        catch { return false; }
        finally { if (memory != IntPtr.Zero) Marshal.FreeHGlobal(memory); }
    }
}
