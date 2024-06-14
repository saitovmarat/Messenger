using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaUI;
public partial class EntryWindow : Window
{
    public EntryWindow()
    {
        InitializeComponent();
    }

    private void EntryButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void RegistrationButton_Click(object sender, RoutedEventArgs e)
    {
        new RegistrationWindow().Show();
        Close();
    }
    private void ForgotPassword_Click(object sender, RoutedEventArgs e)
    {
        new FirstPart_PassRecoveryWindow().Show();
        Close();
    }
}
