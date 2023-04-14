using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using The_Director.Utils;

public class VmfReader
{
    public virtual string VmfPath { get; set; }

    public virtual int RowCount { get; set; }

    public virtual BackgroundWorker Worker { get; set; }

    protected StreamReader Reader { get; set; }

    protected int CurrentRowCount = 0;

    protected void GetRowCount()
    {
        StreamReader streamReader = new(VmfPath);
        RowCount = 0;
        while (streamReader.ReadLine() != null)
        {
            RowCount++;
        }
    }

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
} 