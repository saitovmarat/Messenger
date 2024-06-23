using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
#pragma warning disable CS8604

namespace Messenger.UI;

public partial class FirstPart_PassRecoveryWindow : Window
{
    public FirstPart_PassRecoveryWindow()
    {
        InitializeComponent();
    }

    private void GetCodeButton_Click(object sender, RoutedEventArgs e)
    {
        // using(var db = new UsersDbContext()){
        //     var user = db.Users
        //         .Where(b => b.Mail == MailTextBox.Text)
        //         .ToList();
        //     if(user.Count == 0){
        //         MessageBoxManager.GetMessageBoxStandard("Ошибка", "Пользователя с такой почтой не существует", ButtonEnum.Ok).ShowWindowAsync();
        //     }
        //     else{
        //         int emailCode = 0;
        //         if (Email.SendEmailCode(MailTextBox.Text, ref emailCode)){
                    new SecondPart_PassRecoveryWindow().Show();
                    Close();
        //         }
        //     }
        // }
        
    }
}