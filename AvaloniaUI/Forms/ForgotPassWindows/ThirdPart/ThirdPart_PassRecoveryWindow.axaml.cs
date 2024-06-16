using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;

namespace AvaloniaUI;

public partial class ThirdPart_PassRecoveryWindow : Window
{
    public ThirdPart_PassRecoveryWindow()
    {
        InitializeComponent();
    }

    private void ShowNewPasswordButton_Click(object sender, RoutedEventArgs e){
        if (NewPasswordTextBox.PasswordChar == '*'){
            NewPasswordTextBox.PasswordChar = '\0';
            NewPasswordEye.Source = new Bitmap("Assets/Images/OpenEye.png");
        }
        else{
            NewPasswordTextBox.PasswordChar = '*';
            NewPasswordEye.Source = new Bitmap("Assets/Images/ClosedEye.png");
        }
    }

    private void ShowRepeatPasswordButton_Click(object sender, RoutedEventArgs e){
        if (RepeatPasswordTextBox.PasswordChar == '*'){
            RepeatPasswordTextBox.PasswordChar = '\0';
            RepeatPasswordEye.Source = new Bitmap("Assets/Images/OpenEye.png");
        }
        else{
            RepeatPasswordTextBox.PasswordChar = '*';
            RepeatPasswordEye.Source = new Bitmap("Assets/Images/ClosedEye.png");
        }
    }
    private void ChangePass_Click(object sender, RoutedEventArgs e)
    {
        new EntryWindow().Show();
        Close();
    }
}