namespace SpaceBattle.Lib;

public class SetupTorpedoCommand : ICommand
{
    public readonly IShootable? SpaceShip;
    public readonly ITorpedo? Torpedo;
    public SetupTorpedoCommand(object Torpedo, object SpaceShip)
    {
        this.SpaceShip = (IShootable)SpaceShip;
        this.Torpedo = (ITorpedo)Torpedo;
    }
    public void Execute()
    {
        Torpedo!.SetAngle(SpaceShip!.GetAngle());
        Torpedo.SetLocation(SpaceShip.GetLocation());
        Torpedo.SetVelocity(SpaceShip.GetVelocity());
    }
}
