namespace SpaceBattle.Lib;

public class Torpedo : ITorpedo
{
    public readonly IDictionary<string, object> _torpedo;
    public Torpedo(IDictionary<string, object> Torpedo)
    {
        _torpedo = Torpedo;
    }
    public void SetAngle(Angle angle)
    {
        if (_torpedo.ContainsKey("Angle"))
        {
            _torpedo["Angle"] = angle;
        }
        else
        {
            _torpedo.Add("Angle", angle);
        }
    }
    public void SetLocation(Vector location)
    {
        if (_torpedo.ContainsKey("Location"))
        {
            _torpedo["Location"] = location;
        }
        else
        {
            _torpedo.Add("Location", location);
        }
    }
    public void SetVelocity(Vector velocity)
    {
        if (_torpedo.ContainsKey("Velocity"))
        {
            _torpedo["Velocity"] = velocity;
        }
        else
        {
            _torpedo.Add("Velocity", velocity);
        }
    }
}
