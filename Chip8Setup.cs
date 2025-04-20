public class Chip8Setup
{
    public Chip8Setup()
    {
        _chip8Programs = new();
    }

    public byte[] Start()
    {
        PlayIntro();
        FindPrograms();
        string programName = SelectProgram();
        return File.ReadAllBytes(programName);
    }

    private void FindPrograms()
    {
        ushort index = 0;
        foreach (var file in Directory.EnumerateFiles(
                    "./", "*.ch8", SearchOption.AllDirectories))
        {
            Console.WriteLine($"({index + 1}) {file}");
            _chip8Programs.Add(file);
            index += 1;
        }

        Console.WriteLine($"Total program count: {_chip8Programs.Count}");
    }

    private string SelectProgram()
    {
        Console.WriteLine($"Select program to run on Chip8 Emulator (1 - {_chip8Programs.Count})");

        while (true)
        {
            var userInput = Console.ReadLine();
            if (!int.TryParse(userInput, out int userProgramChoice))
            {
                continue;
            }

            var programName = _chip8Programs.ElementAtOrDefault(userProgramChoice - 1);
            if (programName is null)
            {
                continue;
            }

            return programName;
        }
    }

    private void PlayIntro()
    {
        Console.Clear();
        Console.WriteLine(@"
                                                   ,---.-,    
                                                  '   ,'  '.  
            ,---,                                /   /      \ 
          ,--.' |      ,--,   ,-.----.          .   ;  ,/.  : 
          |  |  :    ,--.'|   \    /  \         '   |  | :  ; 
          :  :  :    |  |,    |   :    |        '   |  ./   : 
   ,---.  :  |  |,--.`--'_    |   | .\ :        |   :       , 
  /     \ |  :  '   |,' ,'|   .   : |: |         \   \     /  
 /    / ' |  |   /' :'  | |   |   |  \ :          ;   ,   '\  
.    ' /  '  :  | | ||  | :   |   : .  |         /   /      \ 
'   ; :__ |  |  ' | :'  : |__ :     |`-'        .   ;  ,/.  : 
'   | '.'||  :  :_:,'|  | '.'|:   : :           '   |  | :  ; 
|   :    :|  | ,'    ;  :    ;|   | :           '   |  ./   : 
 \   \  / `--''      |  ,   / `---'.|           |   :      /  
  `----'              ---`-'    `---`            \   \   .'   
                                                  `---`-'   
        ");

        Thread.Sleep(2000);

        Console.WriteLine("Looking for games in the Chip8 directory...");

        Thread.Sleep(2000);
    }

    private readonly List<string> _chip8Programs;
}
