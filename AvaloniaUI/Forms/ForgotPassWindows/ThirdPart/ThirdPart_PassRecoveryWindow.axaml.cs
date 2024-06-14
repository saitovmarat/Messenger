using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaUI;

public partial class ThirdPart_PassRecoveryWindow : Window
{
    public ThirdPart_PassRecoveryWindow()
    {
        InitializeComponent();
    }

    private void ChangePass_Click(object sender, RoutedEventArgs e)
    {
        new EntryWindow().Show();
        Close();
    }
}