public static class HexExtensions 
{
    public static string ToHex(this ushort value, ushort digits)
    {
        return value.ToString("X" + digits.ToString());
    }

    public static string ToHex(this byte value, ushort digits)
    {
        return value.ToString("X" + digits.ToString());
    }

    public static string ToHex(this int value, ushort digits)
    {
        return value.ToString("X" + digits.ToString());
    }
}
