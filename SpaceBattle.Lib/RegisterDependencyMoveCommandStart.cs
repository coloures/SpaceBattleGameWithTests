namespace SpaceBattle.Lib;
using App;
public class RegisterDependencyMoveCommandStart : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Actions.Move.Start",
          (object[] args) =>
            new StartMoveCommand((ICommand)((IDictionary<string, object>)args[0])["Command"],
            (ISender)((IDictionary<string, object>)args[0])["Sender"],
            (IDictionary<string, object>)((IDictionary<string, object>)args[0])["Object"])).Execute();
    }
}
