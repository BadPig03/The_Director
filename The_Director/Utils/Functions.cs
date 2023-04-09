using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using The_Director.Windows;

namespace The_Director.Utils;

public static class Functions
{
    public static string GetRandomString()
    {
        byte[] b = new byte[4];
        new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
        Random random = new(BitConverter.ToInt32(b, 0));
        string result = null, temp = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        for (int i = 0; i < 16; i++)
        {
            result += temp.Substring(random.Next(0, temp.Length - 1), 1);
        }

        return result;
    } 

    public static bool IsProperInt(string value, int min, int max)
    {
        if (value == string.Empty)
        {
            return false;
        }

        if (!Regex.IsMatch(value, @"^\-?(\d+)?$"))
        {
            return false;
        }

        if (value == "-")
        {
            value = "-1";
        }

        if (int.Parse(value) <= max && int.Parse(value) >= min)
        {
            return true;
        }

        return false;
    }

    public static bool IsProperFloat(string value, float min, float max)
    {
        if (value == string.Empty)
        {
            return false;
        }

        if (value == ".")
        {
            value = "0.0";
        }

        if (Regex.IsMatch(value, @"^\d+\.$"))
        {
            value = value.Insert(value.Length, "0");
        }

        if (Regex.IsMatch(value, @"^\.\d+$"))
        {
            value = value.Insert(0, "0");
        }

        if (!Regex.IsMatch(value, @"^\d+(\.\d+)?$"))
        {
            return false;
        }

        if (float.Parse(value) <= max && float.Parse(value) >= min)
        {
            return true;
        }

        return false;
    }

    public static bool IsProperString(string value)
    {
        if (value == string.Empty)
        {
            return true;
        }

        if (value.Contains("!") || value.Contains("*") || value.Contains("\""))
        {
            return false;
        }

        return true;
    }

    public static int ConvertToInt(string value)
    {
        if (int.TryParse(value, out _))
        {
            return int.Parse(value);
        }
        else
        {
            return -1;
        }
    }

    public static string TotalWaveToString(int value)
    {
        return value switch
        {
            0 => "PANIC",
            1 => "TANK",
            2 => "DELAY",
            3 => "SCRIPTED",
            _ => "",
        };
    }

    public static int TotalWaveToInt(string value)
    {
        return value switch
        {
            "PANIC" => 0,
            "TANK" => 1,
            "DELAY" => 2,
            "SCRIPTED" => 3,
            _ => -1,
        };
    }

    public static void TryOpenMessageWindow(int type, bool flag = false)
    {
        var Title = string.Empty;
        var TextBoxString = string.Empty;

        if(!flag)
        {
            switch (type)
            { 
                case -1:
                    Title = "错误";
                    TextBoxString = "未运行Steam!\n请运行Steam后启动本软件!";
                    break;
                case 0:
                    Title = "警告";
                    TextBoxString = "未设置尸潮阶段数!";
                    break;
                case 1:
                    Title = "错误";
                    TextBoxString = "非法输入!\n只能输入1到99的整数!";
                    break;
                case 2:
                    Title = "错误";
                    TextBoxString = "非法输入!\n只能输入0到1的浮点数!";
                    break;
                case 3:
                    Title = "错误";
                    TextBoxString = "非法输入!\n只能输入非负浮点数!";
                    break;
                case 4:
                    Title = "错误";
                    TextBoxString = "非法输入!\n只能输入非负整数!";
                    break;
                case 5:
                    Title = "错误";
                    TextBoxString = "非法输入!\n只能输入大于等于-1的整数!";
                    break;
                case 6:
                    Title = "错误";
                    TextBoxString = "非法输入!\n不能输入!、*或\"符号或为空!";
                    break;
                case 7:
                    Title = "提示";
                    TextBoxString = "已成功复制至粘贴板!";
                    break;
                case 8:
                    Title = "错误";
                    TextBoxString = "导出时出错!";
                    break;
                default:
                    break;
            }
        }
        else
        {
            Title = "错误";
            TextBoxString = $"阶段{type + 1}未指定对应数据!";
        }

        MessageWindow messageWindow = new()
        {
            Title = Title,
            TextBoxString = TextBoxString,
            Owner = Application.Current.MainWindow,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };
        messageWindow.ShowDialog();
    }

    public static bool TryOpenCompileWindow(int num)
    {
        CompileWindow compileWindow = new ()
        {
            Num = num,
            Owner = Application.Current.MainWindow,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };
        compileWindow.ShowDialog();
        return true;
    }

    public static string GetProcessInput(int num)
    {
        string path = num switch
        {
            0 => Globals.L4D2StandardFinalePath,
            1 => Globals.L4D2ScavengeFinalePath,
            2 => Globals.L4D2GauntletFinalePath,
            _ => string.Empty
        };

        return $"\"{Globals.L4D2VBSPPath}\" -game \"{Globals.L4D2GameInfoPath}\" \"{path}.vmf\"" + $"&\"{Globals.L4D2VVISPath}\" -game \"{Globals.L4D2GameInfoPath}\" \"{path}.bsp\"" + $"&\"{Globals.L4D2VRADPath}\" -game \"{Globals.L4D2GameInfoPath}\" -both -StaticPropLighting -StaticPropPolys \"{path}.bsp\"&exit";
    }

