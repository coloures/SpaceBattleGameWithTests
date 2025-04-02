using App;
using App.Scopes;
using Moq;
namespace SpaceBattle.Tests;
public class RegisterIoCDependencyMacroAuthTest : IDisposable
{
    public RegisterIoCDependencyMacroAuthTest()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }
    [Fact]
    public void MacroAuthCommandIsRegistered()
    {
        var cmd = new Mock<Lib.ICommand>();
        cmd.Setup(x => x.Execute()).Verifiable();
        var temp = new Lib.RegisterIoCDependencyMacroAuth();
        temp.Execute();
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Specs.Auth",
          (object[] args) =>
            new string[] { "Any.Auth" }).Execute();
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Any.Auth",
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

        var AuthCmd = Ioc.Resolve<Lib.MacroCommand>("Macro.Auth", AnyObj.Object);

        AuthCmd.Execute();

        cmd.Verify(x => x.Execute(), Times.Once);

    }
    [Fact]
    public void MacroAuthCommandIsNotRegistered()
    {
        var cmd = new Mock<Lib.ICommand>();
        cmd.Setup(x => x.Execute()).Verifiable();
        var AnyObj = new Mock<object>();

        Assert.Throws<Exception>(() =>
        {
            var AuthCmd = Ioc.Resolve<Lib.MacroCommand>("Macro.Auth", AnyObj.Object);
            AuthCmd.Execute();
        });
        cmd.Verify(x => x.Execute(), Times.Never);
    }
    public void Dispose()
    {
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}
