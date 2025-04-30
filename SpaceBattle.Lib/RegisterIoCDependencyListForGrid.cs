using App;
namespace SpaceBattle.Lib;
public class RegisterIocDependencyListForGrid : ICommand
{
    public readonly string name_grid;
    public readonly int size_x;
    public readonly int size_y;
    public RegisterIocDependencyListForGrid(string name_grid, int size_x, int size_y)
    {
        this.name_grid = name_grid;
        this.size_x = size_x;
        this.size_y = size_y;
    }
    public void Execute()
    {
        var Grid = Enumerable.Range(0, size_x * size_y)
                                       .Select(_ => new List<IDictionary<string, object>>())
                                       .ToArray();
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            $"Grid_{name_grid}_addElem",
            (object[] args) =>
            {
                return new GridCellAddCommand(Grid, (int)args[0], (int)args[1], (IDictionary<string, object>)args[2], size_x, size_y);
            }
        ).Execute();
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            $"Grid_{name_grid}_deleteElem",
            (object[] args) =>
            {
                return new GridCellDeleteCommand(Grid, (int)args[0], (int)args[1], (IDictionary<string, object>)args[2], size_x, size_y);
            }
        ).Execute();
    }
}