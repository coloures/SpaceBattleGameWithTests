namespace SpaceBattle.Lib;
public interface IMovingObject
{
    public Vector Location { get; set; }
    public Vector Velocity { get; }
}
