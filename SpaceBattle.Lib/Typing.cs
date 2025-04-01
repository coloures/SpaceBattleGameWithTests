namespace SpaceBattle.Lib;

public class Typing : ITyping
{
    public readonly IDictionary<string, object> _spaceship;
    public string Type
    {
        set
        {
            if (!_spaceship.ContainsKey("Type"))
            {
                _spaceship.Add("Type", value);
            }
            else
            {
                _spaceship["Type"] = value;
            }
        }
    }
    public Typing(IDictionary<string, object> SpaceShip)
    {
        _spaceship = SpaceShip;
    }
}
