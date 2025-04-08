public class MemoryDumper 
{
    public void HexDump(byte[] program)
    {
        ushort address = 0x00, offset = 0x10;
        ushort rowIndex = 0, dataIndex = 0;
        string padding = "    ";

        foreach (var b in program)
        {
            bool isWord = dataIndex % 2 == 0;
            bool isEndLine = rowIndex % 16 == 0;

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

    private ushort _rowSize = 16;
}
