namespace SpaceBattle.Lib;

public class Shootable : IShootable
{
    public readonly IDictionary<string, object> _Spaceship;
    public Shootable(IDictionary<string, object> Spaceship)
    {
        _Spaceship = Spaceship;
    }
    public Angle GetAngle() => (Angle)_Spaceship["Angle"];
    public Vector GetLocation() => (Vector)_Spaceship["Location"];
    public Vector GetVelocity() => (Vector)_Spaceship["Velocity"];
}
