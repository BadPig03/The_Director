using Microsoft.Win32;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
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

        public List<string> VmfValuesList = new() { "director", "finale_lever", "scavenge_delay", "12", "10", "20", "1", "2", "1", "3", "4" };

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
            PreferredMobDirectionComboBox.ItemsSource = Globals.PreferredMobDirectionList;
            PreferredSpecialDirectionComboBox.ItemsSource = Globals.PreferredSpecialDirectionList;
            MapSelectionComboBox.ItemsSource = Globals.OffcialMapScavengeRescueList;
            MapSelectionComboBox.SelectedIndex = 0;
            ScavengeDict.Add("MSG", new BooleanString(false, string.Empty));
            ScavengeDict.Add("ShowProgress", new BooleanString(false, null));
            ScavengeDict.Add("LockTempo", new BooleanString(false, null));
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
            ScavengeDict.Add("MinimumStageTime", new BooleanString(false, string.Empty));
            ScavengeDict.Add("PreferredSpecialDirection", new BooleanString(false, string.Empty));
            ScavengeDict.Add("ProhibitBosses", new BooleanString(false, null));
            ScavengeDict.Add("ShouldAllowMobsWithTank", new BooleanString(false, null));
            ScavengeDict.Add("ShouldAllowSpecialsWithTank", new BooleanString(false, null));
            ScavengeDict.Add("EscapeSpawnTanks", new BooleanString(true, null));
            ScavengeDict.Add("SpecialRespawnInterval", new BooleanString(false, string.Empty));
            ScavengeDict.Add("SustainPeakMaxTime", new BooleanString(false, string.Empty));
            ScavengeDict.Add("SustainPeakMinTime", new BooleanString(false, string.Empty));
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

            info_directorTextBox.Text = VmfValuesList[0];
            trigger_finaleTextBox.Text = VmfValuesList[1];
            DelayScriptTextBox.Text = VmfValuesList[2];
            CansNeededTextBox.Text = VmfValuesList[3];
            DelayMinTextBox.Text = VmfValuesList[4];
            DelayMaxTextBox.Text = VmfValuesList[5];
            DelayPourThreTextBox.Text = VmfValuesList[6];
            DelayBothThreTextBox.Text = VmfValuesList[7];
            AbortMinTextBox.Text = VmfValuesList[8];
            AbortMaxTextBox.Text = VmfValuesList[9];
            CansBothThreTextBox.Text = VmfValuesList[10];
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
            switch (Functions.TextBoxIndex(Name))
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
                    if (textBox.Text != string.Empty && !Name.Contains(" "))
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
                    if (Name == "info_director")
                    {
                        VmfValuesList[0] = textBox.Text;
                    }
                    else if (Name == "trigger_finale")
                    {
                        VmfValuesList[1] = textBox.Text;
                    }
                    else if (Name == "DelayScript")
                    {
                        VmfValuesList[2] = textBox.Text;
                    }

                    break;
                case 6:
                    if (textBox.Text == string.Empty || !Functions.IsProperInt(textBox.Text, 0, int.MaxValue))
                    {
                        Functions.TryOpenMessageWindow(4);
                        if (Name == "CansNeeded")
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
                        else if (Name == "DelayPourThre")
                        {
                            textBox.Text = "1";
                        }
                        else if (Name == "DelayBothThre")
                        {
                            textBox.Text = "2";
                        }
                        else if (Name == "AbortMin")
                        {
                            textBox.Text = "1";
                        }
                        else if (Name == "AbortMax")
                        {
                            textBox.Text = "3";
                        }
                        else if (Name == "CansBothThre")
                        {
                            textBox.Text = "4";
                        }

                        return;
                    }
                    if (Name == "CansNeeded")
                    {
                        VmfValuesList[3] = textBox.Text;
                    }
                    else if (Name == "DelayMin")
                    {
                        VmfValuesList[4] = textBox.Text;
                    }
                    else if (Name == "DelayMax")
                    {
                        VmfValuesList[5] = textBox.Text;
                    }
                    else if (Name == "DelayPourThre")
                    {
                        VmfValuesList[6] = textBox.Text;
                    }
                    else if (Name == "DelayBothThre")
                    {
                        VmfValuesList[7] = textBox.Text;
                    }
                    else if (Name == "AbortMin")
                    {
                        VmfValuesList[8] = textBox.Text;
                    }
                    else if (Name == "AbortMax")
                    {
                        VmfValuesList[9] = textBox.Text;
                    }
                    else if (Name == "CansBothThre")
                    {
                        VmfValuesList[10] = textBox.Text;
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

            ScriptWindowText.AppendLine($"PANIC <- 0\nTANK <- 1\nDELAY <- 2\nSCRIPTED <- 3\nDelayScript <- \"{VmfValuesList[2]}\"\n\nDirectorOptions <-\n{{");

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
            ScriptWindowText.AppendLine($"CansNeeded <- {VmfValuesList[3]}\nDelayMin <- {VmfValuesList[4]}\nDelayMax <- {VmfValuesList[5]}\nDelayPourThre <- {VmfValuesList[6]}\nDelayBothThre <- {VmfValuesList[7]}\nAbortMin <- {VmfValuesList[8]}\nAbortMax <- {VmfValuesList[9]}\nCansBothThre <- {VmfValuesList[10]}\n");
            ScriptWindowText.AppendLine("GasCansTouched <- 0\nGasCansPoured <- 0\nDelayTouchedOrPoured <- 0\nDelayPoured <- 0\n\nEntFire(\"gascan_progress\", \"SetTotalItems\", CansNeeded);\nEntFire(\"timer_delay_end\", \"LowerRandomBound\", DelayMin);\nEntFire(\"timer_delay_end\", \"UpperRandomBound\", DelayMax);\nEntFire(\"timer_delay_abort\", \"LowerRandomBound\", AbortMin);\nEntFire(\"timer_delay_abort\", \"UpperRandomBound\", AbortMax);\n\nfunction AbortDelay(){}\nfunction EndDelay(){}\n\nNavMesh.UnblockRescueVehicleNav();\n\nfunction GasCanTouched()\n{\n\tGasCansTouched++;\n\tEvalGasCansPouredOrTouched();\n}\n\nfunction GasCanPoured()\n{\n\tGasCansPoured++;\n\tDelayPoured++;\n\tEntFire(\"finale_elevator\", \"SetPosition\", 1.0 *  GasCansPoured / CansNeeded);\n\tif (GasCansPoured == CansNeeded)\n\t{");

            if (ScavengeDict["ShowProgress"].Item1)
            {
                ScriptWindowText.AppendLine("\t\tprintl(\"Gas Cans Needed: \" + GasCansPoured);");
            }

            ScriptWindowText.AppendLine("\t\tEntFire(\"finale_elevator\", \"SetSpeed\", 14.4);\n\t\tEntFire(\"relay_rescue_ready\", \"Enable\");\n\t}\n\tEvalGasCansPouredOrTouched();\n}\n\nfunction EvalGasCansPouredOrTouched()\n{\n\tlocal TouchedOrPoured = GasCansPoured + GasCansTouched;\n\tDelayTouchedOrPoured++;\n\tif ((DelayTouchedOrPoured >= DelayBothThre) || (DelayPoured >= DelayPourThre))\n\t\tAbortDelay();\n\tif (TouchedOrPoured == CansBothThre)\n\t\tEntFire(\"director\", \"EndCustomScriptedStage\");\n}");

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
                TextBoxString = Functions.GetButtonString(label.Content.ToString()),
                HyperlinkUri = Functions.GetButtonHyperlinkUri(label.Content.ToString()),
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
                Functions.SaveVmfToPath(saveFileDialog.FileName, VmfValuesList, 1);
            }

            string fileExtension = VmfValuesList[2].EndsWith(".nut") ? string.Empty : ".nut";
            string scriptFileName = saveFileDialog.SafeFileName.Replace(".vmf", ".nut");


            YesOrNoWindow yesOrNoWindow = new()
            {
                TextBlockString = $"是否一并导出两个脚本文件至scripts\\vscripts文件夹?\n\n脚本文件名将为\"{scriptFileName}\"和\"{VmfValuesList[2]}{fileExtension}\"!",
                SendMessage = SaveToNutReceived,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            yesOrNoWindow.ShowDialog();

            if (IsNutConfirmed)
            {
                Functions.SaveNutToPath($"{Globals.L4D2ScriptsPath}\\{scriptFileName}", ScriptWindow.Text);
                Functions.SaveNutToPath($"{Globals.L4D2ScriptsPath}\\{VmfValuesList[2]}", ScriptWindowSecond.Text);
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
                Functions.SaveNavToPath(saveFileDialog.FileName.Replace(".vmf", ".nav"));
            }
        }

        private void CompileVmfClick(object sender, RoutedEventArgs e)
        {
            Functions.SaveVmfToPath($"{Globals.L4D2StandardFinalePath}", VmfValuesList, 1);
            Functions.SaveNutToPath($"{Globals.L4D2ScriptsPath}\\scavange_finale_finale.nut", ScriptWindow.Text);
            Functions.SaveNutToPath($"{Globals.L4D2ScriptsPath}\\scavange_delay.nut", ScriptWindowSecond.Text);
            Functions.SaveNavToPath($"{Globals.L4D2MapsPath}\\scavange_finale.nav");
            if (Functions.GenerateNewProcess(0))
            {
                if (Functions.GenerateNewProcess(1))
                {
                    if (Functions.GenerateNewProcess(2))
                    {
                        File.Copy($"{Globals.L4D2StandardFinalePath}.bsp", $"{Globals.L4D2MapsPath}\\scavange_finale.bsp", true);
                        Functions.RunL4D2Game();
                    }
                }
            }
        }

        private void PreviewOfficalScriptClick(object sender, RoutedEventArgs e)
        {
            PreviewScriptWindow previewScriptWindow = new()
            {
                Title = $"正在预览{MapSelectionComboBox.SelectedItem}的救援脚本",
                TextBoxString = Functions.GetOffcialScavengeScriptFile(MapSelectionComboBox.SelectedIndex),
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            previewScriptWindow.ShowDialog();
        }
    }
}