namespace SpaceBattle.Lib;
public class GridObject : IGridObject
{
    public readonly IDictionary<string, object> Ship;
    public Vector GridLocationVector;
    public GridObject(IDictionary<string, object> Ship)
    {
        this.Ship = Ship;
    }
    public Vector AbsoluteLocation => (Vector)Ship["Location"];
    public Vector GridLocation { set => GridLocationVector = value; }
}
