namespace SpaceBattle.Lib;

public class TypingTorpedoCommand : ICommand
{
    private readonly ITyping? Torpedo;
    public TypingTorpedoCommand(object Torpedo)
    {
        this.Torpedo = (ITyping)Torpedo;
    }
    public void Execute()
    {
        Torpedo!.Type = "Torpedo";
    }
}
