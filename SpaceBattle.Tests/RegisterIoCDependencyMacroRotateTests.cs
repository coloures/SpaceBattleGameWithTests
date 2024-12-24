using App;
using App.Scopes;
using Moq;
namespace SpaceBattle.Tests;
public class RegisterIoCDependencyMacroRotateTests : IDisposable
{
    public RegisterIoCDependencyMacroRotateTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }
    [Fact]
    public void MacroRotateCommandIsRegistered()
    {
        var cmd = new Mock<Lib.ICommand>();
        cmd.Setup(x => x.Execute()).Verifiable();
        var temp = new Lib.RegisterIoCDependencyMacroRotate();
        temp.Execute();
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Specs.Rotate",
          (object[] args) =>
            new string[] { "Any.Rotate" }).Execute();
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Any.Rotate",
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

        var RotateCmd = Ioc.Resolve<Lib.MacroCommand>("Macro.Rotate", AnyObj.Object);

        RotateCmd.Execute();

        cmd.Verify(x => x.Execute(), Times.Once);

    }
    [Fact]
    public void MacroRotateCommandIsNotRegistered()
    {
        var cmd = new Mock<Lib.ICommand>();
        cmd.Setup(x => x.Execute()).Verifiable();
        var AnyObj = new Mock<object>();

        Assert.Throws<Exception>(() =>
        {
            var RotateCmd = Ioc.Resolve<Lib.MacroCommand>("Macro.Rotate", AnyObj.Object);
            RotateCmd.Execute();
        });
        cmd.Verify(x => x.Execute(), Times.Never);
    }
    public void Dispose()
    {
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}
