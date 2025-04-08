public class OpcodeContext 
{
    public BitManipulator BitManipulator { get; }
    public Registers Registers { get; }
    public ushort LatestOpcode { get; }

    public OpcodeContext(BitManipulator bitExtractor, Registers registers, ushort latestOpcode)
    {
        BitManipulator = bitExtractor;
        Registers = registers;
        LatestOpcode = latestOpcode;
    }
}
