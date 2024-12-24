using App;
namespace SpaceBattle.Lib;

public class RegisterDependencyRotateCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>("IoC.Register", "Commands.Rotate",
            (object[] args) => new Rotate(Ioc.Resolve<IRotatingObject>("Adapters.IRotatingObject", args[0]))).Execute();
    }
}
