using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaUI{
    public partial class EntryWindow : Window
    {
        public EntryWindow()
        {
            InitializeComponent();
        }

        private void EntryButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new RegistrationForm.Show();
        }
    }
}