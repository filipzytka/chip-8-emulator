public class OpcodeProvider 
{
    public Dictionary<string, IOpcode> OpcodeMap { get; init; }

    public OpcodeProvider(KeyPressPublisher publisher, Display display)
    {
        _publisher = publisher;
        OpcodeMap = new();
        _display = display;

        CreateOpcodeInstances();
        SubscribeObserversToPublisher();
    }

    public IOpcode? GetOpcodeInstance(ushort opcode)
    {
        string msNibble = BitManipulator.GetNibble(opcode, 0).ToHex(1);
        KeyValuePair<string, IOpcode>[] opcodeInstancePair = OpcodeMap.Where(o => o.Key[0] == msNibble[0]).ToArray();

        if (opcodeInstancePair.Length == 1)
        {
            return opcodeInstancePair.Single().Value;
        }
        
        if (opcodeInstancePair.Length > 1)
        {
            string lsNibble = BitManipulator.GetNibble(opcode, 3).ToHex(1);
            opcodeInstancePair = opcodeInstancePair.Where(o => o.Key[3] == lsNibble[0]).ToArray();

            if (opcodeInstancePair.Length == 1)
            {
                return opcodeInstancePair.Single().Value;
            }

            if (opcodeInstancePair.Length > 1)
            {
                string thirdNibble = BitManipulator.GetNibble(opcode, 2).ToHex(1);
                opcodeInstancePair = opcodeInstancePair.Where(o => o.Key[3] == lsNibble[0]).ToArray();

                return opcodeInstancePair.Where(o => o.Key[2] == thirdNibble[0]).Single().Value; 
            }
        }

        return null;
    }

    private Display _display;
    private KeyPressPublisher _publisher;

    private void CreateOpcodeInstances()
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

    private void SubscribeObserversToPublisher()
    {
        IKeyObserverOpcode[] observerInstances = OpcodeMap.Values
            .Where(oi => oi is IKeyObserverOpcode)
            .Cast<IKeyObserverOpcode>()
            .ToArray();

        foreach (var ot in observerInstances)
        {
            _publisher.KeyPressed += ot.HandleKeyPress;
        }
    }
}
