using MahApps.Metro.Controls;

namespace The_Director.Windows
{
    public partial class InputNewText : MetroWindow
    {
        public delegate void _SendMessage(string value);
        public _SendMessage SendMessage;
        public InputNewText()
        {
            InitializeComponent();
            this.Width = 480;
            this.Height = 180;
        }
        
        private void ConfirmButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            SendMessage(MSGTextBox.Text);
            DialogResult = true;
            Close();
        }

        private void CancleButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
