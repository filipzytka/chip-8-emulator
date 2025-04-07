public class _00E0 : IOpcode
{
    private readonly Display _display;

    public _00E0(Display display)
    {
        _display = display;
    }

    public Action Invoke() 
    {
        return () => {
                    _display.Clear();
                };
    }
}

