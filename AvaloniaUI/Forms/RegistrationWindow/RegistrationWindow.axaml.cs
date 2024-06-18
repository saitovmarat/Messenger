using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using AvaloniaUI.Database;
#pragma warning disable CS8604 

namespace AvaloniaUI;

public partial class RegistrationWindow : Window
{
    private int EmailCode { get; set; }
    private string Mail { get; set; }
    public RegistrationWindow(string mail, int code)
    {
        EmailCode = code;
        Mail = mail;
        InitializeComponent();
    }
    public RegistrationWindow()
    {
        EmailCode = 0;
        Mail = "";
        InitializeComponent();
    }
    private void ShowNewPasswordButton_Click(object sender, RoutedEventArgs e){
        if (PasswordTextBox.PasswordChar == '*'){
            PasswordTextBox.PasswordChar = '\0';
            NewPasswordEye.Source = new Bitmap("Assets/Images/OpenEye.png");
        }
        else{
            PasswordTextBox.PasswordChar = '*';
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
    private void Register_Click(object sender, RoutedEventArgs e)
    {
        if (Validation.CheckEverything(NameTextBox.Text, SurnameTextBox.Text, PasswordTextBox.Text, RepeatPasswordTextBox.Text, EmailCodeTextBox.Text)){
            bool isCodeNum = int.TryParse(EmailCodeTextBox.Text, out int textBoxNum);
            if (isCodeNum){
                if (textBoxNum == EmailCode){
                    using(var db = new UsersDbContext()){
                        var user = new Users { Name = NameTextBox.Text, Surname = SurnameTextBox.Text, Mail = this.Mail, Password = PasswordTextBox.Text };
                        db.Users.Add(user);
                        db.SaveChanges();
                    }
                    new EntryWindow().Show();
                    Close();
                    
                }
            }
        }
    }
}