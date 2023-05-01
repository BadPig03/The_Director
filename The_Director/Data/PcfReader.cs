using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using The_Director.Utils;
using System.Diagnostics;

public class PcfReader
{
    protected List<string> ReadAPcf(string file)
    {
        List<string> materials = new();
        List<char> chars = new();
        byte[] byteArray = File.ReadAllBytes(file);

        foreach (byte b in byteArray)
        {
            if (b == 0 && chars.Count > 0)
            {
                materials.Add(new string(chars.ToArray()));
                chars.Clear();
            }
            else if (b > 0)
            {
                chars.Add(Convert.ToChar(b));
            }
        }
        materials = materials.Where(x => x.Contains(".vmt")).Select(x => x.Replace("\u0005", "materials\\")).Distinct().ToList();
        return materials;
    }

    public void AnalyzePcf(string file)
    {
        foreach (string material in ReadAPcf(file))
        {
            Debug.WriteLine(material);
        }
    }

    public List<string> HandleAPcf(string filePath)
    {
        foreach (string path in Globals.PossiblePaths)
        {
            string path2 = path + "\\" + filePath.Replace("/", "\\");
            if (File.Exists(path2))
            {
                return ReadAPcf(path2);
            }
        }
        return new List<string>();
    }
}