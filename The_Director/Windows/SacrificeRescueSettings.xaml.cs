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
    public partial class SacrificeRescueSettings : UserControl
    {
        public bool IsNutConfirmed = new();
        public bool IsNavConfirmed = new();
        public bool IsMainScriptWindowActive = true;

        public Dictionary<int, string> TextBoxDicts = new();
        public Dictionary<int, int> ComboBoxDicts = new();
        public Dictionary<string, BooleanString> SacrificeDict = new();

        private delegate void DelegateReadStandardOutput(string result);
        private event DelegateReadStandardOutput ReadStandardOutput;
        private Process process = new();
        private CancellationTokenSource cts = new();

        public void MSGReceived(string value)
        {
            if (value != null)
            {
                SacrificeDict["MSG"] = (true, value);
            }
            else
            {
                MSGCheckBox.IsChecked = false;
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

        public SacrificeRescueSettings()
        {
            InitializeComponent();
            ReadStandardOutput += new DelegateReadStandardOutput(ReadStandardOutputAction);
            PreferredMobDirectionComboBox.ItemsSource = Globals.PreferredMobDirectionList;
            PreferredSpecialDirectionComboBox.ItemsSource = Globals.PreferredSpecialDirectionList;
            MapSelectionComboBox.ItemsSource = Globals.OffcialMapSacrificeRescueList;
            MapSelectionComboBox.SelectedIndex = 0;
            SacrificeDict.Add("MSG", new BooleanString(false, string.Empty));
            SacrificeDict.Add("LockTempo", new BooleanString(false, null));
            SacrificeDict.Add("BuildUpMinInterval", new BooleanString(true, string.Empty));
            SacrificeDict.Add("IntensityRelaxThreshold", new BooleanString(false, string.Empty));
            SacrificeDict.Add("MobRechargeRate", new BooleanString(false, string.Empty));
            SacrificeDict.Add("MobSpawnMaxTime", new BooleanString(false, string.Empty));
            SacrificeDict.Add("MobSpawnMinTime", new BooleanString(false, string.Empty));
            SacrificeDict.Add("MusicDynamicMobScanStopSize", new BooleanString(false, string.Empty));
            SacrificeDict.Add("MusicDynamicMobSpawnSize", new BooleanString(false, string.Empty));
            SacrificeDict.Add("MusicDynamicMobStopSize", new BooleanString(false, string.Empty));
            SacrificeDict.Add("PreferredMobDirection", new BooleanString(false, string.Empty));
            SacrificeDict.Add("RelaxMaxFlowTravel", new BooleanString(false, string.Empty));
            SacrificeDict.Add("RelaxMaxInterval", new BooleanString(false, string.Empty));
            SacrificeDict.Add("RelaxMinInterval", new BooleanString(false, string.Empty));
            SacrificeDict.Add("SustainPeakMaxTime", new BooleanString(false, string.Empty));
            SacrificeDict.Add("SustainPeakMinTime", new BooleanString(false, string.Empty));
            SacrificeDict.Add("PreferredSpecialDirection", new BooleanString(false, string.Empty));
            SacrificeDict.Add("ProhibitBosses", new BooleanString(false, null));
            SacrificeDict.Add("ShouldAllowMobsWithTank", new BooleanString(false, null));
            SacrificeDict.Add("ShouldAllowSpecialsWithTank", new BooleanString(false, null));
            SacrificeDict.Add("EscapeSpawnTanks", new BooleanString(false, null));
            SacrificeDict.Add("SpecialRespawnInterval", new BooleanString(false, string.Empty));
            SacrificeDict.Add("BileMobSize", new BooleanString(false, string.Empty));
            SacrificeDict.Add("BoomerLimit", new BooleanString(false, string.Empty));
            SacrificeDict.Add("ChargerLimit", new BooleanString(false, string.Empty));
            SacrificeDict.Add("CommonLimit", new BooleanString(false, string.Empty));
            SacrificeDict.Add("DominatorLimit", new BooleanString(false, string.Empty));
            SacrificeDict.Add("HunterLimit", new BooleanString(false, string.Empty));
            SacrificeDict.Add("JockeyLimit", new BooleanString(false, string.Empty));
            SacrificeDict.Add("MaxSpecials", new BooleanString(false, string.Empty));
            SacrificeDict.Add("MegaMobSize", new BooleanString(false, string.Empty));
            SacrificeDict.Add("MobMaxPending", new BooleanString(false, string.Empty));
            SacrificeDict.Add("MobMaxSize", new BooleanString(false, string.Empty));
            SacrificeDict.Add("MobMinSize", new BooleanString(false, string.Empty));
            SacrificeDict.Add("MobSpawnSize", new BooleanString(false, string.Empty));
            SacrificeDict.Add("SmokerLimit", new BooleanString(false, string.Empty));
            SacrificeDict.Add("SpitterLimit", new BooleanString(false, string.Empty));
            SacrificeDict.Add("TankLimit", new BooleanString(false, string.Empty));
            SacrificeDict.Add("WitchLimit", new BooleanString(false, string.Empty));
            SacrificeDict.Add("HordeEscapeCommonLimit", new BooleanString(false, string.Empty));
            info_directorTextBox.Text = "director";
            trigger_finaleTextBox.Text = "finale_lever";
            TankLimitTextBox.Text = "4";
            CommonLimitTextBox.Text = "20";
            HordeEscapeCommonLimitTextBox.Text = "15";
        }

        private void TryOpenMSGWindow()
        {
            InputNewText inputNewText = new()
            {
                TextBoxText = SacrificeDict["MSG"].Item2,
                SendMessage = MSGReceived,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            inputNewText.ShowDialog();
            UpdateScriptWindow();
        }

        private void CheckBoxClick(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            var Name = checkBox.Name.Remove(checkBox.Name.Length - 8, 8);
            var IsChecked = (bool)checkBox.IsChecked;

            if (Name != "MSG")
            {
                SacrificeDict[Name] = (IsChecked, null);
            }
            else
            {
                if (!SacrificeDict[Name].Item1 && IsChecked)
                {
                    TryOpenMSGWindow();
                }

                SacrificeDict[Name] = (IsChecked, SacrificeDict[Name].Item2);
            }
            UpdateScriptWindow();
        }

        private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            var Name = textBox.Name.Remove(textBox.Name.Length - 7, 7);
            switch (Globals.TextBoxIndex(Name))
            {
                case 1:
                    if (textBox.Text != string.Empty && !Functions.IsProperFloat(textBox.Text, 0, 1))
                    {
                        Functions.TryOpenMessageWindow(2);
                        textBox.Text = string.Empty;
                    }
                    break;
                case 2:
                    if (textBox.Text != string.Empty && !Functions.IsProperFloat(textBox.Text, 0, float.MaxValue))
                    {
                        Functions.TryOpenMessageWindow(3);
                        textBox.Text = string.Empty;
                    }
                    break;
                case 3:
                    if (textBox.Text != string.Empty && !Functions.IsProperInt(textBox.Text, 0, int.MaxValue))
                    {
                        Functions.TryOpenMessageWindow(4);
                        textBox.Text = string.Empty;
                    }
                    break;
                case 4:
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
                        if (Name == "info_director")
                        {
                            textBox.Text = "director";
                        }
                        else if (Name == "trigger_finale")
                        {
                            textBox.Text = "finale_lever";
                        }
                        return;
                    }
                    break;
                default:
                    break;
            }
            if (Name != "MSG")
            {
                SacrificeDict[Name] = (textBox.Text != string.Empty, textBox.Text);
            }

            UpdateScriptWindow();
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            SacrificeDict[comboBox.Name.Remove(comboBox.Name.Length - 8, 8)] = (comboBox.SelectedItem.ToString() != string.Empty, comboBox.SelectedItem.ToString());
            UpdateScriptWindow();
        }

        private void UpdateScriptWindow()
        {
            StringBuilder ScriptWindowText = new();
            StringBuilder ScriptWindowSecondText = new();

            if ((bool)MSGCheckBox.IsChecked)
            {
                ScriptWindowText.AppendLine($"Msg(\"{SacrificeDict["MSG"].Item2}\");\n");
            }

            ScriptWindowText.AppendLine("ERROR <- -1\nPANIC <- 0\nTANK <- 1\nDELAY <- 2\n\nButtonPressCount <- 1\nLastVOButtonStageNumber <- 0\nPendingWaitAdvance <- true\nQueuedDelayAdvances <- 0\nCurrentFinaleStageNumber <- ERROR\nCurrentFinaleStageType <- ERROR\n\nDirectorOptions <-\n{\n\tA_CustomFinale_StageCount = 8\n\n\tA_CustomFinale1 = PANIC\n\tA_CustomFinaleValue1 = 1\n\tA_CustomFinale2 = TANK\n\tA_CustomFinaleValue2 = 1\n\tA_CustomFinale3 = DELAY\n\tA_CustomFinaleValue3 = 9999\n\tA_CustomFinale4 = PANIC\n\tA_CustomFinaleValue4 = 1\n\tA_CustomFinale5 = TANK\n\tA_CustomFinaleValue5 = 1\n\tA_CustomFinale6 = DELAY\n\tA_CustomFinaleValue6 = 9999\n\tA_CustomFinale7 = TANK\n\tA_CustomFinaleValue7 = 1\n\tA_CustomFinale8 = DELAY\n\tA_CustomFinaleValue8 = 5");

            foreach (var item in SacrificeDict)
            {
                if (item.Value.Item1 && !Globals.SacrificeDictBlackList.Contains(item.Key))
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

            ScriptWindowText.AppendLine("}\n\nfunction OnBeginCustomFinaleStage(num, type)\n{\n\tprintl(\"Beginning custom finale stage \" + num + \" of type \" + type);\n\tprintl(\"PendingWaitAdvance \" + PendingWaitAdvance + \", QueuedDelayAdvances \" + QueuedDelayAdvances);\n\tCurrentFinaleStageNumber = num;\n\tCurrentFinaleStageType = type;\n\tPendingWaitAdvance = false;\n}\n\nfunction GeneratorButtonPressed()\n{\n\tprintl(\"GeneratorButtonPressed finale stage \" + CurrentFinaleStageNumber + \" of type \" +CurrentFinaleStageType);\n\tprintl(\"PendingWaitAdvance \" + PendingWaitAdvance + \", QueuedDelayAdvances \" + QueuedDelayAdvances);\n\tButtonPressCount++;\n\tlocal ImmediateAdvances = 0;\n\tif (CurrentFinaleStageNumber == 1 || CurrentFinaleStageNumber == 4)\n\t{\n\t\tQueuedDelayAdvances++;\n\t\tImmediateAdvances = 1;\n\t}\n\telse if (CurrentFinaleStageNumber == 2 || CurrentFinaleStageNumber == 5)\n\t\tImmediateAdvances = 2;\n\telse if (CurrentFinaleStageNumber == 3 || CurrentFinaleStageNumber == 6)\n\t\tImmediateAdvances = 1;\n\telse if (CurrentFinaleStageNumber == -1 || CurrentFinaleStageNumber == 0)\n\t{\n\t\tQueuedDelayAdvances++;\n\t\tImmediateAdvances = 0;\n\t}\n\telse\n\t\tprintl(\"Unhandled generator button press!\");\n\tif (ImmediateAdvances > 0)\n\t{\n\t\tEntFire(\"finale_lever\", \"Enable\");\n\t\tif (ImmediateAdvances == 1)\n\t\t{\n\t\t\tprintl(\"GeneratorButtonPressed Advancing State ONCE\");\n\t\t\tEntFire(\"finale_lever\", \"AdvanceFinaleState\");\n\t\t}\n\t\telse if (ImmediateAdvances == 2)\n\t\t{\n\t\t\tprintl( \"GeneratorButtonPressed Advancing State TWICE\");\n\t\t\tEntFire(\"finale_lever\", \"AdvanceFinaleState\");\n\t\t\tEntFire(\"finale_lever\", \"AdvanceFinaleState\");\n\t\t}\n\t\tEntFire(\"finale_lever\", \"Disable\");\n\t\tPendingWaitAdvance = true;\n\t}\n}\n\nfunction Update()\n{\n    if (CurrentFinaleStageType == DELAY && QueuedDelayAdvances > 0 && !PendingWaitAdvance)\n\t\tif (!Director.IsTankInPlay() && !Director.IsAnySurvivorInCombat())\n\t\t\tif (Director.GetPendingMobCount() < 1 && Director.GetCommonInfectedCount() < 5)\n\t\t\t{\n\t\t\t\tprintl(\"Update Advancing State finale stage \" + CurrentFinaleStageNumber + \" of type \" +CurrentFinaleStageType);\n\t\t\t\tprintl(\"PendingWaitAdvance \" + PendingWaitAdvance + \", QueuedDelayAdvances \" + QueuedDelayAdvances);\n\t\t\t\tQueuedDelayAdvances--;\n\t\t\t\tEntFire(\"finale_lever\", \"Enable\");\n\t\t\t\tEntFire(\"finale_lever\", \"AdvanceFinaleState\");\n\t\t\t\tEntFire(\"finale_lever\", \"Disable\");\n\t\t\t\tPendingWaitAdvance = true;\n\t\t\t}\n\tif (CurrentFinaleStageType == DELAY && CurrentFinaleStageNumber > 1 && CurrentFinaleStageNumber < 7)\n\t\tif (CurrentFinaleStageNumber != LastVOButtonStageNumber && ButtonPressCount < 3)\n\t\t\tif (QueuedDelayAdvances == 0 && !PendingWaitAdvance)\n\t\t\t\tif (Director.GetPendingMobCount() < 1 && Director.GetCommonInfectedCount() < 1)\n\t\t\t\t\tif (!Director.IsTankInPlay() && !Director.IsAnySurvivorInCombat())\n\t\t\t\t\t{\n\t\t\t\t\t\tprintl(\"Update firing event 1 (VO Prompt)\");\n\t\t\t\t\t\tLastVOButtonStageNumber = CurrentFinaleStageNumber;\n\t\t\t\t\t\tEntFire(\"orator\", \"SpeakResponseConcept\", \"C7M3WaveOver\");\n\t\t\t\t\t}\n}\n\nfunction EnableEscapeTanks()\n{\n\tprintl(\"EnableEscapeTanks finale stage \" + CurrentFinaleStageNumber + \" of type \" +CurrentFinaleStageType);\n\tMapScript.DirectorOptions.EscapeSpawnTanks <- true;\n}");

            ScriptWindow.Text = ScriptWindowText.ToString();

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

            ScriptWindowSecondText.AppendLine("DirectorOptions <-\n{\n\tTankLimit = 0\n\tWitchLimit = 0\n\tProhibitBosses = true\n}\n\nfunction GeneratorButtonPressed()\n{\n\tprintl(\"One generator button has been pressed!\");\n\tEntFire(\"director\", \"EndScript\");\n\tEntFire(\"finale_lever\", \"Enable\");\n\tEntFire(\"finale_lever\", \"ForceFinaleStart\");\n\tEntFire(\"finale_lever\", \"Disable\");\n}");

            ScriptWindowSecond.Text = ScriptWindowSecondText.ToString();
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
            if (IsMainScriptWindowActive)
            {
                Clipboard.SetText(ScriptWindow.Text);
            }
            else
            {
                Clipboard.SetText(ScriptWindowSecond.Text);
            }
            Functions.TryOpenMessageWindow(7);
        }

        private void ChangeButtonClick(object sender, RoutedEventArgs e)
        {
            if (IsMainScriptWindowActive)
            {
                ChangeButton.Content = "切换至主脚本";
                BorderMain.Visibility = Visibility.Hidden;
                BorderSecond.Visibility = Visibility.Visible;
            }
            else
            {
                ChangeButton.Content = "切换至副脚本";
                BorderMain.Visibility = Visibility.Visible;
                BorderSecond.Visibility = Visibility.Hidden;
            }
            IsMainScriptWindowActive = !IsMainScriptWindowActive;
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
                if (IsMainScriptWindowActive)
                {
                    Functions.SaveNutToPath(saveFileDialog.FileName, ScriptWindow.Text);
                }
                else
                {
                    Functions.SaveNutToPath(saveFileDialog.FileName, ScriptWindowSecond.Text);
                }
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

            if (saveFileDialog.FileName == string.Empty)
            {
                return;
            }
            else
            {
                Functions.SaveVmfToPath(saveFileDialog.FileName, new List<string> { SacrificeDict["info_director"].Item2, SacrificeDict["trigger_finale"].Item2 }, 3);
            }

            string scriptFileName = saveFileDialog.SafeFileName.Replace(".vmf", ".nut");
            string scriptFileName2 = saveFileDialog.SafeFileName.Replace(".vmf", "_finale.nut");

            YesOrNoWindow yesOrNoWindow = new()
            {
                TextBlockString = $"是否一并导出脚本文件至scripts\\vscripts文件夹?\n脚本文件名将为\"{scriptFileName}\"和\"{scriptFileName2}\"!",
                SendMessage = SaveToNutReceived,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            yesOrNoWindow.ShowDialog();

            if (IsNutConfirmed)
            {
                Functions.SaveNutToPath($"{Globals.L4D2ScriptsPath}\\{scriptFileName}", ScriptWindowSecond.Text);
                Functions.SaveNutToPath($"{Globals.L4D2ScriptsPath}\\{scriptFileName2}", ScriptWindow.Text);
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
                Functions.SaveNavToPath(saveFileDialog.FileName.Replace(".vmf", ".nav"), 3);
            }
        }

        private void CompileVmfClick(object sender, RoutedEventArgs e)
        {
            CompileBlocker.Visibility = Visibility.Visible;
            CompileViewer.Visibility = Visibility.Visible;
            Functions.SaveVmfToPath($"{Globals.L4D2SacrificeFinalePath}", new List<string> { SacrificeDict["info_director"].Item2, SacrificeDict["trigger_finale"].Item2 }, 3);
            Functions.SaveNutToPath($"{Globals.L4D2ScriptsPath}\\sacrifice_finale.nut", ScriptWindow.Text);
            Functions.SaveNutToPath($"{Globals.L4D2ScriptsPath}\\sacrifice_finale_finale.nut", ScriptWindowSecond.Text);
            Functions.SaveNavToPath($"{Globals.L4D2MapsPath}\\sacrifice_finale.nav", 3);
            StartNewProcess();
        }

        private void PreviewOfficalScriptClick(object sender, RoutedEventArgs e)
        {
            PreviewScriptWindow previewScriptWindow = new()
            {
                Title = $"正在预览{MapSelectionComboBox.SelectedItem}的救援脚本",
                TextBoxString = Functions.GetOffcialSacrificeScriptFile(),
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            previewScriptWindow.ShowDialog();
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
            process.StandardInput.WriteLine(Functions.GetProcessInput(3));
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
            File.Copy($"{Globals.L4D2SacrificeFinalePath}.bsp", $"{Globals.L4D2MapsPath}\\sacrifice_finale.bsp", true);
            Functions.RunL4D2Game(3);
        }
    }
}