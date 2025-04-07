public class Display 
{
    private const int _width = 64;
    private const int _height = 32;
    public const char ON = '█', OFF = ' '; 
 
    public ushort[,] Screen; 

    public Display()
    {
        Screen = new ushort[_height, _width];
        Clear();
    }

    public bool DrawSprite(byte[] sprites, byte x, byte y)
    {
        bool isCollision = false;

        byte n = (byte)sprites.Length;

        byte mask = 0x80;
        byte spriteWidth = 8;

        for (byte i = 0; i < n; i++)
        {
            for (byte j = 0; j < spriteWidth; j++)
            {
                if ((sprites[i] & (mask >> j)) == 0)
                {
                    continue;
                }

                if (Screen[y + i, x + j] == ON) 
                {
                    isCollision = true;
                }

                Screen[y + i, x + j] ^= 1;
            }
        }

        return isCollision;
    }

    public void Clear()
    {
        for (byte row = 0; row < _height; row++)
        {
            for (byte col = 0; col < _width; col++)
            {
                Screen[row, col] ^= 1;
            }
        }
    }

    public void Show()
    {
        for (byte row = 0; row < _height; row++)
        {
            for (byte col = 0; col < _width; col++)
            {
                char pixel = Screen[row, col] == 1 ? ON : OFF;
                Console.Write(pixel);
            }

            Console.WriteLine();
        }
    }
}
