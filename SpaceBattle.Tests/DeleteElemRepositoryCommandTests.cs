using App;
using App.Scopes;
using Moq;
namespace SpaceBattle.Tests;
public class DeleteElemRepositoryCommandTests : IDisposable
{
    public DeleteElemRepositoryCommandTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");

        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }
    [Fact]
    public void TestPositive()
    {
        var dict = new Mock<IDictionary<string, string>>();
        dict.Setup(x => x.Remove(It.IsAny<string>())).Returns(true);
        var key = "first";

        var test = new Lib.DeleteElemRepositoryCommand<string>(dict.Object, key);
        test.Execute();

        dict.Verify(x => x.Remove(key), Times.Once());
    }
    [Fact]
    public void TestNegative()
    {
        var dict = new Mock<IDictionary<string, string>>();
        dict.Setup(x => x.Remove(It.IsAny<string>())).Returns(false);
        var key = "first";

        var test = new Lib.DeleteElemRepositoryCommand<string>(dict.Object, key);
        Assert.ThrowsAny<Exception>(() => test.Execute());

        dict.Verify(x => x.Remove(key), Times.Once());
    }
    public void Dispose()
    {
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}
