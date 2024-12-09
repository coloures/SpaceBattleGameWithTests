namespace SpaceBattle.Lib;
public class StartTurningCommand(ICommand _macrocommand, ISender _isender, IDictionary<string, object> _gameobject) : ICommand
{
    public void Execute()
    {
        _gameobject["Turn"] = _macrocommand;
        _isender.Send(_macrocommand);
    }
}
