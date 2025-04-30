namespace SpaceBattle.Lib;

public interface IGridInfoObject // с помощью адаптера создаётся
{
    IDictionary<string, object> ObjectItself{get;}
    int[] GridLocation {get;}
}