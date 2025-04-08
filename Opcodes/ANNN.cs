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
                _context.Registers.IndexRegister = (ushort)((_context.BitManipulator
                    .GetNibble(_context.LatestOpcode, 1) << 8) |
                    (_context.BitManipulator.GetNibble(_context.LatestOpcode, 2) << 4) |
                    _context.BitManipulator.GetNibble(_context.LatestOpcode, 3));
                };
    }
}
