public class _1NNN : IContextualOpcode
{
    private OpcodeContext _context;

    public void LoadContext(OpcodeContext context)
    {
        _context = context;
    }

    public Action Invoke() 
    {
        return () => {
                    _context.Registers.ProgramCounter = (ushort)(
                            (_context.BitExtractor.GetNibble(_context.LatestOpcode, 1) << 8) |
                            (_context.BitExtractor.GetNibble(_context.LatestOpcode, 2) << 4) |
                            _context.BitExtractor.GetNibble(_context.LatestOpcode, 3)
                    );
                };
    }
}

// Decode
// dekoduje zfetchowana instrukcje (szuka w dictionary)
// jesli znajdzie, to odpala dla tej instancji metode LoadContext
// jak zaladuje kontekst, to opcode moze zostac invoke




