public class KeyPressPublisher
{
    public event EventHandler<KeyPressEventArgs>? KeyPressed;

    public void PublishKeyPress(byte keyIndex)
    {
        OnKeyPressed(new KeyPressEventArgs(keyIndex));
    }

    private void OnKeyPressed(KeyPressEventArgs e)
    {
        if (KeyPressed is null)
        {
            return;
        }

        KeyPressed.Invoke(this, e);
    }
}
