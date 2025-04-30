namespace SpaceBattle.Lib;

public class CheckerNeighbourhood : ICheckerNeighbourhood
{

    public bool Check(ICollisionObject obj1, ICollisionObject obj2)
    {
        if (Math.Abs(obj1.GridLocation[0] - obj2.GridLocation[0]) <= 1 &&
        Math.Abs(obj1.GridLocation[1] - obj2.GridLocation[1]) <= 1)
        {
            return true;
        }

        return false;
    }
}
