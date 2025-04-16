DotNetEnv.Env.Load();

string? path = System.Environment.GetEnvironmentVariable("PATH_TO_CH8");

if (path is null)
{
    throw new InvalidOperationException("Cannot find path to a ch8 file"); 
}

byte[] program = File.ReadAllBytes(path);

KeyPressPublisher publisher = new();
Display display = new();
CPU c = new(program, display, publisher);

MemoryDumper md = new();

while (true) 
{
    Thread.Sleep(70);
    c.ProcessInstruction();

    var i = Keyboard.GetPressedKeyIndex();
    if (i != -1)
    {
        publisher.PublishKeyPress((byte)i);
    }
}
