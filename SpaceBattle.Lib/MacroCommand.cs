namespace SpaceBattle.Lib;

public class MacroCommand(ICommand[] commands) : ICommand
{
    public void Execute()
    {
        Array.ForEach(commands, (cmd) => cmd.Execute());
    }
}
