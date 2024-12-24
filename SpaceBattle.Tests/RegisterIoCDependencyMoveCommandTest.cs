using App;
using App.Scopes;
using Moq;
namespace SpaceBattle.Lib.Tests;

public class RegisterDependencyCommandMoveCommandTest : IDisposable
{
    public RegisterDependencyCommandMoveCommandTest()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void Execute_ShouldRegisterMoveCommandDependency()
    {
        var moveMock = new Mock<IMovingObject>();
        var objectMock = new Mock<IDictionary<string, object>>();
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Adapters.IMovingObject",
            (object[] args) => moveMock.Object
        ).Execute();

        var command_to_reg = new RegisterDependencyCommandMoveCommand();
        command_to_reg.Execute();

        var command_to_resolve = Ioc.Resolve<ICommand>("Commands.Move", objectMock);

        Assert.NotNull(command_to_resolve);
        Assert.IsType<Move>(command_to_resolve);
    }

    [Fact]
    public void Execute_NotShouldRegisterMoveCommandDependency()
    {
        var moveMock = new Mock<object>();

        Assert.ThrowsAny<Exception>(() => Ioc.Resolve<ICommand>("Commands.Move", moveMock));
    }
    public void Dispose()
    {
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}
