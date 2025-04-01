namespace SpaceBattle.Lib;

public class CheckForPermissionCommand : ICommand // компонента макрокоманды Macro.Auth
{
    public readonly IOwner? Player;
    public readonly IDictionary<string, object>? Order;

    public CheckForPermissionCommand(IOwner Player, IDictionary<string, object> Order)
    {
        this.Player = Player;
        this.Order = Order;
    }
    public void Execute()
    {
        if (!Player!.GetRights().Contains(Order!["Command"]))
        {
            throw new Exception();
        }
    }
}