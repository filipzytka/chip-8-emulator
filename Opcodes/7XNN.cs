public class _7XNN : IContextualOpcode
{
    private OpcodeContext _context;

    public void LoadContext(OpcodeContext context)
    {
        _context = context;
    }

    public Action Invoke() 
    {
        return () => {
                    _context.Registers
                        .VRegisters[BitManipulator
                        .GetNibble(_context.LatestOpcode, 1)] += (byte)((BitManipulator
                        .GetNibble(_context.LatestOpcode, 2) << 4) | 
                        BitManipulator.GetNibble(_context.LatestOpcode, 3));
                };
    }
}

