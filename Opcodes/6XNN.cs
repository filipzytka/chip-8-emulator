public class _6XNN : IOpcode
{
    private readonly OpcodeContext _context;

    public _6XNN(OpcodeContext context)
    {
        _context = context;
    }

    public Action Invoke() 
    {
        return () => {
                    _context.Registers
                        .VRegisters[_context.BitExtractor
                        .GetNibble(_context.LatestOpcode, 1)] = (byte)((_context.BitExtractor
                        .GetNibble(_context.LatestOpcode, 2) << 4) | 
                        _context.BitExtractor.GetNibble(_context.LatestOpcode, 3));
                };
    }
}

