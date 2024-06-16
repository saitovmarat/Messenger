using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaUI;

public partial class EmailCodeCheckWindow : Window
{
    private int code;
    public EmailCodeCheckWindow(int code)
    {
        this.code = code;
        InitializeComponent();
    }
    private void RegisterButton_Click(object sender, RoutedEventArgs e)
    {
        bool isCodeCorrect = int.TryParse(EmailCode.Text, out int textBoxNum);
        if (isCodeCorrect){
            if (textBoxNum == code){
                new EntryWindow().Show();
                Close();
            }
        }
    }
}