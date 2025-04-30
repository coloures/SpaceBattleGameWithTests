namespace SpaceBattle.Lib;
public class GridCellAddCommand : ICommand
{
    public readonly IList<IDictionary<string, object>>[] Grid;
    public readonly int x;
    public readonly int y;
    public readonly IDictionary<string, object> ship;
    public readonly int size_x;
    public readonly int size_y;
    public GridCellAddCommand(IList<IDictionary<string, object>>[] Grid, int x, int y, IDictionary<string, object> ship, int size_x, int size_y)
    {
        this.Grid = Grid;
        this.x = x;
        this.y = y;
        this.ship = ship;
        this.size_x = size_x;
        this.size_y = size_y;
    }
    public void Execute()
    {
        if (x <= (size_x - 1) && x >= 0 && y <= (size_y - 1) && y >= 0)
        {
            if (!Grid[x + y * size_x].Contains(ship))
            {
                Grid[x + y * size_x].Add(ship);
            }
        }
    }
}
