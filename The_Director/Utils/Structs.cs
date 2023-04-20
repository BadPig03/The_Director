using System.Collections.Generic;

namespace The_Director.Utils;

public record struct BooleanString(bool Item1, string Item2)
{
    public static implicit operator (bool, string)(BooleanString value)
    {
        return (value.Item1, value.Item2);
    }

    public static implicit operator BooleanString((bool, string) value)
    {
        return new BooleanString(value.Item1, value.Item2);
    }
}

public record struct KeyValue(string Key, string Value)
{
    public static implicit operator (string, string)(KeyValue value)
    {
        return (value.Key, value.Value);
    }

    public static implicit operator KeyValue((string, string) value)
    {
        return new KeyValue(value.Item1, value.Item2);
    }
}

public record struct KeyValuePair(string Header, List<KeyValue> KeyValue)
{
    public static implicit operator (string, List<KeyValue>)(KeyValuePair value)
    {
        return (value.Header, value.KeyValue);
    }

    public static implicit operator KeyValuePair((string, List<KeyValue>) value)
    {
        return new KeyValuePair(value.Item1, value.Item2);
    }
}

public record struct InputOutput(string OutputName, string TargetEntity, string InputName, string Parameters, string TimeDelay, string FireTimes)
{
    public static implicit operator (string, string, string, string, string, string)(InputOutput value)
    {
        return (value.OutputName, value.TargetEntity, value.InputName, value.Parameters, value.TimeDelay, value.FireTimes);
    }

    public static implicit operator InputOutput((string, string, string, string, string, string) value)
    {
        return new InputOutput(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6);
    }
}