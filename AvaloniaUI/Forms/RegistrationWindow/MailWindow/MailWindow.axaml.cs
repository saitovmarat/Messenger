using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaUI.Database;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
#pragma warning disable CS8604 

namespace AvaloniaUI;

public partial class MailWindow : Window
{
    public MailWindow()
    {
        InitializeComponent();
    }

    private void GetCode_Click(object sender, RoutedEventArgs e)
    {
        using(var db = new UsersDbContext()){
            var user = db.Users
                .Where(b => b.Mail == MailTextBox.Text)
                .ToList();
            if(user.Count == 0){
                int emailCode = 0;
                if (Email.SendEmailCode(MailTextBox.Text, ref emailCode)){
                    new RegistrationWindow(MailTextBox.Text,emailCode).Show();
                    Close();
                }
            }
            else{
                MessageBoxManager.GetMessageBoxStandard("Ошибка", "Пользователь с такой почтой уже зарегистрирован", ButtonEnum.Ok).ShowWindowAsync();
            }
        }
    }
}