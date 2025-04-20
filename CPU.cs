public class CPU 
{
    public static bool IsExecutionPaused { get; set; } = false;
    public DebugLogger Logger { get; }

    public CPU(byte[] program, Display display, KeyPressPublisher publisher)
    {
        _display = display;
        _publisher = publisher;

        _registers = new Registers(_programStartAddress, _memorySize);
        _opcodeHandler = new OpcodeHandler();
        _opcodeProvider = new OpcodeProvider(_publisher, _display);

        Logger = new DebugLogger(_registers);

        LoadToMemory(Font.Sprites, Font.StartAddress);
        LoadToMemory(program);
    }

    public void ProcessInstruction() 
    {
        switch (_instructionPhase)
        {
            case ExecutionPhase.Fetch:
                Fetch();
                IncrementProgramCounter();
                MoveNext();
                break;
            case ExecutionPhase.Decode:
                Decode();
                MoveNext();
                break;
            case ExecutionPhase.Execute:
                Execute();
                MoveNext();
                break;
            default:
                break;
        }
    }

    private ExecutionPhase _instructionPhase = ExecutionPhase.Fetch;
    private Action? _currentExecute;
    private ushort _latestOpcode = 0;

    private Display _display;
    private Registers _registers;
    private OpcodeHandler _opcodeHandler;
    private OpcodeProvider _opcodeProvider;
    private KeyPressPublisher _publisher;

    private const ushort _memorySize = 4096;
    private const ushort _programStartAddress = 0x200;

    private enum ExecutionPhase
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

        _latestOpcode = BitManipulator.Build2Bytes(msByte, lsByte);
    }

    private void Decode() 
    {
        var opcodeInstance = _opcodeProvider.GetOpcodeInstance(_latestOpcode);
        if (opcodeInstance is null)
        {
            return;
        }

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
        if (_instructionPhase == ExecutionPhase.Execute)
        {
            _instructionPhase = ExecutionPhase.Fetch;
            return;
        }

        _instructionPhase += 1;
    }

    private void IncrementProgramCounter()
    {
        if (!IsExecutionPaused)
        {
            _registers.ProgramCounter += 2;
        }
    }

    public void DecrementTimers()
    {
        if (_registers.DelayTimer > 0)
        {
            _registers.DelayTimer -= 1;
        }

        if (_registers.SoundTimer > 0)
        {
            Console.Beep();
            _registers.SoundTimer -= 1; 
        }
    }
}
