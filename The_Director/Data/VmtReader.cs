using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using The_Director.Utils;

public class VmtReader
{
    protected List<string> ReadAVmt(string filePath, string originFilePath)
    {
        List<string> list = new() { originFilePath };
        foreach (string line in File.ReadAllText(filePath).ToLowerInvariant().Replace("\"", "").Replace("\t", " ").Split('\n'))
        {
            foreach (string paramter in Globals.MaterialParameterList)
            {
                if (line.Contains($"${paramter}"))
                {
                    string vtfFileName = Regex.Match(line, $"({paramter})( +)(.+)").Groups[3].ToString().Replace("\r", "");
                    
                    if (vtfFileName == "env_cubemap" || vtfFileName == string.Empty || Globals.OfficalMaterialsPaths.Contains(("materials\\" + vtfFileName).Replace("/", "\\").ToLowerInvariant()))
                    {
                        continue;
                    }

                    list.Add("materials\\" + vtfFileName + ".vtf");
                }
            }
        }
        return list;
    }

    public List<string> HandleAVmt(string filePath)
    {
        foreach (string path in Globals.PossiblePaths)
        {
            string path2 = path + "\\" + filePath.Replace("/", "\\");
            if (File.Exists(path2))
            {
                return ReadAVmt(path2, filePath);
            }
        }
        return new List<string> ();
    }
}