public class _DXYN : IContextualOpcode, IDisplayableOpcode
{
    private OpcodeContext _context;
    private Display _display;

    public void LoadContext(OpcodeContext context)
    {
        _context = context;
    }

    public void LoadDisplay(Display display)
    {
        _display = display;
    }

    public Action Invoke()
    {
        return () => {
                //_display.Clear();
                byte n = BitManipulator.GetNibble(_context.LatestOpcode, 3);
                byte vx = _context.Registers.VRegisters[
                    BitManipulator.GetNibble(_context.LatestOpcode, 1)];
                byte vy = _context.Registers.VRegisters[
                    BitManipulator.GetNibble(_context.LatestOpcode, 2)];

                byte[] sprite = new byte[n];

                for (byte i = 0; i < n; i++)
                {
                    sprite[i] = _context.Registers.Memory[
                        _context.Registers.IndexRegister + i];
                }

                bool isCollision = _display.DrawSprite(sprite, vx, vy);
                if (isCollision) 
                {
                    _context.Registers.VRegisters[0xF] = 1;
                }
                else
                    _context.Registers.VRegisters[0xF] = 0;

                _display.Show();
        };
    }
}
