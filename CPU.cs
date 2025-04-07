public delegate void InstructionToExecute();

public class CPU 
{
    public byte[] Memory { get; }
    public byte[] VRegisters { get; set; }

    public ushort ProgramCounter = _programStart;
    public ushort IndexRegister = 0;

    public CPU()
    {
        VRegisters = new byte[_vSize];
        Memory = new byte[_memSize];
        LoadFont();
        _display = new Display();
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

            Memory[memIndex] = program[programIndex];
            programIndex += 1;

            memIndex += 1;
        }
    }

    private void Fetch()
    {
        if (ProgramCounter > _memSize - 1)
        {
            throw new OutOfMemoryException();
        }

        _co = (ushort)((Memory[ProgramCounter] << 8) | Memory[ProgramCounter + 1]);

        ProgramCounter += 2;
    }

    private void Decode() 
    {
        var msbNibble = GetNibble(_co, 0);

        switch (msbNibble)
        {
            case 0x00: 
                if (_co == 0x00E0) 
                {
                    _currentExecute = () => {
                        _display.Clear();
                    };
                }
                else if (_co == 0x00EE)
                {
                    _currentExecute = () => {
                    };
                }
                break;
            case 0x1: 
                _currentExecute = () => {
                    ProgramCounter = (ushort)(
                            (GetNibble(_co, 1) << 8) |
                            (GetNibble(_co, 2) << 4) |
                            GetNibble(_co, 3)
                    );
                };
                break;
            case 0x3:
                _currentExecute = () => {
                    if (VRegisters[GetNibble(_co, 1)] == ((GetNibble(_co, 2) << 4) | GetNibble(_co, 3)))
                    {
                        ProgramCounter += 2;
                    }
                };
                break;
            case 0x4:
                _currentExecute = () => { 
                    if (VRegisters[GetNibble(_co, 1)] != ((GetNibble(_co, 2) << 4) | GetNibble(_co, 3)))
                        {
                            ProgramCounter += 2;
                        }
                    };
                break;
            case 0x5:
                _currentExecute = () => { 
                    if (VRegisters[GetNibble(_co, 1)] == VRegisters[GetNibble(_co, 2)])
                        {
                            ProgramCounter += 2;
                        }
                    };
                break;
            case 0x6:
                _currentExecute = () => {
                    VRegisters[GetNibble(_co, 1)] = (byte)((GetNibble(_co, 2) << 4) | GetNibble(_co, 3));
                };
                break;
            case 0x7:
                _currentExecute = () => {
                    VRegisters[GetNibble(_co, 1)] += (byte)((GetNibble(_co, 2) << 4) | GetNibble(_co, 3));
                };
                break;
             case 0x8:
                _currentExecute = () => {
                    if (GetNibble(_co, 3) == 0)
                    {
                        VRegisters[GetNibble(_co, 1)] = VRegisters[GetNibble(_co, 2)];
                    }
                    else if (GetNibble(_co, 3) == 1)
                    {
                        VRegisters[GetNibble(_co, 1)] |= VRegisters[GetNibble(_co, 2)];
                    }
                    else if (GetNibble(_co, 3) == 2)
                    {
                        VRegisters[GetNibble(_co, 1)] &= VRegisters[GetNibble(_co, 2)];
                    }
                };
                break;       
            case 0xA:
                _currentExecute = () => {
                    IndexRegister = (ushort)((GetNibble(_co, 1) << 8) | (GetNibble(_co, 2) << 4) | GetNibble(_co, 3));
                };
                break;
            case 0xB:
                _currentExecute = () => {
                    ProgramCounter = (ushort)((GetNibble(_co, 1) << 8) | (GetNibble(_co, 2) << 4) | GetNibble(_co, 3) + VRegisters[0]);
                };
                break;
            case 0xC:
                _currentExecute = () => {
                    Random r = new Random();
                    byte b = (byte)r.Next(0, 256); 
                    ushort NN = (byte)(GetNibble(_co, 2) << 4 | GetNibble(_co, 3));
                    VRegisters[GetNibble(_co, 1)] = (byte)(b & NN);
                };
                break;
            case 0xD:
                _currentExecute = () => {
                    VRegisters[0xF] = 0;

                    byte n = GetNibble(_co, 3);
                    byte[] sprite = new byte[n];

                    for (byte i = 0; i < n; i++)
                    {
                        sprite[i] = Memory[IndexRegister + i];
                    }

                    bool isCollision = _display.DrawSprite(sprite, VRegisters[(GetNibble(_co, 1))], VRegisters[(GetNibble(_co, 2))]);
                    if (isCollision) VRegisters[0xD] = 1;
                    Console.Clear();
                    _display.Show();
                };
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

    private byte GetNibble(ushort opcode, byte index)
    {
        switch (index)
        {
            case 0: 
                return (byte)((opcode >> 12));
            case 1:
                return (byte)((opcode & 0xF00) >> 8);
            case 2:
                return (byte)((opcode & 0xF0) >> 4);
            case 3:
                return (byte)((opcode & 0xF));
            default:
                throw new IndexOutOfRangeException();
        }
    }

    private InstructionToExecute? _currentExecute;

    private const byte _fontStart = 0x50;
    private const byte _vSize = 16;
    private ushort _co = 0;

    private Display _display;

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
            Memory[memIndex] = Font.Sprites[fontIndex];

            fontIndex += 1;
            memIndex += 1;
        }
    }
}

