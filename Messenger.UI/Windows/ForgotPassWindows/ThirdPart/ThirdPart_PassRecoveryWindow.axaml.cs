using System.Linq;
using System.Net.Http;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Messenger.UI.Services;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace Messenger.UI;

public partial class ThirdPart_PassRecoveryWindow : Window
{
    private string? Email { get; set; }
    public ThirdPart_PassRecoveryWindow(string email)
    {
        Email = email;
        InitializeComponent();
    }
    public ThirdPart_PassRecoveryWindow() =>
        InitializeComponent();
        
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
    private async void ChangePass_Click(object sender, RoutedEventArgs e)
    {
        if(NewPasswordTextBox.Text != RepeatNewPasswordTextBox.Text){
            await MessageBoxManager.GetMessageBoxStandard("Error", "Пароли не совпадают", ButtonEnum.Ok).ShowWindowAsync();
            return;
        }
        using (HttpClient client = new HttpClient())
        {
            var content = JsonContentService.GetChangePasswordContent(Email, NewPasswordTextBox.Text);

            HttpResponseMessage response = await client.PostAsync("http://localhost:5243/api/ChangePassword", content);

            if (response.IsSuccessStatusCode){
                new EntryWindow().Show();
                Close();
            }
            else{
                await MessageBoxManager.GetMessageBoxStandard("Error", $"Ошибка отправки запроса: {response.StatusCode}", ButtonEnum.Ok).ShowWindowAsync();
            }
        }
    }
}