public class BitManipulator
{
    public byte GetNibble(ushort opcode, byte index)
    {
        switch (index)
        {
            case 0: 
                return (byte)((opcode >> 12));
            case 1:
                return (byte)((opcode & 0xF00) >> 8);
            case 2:
                return (byte)((opcode & 0xF0) >> 4);
            case 3:
                return (byte)((opcode & 0xF));
            default:
                throw new IndexOutOfRangeException();
        }
    }

    public ushort ToUShort(byte msByte, byte lsByte)
    {
        return (ushort)((msByte << 8) | lsByte);
    }
}

