using System.ComponentModel;
using System.IO;
using The_Director.Utils;

public class SoundCachePacker
{
    public string OldFilePath { get; set; }
    public string FilePath { get; set; }
    public virtual BackgroundWorker Worker { get; set; }

    public void StartProcess()
    {
        if (Directory.Exists(Globals.L4D2CustomPackPath))
        {
            Directory.Delete(Globals.L4D2CustomPackPath, true);
        } 

        Directory.CreateDirectory(Globals.L4D2CustomPackPath + "\\sound");

        Worker.ReportProgress(25);

        File.Copy(OldFilePath, Globals.L4D2CustomPackPath + "\\sound\\sound.cache", true);

        Worker.ReportProgress(50);

        Functions.RunVPK(Globals.L4D2CustomPackPath);

        Worker.ReportProgress(75);

        File.Copy(Globals.L4D2TempPath + "pack.vpk", FilePath, true);

        Directory.Delete(Globals.L4D2CustomPackPath, true);

        Worker.ReportProgress(100);
    }
}
