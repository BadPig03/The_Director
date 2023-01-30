using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using The_Director.Data;
using System.Linq;

namespace The_Director
{
    public partial class MainWindow : MetroWindow
    {
        public List<NavMenu> NavMenus { get; set; } = new();

        private readonly Dictionary<string, UserControl> NavDictionarys = new();
        public static Window MainWindowInstance { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            MainWindowInstance = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;
            NavMenus.Add(new() { Title = "救援脚本", ViewName = "RescueScriptPage" , Index = 0});
            NavMenus.Add(new() { Title = "软件设置", ViewName = "SettingsPage", Index = 1 });
            NavMenus.Add(new() { Title = "关于作者", ViewName = "AboutPage", Index = 2 });
            NavMenus.ForEach(menu =>
            {
                var type = Type.GetType($"The_Director.Windows.{menu.ViewName}");
                NavDictionarys.Add(menu.ViewName, Activator.CreateInstance(type) as UserControl);
            });
        }

        private void MenuReselected(object sender, SelectionChangedEventArgs e)
        {
            WindowFrame.Content = NavDictionarys.Values.ElementAt(Menu.SelectedIndex);
        }
    }
}
