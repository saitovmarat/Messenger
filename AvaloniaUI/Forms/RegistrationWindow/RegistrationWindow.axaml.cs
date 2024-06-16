using System;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace AvaloniaUI;

public partial class RegistrationWindow : Window
{
    public RegistrationWindow()
    {
        InitializeComponent();
    }
    private bool CheckEmail(string email)
    {
        // Проверка формата адреса электронной почты с помощью регулярного выражения
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        bool isValid = Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        return isValid;
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
    private void GetEmailCodeButton_Click(object sender, RoutedEventArgs e)
    {
        if (Validation.CheckEverything(NameTextBox.Text, SurnameTextBox.Text, NewPasswordTextBox.Text, RepeatPasswordTextBox.Text, EmailTextBox.Text)){
            int emailCode = 0;
            if(Email.SendEmailCode(EmailTextBox.Text, ref emailCode)){
                new EmailCodeCheckWindow(emailCode).Show();
                Close();
            }
        }
    }
}