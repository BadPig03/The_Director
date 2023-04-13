using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using The_Director.Utils;

namespace The_Director.Windows
{
    public partial class StandardRescueSettings : UserControl
    {
        private int? TotalWaveCount = null;
        private bool IsTotalWaveConfirmed = new();
        private bool IsNutConfirmed = new();
        private bool IsNavConfirmed = new();
        private bool? IsScriptWindowEnabled = new();

        private Dictionary<string, string> TotalWaveDicts = new();
        private Dictionary<int, string> TextBoxDicts = new();
        private Dictionary<int, int> ComboBoxDicts = new();
        private Dictionary<string, BooleanString> StandardDict = new();

        private delegate void DelegateReadStandardOutput(string result);
        private event DelegateReadStandardOutput ReadStandardOutput;
        private Process process = new();
        private CancellationTokenSource cts = new();

        public void MSGReceived(string value)
        {
            if (value != null)
            {
                StandardDict["MSG"] = (true, value);
            }
            else
            {
                MSGCheckBox.IsChecked = false;
            }
        }

        public void TotalWaveReceived(string value)
        {
            if (value == null)
            {
                IsTotalWaveConfirmed = false;
            }
            else
            {
                TotalWaveDicts.Clear();
                IsTotalWaveConfirmed = true;
            }
        }

        public void SaveToNutReceived(string value)
        {
            IsNutConfirmed = value != null;
        }

        public void SaveToNavReceived(string value)
        {
            IsNavConfirmed = value != null;
        }

        public StandardRescueSettings()
        {
            InitializeComponent();
            ReadStandardOutput += new DelegateReadStandardOutput(ReadStandardOutputAction);
            PreferredMobDirectionComboBox.ItemsSource = Globals.PreferredMobDirectionList;
            PreferredSpecialDirectionComboBox.ItemsSource = Globals.PreferredSpecialDirectionList;
            MapSelectionComboBox.ItemsSource = Globals.OffcialMapStandardRescueList;
            MapSelectionComboBox.SelectedIndex = 0;
            StandardDict.Add("MSG", new BooleanString(false, string.Empty));
            StandardDict.Add("ShowStage", new BooleanString(false, null));
            StandardDict.Add("info_director", new BooleanString(false, string.Empty));
            StandardDict.Add("trigger_finale", new BooleanString(false, string.Empty));
            StandardDict.Add("ScriptFile", new BooleanString(false, string.Empty));
            StandardDict.Add("LockTempo", new BooleanString(false, null));
            StandardDict.Add("BuildUpMinInterval", new BooleanString(false, string.Empty));
            StandardDict.Add("IntensityRelaxThreshold", new BooleanString(false, string.Empty));
            StandardDict.Add("MobRechargeRate", new BooleanString(false, string.Empty));
            StandardDict.Add("MobSpawnMaxTime", new BooleanString(false, string.Empty));
            StandardDict.Add("MobSpawnMinTime", new BooleanString(false, string.Empty));
            StandardDict.Add("MusicDynamicMobScanStopSize", new BooleanString(false, string.Empty));
            StandardDict.Add("MusicDynamicMobSpawnSize", new BooleanString(false, string.Empty));
            StandardDict.Add("MusicDynamicMobStopSize", new BooleanString(false, string.Empty));
            StandardDict.Add("PreferredMobDirection", new BooleanString(false, string.Empty));
            StandardDict.Add("RelaxMaxFlowTravel", new BooleanString(false, string.Empty));
            StandardDict.Add("RelaxMaxInterval", new BooleanString(false, string.Empty));
            StandardDict.Add("RelaxMinInterval", new BooleanString(false, string.Empty));
            StandardDict.Add("MinimumStageTime", new BooleanString(false, string.Empty));
            StandardDict.Add("PreferredSpecialDirection", new BooleanString(false, string.Empty));
            StandardDict.Add("ProhibitBosses", new BooleanString(false, null));
            StandardDict.Add("ShouldAllowMobsWithTank", new BooleanString(false, null));
            StandardDict.Add("ShouldAllowSpecialsWithTank", new BooleanString(false, null));
            StandardDict.Add("EscapeSpawnTanks", new BooleanString(true, null));
            StandardDict.Add("SpecialRespawnInterval", new BooleanString(false, string.Empty));
            StandardDict.Add("SustainPeakMaxTime", new BooleanString(false, string.Empty));
            StandardDict.Add("SustainPeakMinTime", new BooleanString(false, string.Empty));
            StandardDict.Add("BileMobSize", new BooleanString(false, string.Empty));
            StandardDict.Add("BoomerLimit", new BooleanString(false, string.Empty));
            StandardDict.Add("ChargerLimit", new BooleanString(false, string.Empty));
            StandardDict.Add("CommonLimit", new BooleanString(false, string.Empty));
            StandardDict.Add("DominatorLimit", new BooleanString(false, string.Empty));
            StandardDict.Add("HunterLimit", new BooleanString(false, string.Empty));
            StandardDict.Add("JockeyLimit", new BooleanString(false, string.Empty));
            StandardDict.Add("MaxSpecials", new BooleanString(false, string.Empty));
            StandardDict.Add("MegaMobSize", new BooleanString(false, string.Empty));
            StandardDict.Add("MobMaxPending", new BooleanString(false, string.Empty));
            StandardDict.Add("MobMaxSize", new BooleanString(false, string.Empty));
            StandardDict.Add("MobMinSize", new BooleanString(false, string.Empty));
            StandardDict.Add("MobSpawnSize", new BooleanString(false, string.Empty));
            StandardDict.Add("SmokerLimit", new BooleanString(false, string.Empty));
            StandardDict.Add("SpitterLimit", new BooleanString(false, string.Empty));
            StandardDict.Add("TankLimit", new BooleanString(false, string.Empty));
            StandardDict.Add("WitchLimit", new BooleanString(false, string.Empty));
            StandardDict.Add("HordeEscapeCommonLimit", new BooleanString(false, string.Empty));
            info_directorTextBox.Text = "director";
            trigger_finaleTextBox.Text = "finale_radio";
            ScriptFileTextBox.Text = "standard_finale.nut";
            EscapeSpawnTanksCheckBox.IsChecked = true;
        }

