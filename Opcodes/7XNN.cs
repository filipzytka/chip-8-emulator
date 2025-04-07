public class _7XNN : IOpcode
{
    private readonly OpcodeContext _context;

    public _7XNN(OpcodeContext context)
    {
        _context = context;
    }

    public Action Invoke() 
    {
        return () => {
                    _context.Registers
                        .VRegisters[_context.BitExtractor
                        .GetNibble(_context.LatestOpcode, 1)] += (byte)((_context.BitExtractor
                        .GetNibble(_context.LatestOpcode, 2) << 4) | 
                        _context.BitExtractor.GetNibble(_context.LatestOpcode, 3));
                };
    }
}

