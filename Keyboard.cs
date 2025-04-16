public static class Keyboard 
{
    public static char[] KeyMap = 
    {
        '1', '2', '3', '4',
        'Q', 'W', 'E', 'R',
        'A', 'S', 'D', 'D',
        'Z', 'X', 'C', 'V'
    };

    public static int GetPressedKeyIndex()
    {
        if (Console.KeyAvailable)
        {
            char pressedKey = char.ToUpper(Console.ReadKey(true).KeyChar);

            if (KeyMap.Contains(pressedKey))
            {
                return Array.IndexOf(KeyMap, pressedKey);
            }
        }

        return -1;
    }
}
