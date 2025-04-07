public class _DXYN : IOpcode
{
    private readonly OpcodeContext _context;
    private readonly Display _display;

    public _DXYN(OpcodeContext context, Display display)
    {
        _context = context;
        _display = display;
    }

    public Action Invoke()
    {
        return () => {
            byte n = _context.BitExtractor.GetNibble(_context.LatestOpcode, 3);
            byte[] sprite = new byte[n];

            for (byte i = 0; i < n; i++)
            {
                sprite[i] = _context.Registers.Memory[_context.Registers.IndexRegister + i];
            }

            bool isCollision = _display.DrawSprite(
                    sprite,
                    _context.Registers
                        .VRegisters[_context.BitExtractor.GetNibble(_context.LatestOpcode, 1)],
                    _context.Registers
                        .VRegisters[_context.BitExtractor.GetNibble(_context.LatestOpcode, 2)]);
            if (isCollision) _context.Registers.VRegisters[0xD] = 1;

            Console.Clear();
            _display.Show();
        };
    }
}
