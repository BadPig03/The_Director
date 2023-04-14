using Microsoft.Win32;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shell;
using The_Director.Utils;

namespace The_Director.Windows
{
    public partial class OtherProcessPage : UserControl
    {
        BackgroundWorker worker = null;

        private List<string> WorkerList = new() { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };

        public OtherProcessPage()
        {
            InitializeComponent();
        }

        private void SoundcacheClick(object sender, RoutedEventArgs e)
        {
            FolderPicker folderPicker = new()
            {
                Title = "请选择单个文件夹",
                InputPath = Globals.L4D2GameInfoPath
            };

            if (folderPicker.ShowDialog().ToString() == string.Empty)
            {
                return;
            }

            SaveFileDialog saveFileDialog = new()
            {
                Title = "请选择保存位置",
                FileName = "sound.cache",
                InitialDirectory = Globals.L4D2RootPath
            };
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName == string.Empty)
            {
                return;
            }

            WorkerList[1] = folderPicker.ResultName;
            WorkerList[2] = saveFileDialog.FileName;

            CancelButton.IsEnabled = false;
            WorkerStart("SoundCacheProcessor");
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

            SaveFileDialog saveFileDialog = new()
            {
                Title = "请选择保存位置",
                Filter = "vpk文件(*.vpk)|*.vpk",
                InitialDirectory = Globals.L4D2AddonPath
            };
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName == string.Empty)
            {
                return;
            }

            WorkerList[3] = openFileDialog.FileName;
            WorkerList[4] = saveFileDialog.FileName;

            CancelButton.IsEnabled = false;
            WorkerStart("SoundCachePacker");
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
            else
            {
                WorkerList[0] = openFileDialog.FileName;
            }

            CancelButton.IsEnabled = true;
            WorkerStart("VmfReader");
        }

        private void WorkerStart(string name)
        {
            worker = new()
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };
            worker.ProgressChanged += WorkerProgressChanged;
            worker.DoWork += WorkerDoWork;
            worker.RunWorkerCompleted += WorkerCompleted;
            worker.RunWorkerAsync(name);
        }

        private void WorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;
        }

        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            if (worker.CancellationPending)
            {
                e.Cancel = true;
            }

            switch((string)e.Argument)
            {
                case "VmfReader":
                    VmfReader vmfReader = new()
                    {
                        VmfPath = WorkerList[0],
                        Worker = worker
                    };
                    vmfReader.BeginReading();
                    break;
                case "SoundCacheProcessor":
                    SoundCacheProcessor soundCacheProcessor = new()
                    {
                        OldFolderPath = WorkerList[1],
                        FilePath = WorkerList[2],
                        Worker = worker
                    };
                    soundCacheProcessor.StartProcess();
                    break;
                case "SoundCachePacker":
                    SoundCachePacker soundCachePack = new()
                    {
                        OldFilePath = WorkerList[3],
                        FilePath = WorkerList[4],
                        Worker = worker
                    };
                    soundCachePack.StartProcess();
                    break;
                default:
                    break;
            }
        }

        private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker backgroundWorker = (BackgroundWorker)sender;
            backgroundWorker.DoWork -= WorkerDoWork;
            backgroundWorker.RunWorkerCompleted -= WorkerCompleted;
            backgroundWorker = null;
            CancelButton.IsEnabled = false;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
        }
    }
}