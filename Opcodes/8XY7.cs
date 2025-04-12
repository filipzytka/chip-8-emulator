public class _8XY7 : IContextualOpcode
{
    private OpcodeContext _context;

    public void LoadContext(OpcodeContext context)
    {
        _context = context;
    }

    public Action Invoke() 
    {
        return () => {
                _context.Registers.VRegisters[0xF] = 1;

                ref byte vx = ref _context.Registers.VRegisters[BitManipulator
                    .GetNibble(_context.LatestOpcode, 1)];

                ref byte vy = ref _context.Registers.VRegisters[BitManipulator
                    .GetNibble(_context.LatestOpcode, 2)];

                if (IsUnderflow(vx, vy))
                {
                    _context.Registers.VRegisters[0xF] = 0;
                }

                vx = (byte)(vy - vx);
        };
    }

    private bool IsUnderflow(byte vx, byte vy) 
    {
        return vy < vx;
    }
}


