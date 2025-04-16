public class _FX33 : IContextualOpcode
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

                byte hundreds = (byte)(vx / 100);
                byte tens = (byte)((vx / 10) % 10);
                byte units = (byte)(vx % 10);

                byte[] memory = _context.Registers.Memory;
                ushort i = _context.Registers.IndexRegister;

                memory[i] = hundreds;
                memory[i + 1] = tens;
                memory[i + 2] = units;
        };
    }

    private OpcodeContext _context;
}
