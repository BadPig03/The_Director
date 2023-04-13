using System.Collections.Generic;
using System.Windows.Documents;

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