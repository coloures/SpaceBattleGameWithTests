namespace SpaceBattle.Lib;
using App;
public class RegisterIoCDependencyMacroAuth : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Macro.Auth",
          (object[] args) =>
            new CreateMacroCommandStrategy("Auth").Resolve(args[0])).Execute();
    }
}