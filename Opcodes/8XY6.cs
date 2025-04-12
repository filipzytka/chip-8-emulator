public class _8XY6 : IContextualOpcode
{
    private OpcodeContext _context;

    public void LoadContext(OpcodeContext context)
    {
        _context = context;
    }

    public Action Invoke() 
    {
        return () => {
                ref byte vx = ref _context.Registers.VRegisters[BitManipulator
                    .GetNibble(_context.LatestOpcode, 1)];

                byte beforeShift = vx;

                vx >>= 1;
            
                _context.Registers.VRegisters[0xF] = (byte)(beforeShift & 0x1);
        };
    }
}

