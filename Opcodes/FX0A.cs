public class _FX0A : IContextualOpcode, IKeyObserverOpcode
{
    public void LoadContext(OpcodeContext context)
    {
        _context = context;
    }
    
    public void HandleKeyPress(object sender, KeyPressEventArgs e)
    {
        if (_isWaitingForKeyPress)
        {
           ref byte vx = ref _context.Registers.VRegisters[
                BitManipulator.GetNibble(_context.LatestOpcode, 1)];

           vx = e.KeyIndex;

            _isWaitingForKeyPress = false;
            CPU.IsExecutionPaused = false;
        }
    }

    public Action Invoke()
    {
        return () => {
               _isWaitingForKeyPress = true;
               CPU.IsExecutionPaused = true;
        };
    }

    private OpcodeContext _context;
    private bool _isWaitingForKeyPress = false;
}
