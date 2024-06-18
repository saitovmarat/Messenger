using Avalonia.Controls;
using Avalonia.Interactivity;
#pragma warning disable CS8604 

namespace AvaloniaUI;

public partial class SecondPart_PassRecoveryWindow : Window
{
    private int MailCode { get; set; }
    private string Mail { get; set; }
    
    public SecondPart_PassRecoveryWindow(string mail, int code)
    {
        Mail = mail;
        MailCode = code;
        InitializeComponent();
    }

    public SecondPart_PassRecoveryWindow()
    {
        Mail = "";
        MailCode = 0;
        InitializeComponent();
    }

    private void GoFurther_Click(object sender, RoutedEventArgs e)
    {
        bool isCodeNum = int.TryParse(MailCodeTextBox.Text, out int textBoxNum);
        if (isCodeNum){
            if (textBoxNum == MailCode){
                new ThirdPart_PassRecoveryWindow(Mail).Show();
                Close();
            }
        }
    }
}