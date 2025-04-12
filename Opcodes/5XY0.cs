public class _5XY0 : IContextualOpcode
{
    private OpcodeContext _context;

    public void LoadContext(OpcodeContext context)
    {
        _context = context;
    }

    public Action Invoke() 
    {
        return () => {
                ref byte vx = ref _context.Registers.VRegisters[
                    BitManipulator.GetNibble(_context.LatestOpcode, 1)];
                ref byte vy = ref _context.Registers.VRegisters[
                    BitManipulator.GetNibble(_context.LatestOpcode, 2)];

                if (vx == vy)
                {
                    _context.Registers.ProgramCounter += 2;
                }
        };
    }
}


