namespace SpaceBattle.Lib;

public interface ICollisionObject
{
    IDictionary<string, object> ObjectItself { get; }
    int[] GridLocation { get; }
    int[] AbsoluteLocation { get; }
    int[] Velocity { get; }
}
