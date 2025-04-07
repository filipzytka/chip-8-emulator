DotNetEnv.Env.Load();

string? path = System.Environment.GetEnvironmentVariable("PATH_TO_CH8");

if (path is null)
{
    throw new InvalidOperationException("Cannot find path to a ch8 file");
}

byte[] program = File.ReadAllBytes(path);

CPU c = new();

MemoryDumper md = new();
Display d = new();
c.LoadProgram(program);

while (true) 
{
    Thread.Sleep(100);
    c.ProcessInstruction();
}

