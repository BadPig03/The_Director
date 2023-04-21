using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using The_Director.Utils;

namespace The_Director.Windows
{
    public partial class EntityPreviewWindow : MetroWindow
    {
        public virtual List<VmfResourcesContainer> Resource { get; set; }
        
        public EntityPreviewWindow()
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
            VmfResourcesGrid.ItemsSource = Resource;
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space || e.Key == Key.Escape)
            {
                ConfirmButtonClick(null, null);
            }
        }

        private new void MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            int selectedIndex = ((DataGrid)sender).SelectedIndex;
            bool flag = true;

            foreach (string path in Globals.PossiblePaths)
            {
                string path2 = path + "\\" + Resource[selectedIndex].Targetname.Replace("/", "\\");
                if (File.Exists(path2))
                {
                    flag = false;
                    Process.Start("notepad.exe", path2);
                    break;
                }
            }

            if (flag)
            {
                Functions.TryOpenMessageWindow(10);
            }
        }
    }
}
