using System.Net.Http;
using System.Security.AccessControl;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Messenger.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Messenger.UI;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new EntryWindow();
            // {
            //     DataContext = new MainWindowViewModel()
            // };
        }

        base.OnFrameworkInitializationCompleted();
    }


}