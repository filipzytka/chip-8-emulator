public class _00E0 : IDisplayableOpcode
{
    private Display _display;

    public void LoadDisplay(Display display)
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

