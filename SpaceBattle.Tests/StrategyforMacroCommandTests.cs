using App;
using App.Scopes;
using Moq;
namespace SpaceBattle.Tests;

public class StrategyforMacroCommandTests: IDisposable
{
    public StrategyforMacroCommandTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }
    [Fact]
    public void TestWithoutError()
    {
        var cmd = new Mock<SpaceBattle.Lib.ICommand>();
        cmd.Setup(x => x.Execute()).Verifiable();
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Specs.Macro.Test",
          (object[] args) =>
            new string[]{"TypicalCommand"}).Execute();
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "TypicalCommand",
          (object[] args) =>
          {
            return cmd.Object;
          }
            ).Execute();
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Command.Macro",
            (object[] args) => {
                var cmds = (SpaceBattle.Lib.ICommand[])args;
                return new SpaceBattle.Lib.MacroCommand(cmds);
            }
            ).Execute();
        var strategy = new SpaceBattle.Lib.CreateMacroCommandStrategy("Macro.Test");
        var MacroCommand = strategy.Resolve(new object());
        MacroCommand.Execute();

        cmd.Verify(x => x.Execute(), Times.Once);
    }
    [Fact]
    public void TestWithErrorInSpecsReference()
    {
        var cmd = new Mock<SpaceBattle.Lib.ICommand>();
        cmd.Setup(x => x.Execute()).Verifiable();
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "TypicalCommand",
          (object[] args) =>
          {
            return cmd.Object;
          }
            ).Execute();
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Command.Macro",
            (object[] args) => {
                var cmds = (SpaceBattle.Lib.ICommand[])args;
                return new SpaceBattle.Lib.MacroCommand(cmds);
            }
            ).Execute();
        var strategy = new SpaceBattle.Lib.CreateMacroCommandStrategy("Macro.Test");
        Assert.Throws<Exception>(()=> 
        {
            var MacroCommand = strategy.Resolve(new object());
            MacroCommand.Execute();
        });
        cmd.Verify(x => x.Execute(), Times.Never);
    }
    [Fact]
    public void TestWithErrorInCommandReference()
    {
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Specs.Macro.Test",
          (object[] args) =>
            new string[]{"TypicalCommand"}).Execute();
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Command.Macro",
            (object[] args) => {
                var cmds = (SpaceBattle.Lib.ICommand[])args;
                return new SpaceBattle.Lib.MacroCommand(cmds);
            }
            ).Execute();
        var strategy = new SpaceBattle.Lib.CreateMacroCommandStrategy("Macro.Test");
        Assert.Throws<Exception>(()=> 
        {
            var MacroCommand = strategy.Resolve(new object());
            MacroCommand.Execute();
        });
    }
    [Fact]
    public void TestWithErrorInMacroCommandReference()
    {
        var cmd = new Mock<SpaceBattle.Lib.ICommand>();
        cmd.Setup(x => x.Execute()).Verifiable();
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Specs.Macro.Test",
          (object[] args) =>
            new string[]{"TypicalCommand"}).Execute();
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "TypicalCommand",
          (object[] args) =>
          {
            return cmd.Object;
          }
            ).Execute();
        var strategy = new SpaceBattle.Lib.CreateMacroCommandStrategy("Macro.Test");
        Assert.Throws<Exception>(()=> 
        {
            var MacroCommand = strategy.Resolve(new object());
            MacroCommand.Execute();
        });
        cmd.Verify(x => x.Execute(), Times.Never);
    }
    public void Dispose()
    {
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}