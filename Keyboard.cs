public static class Keyboard 
{
    public static Dictionary<char, byte> KeyMap = new()
    {
        { '1', 0x1 }, { '2', 0x2 }, { '3', 0x3 }, { '4', 0xC },
        { 'Q', 0x4 }, { 'W', 0x5 }, { 'E', 0x6 }, { 'R', 0xD },
        { 'A', 0x7 }, { 'S', 0x8 }, { 'D', 0x9 }, { 'F', 0xE },
        { 'Z', 0xA }, { 'X', 0x0 }, { 'C', 0xB }, { 'V', 0xF },
    };

    public static int ReadKeyCode()
    {
        if (Console.KeyAvailable)
        {
            char pressedKey = char.ToUpper(Console.ReadKey(intercept: true).KeyChar);

            if (KeyMap.ContainsKey(pressedKey))
            {
                return KeyMap[pressedKey];
            }
        }

        return -1;
    }
}
