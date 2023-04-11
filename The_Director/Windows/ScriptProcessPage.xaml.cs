using Microsoft.Win32;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using The_Director.Utils;

namespace The_Director.Windows
{
    public partial class ScriptProcessPage : UserControl
    {
        public ScriptProcessPage()
        {
            InitializeComponent();
            Functions.SaveVice3ToPath();
        }

        private void EncryptClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Title = "请选择文件",
                Filter = "nut文件 (*.nut)|*.nut",
                Multiselect = true,
                CheckFileExists = true,
                CheckPathExists = true
            };
            openFileDialog.ShowDialog();

            foreach (string file in openFileDialog.FileNames)
            {
                Functions.RunVice3(file);
            }

        }

        private void DecryptClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Title = "请选择文件",
                Filter = "nuc文件 (*.nuc)|*.nuc",
                Multiselect = true,
                CheckFileExists = true,
                CheckPathExists = true
            };
            openFileDialog.ShowDialog();

            foreach (string file in openFileDialog.FileNames)
            {
                Functions.RunVice3(file, false);
            }
            
        }
    }
}