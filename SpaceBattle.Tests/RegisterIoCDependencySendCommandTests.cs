using App;
using App.Scopes;
using Moq;
namespace SpaceBattle.Tests;

public class RegisterIoCDependencySendCommandTests : IDisposable
{
    public RegisterIoCDependencySendCommandTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");

        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }
    [Fact]
    public void StartCommandIsRegistered()
    {
        var temp = new SpaceBattle.Lib.RegisterIoCDependencySendCommand();
        temp.Execute();

        var _Sender = new Mock<SpaceBattle.Lib.ISender>();
        _Sender.Setup(x => x.Send(It.IsAny<SpaceBattle.Lib.ICommand>())).Verifiable();
        var _Command = new Mock<SpaceBattle.Lib.ICommand>();

        var _CommandStart = Ioc.Resolve<SpaceBattle.Lib.SendCommand>("Commands.Send", _Command.Object,
         _Sender.Object);

        _CommandStart.Execute();

        _Sender.Verify(x => x.Send(It.IsAny<SpaceBattle.Lib.ICommand>()), Times.Once);

    }
    [Fact]
    public void MoveCommandStartIsNotRegistered()
    {
        var _Sender = new Mock<SpaceBattle.Lib.ISender>();
        _Sender.Setup(x => x.Send(It.IsAny<SpaceBattle.Lib.ICommand>())).Verifiable();
        var _Command = new Mock<SpaceBattle.Lib.ICommand>();

        Assert.ThrowsAny<System.Exception>(
            () =>
            {
                var _CommandStart = Ioc.Resolve<SpaceBattle.Lib.SendCommand>("Commands.Send", _Command.Object,
                    _Sender.Object);
            }
        );

        _Sender.Verify(x => x.Send(It.IsAny<SpaceBattle.Lib.ICommand>()), Times.Never);

    }
    public void Dispose()
    {
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}
