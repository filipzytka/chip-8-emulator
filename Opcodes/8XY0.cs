public class _8XY0 : IContextualOpcode
{
    private OpcodeContext _context;

    public void LoadContext(OpcodeContext context)
    {
        _context = context;
    }

    public Action Invoke() 
    {
        return () => {
                _context.Registers.VRegisters[_context.BitManipulator
                    .GetNibble(_context.LatestOpcode, 1)] = _context.Registers
                    .VRegisters[_context.BitManipulator
                    .GetNibble(_context.LatestOpcode, 2)];                
                };
    }
}
