namespace SpaceBattle.Lib;
using App;
public class RegisterDependencyCommandInjectableCommand : ICommand
{
    public void Execute()
    {
        var RegisterDependencyCommandInjectableCommand = Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Commands.CommandInjectable",
          (object[] args) => new BridgeCommand()
          );
        RegisterDependencyCommandInjectableCommand.Execute();
    }
}
