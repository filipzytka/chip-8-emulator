public class _CXNN : IOpcode
{
    private readonly OpcodeContext _context;
    static Random r = new Random();

    public _CXNN(OpcodeContext context)
    {
        _context = context;
    }

    public Action Invoke() 
    {
        return () => {
            byte b = (byte)r.Next(0, 256); 
            ushort NN = (byte)(_context.BitExtractor
                    .GetNibble(_context.LatestOpcode, 2) << 4 |
                    _context.BitExtractor.GetNibble(_context.LatestOpcode, 3));
            _context.Registers
                .VRegisters[_context.BitExtractor
                .GetNibble(_context.LatestOpcode, 1)] = (byte)(b & NN);
        };
    }
}
