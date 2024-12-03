namespace SpaceBattle.Lib;
public class StartMoveCommand : ICommand
{
    public BridgeCommand _bridgeCommand { get; }
    public ISender _isender { get; }
    public IDictionary<string, object> _gameobject { get; }
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
