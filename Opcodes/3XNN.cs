public class _3XNN : IOpcode
{
    private readonly OpcodeContext _context;

    public _3XNN(OpcodeContext context)
    {
        _context = context;
    }

    public Action Invoke() 
    {
        return () => {
                    if (_context.Registers.VRegisters[_context.BitExtractor
                            .GetNibble(_context.LatestOpcode, 1)] == ((_context.BitExtractor
                                    .GetNibble(_context.LatestOpcode, 2) << 4) 
                                | _context.BitExtractor.GetNibble(_context.LatestOpcode, 3)))
                        {
                            _context.Registers.ProgramCounter += 2;
                        }
                };
    }
}




