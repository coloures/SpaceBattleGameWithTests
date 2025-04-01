using App;
using App.Scopes;
using Moq;
namespace SpaceBattle.Tests;
public class RegisterIoCDependencyMacroShootTest : IDisposable
{
    public RegisterIoCDependencyMacroShootTest()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");

        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }
    [Fact]
    public void MacroShootIsRegistered()
    {
        var cmd1 = new Mock<SpaceBattle.Lib.ICommand>();
        cmd1.Setup(x => x.Execute()).Verifiable();
        var cmd2 = new Mock<SpaceBattle.Lib.ICommand>();
        cmd2.Setup(x => x.Execute()).Verifiable();
        var cmd3 = new Mock<SpaceBattle.Lib.ICommand>();
        cmd3.Setup(x => x.Execute()).Verifiable();
        var m = new string[3] { "doing_something", "making_something", "implementing_something" };
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Specs.Shoot",
          (object[] args) =>
            m).Execute();
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "doing_something",
          (object[] args) =>
            cmd1.Object).Execute();
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "making_something",
          (object[] args) =>
            cmd2.Object).Execute();
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "implementing_something",
          (object[] args) =>
            cmd3.Object).Execute();
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Command.Macro",
          (object[] args) =>
            new SpaceBattle.Lib.MacroCommand((SpaceBattle.Lib.ICommand[])args)).Execute();
        new SpaceBattle.Lib.RegisterIoCDependencyMacroShoot().Execute();

        var cmd = Ioc.Resolve<SpaceBattle.Lib.ICommand>("Macro.Shoot", "something");
        cmd.Execute();

        cmd1.Verify(x => x.Execute(), Times.Once);
        cmd2.Verify(x => x.Execute(), Times.Once);
        cmd3.Verify(x => x.Execute(), Times.Once);

    }
    [Fact]
    public void MacroShootIsNotRegistered()
    {
        var cmd1 = new Mock<SpaceBattle.Lib.ICommand>();
        cmd1.Setup(x => x.Execute()).Verifiable();
        var cmd2 = new Mock<SpaceBattle.Lib.ICommand>();
        cmd2.Setup(x => x.Execute()).Verifiable();
        var cmd3 = new Mock<SpaceBattle.Lib.ICommand>();
        cmd3.Setup(x => x.Execute()).Verifiable();
        var m = new string[3] { "doing_something", "making_something", "implementing_something" };
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Specs.Shoot",
          (object[] args) =>
            m).Execute();
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "doing_something",
          (object[] args) =>
            cmd1.Object).Execute();
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "making_something",
          (object[] args) =>
            cmd2.Object).Execute();
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "implementing_something",
          (object[] args) =>
            cmd3.Object).Execute();
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Command.Macro",
          (object[] args) =>
            new SpaceBattle.Lib.MacroCommand((SpaceBattle.Lib.ICommand[])args)).Execute();

        Assert.ThrowsAny<Exception>(() =>
        {
            var cmd = Ioc.Resolve<SpaceBattle.Lib.ICommand>("Macro.Shoot", "something");
            cmd.Execute();
        });

        cmd1.Verify(x => x.Execute(), Times.Never);
        cmd2.Verify(x => x.Execute(), Times.Never);
        cmd3.Verify(x => x.Execute(), Times.Never);

    }
    [Fact]
    public void MacroShootIsRegisteredButMacroCommandIsWrongFirstCmd()
    {
        var cmd1 = new Mock<SpaceBattle.Lib.ICommand>();
        cmd1.Setup(x => x.Execute()).Throws<Exception>().Verifiable();
        var cmd2 = new Mock<SpaceBattle.Lib.ICommand>();
        cmd2.Setup(x => x.Execute()).Verifiable();
        var cmd3 = new Mock<SpaceBattle.Lib.ICommand>();
        cmd3.Setup(x => x.Execute()).Verifiable();
        var m = new string[3] { "doing_something", "making_something", "implementing_something" };
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Specs.Shoot",
          (object[] args) =>
            m).Execute();
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "doing_something",
          (object[] args) =>
            cmd1.Object).Execute();
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "making_something",
          (object[] args) =>
            cmd2.Object).Execute();
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "implementing_something",
          (object[] args) =>
            cmd3.Object).Execute();
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Command.Macro",
          (object[] args) =>
            new SpaceBattle.Lib.MacroCommand((SpaceBattle.Lib.ICommand[])args)).Execute();
        new SpaceBattle.Lib.RegisterIoCDependencyMacroShoot().Execute();

        var cmd = Ioc.Resolve<SpaceBattle.Lib.ICommand>("Macro.Shoot", "something");
        Assert.ThrowsAny<Exception>(() => cmd.Execute());

        cmd1.Verify(x => x.Execute(), Times.Once);
        cmd2.Verify(x => x.Execute(), Times.Never);
        cmd3.Verify(x => x.Execute(), Times.Never);

    }
    [Fact]
    public void MacroShootIsRegisteredButMacroCommandIsWrongSecondCmd()
    {
        var cmd1 = new Mock<SpaceBattle.Lib.ICommand>();
        cmd1.Setup(x => x.Execute()).Verifiable();
        var cmd2 = new Mock<SpaceBattle.Lib.ICommand>();
        cmd2.Setup(x => x.Execute()).Throws<Exception>().Verifiable();
        var cmd3 = new Mock<SpaceBattle.Lib.ICommand>();
        cmd3.Setup(x => x.Execute()).Verifiable();
        var m = new string[3] { "doing_something", "making_something", "implementing_something" };
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Specs.Shoot",
          (object[] args) =>
            m).Execute();
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "doing_something",
          (object[] args) =>
            cmd1.Object).Execute();
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "making_something",
          (object[] args) =>
            cmd2.Object).Execute();
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "implementing_something",
          (object[] args) =>
            cmd3.Object).Execute();
        Ioc.Resolve<App.ICommand>(
          "IoC.Register",
          "Command.Macro",
          (object[] args) =>
            new SpaceBattle.Lib.MacroCommand((SpaceBattle.Lib.ICommand[])args)).Execute();
        new SpaceBattle.Lib.RegisterIoCDependencyMacroShoot().Execute();

        var cmd = Ioc.Resolve<SpaceBattle.Lib.ICommand>("Macro.Shoot", "something");
        Assert.ThrowsAny<Exception>(() => cmd.Execute());

        cmd1.Verify(x => x.Execute(), Times.Once);
        cmd2.Verify(x => x.Execute(), Times.Once);
        cmd3.Verify(x => x.Execute(), Times.Never);

    }

    public void Dispose()
    {
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}
