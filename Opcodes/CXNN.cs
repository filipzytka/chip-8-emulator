public class _CXNN : IContextualOpcode
{
    private OpcodeContext _context;
    static Random r = new Random();

    public void LoadContext(OpcodeContext context)
    {
        _context = context;
    }

    public Action Invoke() 
    {
        return () => {
            byte b = (byte)r.Next(0, 256); 
            ushort NN = (byte)(BitManipulator
                    .GetNibble(_context.LatestOpcode, 2) << 4 |
                    BitManipulator.GetNibble(_context.LatestOpcode, 3));
            _context.Registers
                .VRegisters[BitManipulator
                .GetNibble(_context.LatestOpcode, 1)] = (byte)(b & NN);
        };
    }
}
