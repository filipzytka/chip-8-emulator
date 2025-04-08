public class _3XNN : IContextualOpcode
{
    private OpcodeContext _context;

    public void LoadContext(OpcodeContext context)
    {
        _context = context;
    }

    public Action Invoke() 
    {
        return () => {
                    if (_context.Registers.VRegisters[_context.BitManipulator
                            .GetNibble(_context.LatestOpcode, 1)] == ((_context.BitManipulator
                                    .GetNibble(_context.LatestOpcode, 2) << 4) 
                                | _context.BitManipulator.GetNibble(_context.LatestOpcode, 3)))
                        {
                            _context.Registers.ProgramCounter += 2;
                        }
                };
    }
}




