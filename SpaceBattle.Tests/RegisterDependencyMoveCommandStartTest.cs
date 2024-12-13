using App;
using App.Scopes;
using Moq;
namespace SpaceBattle.Tests;
public class RegisterDependencyMoveCommandStartTest
{
    public RegisterDependencyMoveCommandStartTest()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");

        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }
    [Fact]
    public void MoveCommandStartIsRegistered()
    {
        var temp = new SpaceBattle.Lib.RegisterDependencyMoveCommandStart();
        temp.Execute();

        var _Sender = new Mock<SpaceBattle.Lib.ISender>();
        _Sender.Setup(x => x.Send(It.IsAny<SpaceBattle.Lib.ICommand>())).Verifiable();
        var _BridgeCommand = new Mock<SpaceBattle.Lib.ICommand>();
        var _Object = new Mock<IDictionary<string, object>>();
        _Object.SetupSet(x => x["Movement"] = It.IsAny<SpaceBattle.Lib.ICommand>()).Verifiable();
        var order = new Dictionary<string, object>();
        order["Object"] = _Object.Object;
        order["Command"] = _BridgeCommand.Object;
        order["Sender"] = _Sender.Object;

        var _MoveCommandStart = Ioc.Resolve<SpaceBattle.Lib.StartMoveCommand>("Actions.Move.Start", order);

        _MoveCommandStart.Execute();

        _Sender.Verify(x => x.Send(It.IsAny<SpaceBattle.Lib.ICommand>()), Times.Once);
        _Object.VerifySet(x => x["Movement"] = It.IsAny<SpaceBattle.Lib.ICommand>(), Times.Once);

    }
    [Fact]
    public void MoveCommandStartIsNotRegistered()
    {
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create.Empty");
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
        var _Sender = new Mock<SpaceBattle.Lib.ISender>();
        _Sender.Setup(x => x.Send(It.IsAny<SpaceBattle.Lib.ICommand>())).Verifiable();
        var _BridgeCommand = new Mock<SpaceBattle.Lib.ICommand>();
        var _Object = new Mock<IDictionary<string, object>>();
        _Object.SetupSet(x => x["Movement"] = It.IsAny<SpaceBattle.Lib.ICommand>()).Verifiable();
        var order = new Dictionary<string, object>();
        order["Object"] = _Object.Object;
        order["Command"] = _BridgeCommand.Object;
        order["Sender"] = _Sender.Object;

        Assert.Throws<System.Collections.Generic.KeyNotFoundException>(
            () =>
            {
                var _MoveCommandStart = Ioc.Resolve<SpaceBattle.Lib.StartMoveCommand>("Actions.Move.Start", order);
                _MoveCommandStart.Execute();
            }
        );

        _Sender.Verify(x => x.Send(It.IsAny<SpaceBattle.Lib.ICommand>()), Times.Never);
        _Object.VerifySet(x => x["Movement"] = It.IsAny<SpaceBattle.Lib.ICommand>(), Times.Never);

    }
}
