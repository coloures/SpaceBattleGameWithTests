namespace SpaceBattle.Lib;
public class StartMoveCommand : ICommand
{
    private readonly ICommand _bridgeCommand;
    private readonly ISender _isender;
    private readonly IDictionary<string, object> _gameobject;
    public StartMoveCommand(ICommand bridgeCommand, ISender isender, IDictionary<string, object> gameobject)
    {
        _bridgeCommand = bridgeCommand;
        _isender = isender;
        _gameobject = gameobject;
    }
    public void Execute()
    {
        _gameobject["Movement"] = _bridgeCommand;
        try
        {
            _isender.Send(_bridgeCommand);
        }
        catch (Exception ex)
        {
            _gameobject.Remove("Movement");
            throw ex;
        }
    }
}
