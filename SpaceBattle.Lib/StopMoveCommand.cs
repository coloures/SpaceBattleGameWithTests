namespace SpaceBattle.Lib;
public class StopMoveCommand : ICommand
{
    private IDictionary<string, object> _gameobject;
    public StopMoveCommand(IDictionary<string, object> gameobject)
    {
        _gameobject = gameobject;
    }
    public void Execute()
    {
        IBridgeCommand bridgeCommand = _gameobject["Movement"] as IBridgeCommand;
        bridgeCommand.Inject(new EmptyCommand());
    }

}