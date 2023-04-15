using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using The_Director.Utils;

public class MdlExtractor
{
    public virtual List<string> PossiblePaths { get; set; }
    protected void ReadGameInfo()
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

    public void Print()
    {
        ReadGameInfo();
        foreach (string path in PossiblePaths)
        {
            Debug.WriteLine(path);
        }
    }
}