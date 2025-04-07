public class OpcodeExecuter 
{
    public Action Execute(IOpcode opcode)
    {
        return opcode.Invoke();
    }
}

