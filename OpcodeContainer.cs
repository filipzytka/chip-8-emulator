public class OpcodeContainer 
{
    public Dictionary<ushort, IOpcode> OpcodeMap { get; init; }

    public OpcodeContainer()
    {
        OpcodeMap = new();
        GetOpcodesFromAssembly();
    }

    private void GetOpcodesFromAssembly()
    {
        var opcodeInterface = typeof(IOpcode);
        var opcodeTypes = AppDomain.CurrentDomain.GetAssemblies() 
        .SelectMany(a => a.GetTypes()) 
        .Where(t => opcodeInterface.IsAssignableFrom(t)); 

        foreach (var t in opcodeTypes)
        {
            var isParsed = ushort.TryParse(t.Name, out ushort opcode);
            if (!isParsed)
            {
                throw new Exception();
            }

            IOpcode instance = (IOpcode)Activator.CreateInstance(t)!;
            if (instance is null)
            {
                throw new Exception();
            }

            OpcodeMap.Add(opcode, instance);
        }
    }
}
