public class CPU 
{
    public CPU(byte[] program, Display display)
    {
        _display = display;

        _bitExtractor = new BitExtractor();
        _registers = new Registers(_programStartAddress, _memorySize);
        _opcodeHandler = new OpcodeHandler();
        _opcodeContainer = new OpcodeContainer(_bitExtractor, _display);

        LoadToMemory(Font.Sprites, _fontStart);
        LoadToMemory(program);
    }

    public void ProcessInstruction() 
    {
        switch (_instructionPhase)
        {
            case Loop.Fetch:
                Fetch();
                MoveNext();
                break;
            case Loop.Decode:
                Decode();
                MoveNext();
                break;
            case Loop.Execute:
                Execute();
                MoveNext();
                break;
            default:
                break;
        }
    }

    private void LoadToMemory(byte[] programToLoad, ushort startAddress = _programStartAddress) 
    {
        ushort memoryIndex = startAddress;
        ushort programIndex = 0;

        while (programIndex < programToLoad.Length)
        {
            if (memoryIndex > _memorySize - 1)
            {
                throw new OutOfMemoryException();
            }

            _registers.Memory[memoryIndex] = programToLoad[programIndex];

            programIndex += 1;
            memoryIndex += 1;
        }
    }

    private Loop _instructionPhase = Loop.Fetch;
    private Action? _currentExecute;
    private ushort _latestOpcode = 0;

    private BitExtractor _bitExtractor;
    private Display _display;
    private Registers _registers;
    private OpcodeHandler _opcodeHandler;
    private OpcodeContainer _opcodeContainer;

    private const ushort _memorySize = 4096;
    private const ushort _programStartAddress = 0x200;
    private const byte _fontStart = 0x50;

    private void Fetch()
    {
        if (_registers.ProgramCounter > _memorySize - 1)
        {
            throw new OutOfMemoryException();
        }

        _latestOpcode = (ushort)((_registers.Memory[_registers.ProgramCounter] << 8) | _registers.Memory[_registers.ProgramCounter + 1]);

        _registers.ProgramCounter += 2;
    }

    private void Decode() 
    {
        var currentOpcodeInstance = _opcodeContainer.GetOpcodeInstance(_latestOpcode);
        if (currentOpcodeInstance is null) return;

        _opcodeContainer.SetupOpcode(currentOpcodeInstance, new OpcodeContext(_bitExtractor, _registers, _latestOpcode));
        _currentExecute = _opcodeHandler.Handle(currentOpcodeInstance);
    }

    private void Execute() 
    {
        if (_currentExecute is null)
        {
            return;
        }

        _currentExecute();
    }

    private void MoveNext()
    {
        if (_instructionPhase == Loop.Execute)
        {
            _instructionPhase = Loop.Fetch;
            return;
        }

        _instructionPhase += 1;
    }
}
