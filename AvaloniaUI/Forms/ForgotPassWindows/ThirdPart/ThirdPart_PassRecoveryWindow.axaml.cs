using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using AvaloniaUI.Database;

namespace AvaloniaUI;

public partial class ThirdPart_PassRecoveryWindow : Window
{
    private string Mail { get; set; }
    public ThirdPart_PassRecoveryWindow(string mail)
    {
        Mail = mail;
        InitializeComponent();
    }
    public ThirdPart_PassRecoveryWindow()
    {
        Mail = "";
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
        if (RepeatNewPasswordTextBox.PasswordChar == '*'){
            RepeatNewPasswordTextBox.PasswordChar = '\0';
            RepeatPasswordEye.Source = new Bitmap("Assets/Images/OpenEye.png");
        }
        else{
            RepeatNewPasswordTextBox.PasswordChar = '*';
            RepeatPasswordEye.Source = new Bitmap("Assets/Images/ClosedEye.png");
        }
    }
    private void ChangePass_Click(object sender, RoutedEventArgs e)
    {
        if(NewPasswordTextBox.Text == RepeatNewPasswordTextBox.Text 
        && !string.IsNullOrEmpty(NewPasswordTextBox.Text))
        {
            using(var db = new UsersDbContext()){
                var user = db.Users
                    .Where(b => b.Mail == Mail)
                    .ToList();
                if(user.Count != 0){
                    user[0].Password = NewPasswordTextBox.Text;
                    db.SaveChanges();
                }
            }
            new EntryWindow().Show();
            Close();
        }
    }
}