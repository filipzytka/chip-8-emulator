public class CPU 
{
    public CPU()
    {
        _bitExtractor = new BitExtractor();
        _registers = new Registers(_programStart, _memSize);
        _display = new Display();
        _opcodeExecuter = new OpcodeExecuter();
        LoadFont();
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

    public void LoadProgram(byte[] program) 
    {
        ushort memIndex = _programStart;
        ushort programIndex = 0;
        
        while (programIndex < program.Length)
        {
            if (memIndex > _memSize - 1)
            {
                throw new OutOfMemoryException();
            }

            _registers.Memory[memIndex] = program[programIndex];
            programIndex += 1;

            memIndex += 1;
        }
    }

    private void Fetch()
    {
        if (_registers.ProgramCounter > _memSize - 1)
        {
            throw new OutOfMemoryException();
        }

        _latestOpcode = (ushort)((_registers.Memory[_registers.ProgramCounter] << 8) | _registers.Memory[_registers.ProgramCounter + 1]);

        _registers.ProgramCounter += 2;
    }

    private void Decode() 
    {
        var msbNibble = _bitExtractor.GetNibble(_latestOpcode, 0);

        switch (msbNibble)
        {
            case 0x00: 
                if (_latestOpcode == 0x00E0) 
                {
                    _currentExecute = _opcodeExecuter.Execute(new _00E0(_display));
                }
                else if (_latestOpcode == 0x00EE)
                {
                    _currentExecute = _opcodeExecuter.Execute(new _00EE());
                }
                break;
            case 0x1: 
                _context = new(_bitExtractor, _registers, _latestOpcode);
                _currentExecute = _opcodeExecuter.Execute(new _1NNN(_context));
                break;
            case 0x3:
                _context = new(_bitExtractor, _registers, _latestOpcode);
                _currentExecute = _opcodeExecuter.Execute(new _3XNN(_context));
                break;
            case 0x4:
                _context = new(_bitExtractor, _registers, _latestOpcode);
                _currentExecute = _opcodeExecuter.Execute(new _4XNN(_context));
                break;
            case 0x5:
                _context = new(_bitExtractor, _registers, _latestOpcode);
                _currentExecute = _opcodeExecuter.Execute(new _5XY0(_context));
                break;
            case 0x6:
                _context = new(_bitExtractor, _registers, _latestOpcode);
                _currentExecute = _opcodeExecuter.Execute(new _6XNN(_context));
                break;
            case 0x7:
                _context = new(_bitExtractor, _registers, _latestOpcode);
                _currentExecute = _opcodeExecuter.Execute(new _7XNN(_context));
                break;
             case 0x8:
                    if (_bitExtractor.GetNibble(_latestOpcode, 3) == 0)
                    {
                        _context = new(_bitExtractor, _registers, _latestOpcode);
                        _currentExecute = _opcodeExecuter.Execute(new _8XY0(_context));
                    }
                    else if (_bitExtractor.GetNibble(_latestOpcode, 3) == 1)
                    {
                        _context = new(_bitExtractor, _registers, _latestOpcode);
                        _currentExecute = _opcodeExecuter.Execute(new _8XY1(_context));
                    }
                    else if (_bitExtractor.GetNibble(_latestOpcode, 3) == 2)
                    {
                        _context = new(_bitExtractor, _registers, _latestOpcode);
                        _currentExecute = _opcodeExecuter.Execute(new _8XY2(_context));
                    }
                break;       
            case 0xA:
                _context = new(_bitExtractor, _registers, _latestOpcode);
                _currentExecute = _opcodeExecuter.Execute(new _ANNN(_context));
                break;
            case 0xB:
                _context = new(_bitExtractor, _registers, _latestOpcode);
                _currentExecute = _opcodeExecuter.Execute(new _BNNN(_context));
                break;
            case 0xC:
                _context = new(_bitExtractor, _registers, _latestOpcode);
                _currentExecute = _opcodeExecuter.Execute(new _CXNN(_context));
                break;
            case 0xD:
                _context = new(_bitExtractor, _registers, _latestOpcode);
                _currentExecute = _opcodeExecuter.Execute(new _DXYN(_context, _display));
                break;
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

    private Action? _currentExecute;

    private const byte _fontStart = 0x50;
    private ushort _latestOpcode = 0;

    private BitExtractor _bitExtractor;
    private Display _display;
    private Registers _registers;
    private OpcodeExecuter _opcodeExecuter;
    private OpcodeContext? _context;

    private const ushort _memSize = 4096;
    private const ushort _programStart = 0x200;
    
    private Loop _instructionPhase = Loop.Fetch;

    private void MoveNext()
    {
        if (_instructionPhase == Loop.Execute)
        {
            _instructionPhase = Loop.Fetch;
            return;
        }

        _instructionPhase += 1;
    }

    private void LoadFont() 
    {
        ushort memIndex = _fontStart, fontIndex = 0;
        
        while (fontIndex < Font.Sprites.Length)
        {
            _registers.Memory[memIndex] = Font.Sprites[fontIndex];

            fontIndex += 1;
            memIndex += 1;
        }
    }
}

