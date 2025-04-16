public class _EX9E : IContextualOpcode, IKeyObserverOpcode
{
    public byte LastPressedKeyIndex { get; set; }

    public void LoadContext(OpcodeContext context)
    {
        _context = context;
    }
    
    public void HandleKeyPress(object sender, KeyPressEventArgs e)
    {
        LastPressedKeyIndex = e.KeyIndex;
    }

    public Action Invoke()
    {
        return () => {
                byte vx = _context.Registers.VRegisters[
                    BitManipulator.GetNibble(_context.LatestOpcode, 1)];
                byte lsVxNibble = BitManipulator.GetNibble(vx, 1);

                if (LastPressedKeyIndex == lsVxNibble)
                {
                    _context.Registers.ProgramCounter += 2;
                }
        };
    }

    private OpcodeContext _context;
}
