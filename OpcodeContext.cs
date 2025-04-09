public class OpcodeContext 
{
    public Registers Registers { get; }
    public ushort LatestOpcode { get; }

    public OpcodeContext(Registers registers, ushort latestOpcode)
    {
        Registers = registers;
        LatestOpcode = latestOpcode;
    }
}
