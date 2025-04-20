using System.Diagnostics;

public class Chip8Emulator
{
    public bool IsDebugMode { get; set; } = false;

    public Chip8Emulator(byte[] program, bool debugMode = false)
    {
        Console.Clear();
        IsDebugMode = debugMode;

        _publisher = new KeyPressPublisher();
        _display = new Display();
        _cpu = new(program, _display, _publisher);
        _timerStopwatch = Stopwatch.StartNew();
        _instructionStopwatch = Stopwatch.StartNew();
    }

    public void Run()
    {
        while (true) 
        {
            if (_timerStopwatch.ElapsedMilliseconds >= _timerTick)
            {
                _timerStopwatch.Restart();
                _cpu.DecrementTimers();
            }
            
            if (_instructionStopwatch.ElapsedMilliseconds >= _instructionTick)
            {
                _instructionStopwatch.Restart();
                _cpu.ProcessInstruction();
                if (IsDebugMode) _cpu.Logger.LogRegisters();
            }

            var keycode = Keyboard.ReadKeyCode();
            if (keycode != -1)
            {
                _publisher.PublishKeyPress((byte)keycode);
            }
        }
    }

    private readonly CPU _cpu;
    private readonly KeyPressPublisher _publisher;
    private readonly Display _display;
    private readonly Stopwatch _timerStopwatch;
    private readonly Stopwatch _instructionStopwatch;
    private const ushort _timerTick = 16;
    private const ushort _instructionTick = 30;

}
