namespace SpaceBattle.Lib;
using App;
public class RegisterIoCDependencyMacroMove : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Macro.Move",
          (object[] args) =>
            new CreateMacroCommandStrategy("Move").Resolve(args[0])).Execute();
    }
}
