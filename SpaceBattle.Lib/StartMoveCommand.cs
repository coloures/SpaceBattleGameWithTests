namespace SpaceBattle.Lib;
public class StartMoveCommand : ICommand
{
    private readonly BridgeCommand _bridgeCommand;
    private readonly ISender _isender;
    private readonly IDictionary<string, object> _gameobject;
    public StartMoveCommand(BridgeCommand bridgeCommand, ISender isender, IDictionary<string, object> gameobject)
    {
        _bridgeCommand = bridgeCommand;
        _isender = isender;
        _gameobject = gameobject;
    }
    public void Execute()
    {
        _gameobject["Movement"] = _bridgeCommand;
        _isender.Send(_bridgeCommand);
    }
}
