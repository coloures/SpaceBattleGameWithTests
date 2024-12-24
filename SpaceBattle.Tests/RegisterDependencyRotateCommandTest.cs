using App;
using App.Scopes;
using Moq;
namespace SpaceBattle.Lib.Tests;

public class RegisterIoCDependencyRotateCommandTest : IDisposable
{
    public RegisterIoCDependencyRotateCommandTest()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void RegisterRotateCommandDependencyDone()
    {
        var rotatingObjectMock = new Mock<IRotatingObject>();
        var mockGameObject = new Mock<IDictionary<string, object>>();
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Adapters.IRotatingObject",
            (object[] args) => rotatingObjectMock.Object
        ).Execute();

        var command_to_register = new RegisterDependencyRotateCommand();

        command_to_register.Execute();

        var command_to_resolve = Ioc.Resolve<ICommand>("Commands.Rotate", mockGameObject.Object);
        Assert.NotNull(command_to_resolve);
        Assert.IsType<Rotate>(command_to_resolve);
    }

    [Fact]
    public void Execute_ShouldNotFindRotateCommandDependencyInNewScope()
    {
        var rotatingObjectMock = new Mock<IRotatingObject>();
        // Act & Assert
        Assert.Throws<Exception>(() => Ioc.Resolve<ICommand>("Commands.Rotate", rotatingObjectMock.Object));
    }

    public void Dispose()
    {
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}
