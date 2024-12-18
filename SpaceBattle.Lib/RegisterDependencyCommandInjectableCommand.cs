namespace SpaceBattle.Lib;
using App;
public class RegisterDependencyCommandInjectableCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Commands.CommandInjectable",
          (object[] args) => new BridgeCommand()
          ).Execute();
    }
}
