namespace SpaceBattle.Lib;
public class CheckForPermissionsToUseShipCommand : ICommand // компонента макрокоманды Macro.Auth
{
    public readonly List<string> PlayerShipsId;
    public readonly string SpaceShipId;
    public CheckForPermissionsToUseShipCommand(object PlayerShipsId, object SpaceShipId)
    {
        this.PlayerShipsId = (List<string>)PlayerShipsId;
        this.SpaceShipId = (string)SpaceShipId;
    }
    public void Execute()
    {
        if (!PlayerShipsId.Contains(SpaceShipId))
        {
            throw new Exception();
        }
    }
}
