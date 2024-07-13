using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using System.Net.Http;
using Newtonsoft.Json;
using Messenger.UI.Services;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Linq;
using System;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.

namespace Messenger.UI;

public partial class RegistrationWindow : Window
{
    public RegistrationWindow()
    {
        InitializeComponent();
    }
    private void ShowNewPasswordButton_Click(object sender, RoutedEventArgs e){
        if (PasswordTextBox.PasswordChar == '*'){
            PasswordTextBox.PasswordChar = '\0';
            NewPasswordEye.Source = new Bitmap("Assets/OpenEye.png");
        }
        else{
            PasswordTextBox.PasswordChar = '*';
            NewPasswordEye.Source = new Bitmap("Assets/ClosedEye.png");
        }
    }

    private void ShowRepeatPasswordButton_Click(object sender, RoutedEventArgs e){
        if (RepeatPasswordTextBox.PasswordChar == '*'){
            RepeatPasswordTextBox.PasswordChar = '\0';
            RepeatPasswordEye.Source = new Bitmap("Assets/OpenEye.png");
        }
        else{
            RepeatPasswordTextBox.PasswordChar = '*';
            RepeatPasswordEye.Source = new Bitmap("Assets/ClosedEye.png");
        }
    }

    private void GoBack_Click(object sender, RoutedEventArgs e)
    {
        new EntryWindow().Show();
        Close();
    }
    private async void Register_Click(object sender, RoutedEventArgs e)
    {
        HttpClient client = new();
        var content = JsonContentService.GetRegistrationContent(
            UserNameTextBox.Text, EmailTextBox.Text, PasswordTextBox.Text, RepeatPasswordTextBox.Text);

        HttpResponseMessage response = await client.PostAsync("http://localhost:5243/api/Register", content);
        if (response.IsSuccessStatusCode){
            new EntryWindow().Show();
            Close();
        }
        else{
            string errorMessage = await GetErrorMessage(response);
            await MessageBoxManager.GetMessageBoxStandard("Error", errorMessage, ButtonEnum.Ok).ShowWindowAsync();
        }
    }

    private static async Task<string> GetErrorMessage(HttpResponseMessage response)
    {
        string errorResponse = await response.Content.ReadAsStringAsync();
        string errorMessage = "";

        try
        {
            JToken errorToken = JToken.Parse(errorResponse);

            if (errorToken.Type == JTokenType.Array)
            {
                JArray errorArray = JArray.Parse(errorResponse);
                foreach (JObject errorObject in errorArray.Cast<JObject>())
                {
                    string errorCode = errorObject["code"].ToString();
                    string errorDescription = errorObject["description"].ToString();

                    errorMessage += $"• {errorCode}: {errorDescription}\n";
                }
            }
            else if (errorToken.Type == JTokenType.Object)
            {
                JObject errorObject = (JObject)errorToken;

                if (errorObject.ContainsKey("type") && errorObject.ContainsKey("errors"))
                {
                    string errorType = errorObject["type"].ToString();
                    JObject errors = (JObject)errorObject["errors"];

                    foreach (var error in errors)
                    {
                        string fieldName = error.Key;
                        JArray errorDescriptions = (JArray)error.Value;

                        foreach (var description in errorDescriptions)
                        {
                            errorMessage += $"• {fieldName}: {description}\n";
                        }
                    }
                }
                else
                {
                    errorMessage = errorObject.ToString();
                }
            }
        }
        catch (Exception)
        {
            errorMessage = errorResponse.ToString();
        }

        return errorMessage;
    }

}