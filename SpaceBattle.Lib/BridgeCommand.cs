namespace SpaceBattle.Lib;
public class BridgeCommand : ICommand, IInjectable
{
    private ICommand? command;
    public void Execute()
    {
        command!.Execute();
    }
    public void Inject(ICommand cmd)
    {
        command = cmd;
    }
}
