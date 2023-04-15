using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    
    protected List<Tuple<KeyValuePair, List<InputOutput>, List<string>>> entityTuple = new();

    protected void GetRowCount()
    {
        StreamReader streamReader = new(VmfPath);
        RowCount = 0;
        while (streamReader.ReadLine() != null)
        {
            RowCount++;
        }
    }

#if false
    protected void ReadEntityTuple()
    {
        foreach (var kvp in entityTuple)
        {
            foreach (var item in kvp.Item1.KeyValue)
            {
                Debug.WriteLine($"{kvp.Item1.Header}: {item.Key} = {item.Value}");
            }
            foreach (var item in kvp.Item2)
            {
                Debug.WriteLine($"{item.OutputName}, {item.TargetEntity}, {item.TargetEntity}, {item.Parameters}, {item.TimeDelay}, {item.FireTimes}");
            }
            foreach (var item in kvp.Item3)
            {
                Debug.WriteLine($"{kvp.Item1.Header}: {item}");
            }
        }
    }
#endif

    protected void ReadEntities()
    {
        Tuple<KeyValuePair, List<InputOutput>, List<string>> tuple = ReadWorldSpawn();
        entityTuple.Add(tuple);
        while (!Reader.EndOfStream)
        {
            Tuple<KeyValuePair, List<InputOutput>, List<string>> tuple2 = ReadAnEntity();
            entityTuple.Add(tuple2);
        }
    }

    protected Tuple<KeyValuePair, List<InputOutput>, List<string>> ReadWorldSpawn()
    {
        Reader = new(VmfPath);
        List<string> tempList = new();
        List<string> solidList = new();
        List<bool> flagList = new() { false, false };
        string row;
        while ((row = Reader.ReadLine()) != null)
        {
            if (row == "world")
            {
                Reader.ReadLine();
                flagList[0] = true;
                continue;
            }

            if (flagList[0])
            {
                if (!flagList[1] && row == "\tsolid")
                {
                    Reader.ReadLine();
                    flagList[1] = true;
                    continue;
                }

                if (Regex.IsMatch(row, "^\t\".+\" \".+\""))
                {
                    tempList.Add(row);
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
        return new Tuple<KeyValuePair, List<InputOutput>, List<string>>(ChunkProcess("worldspawn", tempList), new List<InputOutput>(), SolidProcess(solidList));
    }

    protected Tuple<KeyValuePair, List<InputOutput>, List<string>> ReadAnEntity()
    {
        List<string> tempList = new();
        List<string> IOList = new();
        List<string> solidList = new();
        List<bool> flagList = new() { false, false, false };
        string row;
        string classname = "entity";
        while ((row = Reader.ReadLine()) != null)
        {
            if (row == "entity")
            {
                Reader.ReadLine();
                flagList[0] = true;
                continue;
            }

            if (flagList[0])
            {
                if (row == "\tconnections")
                {
                    Reader.ReadLine();
                    flagList[1] = true;
                    continue;
                }

                if (row == "\teditor")
                {
                    Reader.ReadLine();
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

                if (flagList[1] && Regex.IsMatch(row, "^\t\t\".+\" \".+\"") && row.Contains("\x1b"))
                {
                    IOList.Add(row);
                    continue;
                }

                if (flagList[2] && Regex.IsMatch(row, "^\t\t\".+\" \".+\""))
                {
                    continue;
                }

                if (Regex.IsMatch(row, "^\t\t\t\"material\" \".+\""))
                {
                    solidList.Add(row);
                    continue;
                }
            }
        }
        return new Tuple<KeyValuePair, List<InputOutput>, List<string>> (ChunkProcess(classname, tempList), IOProcess(IOList), SolidProcess(solidList));
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

    protected List<InputOutput> IOProcess(List<string> rawList)
    {
        List<InputOutput> inputOutputList = new();
        foreach (string line in rawList)
        {
            if (Regex.IsMatch(line, "^\t\t\".+\" \".+\""))
            {
                InputOutput inputOutput = new();
                foreach (string line2 in Regex.Split(line, "\" \""))
                {
                    if (Regex.IsMatch(line2, "^\t\t\""))
                    {
                        inputOutput.OutputName = Regex.Split(line2, "\"")[1];
                    }
                    else
                    {
                        inputOutput.TargetEntity = line2.Split('\x1b')[0];
                        inputOutput.InputName = line2.Split('\x1b')[1];
                        inputOutput.Parameters = line2.Split('\x1b')[2];
                        inputOutput.TimeDelay = line2.Split('\x1b')[3];
                        inputOutput.FireTimes = line2.Split('\x1b')[4].Replace("\"", "");
                    }
                }
                inputOutputList.Add(inputOutput);
            }
        }
        return inputOutputList;
    }

    protected List<string> SolidProcess(List<string> rawList)
    {
        List<string> materialList = new();
        foreach (string line in rawList)
        {
            string material = Regex.Split(line, "\" \"")[1].Replace("\"", "").ToLower();
            if (!materialList.Contains(material))
            {
                materialList.Add(material);
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
            bool isPointEntity = true;
            foreach (var item in kvp.Item1.KeyValue)
            {
                switch (item.Key)
                {
                    case "id":
                        id = Functions.ConvertToInt(item.Value);
                        break;
                    case "classname":
                        classname = item.Value;
                        break;
                    case "targetname":
                        targetname = item.Value;
                        break;
                    case "origin":
                        origin = item.Value;
                        break;
                    default:
                        break;
                }
            }
#if false
            foreach (var item in kvp.Item2)
            {
                Debug.WriteLine($"{item.OutputName}, {item.TargetEntity}, {item.TargetEntity}, {item.Parameters}, {item.TimeDelay}, {item.FireTimes}");
            }
            foreach (var item in kvp.Item3)
            {
                Debug.WriteLine($"{kvp.Item1.Header}: {item}");
            }
#endif
            containerList.Add(new VmfResourcesContainer(id, classname, targetname, origin, isPointEntity));
        }
        return containerList;
    }

    public List<VmfResourcesContainer> BeginReading()
    {
        GetRowCount();
        ReadEntities();
        //ReadEntityTuple();
        return ConvertToContainers();
    }
} 