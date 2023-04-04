using System;
using System.Windows.Controls;
using The_Director.Utils;
using Steamworks;
using System.Diagnostics;
using System.IO;

namespace The_Director.Windows
{
    public partial class SettingsPage : UserControl
    {
        public SettingsPage()
        {
            InitializeComponent();
            GamePathTextBox.Text = Clients.GetSteamPath();
            //Globals.FileToBase64String();
        }
    }
}
