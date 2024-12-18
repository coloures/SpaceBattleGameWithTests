namespace SpaceBattle.Lib;
using App;
public class RegisterIocDependencyActionsStart : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Actions.Start",
          (object[] args) =>
            new StartCommand((ICommand)((IDictionary<string, object>)args[0])["Command"],
            (ISender)((IDictionary<string, object>)args[0])["Sender"],
            (IDictionary<string, object>)((IDictionary<string, object>)args[0])["Object"],
            (string)((IDictionary<string, object>)args[0])["Label"])).Execute();
    }
}
