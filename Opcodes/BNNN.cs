public class _BNNN : IContextualOpcode
{
    private OpcodeContext _context;

    public void LoadContext(OpcodeContext context)
    {
        _context = context;
    }

    public Action Invoke() 
    {
        return () => {
            _context.Registers.ProgramCounter = (ushort)((_context.BitExtractor.GetNibble(_context.LatestOpcode, 1) << 8) |
                    (_context.BitExtractor.GetNibble(_context.LatestOpcode, 2) << 4) |
                    _context.BitExtractor.GetNibble(_context.LatestOpcode, 3) +
                    _context.Registers.VRegisters[0]);
        };
    }
}
