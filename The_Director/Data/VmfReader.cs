using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using The_Director.Utils;

public class VmfReader
{
    public virtual string VmfPath { get; set; }

    public virtual int RowCount { get; set; }

    public virtual BackgroundWorker Worker { get; set; }

    protected StreamReader Reader { get; set; }

    protected int CurrentRowCount = 0;
    protected List<Tuple<KeyValuePair, string, List<string>>> entityTuple = new();

    protected void GetRowCount()
    {
        StreamReader streamReader = new(VmfPath);
        RowCount = 0;
        while (streamReader.ReadLine() != null)
        {
            RowCount++;
        }
    }

    protected void ReadEntities()
    {
        Tuple<KeyValuePair, string, List<string>> tuple = ReadWorldSpawn();
        entityTuple.Add(tuple);
        while (!Reader.EndOfStream)
        {
            Tuple<KeyValuePair, string, List<string>> tuple2 = ReadAnEntity();
            entityTuple.Add(tuple2);
        }
    }

    protected Tuple<KeyValuePair, string, List<string>> ReadWorldSpawn()
    {
        Reader = new(VmfPath);
        List<string> tempList = new();
        List<string> solidList = new();
        List<bool> flagList = new() { false, false };
        string row;
        string skyName = string.Empty;
        while (true)
        {
            row = Reader.ReadLine();
            CurrentRowCount++;

            if (row == null)
            {
                break;
            }

            if (row == "world")
            {
                Reader.ReadLine();
                CurrentRowCount++;
                flagList[0] = true;
                continue;
            }

            if (flagList[0])
            {
                if (!flagList[1] && row == "\tsolid")
                {
                    Reader.ReadLine();
                    CurrentRowCount++;
                    flagList[1] = true;
                    continue;
                }

                if (Regex.IsMatch(row, "^\t\".+\" \".+\""))
                {
                    tempList.Add(row);
                    if (Regex.IsMatch(row, "^\t\"skyname\" \".+\""))
                    {
                        skyName = Regex.Split(row, "\" \"")[1].Replace("\"", "");
                    }
                    continue;
                }

                if (flagList[1])
                {
                    if (Regex.IsMatch(row, "^\t\t\t\"material\" \".+\""))
                    {
                        solidList.Add(row);
                        continue;
                    }
                }

                if (row == "}")
                {
                    break;
                }
            }
        }
        Worker.ReportProgress(100 * CurrentRowCount / RowCount);
        return new Tuple<KeyValuePair, string, List<string>>(ChunkProcess("worldspawn", tempList), skyName, SolidProcess(solidList));
    }

    protected Tuple<KeyValuePair, string, List<string>> ReadAnEntity()
    {
        List<string> tempList = new();
        List<string> solidList = new();
        List<bool> flagList = new() { false, false, false };
        string row;
        string classname = "entity";
        while (true)
        {
            row = Reader.ReadLine();
            CurrentRowCount++;

            if (row == null)
            {
                break;
            }

            if (row == "entity")
            {
                Reader.ReadLine();
                CurrentRowCount++;
                flagList[0] = true;
                continue;
            }

            if (flagList[0])
            {
                if (row == "\tconnections")
                {
                    Reader.ReadLine();
                    CurrentRowCount++;
                    flagList[1] = true;
                    continue;
                }

                if (row == "\teditor")
                {
                    Reader.ReadLine();
                    CurrentRowCount++;
                    flagList[2] = true;
                    continue;
                }

                if (row == "\t}")
                {
                    flagList[1] = false;
                    continue;
                }

                if (row == "\t\t}")
                {
                    flagList[2] = false;
                    continue;
                }

                if (row == "}")
                {
                    break;
                }

                if (Regex.IsMatch(row, "^\t\".+\" \".+\""))
                {
                    tempList.Add(row);
                    if (Regex.IsMatch(row, "^\t\"classname\" \".+\""))
                    {
                        classname = Regex.Split(row, "\" \"")[1].Replace("\"","");
                    }
                    continue;
                }

                if (flagList[2] && Regex.IsMatch(row, "^\t\t\".+\" \".+\""))
                {
                    continue;
                }

                if (Regex.IsMatch(row, "^\t\t\t\"material\" \".+\""))
                {
                    if (!solidList.Contains(row))
                    {
                        solidList.Add(row);
                    }
                    continue;
                }
            }
        }
        Worker.ReportProgress(100 * CurrentRowCount / RowCount);
        return new Tuple<KeyValuePair, string, List<string>> (ChunkProcess(classname, tempList), string.Empty, SolidProcess(solidList));
    }

    protected KeyValuePair ChunkProcess(string header, List<string> rawList)
    {
        List<KeyValue> keyValueList = new();
        foreach (string line in rawList)
        {
            if (Regex.IsMatch(line, "^\t\".+\" \".+\""))
            {
                KeyValue keyValue = new();
                foreach (string line2 in Regex.Split(line, "\" \""))
                {
                    if (Regex.IsMatch(line2, "^\t\""))
                    {
                        keyValue.Key = Regex.Split(line2, "\"")[1];
                    }
                    else
                    {
                        keyValue.Value = line2.Replace("\"", "");
                    }
                }
                keyValueList.Add(keyValue);
            }
        }
        return new KeyValuePair() { Header = header, KeyValue = keyValueList };
    }

    protected List<string> SolidProcess(List<string> rawList)
    {
        List<string> materialList = new();
        foreach (string line in rawList)
        {
            string material = Regex.Split(line, "\" \"")[1].Replace("\"", "").ToLowerInvariant();
            if (!materialList.Contains("materials\\" + material + ".vmt"))
            {
                materialList.Add("materials\\" + material + ".vmt");
            }
        }
        return materialList;
    }

    protected List<VmfResourcesContainer> ConvertToContainers()
    {
        List<VmfResourcesContainer> containerList = new();
        foreach (var kvp in entityTuple)
        {
            int id = 0;
            string classname = string.Empty;
            string targetname = string.Empty;
            string origin = string.Empty;
            string model = string.Empty;
            foreach (var item in kvp.Item1.KeyValue)
            {
                switch (item.Key)
                {
                    case "id":
                        id = Functions.ConvertToInt(item.Value);
                        break;
                    case "classname":
                        classname = item.Value;
                        if (item.Value == "worldspawn")
                        {
                            model = kvp.Item2;
                        }
                        break;
                    case "targetname":
                        targetname = item.Value;
                        break;
                    case "origin":
                        origin = item.Value;
                        break;
                    case "effect_name":
                    case "message":
                    case "model":
                        model = item.Value.Replace(".spr", ".vmt").ToLowerInvariant();
                        break;
                    default:
                        break;
                }
            }
            containerList.Add(new VmfResourcesContainer(id, classname, targetname, origin, model, kvp.Item3));
        }
        Worker.ReportProgress(100);
        return containerList;
    }

    public List<VmfResourcesContainer> BeginReading()
    {
        Worker.ReportProgress(0);
        GetRowCount();
        Worker.ReportProgress(5);
        ReadEntities();
        return ConvertToContainers();
    }
} 