using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaUI;

public partial class SecondPart_PassRecoveryWindow : Window
{
    int code;
    public SecondPart_PassRecoveryWindow(int code)
    {
        this.code = code;
        InitializeComponent();
    }

    private void GoFurther_Click(object sender, RoutedEventArgs e)
    {
        bool isCodeNum = int.TryParse(EmailCode.Text, out int textBoxNum);
        if (isCodeNum){
            if (textBoxNum == code){
                new ThirdPart_PassRecoveryWindow().Show();
                Close();
            }
        }
    }
}