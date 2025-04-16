public class _FX1E : IContextualOpcode
{
    public void LoadContext(OpcodeContext context)
    {
        _context = context;
    }
    
    public Action Invoke()
    {
        return () => {
                byte vx = _context.Registers.VRegisters[
                    BitManipulator.GetNibble(_context.LatestOpcode, 1)];

                _context.Registers.IndexRegister += vx;
        };
    }

    private OpcodeContext _context;
}
