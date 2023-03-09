using System;
using System.Windows.Controls;
using The_Director.Utils;
using Steamworks;
using System.Diagnostics;

namespace The_Director.Windows
{
    public partial class SettingsPage : UserControl
    {
        public SettingsPage()
        {
            InitializeComponent();
            GamePathTextBox.Text = Clients.GetSteamPath();
            SteamClient.Init(Globals.L4D2AppID);
            Console.WriteLine(SteamApps.AppInstallDir(Globals.L4D2AppID));
        }
    }
}
