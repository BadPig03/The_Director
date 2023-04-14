using System.ComponentModel;
using System.Diagnostics;
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

        Worker.ReportProgress(45);

        File.Copy(OldFilePath, Globals.L4D2CustomPackPath + "\\sound\\sound.cache", true);

        Worker.ReportProgress(-1);

        Process process = new();
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.RedirectStandardInput = true;
        process.Start();
        process.StandardInput.WriteLine($"\"{Globals.L4D2VPKPath}\" \"{Globals.L4D2CustomPackPath}\"&exit");
        process.StandardInput.AutoFlush = true;
        process.WaitForExit();
        process.Close();

        File.Copy(Globals.L4D2TempPath + "pack.vpk", FilePath, true);

        Directory.Delete(Globals.L4D2CustomPackPath, true);

        Worker.ReportProgress(100);
    }
}
