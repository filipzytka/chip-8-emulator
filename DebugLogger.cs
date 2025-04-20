public class DebugLogger
{
    public DebugLogger(Registers registers)
    {
        _registers = registers;
    }
     
    public void LogRegisters()
    {
        LogVRegisters();
        LogTimers();
        LogProgramCounter();
        LogIndexRegister();
        Console.SetCursorPosition(0, 0);
    }

    public void HexDump(byte[] program)
    {
        ushort address = 0x00, offset = 0x10;
        ushort rowIndex = 0, dataIndex = 0;
        string padding = "    ";

        foreach (var b in program)
        {
            bool isWord = dataIndex % 2 == 0;
            bool isEndLine = rowIndex % _rowSize == 0;

            if (rowIndex == 0)
            {
                Console.Write(address.ToHex(4) + padding);
            }
            else if (isEndLine) 
            {
                address += offset;
                Console.WriteLine();
                Console.Write(address.ToHex(4) + padding);
            }
            else if (isWord) 
            {
                Console.Write(" ");
            }

            Console.Write(b.ToHex(2));

            rowIndex += 1;
            dataIndex += 1;
        }
    }

    private void LogProgramCounter()
    {
        Log($"Program Counter: {_registers.ProgramCounter.ToHex(3)} ");
    }

    private void LogIndexRegister()
    {
        Log($"Index Register: {_registers.IndexRegister.ToHex(3)} ");
    }

    private void LogVRegisters()
    {
        byte vIndex = 0;

        foreach (var v in _registers.VRegisters)
        {
            Log($"V{vIndex.ToHex(1)}: {v} ");
            vIndex += 1;
        }
    }

    private void LogTimers()
    {
        Log($"Delay Timer: {_registers.DelayTimer} ");
        Log($"Sound Timer: {_registers.SoundTimer} ");
    }

    private void Log(string str)
    {
        Console.SetCursorPosition(_offsetLeft, Console.CursorTop + 1);
        Console.Write(str);
    }

    private const ushort _rowSize = 16;
    private const byte _offsetLeft = 80;
    private readonly Registers _registers;
}
