using App;
using App.Scopes;
using Moq;
namespace SpaceBattle.Tests;
public class RegisterIoCDependencyListForGridTest : IDisposable
{
    public RegisterIoCDependencyListForGridTest()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");

        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }
    [Fact]
    public void RegisterIoCDependencyListForGridTestIsRegistered()
    {
        var cmd = new SpaceBattle.Lib.RegisterIocDependencyListForGrid("testing", 10, 10);
        cmd.Execute();
        var obj = new Mock<IDictionary<string, object>>();
        Ioc.Resolve<SpaceBattle.Lib.ICommand>("Grid_testing_addElem", 8, 7, obj.Object).Execute();
        Ioc.Resolve<SpaceBattle.Lib.ICommand>("Grid_testing_deleteElem", 8, 7, obj.Object).Execute();
    }
    [Fact]
    public void RegisterIoCDependencyListForGridTestIsNotRegistered()
    {
        var obj = new Mock<IDictionary<string, object>>();
        Assert.ThrowsAny<Exception>(() =>Ioc.Resolve<SpaceBattle.Lib.ICommand>("Grid_testing_addElem", 8, 7, obj.Object).Execute());
        Assert.ThrowsAny<Exception>(() =>Ioc.Resolve<SpaceBattle.Lib.ICommand>("Grid_testing_deleteElem", 8, 7, obj.Object).Execute());
    }
    public void Dispose()
    {
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}
