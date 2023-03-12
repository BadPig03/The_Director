using System.Windows;
using MahApps.Metro.Controls;
using System.Windows.Input;

namespace The_Director.Windows
{
    public partial class PreviewScriptWindow : MetroWindow
    {
        public string TextBoxString { get; set; }

        public PreviewScriptWindow()
        {
            InitializeComponent();
        }

        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            ScriptTextBox.Text = TextBoxString;
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space || e.Key == Key.Escape)
                ConfirmButtonClick(null, null);
        }
    }
}
