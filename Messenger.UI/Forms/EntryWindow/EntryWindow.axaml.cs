using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace Messenger.UI;

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
        // using(var db = new UsersDbContext()){
        //     var user = db.Users
        //         .Where(b => (b.Name == NameTextBox.Text || b.Mail == NameTextBox.Text) && b.Password == PasswordTextBox.Text)
        //         .ToList();
        //     if(user.Count != 0){
                new MainWindow().Show();
                Close();
        //     }
        //     else{
        //         MessageBoxManager.GetMessageBoxStandard("Ошибка", "Такого пользователя не существует", ButtonEnum.Ok).ShowWindowAsync();
        //     }
        // }
    }

    private void ForgotPassword_Click(object sender, RoutedEventArgs e)
    {
        new FirstPart_PassRecoveryWindow().Show();
        Close();
    }

    private void RegistrationButton_Click(object sender, RoutedEventArgs e)
    {
        new RegistrationWindow().Show();
        Close();
    }
    
}
