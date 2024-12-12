namespace SpaceBattle.Lib;
public class StopTurningCommand(ISender _isender, IDictionary<string, object> _gameobject) : ICommand
{
    public void Execute()
    {
        _gameobject["Turn"] = new EmptyCommand();
        _isender.Send(new EmptyCommand());
    }
}
