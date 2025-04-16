public class _FX29 : IContextualOpcode
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

                _context.Registers.IndexRegister = (ushort)(Font.StartAddress
                        + (vx * 5));
        };
    }

    private OpcodeContext _context;
}
