namespace SpaceBattle.Lib;
using App;
public class RegisterIocDependencyActionsStop : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
        "IoC.Register",
        "Actions.Stop",
        (object[] args) =>
        new StopCommand((ISender)((IDictionary<string, object>)args[0])["Sender"],
        (IDictionary<string, object>)((IDictionary<string, object>)args[0])["Object"],
        (string)((IDictionary<string, object>)args[0])["Label"])).Execute();
    }
}