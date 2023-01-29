using System;
using System.Windows;
using MahApps.Metro.Controls;

namespace The_Director
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Menu.SelectedIndex = 2;
        }

        private void Menu_Reselected(object sender, RoutedEventArgs e)
        {
            switch(Menu.SelectedIndex)
            {
                case 1:
                    this.WindowFrame.Source = new Uri("/Windows/SettingsPage.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case 2:
                    this.WindowFrame.Source = new Uri("/Windows/AboutPage.xaml", UriKind.RelativeOrAbsolute);
                    break;
                default:
                    this.WindowFrame.Source = null;
                    break;
            }
        }
    }
}
