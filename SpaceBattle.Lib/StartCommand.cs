namespace SpaceBattle.Lib;
public class StartCommand : ICommand
{
    private readonly ICommand _bridgeCommand;
    private readonly ISender _isender;
    private readonly IDictionary<string, object> _gameobject;
    private readonly string _label;
    public StartCommand(ICommand bridgeCommand, ISender isender, IDictionary<string, object> gameobject, string Label)
    {
        _bridgeCommand = bridgeCommand;
        _isender = isender;
        _gameobject = gameobject;
        _label = Label;
    }
    public void Execute()
    {
        _gameobject[_label] = _bridgeCommand;
        try
        {
            _isender.Send(_bridgeCommand);
        }
        catch (Exception)
        {
            _gameobject.Remove(_label);
        }
    }
}