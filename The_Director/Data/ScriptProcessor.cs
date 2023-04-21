using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using The_Director.Utils;

public class ScriptProcessor
{
    public virtual List<string> Paths { get; set; }
    public virtual BackgroundWorker Worker { get; set; }

    protected int FileCount = 0;

    protected int TotalCounts = 0;

    protected void SaveVice3()
    {
        if (!File.Exists(Globals.L4D2Vice3Path))
            Functions.SaveVice3ToPath();
    }

    protected void ProcessAFile(string filePath)
    {
        Process process = new();
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.RedirectStandardInput = true;
        process.Start();
        process.StandardInput.WriteLine($"\"{Globals.L4D2Vice3Path}\"" + ((filePath.EndsWith(".nut")) ? " -x .nuc" : " -d -x .nut") + $" -k SDhfi878 \"{filePath}\"&exit");
        process.StandardInput.AutoFlush = true;

        process.WaitForExit();
        process.Close();

        FileCount++;
    }

    public void StartProcessFiles()
    {
        SaveVice3();

        TotalCounts = Paths.Count;

        Worker.ReportProgress(30);

        foreach (string filePath in Paths)
        {
            ProcessAFile(filePath);
            Worker.ReportProgress(30 + (70 * FileCount / TotalCounts));
        }

        Worker.ReportProgress(100);
    }

    public void StartProcessFolder()
    {
        SaveVice3();

        foreach (string path in Paths)
        {
            TotalCounts += Functions.GetAllFileInfo(new DirectoryInfo(path), new List<FileInfo>()).Count;
        }

        Worker.ReportProgress(30);

        foreach (string path in Paths)
        {
            foreach (FileInfo file in Functions.GetAllFileInfo(new DirectoryInfo(path), new List<FileInfo>()))
            {
                ProcessAFile(file.FullName);
                Worker.ReportProgress(30 + (70 * FileCount / TotalCounts));
            }
        }

        Worker.ReportProgress(100);
    }
}
