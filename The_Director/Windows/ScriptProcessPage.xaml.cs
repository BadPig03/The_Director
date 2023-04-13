using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
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
                Title = "请选择单个或多个文件",
                Filter = "nut文件 (*.nut)|*.nut",
                Multiselect = true,
                CheckFileExists = true,
                CheckPathExists = true
            };
            if (!(bool)openFileDialog.ShowDialog())
            {
                return;
            }

            foreach (string file in openFileDialog.FileNames)
            {
                Functions.RunVice3(file);
            }

            Functions.TryOpenMessageWindow(9);
        }

        private void EncryptFolderClick(object sender, RoutedEventArgs e)
        {
            FolderPicker folderPicker = new()
            {
                Title = "请选择单个或多个文件夹",
                Multiselect = true,
                InputPath = Globals.L4D2ScriptsPath,
            };
            if (folderPicker.ShowDialog().ToString() == string.Empty)
            {
                return;
            }

            foreach (string path in folderPicker.ResultPaths)
            {
                foreach (var file in Functions.GetAllFileInfo(new System.IO.DirectoryInfo(path)))
                {
                    if (file.Extension == ".nut")
                        Functions.RunVice3(file.FullName);
                }
            }

            Functions.TryOpenMessageWindow(9);
        }

        private void SaveAsNucClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new()
            {
                Title = "保存文件",
                Filter = "nuc文件 (*.nuc)|*.nuc"
            };
            if (!(bool)saveFileDialog.ShowDialog())
            {
                return;
            }

            string fileDir = Globals.L4D2TempPath + saveFileDialog.ToString().Substring(saveFileDialog.ToString().LastIndexOf("\\") + 1).Replace(".nuc", ".nut");
            Functions.SaveTextToPath(fileDir, ScriptWindow.Text);
            Functions.RunVice3(fileDir, true, saveFileDialog.FileName);
        }

        private void DecryptClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Title = "请选择单个或多个文件",
                Filter = "nuc文件 (*.nuc)|*.nuc",
                Multiselect = true,
                CheckFileExists = true,
                CheckPathExists = true
            };
            if (!(bool)openFileDialog.ShowDialog())
            {
                return;
            }

            foreach (string file in openFileDialog.FileNames)
            {
                Functions.RunVice3(file, false);
            }

            Functions.TryOpenMessageWindow(9);
        }

        private void DecryptFolderClick(object sender, RoutedEventArgs e)
        {
            FolderPicker folderPicker = new()
            {
                Title = "请选择单个或多个文件夹",
                Multiselect = true,
                InputPath = Globals.L4D2ScriptsPath,
            };
            if (folderPicker.ShowDialog().ToString() == string.Empty)
            {
                return;
            }

            foreach (string path in folderPicker.ResultPaths)
            {
                foreach (var file in Functions.GetAllFileInfo(new DirectoryInfo(path)))
                {
                    if (file.Extension == ".nuc")
                        Functions.RunVice3(file.FullName, false);
                }
            }

            Functions.TryOpenMessageWindow(9);
        }

        private void SaveAsNutClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new()
            {
                Title = "保存文件",
                Filter = "nut文件 (*.nut)|*.nut"
            };
            if (!(bool)saveFileDialog.ShowDialog())
            {
                return;
            }
            Functions.SaveTextToPath(saveFileDialog.FileName, ScriptWindow.Text);
        }
    }
}