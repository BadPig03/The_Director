using System.IO;
using The_Director.Utils;

public class SoundReader
{
    public void GetOfficialFiles()
    {
        foreach (string file in Functions.GetOfficialSoundFiles().Split('|'))
        {
            Globals.OfficialSoundPaths.Add(file);
        }
    }

    public string HandleASoundFile(string filePath)
    {
        foreach (string path in Globals.PossiblePaths)
        {
            string path2 = path + "\\" + filePath.Replace("/", "\\");
            if (File.Exists(path2))
            {
                return filePath;
            }
        }
        return string.Empty;
    }
}