using App;
using App.Scopes;
using Moq;
using SpaceBattle.Lib;
namespace SpaceBattle.Tests;

public class TreeAdderElemTest : IDisposable
{
    public TreeAdderElemTest()
    {
        new InitCommand().Execute();
        var scope = Ioc.Resolve<object>("IoC.Scope.Create");
        var setScopeCmd = Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", scope);
        setScopeCmd.Execute();
    }

    [Fact]
    public void Add_ResolvesCorrectCommandAndExecutesIt()
    {
        var mockCommand = new Mock<Lib.ICommand>();
        Ioc.Resolve<App.ICommand>("IoC.Register", "TypeAandTypeBTreeAddSet",
            (object[] args) => mockCommand.Object).Execute();
        var type1 = "TypeA";
        var type2 = "TypeB";
        var ints = new[] { 1, 2, 3, 4 };
        var treeAdder = new TreeAdderElem();
        treeAdder.Add(type1, type2, ints);
        mockCommand.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void Add_ThrowsException_WhenIntsArrayTooShort()
    {
        var mockCommand = new Mock<Lib.ICommand>();
        Ioc.Resolve<App.ICommand>("IoC.Register", "TypeAandTypeBTreeAddSet",
            (object[] args) => mockCommand.Object).Execute();
        var type1 = "TypeA";
        var type2 = "TypeB";
        var ints = new[] { 1, 2, 3 };
        var treeAdder = new TreeAdderElem();
        Assert.Throws<IndexOutOfRangeException>(() => treeAdder.Add(type1, type2, ints));
    }
    [Fact]
    public void Add_ThrowsException_StrategyIsNotRegistered()
    {
        var mockCommand = new Mock<Lib.ICommand>();
        var type1 = "TypeA";
        var type2 = "TypeB";
        var ints = new[] { 1, 2, 3, 4 };
        var treeAdder = new TreeAdderElem();
        Assert.ThrowsAny<Exception>(() => treeAdder.Add(type1, type2, ints));
    }

    public void Dispose()
    {
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}
