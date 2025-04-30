namespace SpaceBattle.Lib;

public class DeleteFromGridCommand: ICommand // выполнение этой команды идет ДО UpdateGridCommand
{ // +
    public readonly string name_grid;
    public readonly IGridInfoObject obj;
    public readonly IGridDeleterElem deleter;
    public DeleteFromGridCommand(string name_grid, IGridInfoObject obj, IGridDeleterElem deleter)
    {
        this.name_grid = name_grid;
        this.obj = obj;
        this.deleter = deleter;
    }
    public void Execute()
    {
        var result = Enumerable.Range(obj.GridLocation[0] - 1, 3)
                               .SelectMany(currentX => Enumerable.Range(obj.GridLocation[1] - 1, 3),
                                           (currentX, currentY) => new int[] {currentX, currentY})
                               .ToList();
        result.ForEach(cell => deleter.Delete(cell[0], cell[1], obj.ObjectItself, name_grid));
    }
}