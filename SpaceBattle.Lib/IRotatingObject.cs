namespace SpaceBattle.Lib;

public interface IRotatingObject
{
    public Angle angle { get; set; }
    public Angle Velocity { get; }
}