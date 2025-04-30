namespace SpaceBattle.Lib;

public class CollisionObject : ICollisionObject
{
    public readonly IDictionary<string, object> Object;
    public readonly IGetterGrid getterGrid;
    public CollisionObject(IDictionary<string, object> Object, IGetterGrid getterGrid)
    {
        this.Object = Object;
        this.getterGrid = getterGrid;
    }
    public IDictionary<string, object> ObjectItself => Object;
    public int[] GridLocation => getterGrid.GetGridLocation((Vector)Object["Location"]).GetCoordinates();
    public int[] AbsoluteLocation => ((Vector)Object["Location"]).GetCoordinates();
    public int[] Velocity => ((Vector)Object["Velocity"]).GetCoordinates();
}
