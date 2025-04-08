public class OpcodeContext 
{
    public OpcodeContext(BitManipulator bitExtractor, Registers registers, ushort latestOpcode)
    {
        BitManipulator = bitExtractor;
        Registers = registers;
        LatestOpcode = latestOpcode;
    }

    public BitManipulator BitManipulator { get; }
    public Registers Registers { get; }
    public ushort LatestOpcode { get; }
}
