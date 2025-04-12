public class _7XNN : IContextualOpcode
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
                byte nn = BitManipulator.BuildByte(
                        BitManipulator.GetNibble(_context.LatestOpcode, 2),
                        BitManipulator.GetNibble(_context.LatestOpcode, 3)
                );

                vx += nn;
        };
    }
}

