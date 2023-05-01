using Microsoft.Win32;
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

        private List<string> WorkerList = new() { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
        private List<string> WorkerList2 = new();

        private List<VmfResourcesContainer> vmfResourcesContainer = new();
        private SoundCacheProcessor soundCacheProcessor = new();
        private MdlExtractor mdlExtractor = new();
        private VmtReader vmtReader = new();
        private PcfReader pcfReader = new();

        public OtherProcessPage()
        {
            InitializeComponent();
            VmfResourcesGrid.ItemsSource = vmfResourcesContainer;
            Globals.SplitStrings();
            mdlExtractor.ReadGameInfo();
            pcfReader.AnalyzePcf("D:\\l4d2maps\\origins\\particles\\coldstream.pcf");
        }

        private void SoundCacheProcessorClick(object sender, RoutedEventArgs e)
        {
            FolderPicker folderPicker = new()
            {
                Title = "请选择单个文件夹",
                InputPath = Globals.L4D2GameInfoPath
            };

            folderPicker.ShowDialog();

            if (string.IsNullOrWhiteSpace(folderPicker.ResultName))
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

            folderPicker.ShowDialog();

            if (folderPicker.ResultNames.Count == 0)
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

            vmfResourcesContainer = new List<VmfResourcesContainer> ();
            VmfResourcesGrid.ItemsSource = vmfResourcesContainer;

            WorkerStart("VmfReader");
        }

        private void WorkerStart(string name)
        {
            worker = new()
            {
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
                    soundCacheProcessor = new()
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
                case "VmfExtractor":
                    VmfExtractor vmfExtractor = new()
                    {
                        VmfResourcesContainer = vmfResourcesContainer,
                        MdlExtractor = mdlExtractor,
                        VmtReader = vmtReader,
                        SavePath = WorkerList[7],
                        Worker = worker
                    };
                    vmfExtractor.StartProcess();
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
            vmfResourcesContainer.RemoveAt(vmfResourcesContainer.Count - 1);
            VmfResourcesGrid.ItemsSource = vmfResourcesContainer;
            ExtractButton.IsEnabled = true;
        }

        private void ExtractButtonClick(object sender, RoutedEventArgs e)
        {
            FolderPicker folderPicker = new()
            {
                Title = "请选择单个文件夹",
                InputPath = Globals.L4D2AddonPath
            };

            folderPicker.ShowDialog();

            string savePath = folderPicker.ResultName;

            if (string.IsNullOrWhiteSpace(savePath))
            {
                return;
            }

            WorkerList[7] = savePath;

            WorkerStart("VmfExtractor");
        }

        private new void MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            int selectedIndex = ((DataGrid)sender).SelectedIndex;
            VmfResourcesContainer vmfResources = vmfResourcesContainer[selectedIndex];
            List<VmfResourcesContainer> vmfResourcesList = new();

            if (vmfResources.Model != string.Empty)
            {
                if (vmfResources.Model.EndsWith(".mdl"))
                {
                    if (Globals.OfficialModelPaths.Contains(vmfResources.Model.Replace('/', '\\')))
                    {
                        Functions.TryOpenMessageWindow(11);
                        return;
                    }

                    List<string> fileList = mdlExtractor.HandleAMdl(vmfResources.Model);

                    if (fileList.Count == 0)
                    {
                        Functions.TryOpenMessageWindow(10);
                        return;
                    }

                    foreach (string material in fileList)
                    {
                        vmfResourcesList.Add(new VmfResourcesContainer(-1, material.Split('\\').Last().Replace(".vmt", ""), material));
                    }
                }
                else if (vmfResources.Model.EndsWith(".vmt"))
                {
                    if (Globals.OfficialMaterialsPaths.Contains("materials\\" + vmfResources.Model.Replace('/', '\\')))
                    {
                        Functions.TryOpenMessageWindow(11);
                        return;
                    }

                    List<string> fileList = vmtReader.HandleAVmt("materials\\" + vmfResources.Model.Replace('/', '\\'));

                    if (fileList.Count == 0)
                    {
                        Functions.TryOpenMessageWindow(10);
                        return;
                    }

                    foreach (string material in fileList)
                    {
                        vmfResourcesList.Add(new VmfResourcesContainer(-1, material.Split('\\').Last().Replace(".vmt", "").Replace('/', '\\'), material.Replace('/', '\\')));
                    }
                }
                else if (vmfResources.Classname == "worldspawn")
                {
                    foreach (string extension in Globals.SkyboxExtensionList)
                    {
                        string skyName = "materials\\skybox\\" + vmfResources.Model + extension;
                        if (Globals.OfficialMaterialsPaths.Contains(skyName + ".vmt"))
                        {
                            Functions.TryOpenMessageWindow(11);
                            return;
                        }

                        List<string> fileList = vmtReader.HandleAVmt(skyName + ".vmt");

                        if (fileList.Count == 0)
                        {
                            Functions.TryOpenMessageWindow(10);
                            return;
                        }

                        foreach (string material in fileList)
                        {
                            vmfResourcesList.Add(new VmfResourcesContainer(-1, material.Split('\\').Last().Replace(".vmt", "").Replace('/', '\\'), material.Replace('/', '\\')));
                        }
                    }
                }
                else if (vmfResources.Classname == "info_particle_system")
                {
                    string pcfName = "particles\\" + vmfResources.Model + ".pcf";
                    if (Globals.OfficialParticleFileList.Contains(pcfName))
                    {
                        Functions.TryOpenMessageWindow(11);
                        return;
                    }

                    List<string> fileList = pcfReader.HandleAPcf(pcfName);

                    if (fileList.Count == 0)
                    {
                        Functions.TryOpenMessageWindow(10);
                        return;
                    }

                    foreach (string material in fileList)
                    {
                        Debug.WriteLine(material);
                    }
                }

                EntityPreviewWindow entityPreviewWindow = new()
                {
                    Title = $"Entity ID {vmfResources.Id}",
                    Resource = vmfResourcesList,
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };
                entityPreviewWindow.ShowDialog();
            }
            else
            {
                Functions.TryOpenMessageWindow(12);
            }
        }
    }
}