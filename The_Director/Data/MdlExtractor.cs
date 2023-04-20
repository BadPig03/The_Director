using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using The_Director.Utils;

public class MdlExtractor
{
    public virtual List<string> PossiblePaths { get; set; }
    
    protected List<string> ReadAMdlTexture(string filePath)
    {
        List<string> rawMaterials = new();
        List<string> materials = new();
        List<char> chars = new();

        byte[] byteArray = File.ReadAllBytes(filePath);
        
        foreach (byte b in byteArray)
        {
            if (b == 0 && chars.Count > 0)
            {
                rawMaterials.Add(new string(chars.ToArray()).ToLowerInvariant());
                chars.Clear();
            }
            else if (b > 0)
            {
                chars.Add(Convert.ToChar(b));
            }
        }

        string materialPath = rawMaterials.Last();

        for (int i = 0; i < byteArray[204]; i++)
        {
            materials.Add($"materials\\{materialPath}{rawMaterials[rawMaterials.Count - 2 - i]}.vmt");
        }

        return materials;
    }

    public void ReadGameInfo()
    {
        PossiblePaths = new List<string>();
        StreamReader streamReader = new(Globals.L4D2GameInfoTxtPath);
        string row;
        bool flag = false;
        while ((row = streamReader.ReadLine()) != null)
        {
            if (row == "\t\tSearchPaths")
            {
                streamReader.ReadLine();
                flag = true;
                continue;
            }

            if (flag)
            {
                if (Regex.IsMatch(row, "Game.+"))
                {
                    row = row.Replace("\t", "").Replace("Game", "");
                    if (row == "|gameinfo_path|.")
                    {
                        PossiblePaths.Add(Globals.L4D2GameInfoPath);
                        continue;
                    }
                    else if (row.Contains("\"") || row.Contains("\\"))
                    {
                        PossiblePaths.Add(row.Replace("\"", ""));
                        continue;
                    }
                    else
                    {
                        PossiblePaths.Add($"{Globals.L4D2RootPath}\\{row}");
                        continue;
                    }
                }

                if (row == "\t\t}")
                {
                    break;
                }
            }
        }
    }

    public void GetOffcialFiles()
    {
        foreach (FileInfo file in Functions.GetAllFileInfo(new DirectoryInfo("D:\\l4d2maps\\origin_sources\\materials"), new List<FileInfo>()))
        {
            if (file.Extension == ".vmt")
            {
                Debug.Write("" + file.FullName.Replace("D:\\l4d2maps\\origin_sources\\", "") + ", ");
            }
        }
    }

    public void HandleAMdl(string filePath)
    {
        foreach (string path in PossiblePaths)
        {
            string path2 = path + "\\" + filePath.Replace("/", "\\");
            if (File.Exists(path2))
            {
                foreach (string path3 in ReadAMdlTexture(path2))
                {
                    Debug.WriteLine(path3);
                }
                break;
            }
        }
    }
}