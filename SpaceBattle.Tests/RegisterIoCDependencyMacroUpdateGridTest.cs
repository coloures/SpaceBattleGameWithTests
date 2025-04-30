namespace SpaceBattle.Tests;
using App;
using App.Scopes;
using Moq;
public class RegisterIoCDependencyMacroUpdateGridTest : IDisposable
{
    public RegisterIoCDependencyMacroUpdateGridTest()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }
    [Fact]
    public void MacroUpdateGridCommandIsRegistered()
    {
        var cmd = new Mock<Lib.ICommand>();
        cmd.Setup(x => x.Execute()).Verifiable();
        var temp = new Lib.RegisterIoCDependencyMacroUpdateGrid();
        temp.Execute();
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Specs.UpdateGrid",
          (object[] args) =>
            new string[] { "Any.UpdateGrid" }).Execute();
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Any.UpdateGrid",
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

        var UpdateGrid = Ioc.Resolve<Lib.MacroCommand>("Macro.UpdateGrid", AnyObj.Object);

        UpdateGrid.Execute();

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
            var UpdateGrid = Ioc.Resolve<Lib.MacroCommand>("Macro.UpdateGrid", AnyObj.Object);
            UpdateGrid.Execute();
        });
        cmd.Verify(x => x.Execute(), Times.Never);
    }
    public void Dispose()
    {
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}
