public class _FX07 : IContextualOpcode
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

                vx = _context.Registers.DelayTimer;
        };
    }

    private OpcodeContext _context;
}
