using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Messenger.UI.ViewModels;
#pragma warning disable CS8604 // Possible null reference argument.


namespace Messenger.UI;

public class ViewLocator : IDataTemplate
{
    public Control Build(object? data)
    {
        var viewModelName = data?.GetType().FullName;
        var viewName = viewModelName?.Replace("Messenger.UI.ViewModels.SplitViewPane", "Messenger.UI.Views")
                                    .Replace("ViewModel", "View");
        var type = Type.GetType(viewName);
        if (type != null)
        {
            return (Control)Activator.CreateInstance(type)!;
        }

        return new TextBlock { Text = "Not Found: " + viewName };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
