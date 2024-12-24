namespace SpaceBattle.Lib;
using App;
public class RegisterDependencyMacroCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Commands.Macro",
          (object[] args) =>
            new MacroCommand((ICommand[])args)).Execute();
    }
}