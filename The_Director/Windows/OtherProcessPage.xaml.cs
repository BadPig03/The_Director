using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using The_Director.Utils;

namespace The_Director.Windows
{
    public partial class OtherProcessPage : UserControl
    {
        BackgroundWorker worker = null;

        private List<string> WorkerList = new() { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
        private List<string> WorkerList2 = new();

        private List<VmfResourcesContainer> vmfResourcesContainer = new();

        public OtherProcessPage()
        {
            InitializeComponent();
            VmfResourcesGrid.ItemsSource = vmfResourcesContainer;
#if true
            MdlExtractor mdlExtractor = new();
            mdlExtractor.Print();
#endif
        }

        private void SoundCacheProcessorClick(object sender, RoutedEventArgs e)
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

        private void SoundCachePackerClick(object sender, RoutedEventArgs e)
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

        private void CubemapBuilderClick(object sender, RoutedEventArgs e)
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

            WorkerList[5] = openFileDialog.FileName;
            WorkerList[6] = openFileDialog.SafeFileName;

            CancelButton.IsEnabled = false;
            WorkerStart("CubemapBuilder");
        }

        private void ScriptProcessorFilesClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Title = "请选择单个或多个文件",
                Filter = "nut文件 (*.nut)|*.nut|nuc文件 (*.nuc)|*.nuc",
                Multiselect = true,
                CheckFileExists = true,
                CheckPathExists = true
            };
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName == string.Empty)
            {
                return;
            }

            WorkerList2 = openFileDialog.FileNames.ToList();
            WorkerStart("ScriptProcessorFiles");
        }

        private void ScriptProcessorFolderClick(object sender, RoutedEventArgs e)
        {
            FolderPicker folderPicker = new()
            {
                Title = "请选择单个或多个文件夹",
                Multiselect = true,
                InputPath = Globals.L4D2ScriptsPath
            };

            if (folderPicker.ShowDialog().ToString() == string.Empty)
            {
                return;
            }

            WorkerList2 = folderPicker.ResultNames.ToList();
            WorkerStart("ScriptProcessorFolder");
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

            WorkerList[0] = openFileDialog.FileName;

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
            if(e.ProgressPercentage >= 0 && e.ProgressPercentage <= 100)
            {
                ProgressBar.IsIndeterminate = false;
                ProgressBar.Value = e.ProgressPercentage;
            }
            else
            {
                ProgressBar.IsIndeterminate = true;
            }
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
                    vmfResourcesContainer = vmfReader.BeginReading();
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
                case "CubemapBuilder":
                    CubemapBuilder cubemapBuilder = new()
                    {
                        FilePath = WorkerList[5],
                        FileName = WorkerList[6],
                        Worker = worker
                    };
                    cubemapBuilder.StartProcess();
                    break;
                case "ScriptProcessorFiles":
                    ScriptProcessor scriptProcessor = new()
                    {
                        Paths = WorkerList2,
                        Worker = worker
                    };
                    scriptProcessor.StartProcessFiles();
                    break;
                case "ScriptProcessorFolder":
                    ScriptProcessor scriptProcessorFolder = new()
                    {
                        Paths = WorkerList2,
                        Worker = worker
                    };
                    scriptProcessorFolder.StartProcessFolder();
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
            vmfResourcesContainer.RemoveAt(vmfResourcesContainer.Count - 1);
            VmfResourcesGrid.ItemsSource = vmfResourcesContainer;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
        }

        private new void MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            int selectedIndex = ((DataGrid)sender).SelectedIndex;
            VmfResourcesContainer vmfResources = vmfResourcesContainer[selectedIndex];
            Debug.WriteLine(vmfResources.Id);
        }
    }
}