using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using The_Director.Utils;

namespace The_Director.Windows
{
    public partial class LoginWindow : MetroWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            string account = AccountTextBox.Text;
            string password = PasswordTextBox.Text;
            if (account == Globals.AdminLogin.Key && password == Globals.AdminLogin.Value)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                DialogResult = false;
                Close();
            }
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ConfirmButtonClick(null, null);
            }
            else if (e.Key == Key.Escape)
            {
                ExitButtonClick(null, null);
            }
        }
    }
}