        private void TryOpenMSGWindow()
        {
            InputNewText inputNewText = new()
            {
                TextBoxText = StandardDict["MSG"].Item2,
                SendMessage = MSGReceived,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            inputNewText.ShowDialog();
            UpdateScriptWindow();
        }

        private bool TryOpenTotalWaveWindow(int WaveCount)
        {
            TextBoxDicts.Clear();
            ComboBoxDicts.Clear();

            TotalWaveSettings totalWaveSettings = new()
            {
                WaveCounts = WaveCount,
                TotalWaveDicts = TotalWaveDicts,
                SendMessage = TotalWaveReceived,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            totalWaveSettings.ShowDialog();

            if (!IsTotalWaveConfirmed)
            {
                return true;
            }

            IsScriptWindowEnabled = true;

            foreach (var item in totalWaveSettings.TotalWaveGrid.Children)
            {
                if (item is TextBox textBox)
                {
                    if (textBox.Name.StartsWith("__"))
                    {
                        TextBoxDicts[Functions.ConvertToInt(textBox.Name.Remove(0, 2))] += $"{((textBox.Text != string.Empty) ? "\x1c" : string.Empty)}{textBox.Text}";
                    }
                    else
                    {
                        TextBoxDicts[Functions.ConvertToInt(textBox.Name.Remove(0, 1))] = textBox.Text;
                    }
                }
                if (item is ComboBox comboBox)
                {
                    ComboBoxDicts[Functions.ConvertToInt(comboBox.Name.Remove(0, 1))] = comboBox.SelectedIndex;
                }
            }
            foreach (var item1 in ComboBoxDicts)
            {
                foreach (var item2 in TextBoxDicts)
                {
                    if (item1.Key == item2.Key)
                    {
                        TotalWaveDicts[$"{item1.Key + 1}"] = $"{Functions.TotalWaveToString(item1.Value)}\x1b{item2.Value}";
                    }
                }
            }

            TotalWaveCount = WaveCount;

            UpdateScriptWindow();

            return false;
        }

        private void TotalWaveButtonClick(object sender, RoutedEventArgs e)
        {
            if (Functions.IsProperInt(TotalWaveTextBox.Text, 1, int.MaxValue))
            {
                if (TryOpenTotalWaveWindow(Functions.ConvertToInt(TotalWaveTextBox.Text)))
                {
                    TotalWaveTextBox.Text = TotalWaveCount.ToString();
                }
                else
                {
                    TotalWaveCount = Functions.ConvertToInt(TotalWaveTextBox.Text);
                }
            }
        }

        private void CheckBoxClick(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            var Name = checkBox.Name.Remove(checkBox.Name.Length - 8, 8);
            var IsChecked = (bool)checkBox.IsChecked;

            if (Name == "TriggerEscapeStage")
            {
                ToggleAllObjects(!IsChecked);
                return;
            }

            if (IsChecked && (TotalWaveCount <= 0 || TotalWaveCount == null))
            {
                Functions.TryOpenMessageWindow(0);
                checkBox.IsChecked = false;
                return;
            }

            if (Name != "MSG")
            {
                StandardDict[Name] = (IsChecked, null);
            }
            else
            {
                if (!StandardDict[Name].Item1 && IsChecked)
                {
                    TryOpenMSGWindow();
                }

                StandardDict[Name] = (IsChecked, StandardDict[Name].Item2);
            }
            UpdateScriptWindow();
        }

        private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            var Name = textBox.Name.Remove(textBox.Name.Length - 7, 7);
            switch (Globals.TextBoxIndex(Name))
            {
                case 0:
                    if (textBox.Text != string.Empty && Functions.IsProperInt(textBox.Text, 1, 99))
                    {
                        TotalWaveButton.IsEnabled = true;
                        if (Functions.IsProperInt(textBox.Text, 10, 99))
                        {
                            TotalWaveButtonClick(TotalWaveButton, null);
                        }
                    }
                    else
                    {
                        if(textBox.Text != string.Empty)
                        {
                            Functions.TryOpenMessageWindow(1);
                        }

                        IsScriptWindowEnabled = false;
                        TotalWaveButton.IsEnabled = false;
                        textBox.Text = string.Empty;
                        if (TotalWaveCount != null)
                        {
                            TotalWaveCount = null;
                        }
                    }
                    break;
                case 1:
                    if (textBox.Text != string.Empty && (TotalWaveCount <= 0 || TotalWaveCount == null))
                    {
                        Functions.TryOpenMessageWindow(0);
                        textBox.Text = string.Empty;
                        return;
                    }
                    if (textBox.Text != string.Empty && !Functions.IsProperFloat(textBox.Text, 0, 1))
                    {
                        Functions.TryOpenMessageWindow(2);
                        textBox.Text = string.Empty;
                    }
                    break;
                case 2:
                    if (textBox.Text != string.Empty && (TotalWaveCount <= 0 || TotalWaveCount == null))
                    {
                        Functions.TryOpenMessageWindow(0);
                        textBox.Text = string.Empty;
                        return;
                    }
                    if (textBox.Text != string.Empty && !Functions.IsProperFloat(textBox.Text, 0, float.MaxValue))
                    {
                        Functions.TryOpenMessageWindow(3);
                        textBox.Text = string.Empty;
                    }
                    break;
                case 3:
                    if (textBox.Text != string.Empty && (TotalWaveCount <= 0 || TotalWaveCount == null) && !Name.Contains(" "))
                    {
                        Functions.TryOpenMessageWindow(0);
                        textBox.Text = string.Empty;
                        return;
                    }
                    if (textBox.Text != string.Empty && !Functions.IsProperInt(textBox.Text, 0, int.MaxValue))
                    {
                        Functions.TryOpenMessageWindow(4);
                        textBox.Text = string.Empty;
                    }
                    break;
                case 4:
                    if (textBox.Text != string.Empty && (TotalWaveCount <= 0 || TotalWaveCount == null))
                    {
                        Functions.TryOpenMessageWindow(0);
                        textBox.Text = string.Empty;
                        return;
                    }
                    if (textBox.Text != string.Empty && !Functions.IsProperInt(textBox.Text, -1, int.MaxValue))
                    {
                        Functions.TryOpenMessageWindow(5);
                        textBox.Text = string.Empty;
                    }
                    break;
                case 5:
                    if (textBox.Text == string.Empty || !Functions.IsProperString(textBox.Text))
                    {
                        Functions.TryOpenMessageWindow(6);
                        if(Name == "info_director")
                        {
                            textBox.Text = "director";
                        }
                        else if (Name == "trigger_finale")
                        {
                            textBox.Text = "finale_radio";
                        }
                        else if (Name == "ScriptFile")
                        {
                            textBox.Text = "standard_finale.nut";
                        }
                        return;
                    }
                    break;
                default:
                    break;
            }
            if (Name != "TotalWave" || Name != "MSG")
            {
                StandardDict[Name] = (textBox.Text != string.Empty, textBox.Text);
            }

            UpdateScriptWindow();
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            if (comboBox.SelectedIndex != 0 && (TotalWaveCount <= 0 || TotalWaveCount == null))
            {
                Functions.TryOpenMessageWindow(0);
                comboBox.SelectedIndex = 0;
                return;
            }

            StandardDict[comboBox.Name.Remove(comboBox.Name.Length - 8, 8)] = (comboBox.SelectedItem.ToString() != string.Empty, comboBox.SelectedItem.ToString());
            UpdateScriptWindow();
        }

        private void UpdateScriptWindow()
        {
            if (IsScriptWindowEnabled == false)
            {
                ScriptWindow.Text = string.Empty;
            }
            else if (IsScriptWindowEnabled == true)
            {
                StringBuilder ScriptWindowText = new();

                if ((bool)MSGCheckBox.IsChecked)
                {
                    ScriptWindowText.AppendLine($"Msg(\"{StandardDict["MSG"].Item2}\");\n");
                }

                if (TotalWaveCount > 0)
                {
                    ScriptWindowText.AppendLine("PANIC <- 0\nTANK <- 1\nDELAY <- 2\nSCRIPTED <- 3\n\nDirectorOptions <-\n{");
                }

                if (TotalWaveDicts.Count != 0 && TotalWaveCount > 0)
                {
                    ScriptWindowText.AppendLine($"\tA_CustomFinale_StageCount = {TotalWaveCount}\n");
                    foreach (var item in TotalWaveDicts)
                    {
                        ScriptWindowText.AppendLine($"\tA_CustomFinale{item.Key} = {item.Value.Split('\x1b')[0]}");
                        if (item.Value.Split('\x1b')[1].Contains("\x1c"))
                        {
                            ScriptWindowText.AppendLine($"\tA_CustomFinaleValue{item.Key} = {item.Value.Split('\x1b')[1].Split('\x1c')[0]}");
                            if (item.Value.Split('\x1b')[0] == "TANK")
                            {
                                ScriptWindowText.AppendLine($"\tA_CustomFinaleMusic{item.Key} = {item.Value.Split('\x1b')[1].Split('\x1c')[1]}");
                            }
                        }
                        else
                        {
                            ScriptWindowText.AppendLine($"\tA_CustomFinaleValue{item.Key} = {item.Value.Split('\x1b')[1]}");
                        }
                    }
                    ScriptWindowText.AppendLine("");
                }

                foreach (var item in StandardDict)
                {
                    if (item.Value.Item1 && !Globals.StandardDictBlackList.Contains(item.Key))
                    {
                        if (item.Value.Item2 != null)
                        {
                            ScriptWindowText.AppendLine($"\t{item.Key} = {item.Value.Item2}");
                        }
                        else
                        {
                            ScriptWindowText.AppendLine($"\t{item.Key} = {item.Value.Item1.ToString().ToLower()}");
                        }
                    }
                    else if (!item.Value.Item1 && item.Key == "EscapeSpawnTanks")
                    {
                        ScriptWindowText.AppendLine($"\t{item.Key} = {item.Value.Item1.ToString().ToLower()}");
                    }
                }

                if (TotalWaveCount > 0)
                {
                    ScriptWindowText.AppendLine("}");
                }

                if (StandardDict["ShowStage"].Item1)
                {
                    ScriptWindowText.AppendLine("\nfunction OnBeginCustomFinaleStage(num, type)\n{\n\tprintl(\"Beginning custom finale stage \" + num + \" of type \"+ type);\n}");
                }

                ScriptWindow.Text = ScriptWindowText.ToString();
            }
            else
            {
                ScriptWindow.Text = "Msg(\"For rescue debug purpose only.\\n\");\n\nDELAY <- 2\n\nDirectorOptions <-\n{\n\tA_CustomFinale_StageCount = 1\n\n\tA_CustomFinale1 = DELAY\n\tA_CustomFinaleValue1 = 1\n}\n";
            }

            if (ScriptWindow.Text != string.Empty)
            {
                PasteToClipboardButton.IsEnabled = true;
                SaveAsNutButton.IsEnabled = true;
                SaveAsVmfButton.IsEnabled = true;
                CompileVmfButton.IsEnabled = true;
            }
            else
            {
                PasteToClipboardButton.IsEnabled = false;
                SaveAsNutButton.IsEnabled = false;
                SaveAsVmfButton.IsEnabled = false;
                CompileVmfButton.IsEnabled = false;
            }
        }

        private void MouseClick(object sender, RoutedEventArgs e)
        {
            Label label = (Label)sender;
            HintWindow hintWindow = new()
            {
                TextBoxString = Globals.GetButtonString(label.Content.ToString()),
                HyperlinkUri = Globals.GetButtonHyperlinkUri(label.Content.ToString()),
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            hintWindow.ShowDialog();
        }

        private void PasteToClipboardClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(ScriptWindow.Text);
            Functions.TryOpenMessageWindow(7);
        }

        private void SaveAsNutClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new()
            {
                Title = "导出脚本文件",
                Filter = "nut文件 (*.nut)|*.nut",
                InitialDirectory = Globals.L4D2ScriptsPath
            };
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != string.Empty)
            {
                Functions.SaveNutToPath(saveFileDialog.FileName, ScriptWindow.Text);
            }
        }

