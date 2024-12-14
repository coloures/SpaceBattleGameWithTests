using App;
using App.Scopes;
namespace SpaceBattle.Tests;
public class RegisterDependencyCommandInjectableCommandTest : IDisposable
{
    public RegisterDependencyCommandInjectableCommandTest()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");

        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }
    [Fact]
    public void CommandInjectableIsRegistered()
    {
        var temp = new SpaceBattle.Lib.RegisterDependencyCommandInjectableCommand();
        temp.Execute();

        var _BridgeCommand1 = Ioc.Resolve<SpaceBattle.Lib.ICommand>("Commands.CommandInjectable");
        var _BridgeCommand2 = Ioc.Resolve<SpaceBattle.Lib.IInjectable>("Commands.CommandInjectable");
        var _BridgeCommand3 = Ioc.Resolve<SpaceBattle.Lib.BridgeCommand>("Commands.CommandInjectable");
        Assert.IsType<SpaceBattle.Lib.BridgeCommand>(_BridgeCommand1);
        Assert.IsType<SpaceBattle.Lib.BridgeCommand>(_BridgeCommand2);
        Assert.IsType<SpaceBattle.Lib.BridgeCommand>(_BridgeCommand3);

    }
    [Fact]
    public void CommandInjectableIsNotRegistered()
    {

        Assert.ThrowsAny<System.Exception>
        (() => { var _BridgeCommand1 = Ioc.Resolve<SpaceBattle.Lib.ICommand>("Commands.CommandInjectable"); });
        Assert.ThrowsAny<System.Exception>
        (() => { var _BridgeCommand2 = Ioc.Resolve<SpaceBattle.Lib.IInjectable>("Commands.CommandInjectable"); });
        Assert.ThrowsAny<System.Exception>
        (() => { var _BridgeCommand3 = Ioc.Resolve<SpaceBattle.Lib.BridgeCommand>("Commands.CommandInjectable"); });

    }
    public void Dispose()
    {
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}
