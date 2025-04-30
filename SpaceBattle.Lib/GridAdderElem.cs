namespace SpaceBattle.Lib;
using App;

public class GridAdderElem: IGridAdderElem
{
    public void Add(int x, int y, IDictionary<string, object> ObjectItself, string name_grid)
    {
        Ioc.Resolve<ICommand>($"Grid_{name_grid}_addElem", x, y, ObjectItself).Execute();
    }
}