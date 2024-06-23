using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Input;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;


namespace Messenger.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {   
            InitializeComponent();
            // TextBlock textBlock1 = new TextBlock();
            // textBlock1.FontSize = 22;
            // textBlock1.Height = 100;
            // textBlock1.Text = "Block 1";
            // textBlock1.Background = Brushes.LightBlue;
            // ChatStackPanel.Children.Add(textBlock1);
        }


        private void Panel_Click(object sender, PointerPressedEventArgs e)
        {
            MessageBoxManager.GetMessageBoxStandard("лалала", "лалала", ButtonEnum.Ok).ShowWindowAsync();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox textBox = (TextBox)sender;
                string? message = textBox.Text;

                textBox.Text = string.Empty;
            }
        }

    }
}
