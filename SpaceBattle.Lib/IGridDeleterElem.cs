namespace SpaceBattle.Lib;
public interface IGridDeleterElem
{
    void Delete(int x, int y, IDictionary<string, object> ObjectItself, string name_grid);
}
