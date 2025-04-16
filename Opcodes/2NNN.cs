public class _2NNN : IContextualOpcode
{
    private OpcodeContext _context;

    public void LoadContext(OpcodeContext context)
    {
        _context = context;
    }

    public Action Invoke() 
    {
        return () => {
                ushort memoryAddress = BitManipulator.Build12Bit(
                        BitManipulator.GetNibble(_context.LatestOpcode, 1),
                        BitManipulator.GetNibble(_context.LatestOpcode, 2),
                        BitManipulator.GetNibble(_context.LatestOpcode, 3)
                );

                ushort programCounter = _context.Registers.ProgramCounter;

                _context.Registers.Stack.Push(programCounter);

                _context.Registers.ProgramCounter = memoryAddress;
        };
    }
}
