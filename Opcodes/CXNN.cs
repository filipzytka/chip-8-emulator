public class _CXNN : IContextualOpcode
{
    public void LoadContext(OpcodeContext context)
    {
        _context = context;
    }

    public Action Invoke() 
    {
        return () => {
                byte randomByte = (byte)r.Next(0, 256); 

                byte vx = _context.Registers.VRegisters[
                    BitManipulator.GetNibble(_context.LatestOpcode, 1)];
                byte nn = BitManipulator.BuildByte(
                        BitManipulator.GetNibble(_context.LatestOpcode, 2),
                        BitManipulator.GetNibble(_context.LatestOpcode, 3)
                );

                vx = (byte)(randomByte & nn);
        };
    }

    private OpcodeContext _context;
    private static Random r = new Random();
}
