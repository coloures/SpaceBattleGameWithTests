namespace SpaceBattle.Lib;
using App;
public class RegisterIoCDependencyMacroUpdateGrid : ICommand
{ // +
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Macro.UpdateGrid",
          (object[] args) =>
            new CreateMacroCommandStrategy("UpdateGrid").Resolve(args[0])).Execute();
    }
}
