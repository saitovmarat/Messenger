using Avalonia;
using Avalonia.ReactiveUI;
using System;

namespace Messenger.UI;

sealed class Program
{
    public static string? accessToken;
    public static string? userName;
    
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
}