        private void SaveAsVmfClick(object sender, RoutedEventArgs e) 
        {
            SaveFileDialog saveFileDialog = new()
            {
                Title = "导出vmf文件",
                Filter = "vmf文件 (*.vmf)|*.vmf",
                InitialDirectory = Globals.L4D2RootPath
            };
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName == string.Empty)
            {
                return;
            }
            else
            {
                Functions.SaveVmfToPath(saveFileDialog.FileName, new List<string> { StandardDict["info_director"].Item2, StandardDict["trigger_finale"].Item2 , StandardDict["ScriptFile"].Item2 }, 0);
            }

            string fileExtension = StandardDict["ScriptFile"].Item2.EndsWith(".nut") ? string.Empty : ".nut";

            YesOrNoWindow yesOrNoWindow = new()
            {
                TextBlockString = $"是否一并导出脚本文件至scripts\\vscripts文件夹?\n\n脚本文件名将为\"{StandardDict["ScriptFile"].Item2}{fileExtension}\"!",
                SendMessage = SaveToNutReceived,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            yesOrNoWindow.ShowDialog();

            if (IsNutConfirmed)
            {
                Functions.SaveNutToPath($"{Globals.L4D2ScriptsPath}\\{StandardDict["ScriptFile"].Item2}", ScriptWindow.Text);
            }

            string navFileName = saveFileDialog.SafeFileName.Replace(".vmf", ".nav");

            YesOrNoWindow yesOrNoWindow2 = new()
            {
                TextBlockString = $"是否一并导出Nav文件至vmf所在文件夹?\n\nNav文件名将为\"{navFileName}\"!",
                SendMessage = SaveToNavReceived,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            yesOrNoWindow2.ShowDialog();

            if (IsNavConfirmed)
            {
                Functions.SaveNavToPath(saveFileDialog.FileName.Replace(".vmf", ".nav"), 0);
            }
        }

        private void CompileVmfClick(object sender, RoutedEventArgs e)
        {
            CompileBlocker.Visibility = Visibility.Visible;
            CompileViewer.Visibility = Visibility.Visible;
            Functions.SaveVmfToPath($"{Globals.L4D2StandardFinalePath}", new List<string> { StandardDict["info_director"].Item2, StandardDict["trigger_finale"].Item2, StandardDict["ScriptFile"].Item2 }, 0);
            Functions.SaveNutToPath($"{Globals.L4D2ScriptsPath}\\standard_finale.nut", ScriptWindow.Text);
            Functions.SaveNavToPath($"{Globals.L4D2MapsPath}\\standard_finale.nav", 0);
            StartNewProcess();
        }

        private void PreviewOfficalScriptClick(object sender, RoutedEventArgs e)
        {
            PreviewScriptWindow previewScriptWindow = new()
            {
                Title = $"正在预览{MapSelectionComboBox.SelectedItem}的救援脚本",
                TextBoxString = Functions.GetOffcialStandardScriptFile(MapSelectionComboBox.SelectedIndex),
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            previewScriptWindow.ShowDialog();
        }

        private void ToggleAllObjects(bool status)
        {
            foreach (var itemName in Globals.StandardObjectList)
            {
                if (itemName.EndsWith("Button"))
                {
                    Button button = (Button)FindName(itemName);
                    button.IsEnabled = status;
                    if (status && (itemName == "SaveAsNutButton" || itemName == "PasteToClipboardButton" || itemName == "TotalWaveButton"))
                    {
                        if ((TotalWaveCount <= 0 || TotalWaveCount == null) && !Functions.IsProperInt(TotalWaveTextBox.Text, 1, 99))
                        {
                            button.IsEnabled = false;
                        }
                    }
                }
                else if (itemName.EndsWith("CheckBox"))
                {
                    CheckBox checkBox = (CheckBox)FindName(itemName);
                    checkBox.IsEnabled = status;
                }
                else if (itemName.EndsWith("TextBox"))
                {
                    TextBox textBox = (TextBox)FindName(itemName);
                    textBox.IsEnabled = status;
                }
                else if (itemName.EndsWith("ComboBox"))
                {
                    ComboBox comboBox = (ComboBox)FindName(itemName);
                    comboBox.IsEnabled = status;
                }
                else if (itemName.EndsWith("Label"))
                {
                    Label label = (Label)FindName(itemName);
                    label.IsEnabled = status;
                }
            }
            IsScriptWindowEnabled = status ? (TotalWaveCount > 0 && TotalWaveCount != null) : null;
            UpdateScriptWindow();
        }

        private void ReadStandardOutputAction(string result)
        {
            CompileTextBox.AppendText($"{result}\r\n");
            CompileViewer.ScrollToEnd();
        }

        private void ProcessOutputHandler(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                Dispatcher.Invoke(ReadStandardOutput, new object[] { e.Data });
            }
        }

        private void ProcessExited(object sender, EventArgs e)
        {
            cts.Cancel();
        }

        private async void StartNewProcess()
        {
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.OutputDataReceived += new DataReceivedEventHandler(ProcessOutputHandler);
            process.EnableRaisingEvents = true;
            process.Exited += new EventHandler(ProcessExited);
            process.Start();
            process.BeginOutputReadLine();
            process.StandardInput.WriteLine(Functions.GetProcessInput(0));
            process.StandardInput.AutoFlush = true;
            try
            {
                await Task.Delay(new TimeSpan(24, 0, 0), cts.Token);
            }
            catch (TaskCanceledException)
            {
                cts.Dispose();
                cts = new CancellationTokenSource();
                process.Dispose();
                process = new Process();
            }
            CompileTextBox.Text = string.Empty;
            CompileBlocker.Visibility = Visibility.Hidden;
            CompileViewer.Visibility = Visibility.Hidden;
            File.Copy($"{Globals.L4D2StandardFinalePath}.bsp", $"{Globals.L4D2MapsPath}\\standard_finale.bsp", true);
            Functions.RunL4D2Game(0);
        }
    }
}
