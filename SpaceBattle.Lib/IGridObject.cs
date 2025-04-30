namespace SpaceBattle.Lib;
public interface IGridObject
{
    Vector AbsoluteLocation { get; }
    Vector GridLocation { set; }
}
