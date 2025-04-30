namespace SpaceBattle.Lib;

public class AddToGridCommand: ICommand // +
{
    public readonly string name_grid;
    public readonly IGridInfoObject obj;
    public readonly IGridAdderElem adder;
    public AddToGridCommand(string name_grid, IGridInfoObject obj, IGridAdderElem adder)
    {
        this.name_grid = name_grid;
        this.obj = obj;
        this.adder = adder;
    }
    public void Execute()
    {
        var result = Enumerable.Range(obj.GridLocation[0] - 1, 3)
                               .SelectMany(currentX => Enumerable.Range(obj.GridLocation[1] - 1, 3),
                                           (currentX, currentY) => new int[] {currentX, currentY})
                               .ToList();
        result.ForEach(cell => adder.Add(cell[0], cell[1], obj.ObjectItself, name_grid));
    }
}