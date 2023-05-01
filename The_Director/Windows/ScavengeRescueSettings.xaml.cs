﻿using Microsoft.Win32;
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
    public partial class ScavengeRescueSettings : UserControl
    {
        public bool IsNutConfirmed = new();
        public bool IsNavConfirmed = new();
        public bool IsMainScriptWindowActive = true;

        public Dictionary<int, string> TextBoxDicts = new();
        public Dictionary<int, int> ComboBoxDicts = new();
        public Dictionary<string, BooleanString> ScavengeDict = new();

        private delegate void DelegateReadStandardOutput(string result);
        private event DelegateReadStandardOutput ReadStandardOutput;
        private Process process = new();
        private CancellationTokenSource cts = new();

        public void MSGReceived(string value)
        {
            if (value != null)
            {
                ScavengeDict["MSG"] = (true, value);
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

        public ScavengeRescueSettings()
        {
            InitializeComponent();
            ReadStandardOutput += new DelegateReadStandardOutput(ReadStandardOutputAction);
            PreferredMobDirectionComboBox.ItemsSource = Globals.PreferredMobDirectionList;
            PreferredSpecialDirectionComboBox.ItemsSource = Globals.PreferredSpecialDirectionList;
            MapSelectionComboBox.ItemsSource = Globals.OfficialMapScavengeRescueList;
            MapSelectionComboBox.SelectedIndex = 0;
            ScavengeDict.Add("MSG", new BooleanString(false, string.Empty));
            ScavengeDict.Add("ShowProgress", new BooleanString(false, null));
            ScavengeDict.Add("info_director", new BooleanString(false, string.Empty));
            ScavengeDict.Add("trigger_finale", new BooleanString(false, string.Empty));
            ScavengeDict.Add("DelayScript", new BooleanString(false, string.Empty));
            ScavengeDict.Add("GasCansNeeded", new BooleanString(false, string.Empty));
            ScavengeDict.Add("DelayMin", new BooleanString(false, string.Empty));
            ScavengeDict.Add("DelayMax", new BooleanString(false, string.Empty));
            ScavengeDict.Add("DelayPouredThreshold", new BooleanString(false, string.Empty));
            ScavengeDict.Add("DelayPouredOrTouchedThreshold", new BooleanString(false, string.Empty));
            ScavengeDict.Add("AbortDelayMin", new BooleanString(false, string.Empty));
            ScavengeDict.Add("AbortDelayMax", new BooleanString(false, string.Empty));
            ScavengeDict.Add("GasCansDividend", new BooleanString(false, string.Empty));
            ScavengeDict.Add("LockTempo", new BooleanString(false, null));
            ScavengeDict.Add("BuildUpMinInterval", new BooleanString(false, string.Empty));
            ScavengeDict.Add("IntensityRelaxThreshold", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MobRechargeRate", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MobSpawnMaxTime", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MobSpawnMinTime", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MusicDynamicMobScanStopSize", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MusicDynamicMobSpawnSize", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MusicDynamicMobStopSize", new BooleanString(false, string.Empty));
            ScavengeDict.Add("PreferredMobDirection", new BooleanString(false, string.Empty));
            ScavengeDict.Add("RelaxMaxFlowTravel", new BooleanString(false, string.Empty));
            ScavengeDict.Add("RelaxMaxInterval", new BooleanString(false, string.Empty));
            ScavengeDict.Add("RelaxMinInterval", new BooleanString(false, string.Empty));
            ScavengeDict.Add("SustainPeakMaxTime", new BooleanString(false, string.Empty));
            ScavengeDict.Add("SustainPeakMinTime", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MinimumStageTime", new BooleanString(false, string.Empty));
            ScavengeDict.Add("PreferredSpecialDirection", new BooleanString(false, string.Empty));
            ScavengeDict.Add("ProhibitBosses", new BooleanString(false, null));
            ScavengeDict.Add("ShouldAllowMobsWithTank", new BooleanString(false, null));
            ScavengeDict.Add("ShouldAllowSpecialsWithTank", new BooleanString(false, null));
            ScavengeDict.Add("EscapeSpawnTanks", new BooleanString(true, null));
            ScavengeDict.Add("SpecialRespawnInterval", new BooleanString(false, string.Empty));
            ScavengeDict.Add("BileMobSize", new BooleanString(false, string.Empty));
            ScavengeDict.Add("BoomerLimit", new BooleanString(false, string.Empty));
            ScavengeDict.Add("ChargerLimit", new BooleanString(false, string.Empty));
            ScavengeDict.Add("CommonLimit", new BooleanString(false, string.Empty));
            ScavengeDict.Add("DominatorLimit", new BooleanString(false, string.Empty));
            ScavengeDict.Add("HunterLimit", new BooleanString(false, string.Empty));
            ScavengeDict.Add("JockeyLimit", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MaxSpecials", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MegaMobSize", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MobMaxPending", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MobMaxSize", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MobMinSize", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MobSpawnSize", new BooleanString(false, string.Empty));
            ScavengeDict.Add("SmokerLimit", new BooleanString(false, string.Empty));
            ScavengeDict.Add("SpitterLimit", new BooleanString(false, string.Empty));
            ScavengeDict.Add("TankLimit", new BooleanString(false, string.Empty));
            ScavengeDict.Add("WitchLimit", new BooleanString(false, string.Empty));
            ScavengeDict.Add("HordeEscapeCommonLimit", new BooleanString(false, string.Empty));
            info_directorTextBox.Text = "director";
            trigger_finaleTextBox.Text = "finale_lever";
            DelayScriptTextBox.Text = "scavenge_delay";
            GasCansNeededTextBox.Text = "12";
            DelayMinTextBox.Text = "10";
            DelayMaxTextBox.Text = "20";
            DelayPouredThresholdTextBox.Text = "1";
            DelayPouredOrTouchedThresholdTextBox.Text = "2";
            AbortDelayMinTextBox.Text = "1";
            AbortDelayMaxTextBox.Text = "3";
            GasCansDividendTextBox.Text = "4";
            EscapeSpawnTanksCheckBox.IsChecked = true;
        }

        private void TryOpenMSGWindow()
        {
            InputNewText inputNewText = new()
            {
                TextBoxText = ScavengeDict["MSG"].Item2,
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
                ScavengeDict[Name] = (IsChecked, null);
            }
            else
            {
                if (!ScavengeDict[Name].Item1 && IsChecked)
                {
                    TryOpenMSGWindow();
                }

                ScavengeDict[Name] = (IsChecked, ScavengeDict[Name].Item2);
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
                        else if (Name == "DelayScript")
                        {
                            textBox.Text = "scavenge_delay";
                        }
                        return;
                    }
                    break;
                case 6:
                    if (textBox.Text == string.Empty || !Functions.IsProperString(textBox.Text))
                    {
                        Functions.TryOpenMessageWindow(6);

                    }
                    else if (textBox.Text != string.Empty && !Functions.IsProperInt(textBox.Text, 0, int.MaxValue))
                    {
                        Functions.TryOpenMessageWindow(4);
                    }
                    else
                    {
                        break;
                    }
                    if (Name == "GasCansNeeded")
                    {
                        textBox.Text = "12";
                    }
                    else if (Name == "DelayMin")
                    {
                        textBox.Text = "10";
                    }
                    else if (Name == "DelayMax")
                    {
                        textBox.Text = "20";
                    }
                    else if (Name == "DelayPouredThreshold")
                    {
                        textBox.Text = "1";
                    }
                    else if (Name == "DelayPouredOrTouchedThreshold")
                    {
                        textBox.Text = "2";
                    }
                    else if (Name == "AbortDelayMin")
                    {
                        textBox.Text = "1";
                    }
                    else if (Name == "AbortDelayMax")
                    {
                        textBox.Text = "3";
                    }
                    else if (Name == "GasCansDividend")
                    {
                        textBox.Text = "4";
                    }
                    break;
                default:
                    break;
            }
            if (Name != "MSG")
            {
                ScavengeDict[Name] = (textBox.Text != string.Empty, textBox.Text);
            }

            UpdateScriptWindow();
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ScavengeDict[comboBox.Name.Remove(comboBox.Name.Length - 8, 8)] = (comboBox.SelectedItem.ToString() != string.Empty, comboBox.SelectedItem.ToString());
            UpdateScriptWindow();
        }

        private void UpdateScriptWindow()
        {
            StringBuilder ScriptWindowText = new();
            StringBuilder ScriptWindowSecondText = new();

            if ((bool)MSGCheckBox.IsChecked)
            {
                ScriptWindowText.AppendLine($"Msg(\"{ScavengeDict["MSG"].Item2}\");\n");
            }

            ScriptWindowText.AppendLine($"PANIC <- 0\nTANK <- 1\nDELAY <- 2\nSCRIPTED <- 3\nDelayScript <- \"{ScavengeDict["DelayScript"].Item2}\"\n\nDirectorOptions <-\n{{");

            for (int i = 1; i <= 31; i++)
            {
                ScriptWindowText.AppendLine($"\tA_CustomFinale{i} = SCRIPTED");
                ScriptWindowText.AppendLine($"\tA_CustomFinaleValue{i} = DelayScript\n");
                if (Globals.TankIndexList.Contains(i+1))
                {
                    ScriptWindowText.AppendLine($"\tA_CustomFinale{++i} = TANK");
                }
                else
                {
                    ScriptWindowText.AppendLine($"\tA_CustomFinale{++i} = PANIC");   
                }
                ScriptWindowText.AppendLine($"\tA_CustomFinaleValue{i} = 1\n");
            }

            foreach (var item in ScavengeDict)
            {
                if (item.Value.Item1 && !Globals.ScavengeDictBlackList.Contains(item.Key))
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

            ScriptWindowText.AppendLine("}\n\n//-----------------------------------------------------\n");
            ScriptWindowText.AppendLine($"GasCansNeeded <- {ScavengeDict["GasCansNeeded"].Item2}\nDelayMin <- {ScavengeDict["DelayMin"].Item2}\nDelayMax <- {ScavengeDict["DelayMax"].Item2}\nDelayPouredThreshold <- {ScavengeDict["DelayPouredThreshold"].Item2}\nDelayPouredOrTouchedThreshold <- {ScavengeDict["DelayPouredOrTouchedThreshold"].Item2}\nAbortDelayMin <- {ScavengeDict["AbortDelayMin"].Item2}\nAbortDelayMax <- {ScavengeDict["AbortDelayMax"].Item2}\nGasCansDividend <- {ScavengeDict["GasCansDividend"].Item2}\n");
            ScriptWindowText.AppendLine("GasCansTouched <- 0\nGasCansPoured <- 0\nDelayTouchedOrPoured <- 0\nDelayPoured <- 0\n\nEntFire(\"gascan_progress\", \"SetTotalItems\", GasCansNeeded);\nEntFire(\"timer_delay_end\", \"LowerRandomBound\", DelayMin);\nEntFire(\"timer_delay_end\", \"UpperRandomBound\", DelayMax);\nEntFire(\"timer_delay_abort\", \"LowerRandomBound\", AbortDelayMin);\nEntFire(\"timer_delay_abort\", \"UpperRandomBound\", AbortDelayMax);\n\nfunction AbortDelay(){}\nfunction EndDelay(){}\n\nNavMesh.UnblockRescueVehicleNav();\n\nfunction GasCanTouched()\n{\n\tGasCansTouched++;\n\tEvalGasCansPouredOrTouched();\n}\n\nfunction GasCanPoured()\n{\n\tGasCansPoured++;\n\tDelayPoured++;\n\tEntFire(\"finale_elevator\", \"SetPosition\", 1.0 *  GasCansPoured / GasCansNeeded);\n\tif (GasCansPoured == GasCansNeeded)\n\t{");

            if (ScavengeDict["ShowProgress"].Item1)
            {
                ScriptWindowText.AppendLine("\t\tprintl(\"Gas Cans Needed: \" + GasCansPoured);");
            }

            ScriptWindowText.AppendLine("\t\tEntFire(\"finale_elevator\", \"SetSpeed\", 14.4);\n\t\tEntFire(\"relay_rescue_ready\", \"Enable\");\n\t\tEntFire(\"gascan_progress\", \"TurnOff\", 1);\n\t}\n\tEvalGasCansPouredOrTouched();\n}\n\nfunction EvalGasCansPouredOrTouched()\n{\n\tlocal TouchedOrPoured = GasCansPoured + GasCansTouched;\n\tDelayTouchedOrPoured++;\n\tif ((DelayTouchedOrPoured >= DelayPouredOrTouchedThreshold) || (DelayPoured >= DelayPouredThreshold))\n\t\tAbortDelay();\n\tif (TouchedOrPoured % GasCansDividend == 0)\n\t\tEntFire(\"director\", \"EndCustomScriptedStage\");\n}");

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

            ScriptWindowSecondText.AppendLine("Msg(\"**Delay Started**\\n\");\n\nDirectorOptions <-\n{\n\tMobMinSize = 2\n\tMobMaxSize = 3\n\n\tBoomerLimit = 0\n\tSmokerLimit = 0\n\tHunterLimit = 0\n\tSpitterLimit = 0\n\tJockeyLimit = 0\n\tChargerLimit = 0\n\n\tMinimumStageTime = 15\n\n\tCommonLimit = 5\n}\n\nDirector.ResetMobTimer();\n\nEntFire(\"timer_delay_end\", \"Enable\");\n\nDelayTouchedOrPoured <- 0\nDelayPoured <- 0\n\nfunction AbortDelay()\n{\n\tEntFire(\"timer_delay_abort\", \"Enable\");\n}\n\nfunction EndDelay()\n{\n\tEntFire(\"timer_delay_end\", \"Disable\");\n\tEntFire(\"timer_delay_end\", \"ResetTimer\");\n\tEntFire(\"timer_delay_abort\", \"Disable\");\n\tEntFire(\"timer_delay_abort\", \"ResetTimer\");\n\tEntFire(\"director\", \"EndCustomScriptedStage\");\n}");

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
            if(IsMainScriptWindowActive) 
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
            if(IsMainScriptWindowActive)
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
                if(IsMainScriptWindowActive)
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
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName == string.Empty)
            {
                return;
            }
            else
            {
                Functions.SaveVmfToPath(saveFileDialog.FileName, new List<string> { ScavengeDict["info_director"].Item2, ScavengeDict["trigger_finale"].Item2 }, 1);
            }

            string fileExtension = ScavengeDict["DelayScript"].Item2.EndsWith(".nut") ? string.Empty : ".nut";
            string scriptFileName = saveFileDialog.SafeFileName.Replace(".vmf", ".nut");

            YesOrNoWindow yesOrNoWindow = new()
            {
                TextBlockString = $"是否一并导出两个脚本文件至scripts\\vscripts文件夹?\n\n脚本文件名将为\"{scriptFileName}\"和\"{ScavengeDict["DelayScript"].Item2}{fileExtension}\"!",
                SendMessage = SaveToNutReceived,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            yesOrNoWindow.ShowDialog();

            if (IsNutConfirmed)
            {
                Functions.SaveNutToPath($"{Globals.L4D2ScriptsPath}\\{scriptFileName}", ScriptWindow.Text);
                Functions.SaveNutToPath($"{Globals.L4D2ScriptsPath}\\{ScavengeDict["DelayScript"].Item2}", ScriptWindowSecond.Text);
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
                Functions.SaveNavToPath(saveFileDialog.FileName.Replace(".vmf", ".nav"), 1);
            }
        }

        private void CompileVmfClick(object sender, RoutedEventArgs e)
        {
            CompileBlocker.Visibility = Visibility.Visible;
            CompileViewer.Visibility = Visibility.Visible;
            Functions.SaveVmfToPath($"{Globals.L4D2ScavengeFinalePath}", new List<string> { ScavengeDict["info_director"].Item2, ScavengeDict["trigger_finale"].Item2 }, 1);
            Functions.SaveNutToPath($"{Globals.L4D2ScriptsPath}\\scavenge_finale_finale.nut", ScriptWindow.Text);
            Functions.SaveNutToPath($"{Globals.L4D2ScriptsPath}\\{ScavengeDict["DelayScript"].Item2}.nut", ScriptWindowSecond.Text);
            Functions.SaveNavToPath($"{Globals.L4D2MapsPath}\\scavenge_finale.nav", 1);
            StartNewProcess();
        }

        private void PreviewOfficialScriptClick(object sender, RoutedEventArgs e)
        {
            PreviewScriptWindow previewScriptWindow = new()
            {
                Title = $"正在预览{MapSelectionComboBox.SelectedItem}的救援脚本",
                TextBoxString = Functions.GetOfficialScavengeScriptFile(MapSelectionComboBox.SelectedIndex),
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
            process.StandardInput.WriteLine(Functions.GetProcessInput(1));
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
            File.Copy($"{Globals.L4D2ScavengeFinalePath}.bsp", $"{Globals.L4D2MapsPath}\\scavenge_finale.bsp", true);
            Functions.RunL4D2Game(1);
        }
    }
}