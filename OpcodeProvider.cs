public class OpcodeProvider 
{
    public Dictionary<string, IOpcode> OpcodeMap { get; init; }

    public OpcodeProvider(Display display)
    {
        OpcodeMap = new();
        _display = display;

        GetOpcodesFromAssembly();
    }

    public IOpcode? GetOpcodeInstance(ushort opcode)
    {
        string msNibble = BitManipulator.GetNibble(opcode, 0).ToHex(1);
        KeyValuePair<string, IOpcode>[] foundOpcodes = OpcodeMap.Where(o => o.Key[0] == msNibble[0]).ToArray();

        if (foundOpcodes.Length == 1)
        {
            return foundOpcodes[0].Value;
        }

        if (foundOpcodes.Length > 1)
        {
            string lsNibble = BitManipulator.GetNibble(opcode, 3).ToHex(1);
            IOpcode finalOpcode = foundOpcodes.Where(o => o.Key[3] == lsNibble[0]).Single().Value;

            return finalOpcode;
        }

        return null;
    }

    public void GetOpcodesFromAssembly()
    {
        var opcodeInterface = typeof(IOpcode);
        var opcodeTypes = AppDomain.CurrentDomain.GetAssemblies() 
        .SelectMany(a => a.GetTypes()) 
        .Where(t => opcodeInterface.IsAssignableFrom(t)
                && !t.IsAbstract);

        foreach (var t in opcodeTypes)
        {
            var opcodeNameNoPrefix = t.Name.Replace("_", "");

            var instance = (IOpcode?)Activator.CreateInstance(t);
            if (instance is null)
            {
                throw new Exception();
            }

            OpcodeMap.Add(opcodeNameNoPrefix, instance);
        }
    }

    private Display _display;
}
