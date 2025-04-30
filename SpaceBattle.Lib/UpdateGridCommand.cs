namespace SpaceBattle.Lib;
public class UpdateGridCommand : ICommand // имеем макрокоманду DeleteFromGridCommand -> UpdateGridCommand -> AddToGridCommand
{ // + все интерфейсы реализованы + все тесты сделаны
    public readonly IGridObject obj;
    public readonly IGetterGrid makerGrid;
    public UpdateGridCommand(IGridObject obj, IGetterGrid makerGrid)
    {
        this.obj = obj;
        this.makerGrid = makerGrid;
    }
    public void Execute()
    {
        obj.GridLocation = makerGrid.GetGridLocation(obj.AbsoluteLocation);
    }
}
