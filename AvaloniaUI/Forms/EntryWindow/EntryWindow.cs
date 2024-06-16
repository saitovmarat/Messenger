using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;

namespace AvaloniaUI;
public partial class EntryWindow : Window
{
    public EntryWindow()
    {
        InitializeComponent();
    }
    private void ShowPasswordButton_Click(object sender, RoutedEventArgs e)
    {
        if (PasswordTextBox.PasswordChar == '*'){
            PasswordTextBox.PasswordChar = '\0';
            PasswordEye.Source = new Bitmap("Assets/Images/OpenEye.png");
        }
        else{
            PasswordTextBox.PasswordChar = '*';
            PasswordEye.Source = new Bitmap("Assets/Images/ClosedEye.png");
        }
    }

    private void EntryButton_Click(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
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
