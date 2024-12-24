namespace SpaceBattle.Lib;
using App;
public class RegisterDependencyCommandMoveCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>("IoC.Register", "Commands.Move", (object[] args) => new Move(Ioc.Resolve<IMovingObject>("Adapters.IMovingObject", args[0]))).Execute();
    }
}
