public class _ANNN : IOpcode
{
    private readonly OpcodeContext _context;

    public _ANNN(OpcodeContext context)
    {
        _context = context;
    }

    public Action Invoke() 
    {
        return () => {
                _context.Registers.IndexRegister = (ushort)((_context.BitExtractor
                    .GetNibble(_context.LatestOpcode, 1) << 8) |
                    (_context.BitExtractor.GetNibble(_context.LatestOpcode, 2) << 4) |
                    _context.BitExtractor.GetNibble(_context.LatestOpcode, 3));
                };
    }
}