    public static void RunL4D2Game(int type)
    {
        string command = type switch
        { 
            0 => "-steam -novid +sv_cheats 1 +director_debug 1 +map standard_finale",
            1 => "-steam -novid +sv_cheats 1 +director_debug 1 +map scavenge_finale",
            2 => "-steam -novid +sv_cheats 1 +director_debug 1 +map gauntlet_finale",
            _ => string.Empty
        };

        Process process = new();
        process.StartInfo.FileName = $"{Globals.L4D2RootPath}\\left4dead2.exe";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.Arguments = command;
        process.Start();
        process.WaitForExit();
        process.Close();
    }

    public static string GetOffcialStandardScriptFile(int index)
    {
        return index switch
        {
            0 => Properties.Resources.c2m5_concert_finale,
            1 => Properties.Resources.c3m4_plantation_finale,
            2 => Properties.Resources.c4m5_milltown_escape_finale,
            3 => Properties.Resources.c8m5_rooftop_finale,
            4 => Properties.Resources.c9m2_lots_finale,
            5 => Properties.Resources.c10m5_houseboat_finale,
            6 => Properties.Resources.c11m5_runway_finale,
            7 => Properties.Resources.c12m5_cornfield_finale,
            _ => string.Empty,
        };
    }

    public static string GetOffcialScavengeScriptFile(int index)
    {
        return index switch
        {
            0 => Properties.Resources.c1m4_atrium_finale,
            1 => Properties.Resources.c1m4_delay,
            2 => Properties.Resources.c7m3_port_finale,
            3 => Properties.Resources.c14m2_lighthouse_finale,
            _ => string.Empty,
        };
    }

    public static string GetOffcialGauntletScriptFile()
    {
        return Properties.Resources.director_gauntlet;
    }

    public static void SaveNavToPath(string saveFilePath, int type)
    {
        string navFile = type switch
        {
            0 => Properties.Resources.FinaleStandardScriptNav,
            1 => Properties.Resources.FinaleScavengeScriptNav,
            2 => Properties.Resources.FinaleGauntletScriptNav,
            _ => string.Empty
        };

        string filePath = saveFilePath + (saveFilePath.EndsWith(".nav") ? string.Empty : ".nav");
        try
        {
            using MemoryStream memoryStream = new(Convert.FromBase64String(navFile));
            using FileStream fileStream = new(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            byte[] bytes = memoryStream.ToArray();
            fileStream.Write(bytes, 0, bytes.Length);
        }
        catch
        {
            TryOpenMessageWindow(8);
            return;
        }
    }

    public static void SaveNutToPath(string saveFilePath, string text)
    {
        string filePath = saveFilePath + (saveFilePath.EndsWith(".nut") ? string.Empty : ".nut");
        File.WriteAllText(filePath, text);
    }

    public static void SaveVmfToPath(string saveFilePath, List<string> VmfValuesList, int type)
    {
        string file = type switch
        {
            0 => Properties.Resources.FinaleStandardScriptVmf,
            1 => Properties.Resources.FinaleScavengeScriptVmf,
            2 => Properties.Resources.FinaleGauntletScriptVmf,
            _ => string.Empty
        };

        file = file.Replace("\"targetname\" \"director\"", $"\"targetname\" \"{VmfValuesList[0]}\"");
        file = file.Replace("\"director\x1b", $"\"{VmfValuesList[0]}\x1b");

        if (type == 0)
        {
            string replacedFile = type switch
            {
                0 => "standard_finale",
                1 => "scavenge_finale",
                2 => "gauntlet_finale",
                _ => string.Empty
            };

            file = file.Replace("\"targetname\" \"finale_radio\"", $"\"targetname\" \"{VmfValuesList[1]}\"");
            file = file.Replace("\"finale_radio\x1b", $"\"{VmfValuesList[1]}\x1b");
            file = file.Replace($"\"ScriptFile\" \"{replacedFile}.nut\"", $"\"ScriptFile\" \"{VmfValuesList[2]}\"");
        }
        else if (type == 1)
        {
            file = file.Replace("\"targetname\" \"finale_lever\"", $"\"targetname\" \"{VmfValuesList[1]}\"");
            file = file.Replace("\"finale_lever\x1b", $"\"{VmfValuesList[1]}\x1b");
        }
        else if (type == 2)
        {
            file = file.Replace("\"targetname\" \"finale_radio\"", $"\"targetname\" \"{VmfValuesList[1]}\"");
            file = file.Replace("\"finale_radio\x1b", $"\"{VmfValuesList[1]}\x1b");
        }

        string filePath = saveFilePath + (saveFilePath.EndsWith(".vmf") ? string.Empty : ".vmf");

        try
        {
            using StreamWriter streamWriter = File.CreateText(filePath);
            streamWriter.Write(file);
        }
        catch
        {
            TryOpenMessageWindow(8);
            return;
        }
    }
}