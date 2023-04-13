using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using The_Director.Windows;

namespace The_Director.Utils;

public static class Functions
{
    public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
    {
        DirectoryInfo dir = new(sourceDirName);

        if (!dir.Exists)
        {
            throw new DirectoryNotFoundException("Source directory does not exist or could not be found:"+ sourceDirName);
        }

        DirectoryInfo[] dirs = dir.GetDirectories();
        if (!Directory.Exists(destDirName))
        {
            Directory.CreateDirectory(destDirName);
        }

        FileInfo[] files = dir.GetFiles();
        foreach (FileInfo file in files)
        {
            string temppath = Path.Combine(destDirName, file.Name);
            file.CopyTo(temppath, false);
        }

        if (copySubDirs)
        {
            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, temppath, copySubDirs);
            }
        }
    }

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

        if (!flag)
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
                case 9:
                    Title = "提示";
                    TextBoxString = "脚本文件处理完成!";
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

    public static string GetProcessInput(int num)
    {
        Globals.CanShutdown = false;

        string path = num switch
        {
            0 => Globals.L4D2StandardFinalePath,
            1 => Globals.L4D2ScavengeFinalePath,
            2 => Globals.L4D2GauntletFinalePath,
            3 => Globals.L4D2SacrificeFinalePath,
            _ => string.Empty
        };

        return $"\"{Globals.L4D2VBSPPath}\" -game \"{Globals.L4D2GameInfoPath}\" \"{path}.vmf\"" + $"&\"{Globals.L4D2VVISPath}\" -game \"{Globals.L4D2GameInfoPath}\" \"{path}.bsp\"" + $"&\"{Globals.L4D2VRADPath}\" -game \"{Globals.L4D2GameInfoPath}\" -both -StaticPropLighting -StaticPropPolys \"{path}.bsp\"&exit";
    }

    public static void RunL4D2Game(int type)
    {
        Globals.CanShutdown = true;

        string command = type switch
        {
            0 => "-steam -novid +sv_cheats 1 +director_debug 1 +map standard_finale",
            1 => "-steam -novid +sv_cheats 1 +director_debug 1 +map scavenge_finale",
            2 => "-steam -novid +sv_cheats 1 +director_debug 1 +map gauntlet_finale",
            3 => "-steam -novid +sv_cheats 1 +director_debug 1 +map sacrifice_finale",
            4 => $"-steam -novid +snd_buildsoundcachefordirectory {Globals.L4D2CustomAudioPath}",
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

    public static bool RunVice3(string file, bool encrypt = true, string dir = "")
    {
        Process process = new();
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.RedirectStandardInput = true;
        process.Start();
        process.StandardInput.WriteLine($"\"{Globals.L4D2Vice3Path}\"" + (encrypt ? " -x .nuc" : " -d -x .nut") + $" -k SDhfi878 \"{file}\"&exit");
        process.StandardInput.AutoFlush = true;
        process.WaitForExit();
        process.Close();

        if (dir != "")
        {
            File.Delete(dir);
            File.Move(file.Replace(".nut", ".nuc"), dir);
        }

        return true;
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
            _ => string.Empty
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
            _ => string.Empty
        };
    }

    public static string GetOffcialGauntletScriptFile()
    {
        return Properties.Resources.director_gauntlet;
    }

    public static string GetOffcialSacrificeScriptFile()
    {
        return Properties.Resources.c7m3_port_finale;
    }

    public static string GetOffcialOnslaughtScriptFile(int index)
    {
        return index switch
        {
            0 => Properties.Resources.c1m1_reserved_wanderers,
            1 => Properties.Resources.c1_gunshop_onslaught,
            2 => Properties.Resources.c1_gunshop_quiet,
            3 => Properties.Resources.c1_mall_ambient,
            4 => Properties.Resources.c1_mall_crescendo,
            5 => Properties.Resources.c1_mall_crescendo_cooldown,
            6 => Properties.Resources.c1_mall_crescendo_wave,
            7 => Properties.Resources.c1_mall_onslaught,
            8 => Properties.Resources.c1_streets_ambush,
            9 => Properties.Resources.c2m1_no_bosses,
            10 => Properties.Resources.c2m1_reserved_wanderers,
            11 => Properties.Resources.c2m4_barns_onslaught,
            12 => Properties.Resources.c2m4_barns_onslaught2,
            13 => Properties.Resources.c2m4_finale_onslaught,
            14 => Properties.Resources.c2m4_finale_quiet,
            15 => Properties.Resources.c2_coaster_onslaught,
            16 => Properties.Resources.c2_fairgrounds_onslaught,
            17 => Properties.Resources.c2_highway_ambush,
            18 => Properties.Resources.c3m1_barge,
            19 => Properties.Resources.c3m1_fog_spawn,
            20 => Properties.Resources.c3m1_nothreat,
            21 => Properties.Resources.c3m1_swamp_fog_spawn,
            22 => Properties.Resources.c3m2_fog_spawn,
            23 => Properties.Resources.c3m2_mob_from_front,
            24 => Properties.Resources.c3m4_nothreat,
            25 => Properties.Resources.c4m4_fog_spawn,
            26 => Properties.Resources.c5m1_nothreat,
            27 => Properties.Resources.c5m2_mob_from_front,
            28 => Properties.Resources.c6m1_riverbank,
            29 => Properties.Resources.c6m2_minifinale,
            30 => Properties.Resources.c7m2_barge,
            31 => Properties.Resources.c8m1_apartment,
            32 => Properties.Resources.c8m3_sewers,
            33 => Properties.Resources.c8m5_rooftop,
            34 => Properties.Resources.c9m1_nobosses,
            35 => Properties.Resources.c10m1_no_bosses,
            36 => Properties.Resources.c10m4_onslaught,
            37 => Properties.Resources.c11m1_no_bosses,
            38 => Properties.Resources.c11m4_minifinale,
            39 => Properties.Resources.c11m4_minifinale_pt2,
            40 => Properties.Resources.c11m4_onslaught,
            41 => Properties.Resources.c11m4_reserved_wanderers,
            42 => Properties.Resources.c12m1_no_bosses,
            43 => Properties.Resources.c12m3_onslaught,
            44 => Properties.Resources.c12m4_onslaught,
            45 => Properties.Resources.c12m4_reserved_wanderers,
            46 => Properties.Resources.c12m5_panic,
            47 => Properties.Resources.c14_junkyard_cooldown,
            48 => Properties.Resources.c14_junkyard_crane,
            49 => Properties.Resources.director_c4_storm,
            50 => Properties.Resources.director_onslaught,
            51 => Properties.Resources.director_quiet,
            _ => string.Empty
        };
    }

    public static void SaveVice3ToPath()
    {
        try
        {
            using MemoryStream memoryStream = new(Convert.FromBase64String(Properties.Resources.vice3));
            using FileStream fileStream = new(Globals.L4D2Vice3Path, FileMode.OpenOrCreate, FileAccess.Write);
            byte[] bytes = memoryStream.ToArray();
            fileStream.Write(bytes, 0, bytes.Length);
        }
        catch
        {
            TryOpenMessageWindow(8);
            return;
        }
    }

    public static void SaveNavToPath(string saveFilePath, int type)
    {
        string navFile = type switch
        {
            0 => Properties.Resources.FinaleStandardScriptNav,
            1 => Properties.Resources.FinaleScavengeScriptNav,
            2 => Properties.Resources.FinaleGauntletScriptNav,
            3 => Properties.Resources.FinaleSacrificeScriptNav,
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
            3 => Properties.Resources.FinaleSacrificeScriptVmf,
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
                3 => "sacrifice_finale",
                _ => string.Empty
            };

            file = file.Replace("\"targetname\" \"finale_radio\"", $"\"targetname\" \"{VmfValuesList[1]}\"");
            file = file.Replace("\"finale_radio\x1b", $"\"{VmfValuesList[1]}\x1b");
            file = file.Replace($"\"ScriptFile\" \"{replacedFile}.nut\"", $"\"ScriptFile\" \"{VmfValuesList[2]}\"");
        }
        else if (type == 1 || type == 3)
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

    public static void SaveTextToPath(string dir, string text)
    {
        try
        {
            using StreamWriter streamWriter = File.CreateText(dir);
            if (dir == "")
            {
                return;
            }
            streamWriter.Write(text);
        }
        catch
        {
            TryOpenMessageWindow(8);
            return;
        }
    }

    public static List<FileInfo> GetAllFileInfo(DirectoryInfo dir)
    {
        FileInfo[] allFile = dir.GetFiles();
        List<FileInfo> FileList = new();
        foreach (FileInfo file in allFile)
        {
            FileList.Add(file);
        }
        DirectoryInfo[] allDir = dir.GetDirectories();
        foreach (DirectoryInfo d in allDir)
        {
            GetAllFileInfo(d);
        }
        return FileList;
    }

    public static string FileToBase64String(string dir)
    {
        string data = "";
        using (MemoryStream msReader = new())
        {
            using (FileStream fs = new(dir, FileMode.Open))
            {
                byte[] buffer = new byte[1024];
                int readLen = 0;
                while ((readLen = fs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    msReader.Write(buffer, 0, readLen);
                }
            }
            data = Convert.ToBase64String(msReader.ToArray());
        }
        return data;
    }
}