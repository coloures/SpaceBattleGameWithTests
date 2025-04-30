using App;
namespace SpaceBattle.Tests;
using App.Scopes;
using Moq;

public class GridAdderElemTest : IDisposable
{
    public GridAdderElemTest()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");

        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void TestPositive()
    {
        var cmd = new Mock<SpaceBattle.Lib.ICommand>();
        var x = 6;
        var y = 8;
        var obj = new Mock<IDictionary<string, object>>();
        cmd.Setup(x => x.Execute()).Verifiable();
        var dct = new List<object>();
        var name_grid = "First";
        Ioc.Resolve<ICommand>(
            "IoC.Register",
            $"Grid_{name_grid}_addElem",
            (object[] args) => {
                dct.Add(args[0]);
                dct.Add(args[1]);
                dct.Add(args[2]);
                return cmd.Object;
            }).Execute();
        
        var adder = new SpaceBattle.Lib.GridAdderElem();
        adder.Add(x, y, obj.Object, name_grid);

        Assert.Equal(3, dct.Count);
        Assert.Equal(x, dct[0]);
        Assert.Equal(y, dct[1]);
        Assert.Same(obj.Object, dct[2]);
        cmd.Verify(x=> x.Execute(), Times.Once);

    }
    [Fact]
    public void TestNegative()
    {
        var x = 6;
        var y = 8;
        var obj = new Mock<IDictionary<string, object>>();
        var name_grid = "First";
        
        var adder = new SpaceBattle.Lib.GridAdderElem();
        Assert.ThrowsAny<Exception>(() => adder.Add(x, y, obj.Object, name_grid));

    }
    public void Dispose()
    {
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}