namespace SpaceBattle.Lib;
using App;
public class RegisterIoCDependencyMacroShoot : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Macro.Shoot",
          (object[] args) =>
            new CreateMacroCommandStrategy("Shoot").Resolve(args[0])).Execute();
    }
}
