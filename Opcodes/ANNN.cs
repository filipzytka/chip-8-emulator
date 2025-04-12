public class _ANNN : IContextualOpcode
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

                _context.Registers.IndexRegister = memoryAddress;
        };
    }
}
