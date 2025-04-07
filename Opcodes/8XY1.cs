public class _8XY1 : IOpcode
{
    private readonly OpcodeContext _context;

    public _8XY1(OpcodeContext context)
    {
        _context = context;
    }

    public Action Invoke() 
    {
        return () => {
                _context.Registers.VRegisters[_context.BitExtractor
                    .GetNibble(_context.LatestOpcode, 1)] |= _context.Registers
                    .VRegisters[_context.BitExtractor
                    .GetNibble(_context.LatestOpcode, 2)];                
                };
    }
}
