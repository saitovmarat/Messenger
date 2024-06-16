using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaUI;

public partial class FirstPart_PassRecoveryWindow : Window
{
    public FirstPart_PassRecoveryWindow()
    {
        InitializeComponent();
    }

    private void GetCodeButton_Click(object sender, RoutedEventArgs e)
    {
        int emailCode = 0;
        if(Email.SendEmailCode(EmailTextBox.Text, ref emailCode)){
            new SecondPart_PassRecoveryWindow(emailCode).Show();
            Close();
        }
    }
}