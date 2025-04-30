namespace SpaceBattle.Lib;

public class CheckCollisionBetweenTwoElemsCommand : ICommand
{
    public readonly ICollisionObject? First_Ship;
    public readonly ICollisionObject? Second_Ship;
    public readonly ICheckerNeighbourhood? Checker;
    public readonly ITreeChecker Tree;

    public CheckCollisionBetweenTwoElemsCommand(ICollisionObject First_Ship, ICollisionObject Second_Ship, ICheckerNeighbourhood Checker, ITreeChecker Tree) // Order**
    {
        this.First_Ship = First_Ship;
        this.Second_Ship = Second_Ship;
        this.Checker = Checker;
        this.Tree = Tree;
    }
    public void Execute()
    {
        if (Checker!.Check(First_Ship!, Second_Ship!))
        {
            var x = Second_Ship!.AbsoluteLocation[0] - First_Ship!.AbsoluteLocation[0];
            var y = Second_Ship!.AbsoluteLocation[1] - First_Ship!.AbsoluteLocation[1];
            var vel_x = Second_Ship!.Velocity[0] - First_Ship!.Velocity[0];
            var vel_y = Second_Ship!.Velocity[1] - First_Ship!.Velocity[1];
            Tree.Check(x, y, vel_x, vel_y);
        }
    }
}
