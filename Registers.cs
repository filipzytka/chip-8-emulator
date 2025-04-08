public class Registers 
{
    public byte[] VRegisters { get; set; }
    public ushort ProgramCounter { get; set; } = 0;
    public ushort IndexRegister { get; set; } = 0;
    public byte[] Memory { get; }

    public Registers(ushort programStart, ushort memorySize)
    {
        Memory = new byte[memorySize];
        VRegisters = new byte[_vSize];
        ProgramCounter = programStart;
    }

    private const byte _vSize = 16;
}

