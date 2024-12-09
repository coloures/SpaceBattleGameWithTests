namespace SpaceBattle.Lib;
public class Move : ICommand
{
    public readonly IMovingObject MovingObject;
    public Move(IMovingObject MovingObject)
    {
        this.MovingObject = MovingObject;
    }
    public void Execute()
    {
        var NewLocation = MovingObject.Location + MovingObject.Velocity;
        MovingObject.Location = NewLocation;
    }
}
