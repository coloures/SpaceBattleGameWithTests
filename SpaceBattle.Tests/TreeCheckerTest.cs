using App;
using App.Scopes;
using Moq;

namespace SpaceBattle.Tests;

public class TreeCheckerTest : IDisposable
{
    public TreeCheckerTest()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void Check_ResolvesCorrectCommandAndExecutesIt()
    {
        var Command = new Mock<Lib.ICommand>();
        Ioc.Resolve<App.ICommand>("IoC.Register", "shipandtorpedoTreeCheckSet",
            (object[] args) => Command.Object).Execute();
        var checker = new SpaceBattle.Lib.TreeChecker();
        int x = 1, y = 2, vel_x = 3, vel_y = 4;
        checker.Check(x, y, vel_x, vel_y);
        Command.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void Check_NotResolvesAndExceptionThrows()
    {
        var checker = new SpaceBattle.Lib.TreeChecker();
        int x = 1, y = 2, vel_x = 3, vel_y = 4;
        Assert.ThrowsAny<Exception>(() => checker.Check(x, y, vel_x, vel_y));
    }

    public void Dispose()
    {
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}
