using System.Text.RegularExpressions;

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

public static class Functions
{
    public static bool IsPositiveNumber(string value)
    {
        return Regex.IsMatch(value, @"^[1-9]\d*$");
    }

    public static int ConvertToInt(string value)
    {
        if (int.TryParse(value, out _))
            return int.Parse(value);
        else
            return -1;
    }
}