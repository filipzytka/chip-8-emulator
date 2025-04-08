DotNetEnv.Env.Load();

string? path = System.Environment.GetEnvironmentVariable("PATH_TO_CH8");

if (path is null)
{
    throw new InvalidOperationException("Cannot find path to a ch8 file");
}

byte[] program = File.ReadAllBytes(path);

Display d = new();
CPU c = new(program, d);

MemoryDumper md = new();

while (true) 
{
    Thread.Sleep(100);
    c.ProcessInstruction();
}

