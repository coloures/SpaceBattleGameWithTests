using App;
using App.Scopes;
using Moq;
namespace SpaceBattle.Tests;
public class RegisterIoCDependencyActionsStopTest : IDisposable
{
    public RegisterIoCDependencyActionsStopTest()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");

        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }
    [Fact]
    public void StopCommandIsRegistered()
    {
        var temp = new SpaceBattle.Lib.RegisterIocDependencyActionsStop();
        temp.Execute();

        var _Sender = new Mock<SpaceBattle.Lib.ISender>();
        _Sender.Setup(x => x.Send(It.IsAny<SpaceBattle.Lib.ICommand>())).Verifiable();
        var _Object = new Mock<IDictionary<string, object>>();
        _Object.SetupSet(x => x["Movement"] = It.IsAny<SpaceBattle.Lib.ICommand>()).Verifiable();
        var order = new Dictionary<string, object>();
        order["Object"] = _Object.Object;
        order["Sender"] = _Sender.Object;
        order["Label"] = "Movement";

        var _CommandStop = Ioc.Resolve<SpaceBattle.Lib.StopCommand>("Actions.Stop", order);

        _CommandStop.Execute();

        _Sender.Verify(x => x.Send(It.IsAny<SpaceBattle.Lib.ICommand>()), Times.Once);
        _Object.VerifySet(x => x["Movement"] = It.IsAny<SpaceBattle.Lib.ICommand>(), Times.Once);

    }
    [Fact]
    public void CommandStopIsNotRegistered()
    {
        var _Sender = new Mock<SpaceBattle.Lib.ISender>();
        _Sender.Setup(x => x.Send(It.IsAny<SpaceBattle.Lib.ICommand>())).Verifiable();
        var _Object = new Mock<IDictionary<string, object>>();
        _Object.SetupSet(x => x["Movement"] = It.IsAny<SpaceBattle.Lib.ICommand>()).Verifiable();
        var order = new Dictionary<string, object>();
        order["Object"] = _Object.Object;
        order["Sender"] = _Sender.Object;
        order["Label"] = "Movement";

        Assert.ThrowsAny<System.Exception>(
            () =>
            {
                var _MoveCommandStart = Ioc.Resolve<SpaceBattle.Lib.StartCommand>("Actions.Stop", order);
            }
        );

        _Sender.Verify(x => x.Send(It.IsAny<SpaceBattle.Lib.ICommand>()), Times.Never);
        _Object.VerifySet(x => x["Movement"] = It.IsAny<SpaceBattle.Lib.ICommand>(), Times.Never);

    }
    public void Dispose()
    {
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}
