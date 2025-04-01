namespace SpaceBattle.Lib;

public class CheckForAbilitiesCommand : ICommand // компонента макрокоманды Macro.Auth
{
    public readonly IObject? Ship;
    public readonly IDictionary<string, object>? Order;

    public CheckForAbilitiesCommand(IObject Ship, IDictionary<string, object> Order) // Order**
    {
        this.Ship = Ship;
        this.Order = Order;
    }
    public void Execute()
    {
        if (!Ship!.GetAbilities().Contains(Order!["Command"]))
        {
            throw new Exception();
        }
    }
}
