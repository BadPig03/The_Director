using Microsoft.Win32;
using Steamworks.Ugc;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using The_Director.Utils;

namespace The_Director.Windows
{
    public partial class OtherProcessPage : UserControl
    {
        public OtherProcessPage()
        {
            InitializeComponent();
        }

        private void SoundcacheClick(object sender, RoutedEventArgs e)
        {
            FolderPicker folderPicker = new()
            {
                Title = "请选择单个文件夹",
                InputPath = Globals.L4D2GameInfoPath,
            };

            if (folderPicker.ShowDialog().ToString() == string.Empty)
            {
                return;
            }

            if (Directory.Exists(Globals.L4D2CustomAudioPath))
            {
                Directory.Delete(Globals.L4D2CustomAudioPath, true);
            }

            Directory.CreateDirectory(Globals.L4D2CustomAudioPath);

            Functions.DirectoryCopy(folderPicker.ResultName, Globals.L4D2CustomAudioPath, true);

            FileInfo fileInfo = new(Globals.L4D2CustomAudioPath + "\\sound.cache");
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }

            Process process = new();
            process.StartInfo.FileName = $"{Globals.L4D2RootPath}\\left4dead2.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.Arguments = $"-steam -insecure -novid -hidden -noborder -x 4096 -y 2160 +snd_buildsoundcachefordirectory {Globals.L4D2CustomAudioPath}";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            while (!new FileInfo(Globals.L4D2CustomAudioPath + "\\sound.cache").Exists)
            {
                Task.Delay(1000);
            }
            process.Kill();
            process.Dispose();

            SaveFileDialog saveFileDialog = new()
            {
                Title = "请选择保存位置",
                FileName = "sound.cache",
                InitialDirectory = Globals.L4D2RootPath
            };
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != string.Empty)
            {
                File.Move(Globals.L4D2CustomAudioPath + "\\sound.cache", Path.GetDirectoryName(saveFileDialog.FileName) + "\\sound.cache");
            }

            Directory.Delete(Globals.L4D2CustomAudioPath, true);
        }

        private void PackSoundcacheClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Title = "请选择音频缓存文件",
                Filter = "sound.cache(sound.cache)|sound.cache",
                CheckFileExists = true,
                CheckPathExists = true,
                InitialDirectory = Globals.L4D2RootPath
            };

            openFileDialog.ShowDialog();

            if (openFileDialog.FileName == string.Empty)
            {
                return;
            }

            if (Directory.Exists(Globals.L4D2CustomPackPath))
            {
                Directory.Delete(Globals.L4D2CustomPackPath, true);
            }

            Directory.CreateDirectory(Globals.L4D2CustomPackPath + "\\sound");

            File.Copy(openFileDialog.FileName, Globals.L4D2CustomPackPath + "\\sound\\sound.cache", true);

            Functions.RunVPK(Globals.L4D2CustomPackPath);

            SaveFileDialog saveFileDialog = new()
            {
                Title = "请选择保存位置",
                Filter = "vpk文件(*.vpk)|*.vpk",
                InitialDirectory = Globals.L4D2AddonPath
            };
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != string.Empty)
            {
                File.Move(Globals.L4D2TempPath + "pack.vpk", saveFileDialog.FileName);
            }

            Directory.Delete(Globals.L4D2CustomPackPath, true);
        }

        private void BuildCubemapsClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Title = "请选择地图bsp文件",
                Filter = "bsp文件(*.bsp)|*.bsp",
                CheckFileExists = true,
                CheckPathExists = true,
                InitialDirectory = Globals.L4D2MapsPath
            };

            openFileDialog.ShowDialog();

            if (openFileDialog.FileName == string.Empty)
            {
                return;
            }

            File.Copy(openFileDialog.FileName, $"{Globals.L4D2MapsPath}\\{openFileDialog.SafeFileName}", true);

            string mapName = openFileDialog.SafeFileName.Replace(".bsp", "");

            Process process = new();
            process.StartInfo.FileName = $"{Globals.L4D2RootPath}\\left4dead2.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.Arguments = $"-steam -insecure -novid -hidden -nosound -noborder -x 4096 -y 2160 +map {mapName} -buildcubemaps";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
            process.Close();
        }

        private void ExtractVmfResourcesClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Title = "请选择vmf文件",
                Filter = "vmf文件(*.vmf)|*.vmf",
                CheckFileExists = true,
                CheckPathExists = true,
                InitialDirectory = Globals.L4D2MapsPath
            };

            openFileDialog.ShowDialog();

            if (openFileDialog.FileName == string.Empty)
            {
                return;
            }

            ProgressWindow progressWindow = new()
            {
                FilePath = openFileDialog.FileName,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            progressWindow.ShowDialog();
        }
    }
}