namespace SpaceBattle.Lib;
using App;
public class RegisterIoCDependencyMacroRotate : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Macro.Rotate",
          (object[] args) =>
            new CreateMacroCommandStrategy("Rotate").Resolve(args[0])).Execute();
    }
}
