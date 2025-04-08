public class OpcodeHandler 
{
    public Action Handle(IOpcode opcode)
    {
        return opcode.Invoke();
    }
}

