using System.Windows;
using System.Windows.Threading;

namespace KeyboardLayoutToast;

public partial class App : System.Windows.Application
{
    private TrayController? _tray;
    private string? _mutexName;
    private Mutex? _mutex;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        _mutexName = $"Local\\KeyboardLayoutToast-{Environment.UserName}";
        _mutex = new Mutex(true, _mutexName, out bool created);
        if (!created)
        {
            Shutdown();
            return;
        }

        DispatcherUnhandledException += OnDispatcherException;
        AppDomain.CurrentDomain.UnhandledException += (_, args) =>
        {
            if (args.ExceptionObject is Exception ex) Log.Write("Необработанная ошибка", ex);
        };
        _tray = new TrayController();
        _tray.Start();
    }

    private void OnDispatcherException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        Log.Write("Ошибка интерфейса", e.Exception);
        e.Handled = true;
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _tray?.Dispose();
        _mutex?.ReleaseMutex();
        _mutex?.Dispose();
        base.OnExit(e);
    }
}
