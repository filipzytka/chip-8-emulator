public class _8XY4 : IContextualOpcode
{
    private OpcodeContext _context;

    public void LoadContext(OpcodeContext context)
    {
        _context = context;
    }

    public Action Invoke() 
    {
        return () => {
                _context.Registers.VRegisters[0xF] = 0;

                ref byte vx = ref _context.Registers.VRegisters[BitManipulator
                    .GetNibble(_context.LatestOpcode, 1)];

                ref byte vy = ref _context.Registers.VRegisters[BitManipulator
                    .GetNibble(_context.LatestOpcode, 2)];

                if (IsOverflow(vx, vy))
                {
                    _context.Registers.VRegisters[0xF] = 1;
                }

                vx += vy;
        };
    }

    private bool IsOverflow(byte vx, byte vy) 
    {
        return vx > vy;
    }
}


