using App;
using App.Scopes;
using Moq;
namespace SpaceBattle.Tests;
public class RegisterDependencyMacroCommandTests : IDisposable
{
    public RegisterDependencyMacroCommandTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");

        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }
    [Fact]
    public void MacroCommandIsRegistered()
    {
        var cmd = new Mock<Lib.ICommand>();
        cmd.Setup(x => x.Execute()).Verifiable();
        var temp = new Lib.RegisterDependencyMacroCommand();
        temp.Execute();

        var MacroCmd = Ioc.Resolve<Lib.MacroCommand>("Commands.Macro",  new Lib.ICommand[] { cmd.Object, cmd.Object });

        MacroCmd.Execute();

        cmd.Verify(x => x.Execute(), Times.AtLeastOnce);

    }
    [Fact]
    public void MacroCommandIsNotRegistered()
    {
        var cmd = new Mock<Lib.ICommand>();
        cmd.Setup(x => x.Execute()).Verifiable();

        Assert.Throws<Exception>(() =>
        {
            var MacroCmd = Ioc.Resolve<Lib.MacroCommand>("Commands.Macro",new Lib.ICommand[] { cmd.Object, cmd.Object });
            MacroCmd.Execute();
        });
        cmd.Verify(x => x.Execute(), Times.Never);
    }
    public void Dispose()
    {
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}