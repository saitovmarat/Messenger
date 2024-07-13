using Avalonia.Controls;

namespace Messenger.UI.Views;

public partial class ChatPageView : UserControl
{
    public ChatPageView()
    {
        InitializeComponent();
        MessageScrollViewer.ScrollToEnd();
    }
}