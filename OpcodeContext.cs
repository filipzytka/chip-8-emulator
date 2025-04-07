public class OpcodeContext 
{
    public OpcodeContext(BitExtractor bitExtractor, Registers registers, ushort latestOpcode)
    {
        BitExtractor = bitExtractor;
        Registers = registers;
        LatestOpcode = latestOpcode;
    }

    public BitExtractor BitExtractor { get; }
    public Registers Registers { get; }
    public ushort LatestOpcode { get; }
}
