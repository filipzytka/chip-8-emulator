public class _8XYE : IContextualOpcode
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

                byte beforeShift = vx;

                vx <<= 1;

                if ((beforeShift & 0x8) == 1)
                {
                    _context.Registers.VRegisters[0xF] = 1;
                }
        };
    }
}


