public static class BitManipulator
{
    public static byte GetNibble(ushort opcode, byte index)
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

    public static ushort Build2Bytes(byte msByte, byte lsByte)
    {
        return (ushort)((msByte << 8) | lsByte);
    }

    public static byte BuildByte(byte msNibble, byte lsNibble)
    {
        return (byte)((msNibble << 4) | lsNibble);
    }

    public static ushort Build12Bit(byte msNibble, byte mNibble, byte lsNibble)
    {
        return (ushort)((msNibble << 8) | (mNibble << 4) | lsNibble);
    }
}

