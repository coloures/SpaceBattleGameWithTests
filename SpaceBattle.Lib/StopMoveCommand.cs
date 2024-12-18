namespace SpaceBattle.Lib;
public class StopMoveCommand(ISender _isender, IDictionary<string, object> _gameobject) : ICommand
{
    public void Execute()
    {
        _gameobject["Movement"] = new EmptyCommand();
        _isender.Send(new EmptyCommand());
    }
}
