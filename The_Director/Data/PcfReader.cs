using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using The_Director.Utils;
using System.Diagnostics;
using System.Text;

public class PcfReader
{
    public void SaveOfficialPcfFiles()
    {
        string file = Functions.GetOfficialParticleFiles();
        if (!Directory.Exists(Globals.L4D2ParticlesPath))
        {
            Directory.CreateDirectory(Globals.L4D2ParticlesPath);
        }
        foreach (string file2 in file.Split('?'))
        {
            string[] fileArray = file2.Split('|');
            using MemoryStream memoryStream = new(Convert.FromBase64String(fileArray[1]));
            using FileStream fileStream = new(Globals.L4D2ParticlesPath + "\\" + fileArray[0] + ".pcf", FileMode.OpenOrCreate, FileAccess.Write);
            byte[] bytes = memoryStream.ToArray();
            fileStream.Write(bytes, 0, bytes.Length);
        }
    }

    protected string ConvertPcfFiles()
    {
        string particleFolderPath = "";
        StringBuilder stringBuilder = new();
        foreach(string file in Directory.GetFiles(particleFolderPath))
        {
            string fileName = Path.GetFileNameWithoutExtension(file);
            if (file.EndsWith(".pcf"))
            {
                stringBuilder.Append(fileName + "|" + Functions.FileToBase64String(file) + "|?");
            }
        }
        return stringBuilder.ToString();
    }

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

    public string ContainsEffect(string effect)
    {
        if (effect == string.Empty)
        {
            return string.Empty;
        }

        foreach (string file in Directory.GetFiles(Globals.L4D2ParticlesPath))
        {
            byte[] byteArray = File.ReadAllBytes(file);
            List<string> list = new();
            List<char> chars = new();

            foreach (byte b in byteArray)
            {
                if (b == 0 && chars.Count > 0)
                {
                    list.Add(new string(chars.ToArray()).ToLowerInvariant());
                    chars.Clear();
                }
                else if (b > 0)
                {
                    chars.Add(Convert.ToChar(b));
                }
            }

            if (list.Contains(effect))
            {
                return Path.GetFileNameWithoutExtension(file);
            }
        }

        return string.Empty;
    }

    public string ContainsEffectInFile(string effect, string file)
    {
        if (effect == string.Empty)
        {
            return string.Empty;
        }

        byte[] byteArray = File.ReadAllBytes(file);
        List<string> list = new();
        List<char> chars = new();

        foreach (byte b in byteArray)
        {
            if (b == 0 && chars.Count > 0)
            {
                list.Add(new string(chars.ToArray()).ToLowerInvariant());
                chars.Clear();
            }
            else if (b > 0)
            {
                chars.Add(Convert.ToChar(b));
            }
        }

        if (list.Contains(effect))
        {
            return Path.GetFileNameWithoutExtension(file);
        }

        return string.Empty;
    }


    public void GetCustomPcfPaths()
    { 
        foreach (string path in Globals.PossiblePaths)
        {
            List<FileInfo> list = new();
            if (Directory.Exists(path + "\\particles"))
            {
                foreach (FileInfo file in Functions.GetAllFileInfo(new DirectoryInfo(path + "\\particles"), list))
                {
                    if (file.Extension == ".pcf")
                    {
                        if (!Globals.CustomParticlePaths.Contains(file.FullName))
                        {
                            Globals.CustomParticlePaths.Add(file.FullName);
                        }
                    }
                }
            }
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