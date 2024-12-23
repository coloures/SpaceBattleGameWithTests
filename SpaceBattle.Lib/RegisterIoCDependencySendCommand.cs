namespace SpaceBattle.Lib;
using App;

public class RegisterIoCDependencySendCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Commands.Send",
          (object[] args) =>
            new SendCommand((ICommand)args[0], (ISender)args[1])).Execute();
    }
}
