public class _BNNN : IOpcode
{
    private readonly OpcodeContext _context;

    public _BNNN(OpcodeContext context)
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
