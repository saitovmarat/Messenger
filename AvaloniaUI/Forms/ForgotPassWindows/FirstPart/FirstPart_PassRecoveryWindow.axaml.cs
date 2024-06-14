using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaUI;

public partial class FirstPart_PassRecoveryWindow : Window
{
    public FirstPart_PassRecoveryWindow()
    {
        InitializeComponent();
    }

    private void GetCode_Click(object sender, RoutedEventArgs e)
    {
        new SecondPart_PassRecoveryWindow().Show();
        Close();
    }
}