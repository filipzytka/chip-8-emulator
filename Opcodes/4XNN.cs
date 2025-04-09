public class _4XNN : IContextualOpcode
{
    private OpcodeContext _context;

    public void LoadContext(OpcodeContext context)
    {
        _context = context;
    }

    public Action Invoke() 
    {
        return () => {
                    if (_context.Registers.VRegisters[BitManipulator
                            .GetNibble(_context.LatestOpcode, 1)] != ((BitManipulator
                                    .GetNibble(_context.LatestOpcode, 2) << 4) 
                                | BitManipulator.GetNibble(_context.LatestOpcode, 3)))
                        {
                            _context.Registers.ProgramCounter += 2;
                        }
                };
    }
}



