public class KeyPressEventArgs : EventArgs 
{
    public byte KeyIndex { get; }

    public KeyPressEventArgs(byte keyIndex)
    {
        KeyIndex = keyIndex;
    }
}
