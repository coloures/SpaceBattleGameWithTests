namespace SpaceBattle.Lib;
using App;
public class GridDeleterElem : IGridDeleterElem
{
    public void Delete(int x, int y, IDictionary<string, object> ObjectItself, string name_grid)
    {
        Ioc.Resolve<ICommand>($"Grid_{name_grid}_deleteElem", x, y, ObjectItself).Execute();
    }
}
