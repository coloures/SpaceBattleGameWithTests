using App;
using App.Scopes;
using Moq;
namespace SpaceBattle.Tests;
public class RegisterIoCDependencyMacroMoveTests : IDisposable
{
    public RegisterIoCDependencyMacroMoveTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");

        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }
    [Fact]
    public void MacroMoveCommandIsRegistered()
    {
        var cmd = new Mock<Lib.ICommand>();
        cmd.Setup(x => x.Execute()).Verifiable();
        var temp = new Lib.RegisterIoCDependencyMacroMove();
        temp.Execute();
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Specs.Move",
          (object[] args) =>
            new string[] { "Any.Move" }).Execute();
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Any.Move",
            (object[] args) =>
            cmd.Object).Execute();
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Command.Macro",
            (object[] args) =>
            {
                var cmds = (SpaceBattle.Lib.ICommand[])args;
                return new SpaceBattle.Lib.MacroCommand(cmds);
            }
            ).Execute();
        var AnyObj = new Mock<object>();

        var MoveCmd = Ioc.Resolve<Lib.MacroCommand>("Macro.Move", AnyObj.Object);

        MoveCmd.Execute();

        cmd.Verify(x => x.Execute(), Times.Once);

    }
    [Fact]
    public void MacroMoveCommandIsNotRegistered()
    {
        var cmd = new Mock<Lib.ICommand>();
        cmd.Setup(x => x.Execute()).Verifiable();
        var AnyObj = new Mock<object>();

        Assert.Throws<Exception>(() =>
        {
            var MoveCmd = Ioc.Resolve<Lib.MacroCommand>("Macro.Move", AnyObj.Object);
            MoveCmd.Execute();
        });
        cmd.Verify(x => x.Execute(), Times.Never);
    }
    public void Dispose()
    {
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}
