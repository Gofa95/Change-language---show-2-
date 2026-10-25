using System.Runtime.InteropServices;

namespace KeyboardLayoutToast;

internal static class NativeMethods
{
    internal const int WmInputLangChange = 0x0051;
    internal const int WmHotKey = 0x0312;
    internal const int WmAppShowCurrent = 0x8001;
    internal const int WmAppExit = 0x8002;
    internal const int WmShellHook = 0x031D;
    internal const int HshellLanguage = 13;
    internal const int GwlExStyle = -20;
    internal const int WsExNoActivate = 0x08000000;
    internal const int WsExToolWindow = 0x00000080;
    internal const int WsExLayered = 0x00080000;
    internal const int WsExTransparent = 0x00000020;
    internal const int SwHide = 0;
    internal const int SwShowNoActivate = 4;
    internal const uint WdaExcludeFromCapture = 0x11;
    internal const uint MfsByCommand = 0x00000000;
    internal const uint MfString = 0x00000000;
    internal const uint MfSeparator = 0x00000800;
    internal const uint CfShowing = 0x00000003;
    internal const int HotkeyId = 0x4B10;
    internal const int WhKeyboardLl = 13;
    internal const int WmKeyDown = 0x0100;
    internal const int WmSysKeyDown = 0x0104;
    internal const int VkShift = 0x10;
    internal const int VkMenu = 0x12;
    internal const uint ModControl = 0x0002;
    internal const uint ModAlt = 0x0001;
    internal const uint VkF10 = 0x79;
    internal const int WmCommand = 0x0111;
    internal const int CommandShow = 1001;
    internal const int CommandExit = 1002;

    [DllImport("user32.dll", SetLastError = true)] internal static extern uint RegisterWindowMessageW(string name);
    [DllImport("user32.dll", SetLastError = true)] internal static extern bool RegisterShellHookWindow(IntPtr hwnd);
    [DllImport("user32.dll", SetLastError = true)] internal static extern bool DeregisterShellHookWindow(IntPtr hwnd);
    [DllImport("user32.dll", SetLastError = true)] internal static extern bool RegisterHotKey(IntPtr hwnd, int id, uint modifiers, uint key);
    [DllImport("user32.dll", SetLastError = true)] internal static extern IntPtr SetWindowsHookExW(int hookType, LowLevelKeyboardProc callback, IntPtr module, uint threadId);
    [DllImport("user32.dll", SetLastError = true)] internal static extern bool UnhookWindowsHookEx(IntPtr hook);
    [DllImport("user32.dll", SetLastError = true)] internal static extern IntPtr CallNextHookEx(IntPtr hook, int code, IntPtr wParam, IntPtr lParam);
    internal delegate IntPtr LowLevelKeyboardProc(int code, IntPtr wParam, IntPtr lParam);
    [DllImport("user32.dll", SetLastError = true)] internal static extern bool UnregisterHotKey(IntPtr hwnd, int id);
    [DllImport("user32.dll", SetLastError = true)] internal static extern bool ShowWindow(IntPtr hwnd, int command);
    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtrW", SetLastError = true)] internal static extern IntPtr GetWindowLongPtrW(IntPtr hwnd, int index);
    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtrW", SetLastError = true)] internal static extern IntPtr SetWindowLongPtrW(IntPtr hwnd, int index, IntPtr value);
    [DllImport("user32.dll", SetLastError = true)] internal static extern bool SetWindowDisplayAffinity(IntPtr hwnd, uint affinity);
    [DllImport("user32.dll", SetLastError = true)] internal static extern IntPtr GetSystemMenu(IntPtr hwnd, bool revert);
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)] internal static extern bool AppendMenuW(IntPtr menu, uint flags, nuint id, string? text);
    [DllImport("user32.dll", SetLastError = true)] internal static extern bool DestroyMenu(IntPtr menu);
    [DllImport("user32.dll", SetLastError = true)] internal static extern bool SetForegroundWindow(IntPtr hwnd);
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)] internal static extern IntPtr LoadKeyboardLayoutW(string id, uint flags);
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)] internal static extern int GetKeyboardLayoutNameW(System.Text.StringBuilder buffer);
    [DllImport("user32.dll", SetLastError = true)] internal static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint processId);
    [DllImport("imm32.dll", SetLastError = true)] internal static extern IntPtr ImmGetDefaultIMEWnd(IntPtr hwnd);
    [DllImport("user32.dll", SetLastError = true)] internal static extern IntPtr SendMessageW(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);

    internal static string WindowMessageName => "KeyboardLayoutToast.SingleInstance.ShowCurrent";
}
