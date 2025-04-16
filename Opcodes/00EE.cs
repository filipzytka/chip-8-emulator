public class _00EE : IContextualOpcode
{
    private OpcodeContext _context;

    public void LoadContext(OpcodeContext context)
    {
        _context = context;
    }

    public Action Invoke() 
    {
        return () => {
                ushort address = _context.Registers.Stack.Pop();
                _context.Registers.ProgramCounter = address;
        };
    }
}
