namespace SpaceBattle.Lib;
public interface IBridgeCommand : ICommand, IInjectable
{
    public ICommand command { get; set; }
    public void Execute();

    public void Inject(ICommand cmd);
}
public class BridgeCommand : IBridgeCommand
{
    public ICommand command { get; set; }
    public BridgeCommand(ICommand _Cmd)
    {
        command = _Cmd;
    }

    public void Execute()
    {
        command.Execute();
    }
    public void Inject(ICommand cmd)
    {
        command = cmd;
    }
}