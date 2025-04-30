using SpaceBattle.Lib;
namespace SpaceBattle.Tests;

public class GridCellDeleteCommandTest
{
    [Fact]
    public void TestPositive()
    {
        var ship = new Dictionary<string, object>();
        var grid = new List<IDictionary<string, object>>[2];
        grid[0] = new List<IDictionary<string, object>>() { ship };

        var cmd = new GridCellDeleteCommand(grid, 0, 0, ship, 2, 1);

        cmd.Execute();

        Assert.Empty(grid[0]);
    }
}
