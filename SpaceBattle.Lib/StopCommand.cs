namespace SpaceBattle.Lib;
public class StopCommand : ICommand
{
    private readonly ISender _isender;
    private readonly IDictionary<string, object> _gameobject;
    private readonly string _label;
    public StopCommand(ISender isender, IDictionary<string, object> gameobject, string label)
    {
        _isender = isender;
        _gameobject = gameobject;
        _label = label;
    }
    public void Execute()
    {
        _gameobject[_label] = new EmptyCommand();
        _isender.Send(new EmptyCommand());
        _gameobject.Remove(_label);
    }
}
