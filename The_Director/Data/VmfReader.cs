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

    protected List<KeyValuePair> informationList = new() { Globals.emptyKeyValuePair };
    protected List<KeyValuePair> entityList = new() { Globals.emptyKeyValuePair };

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
    protected KeyValuePair? ReadAChunk()
    {
        KeyValuePair keyValuePair = new();
        List<KeyValue> keyValueList = new();
        string row;
        bool readInside = false;

        if (Reader.Peek() == -1)
        {
            return null;
        }

        while ((row = Reader.ReadLine()) != null)
        {
            if (Worker.CancellationPending)
            {
                Worker.ReportProgress(0);
                return null;
            }

            CurrentRowCount++;
            if (readInside)
            {
                if (Regex.IsMatch(row, "^\t+\".+\" \".+\"$"))
                {
                    KeyValue keyValue = new();
                    foreach(string rowSplit in Regex.Split(row, "\" \""))
                    {
                        if (Regex.IsMatch(rowSplit, "^\t+\""))
                        {
                            keyValue.Key = Regex.Split(rowSplit, "\"")[1];
                        }
                        else
                        {
                            keyValue.Value = rowSplit.Replace("\"", "");
                        }
                    }
                    keyValueList.Add(keyValue);
                }
                if (row == "}")
                {
                    keyValuePair.KeyValue = keyValueList;
                    return keyValuePair;
                }
            }
            if (!(row.Contains(" ") || row.Contains("\"") || row.Contains("{") || row.Contains("}") || row.Contains("\t")))
            {
                readInside = true;
                keyValuePair.Header = row;
                if (Reader.ReadLine() == "{")
                {
                    CurrentRowCount++;
                    if (Reader.Peek() == 125)
                    {
                        return new KeyValuePair { Header = row, KeyValue = Globals.emptyKeyValueList };
                    }
                }
                
            }
        }
        return null;
    }
#endif

    protected void ReadAChunk()
    {
        Reader = new(VmfPath);
        string row;
        bool bracket = false;
        while ((row = Reader.ReadLine()) != null )
        {
            if (row == "{")
            {
                bracket = true;
                continue;
            }
            if (bracket)
            {

            }
        }
    }

    protected void ReadVersionInfo()
    {
        StreamReader streamReader = new(VmfPath);
        List<string> tempList = new();
        string row;
        bool flag = false;
        while ((row = streamReader.ReadLine()) != null)
        {
            if (row == "versioninfo")
            {
                streamReader.ReadLine();
                flag = true;
                continue;
            }
            if (flag)
            {
                if (row == "}")
                {
                    break;
                }
                tempList.Add(row);
            }
        }
        informationList[0] = ChunkProcess("versioninfo", tempList);
    }

    protected void ReadEntities()
    {
        Reader = new(VmfPath);
        while (!Reader.EndOfStream)
        {
            Tuple<KeyValuePair, List<InputOutput>, List<string>> tuple = ReadAnEntity();
            foreach (var item in tuple.Item1.KeyValue)
            {
                Debug.WriteLine(tuple.Item1.Header + ": " + item.Key + " = " + item.Value);
            }
            foreach (var item in tuple.Item2)
            {
                Debug.WriteLine($"{item.OutputName}, {item.TargetEntity}, {item.TargetEntity}, {item.Parameters}, {item.TimeDelay}, {item.FireTimes}");
            }
            foreach (var item in tuple.Item3)
            {
                Debug.WriteLine(item);
            }
        }
    }

    protected void ReadWorldSpawn()
    {
        StreamReader streamReader = new(VmfPath);
        string row;
        bool flag = false;
        while ((row = Reader.ReadLine()) != null)
        {
            if (row == "world")
            {
                Reader.ReadLine();
                flag = true;
                continue;
            }

            if (flag)
            {

            }
        }
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

    protected void PrintAll()
    {
        foreach (var item in informationList)
        {
            foreach (var item2 in item.KeyValue)
            {
                Console.WriteLine(item.Header + ": " + item2.Key + " = " + item2.Value);
            }
        }
    }

#if false
    public void BeginReading()
    {
        GetRowCount();
        Reader = new(VmfPath);
        //KeyValuePair? chunk;
        while (ReadAChunk() != null)
        {
            Worker.ReportProgress(100 * CurrentRowCount / RowCount);
            //foreach (var item in chunk.Value.KeyValue)
                //Debug.WriteLine(chunk.Value.Header + ": " + item.Key + " = " + item.Value);
        }
    }
#endif

    public void BeginReading()
    {
        GetRowCount();
        ReadVersionInfo();
        ReadEntities();
        //PrintAll();
    }
} 