Chip8Setup setup = new();
var program = setup.Start();

Chip8Emulator emulator = new(program, true);
emulator.Run();
