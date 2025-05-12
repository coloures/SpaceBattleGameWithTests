namespace SpaceBattle.Lib;

public interface ICheckerNeighbourhood
{
    bool Check(ICollisionObject obj1, ICollisionObject obj2);
}
