public class CPU 
{
    public CPU(byte[] program, Display display)
    {
        _display = display;

        _registers = new Registers(_programStartAddress, _memorySize);
        _opcodeHandler = new OpcodeHandler();
        _opcodeProvider = new OpcodeProvider(_display);

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

    private Loop _instructionPhase = Loop.Fetch;
    private Action? _currentExecute;
    private ushort _latestOpcode = 0;

    private Display _display;
    private Registers _registers;
    private OpcodeHandler _opcodeHandler;
    private OpcodeProvider _opcodeProvider;

    private const ushort _memorySize = 4096;
    private const ushort _programStartAddress = 0x200;
    private const byte _fontStart = 0x50;

    private enum Loop
    {
        Fetch,
        Decode,
        Execute
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

    private void Fetch()
    {
        if (_registers.ProgramCounter > _memorySize - 1)
        {
            throw new OutOfMemoryException();
        }

        byte msByte = _registers.Memory[_registers.ProgramCounter];
        byte lsByte = _registers.Memory[_registers.ProgramCounter + 1];

        _latestOpcode = BitManipulator.ToUShort(msByte, lsByte);

        _registers.ProgramCounter += 2;
    }

    private void Decode() 
    {
        var opcodeInstance = _opcodeProvider.GetOpcodeInstance(_latestOpcode);
        if (opcodeInstance is null) return;

        LoadContext(opcodeInstance);
        LoadDisplay(opcodeInstance);

        _currentExecute = _opcodeHandler.Handle(opcodeInstance);
    }

    private void LoadContext(IOpcode opcodeInstance)
    {
        if (opcodeInstance is IContextualOpcode contextualOpcode)
        {
            OpcodeContext context = new(_registers, _latestOpcode);
            contextualOpcode.LoadContext(context);
        }
    }

    private void LoadDisplay(IOpcode opcodeInstance)
    {
        if (opcodeInstance is IDisplayableOpcode displayableOpcode)
        {
            displayableOpcode.LoadDisplay(_display);
        }
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
