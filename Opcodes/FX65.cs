public class _FX65 : IContextualOpcode
{
    public void LoadContext(OpcodeContext context)
    {
        _context = context;
    }
    
    public Action Invoke()
    {
        return () => {
                byte x = BitManipulator.GetNibble(_context.LatestOpcode, 1);

                byte[] vRegisters = _context.Registers.VRegisters;
                byte[] memory = _context.Registers.Memory;
                ushort indexRegister = _context.Registers.IndexRegister;

                for (byte i = 0; i <= x; i++)
                {
                    vRegisters[i] = memory[indexRegister + i];
                }
        };
    }

    private OpcodeContext _context;
}

