namespace SpaceBattle.Lib;
public class GetterGrid: IGetterGrid
{
    public readonly int size_x;
    public readonly int size_y;
    public readonly int shift_x;
    public readonly int shift_y;
    public GetterGrid(IDictionary<string, object> Grid)
    {
        size_x = (int)Grid["size_x"];
        size_y = (int)Grid["size_y"];
        shift_x = (int)Grid["shift_x"];
        shift_y = (int)Grid["shift_y"];
    }
    public Vector GetGridLocation(Vector location)
    {
        var location_ints = location.GetCoordinates();
        return new Vector((location_ints[0] - shift_x)/size_x, (location_ints[1] - shift_y)/size_y);
    }
}