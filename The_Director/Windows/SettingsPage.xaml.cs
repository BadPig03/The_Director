using System;
using System.Windows.Controls;
using The_Director.Utils;

namespace The_Director.Windows
{


    public partial class SettingsPage : UserControl
    {
        public SettingsPage()
        {
            InitializeComponent();
            GamePathTextBox.Text = Clients.GetSteamPath();
        }
    }
}
