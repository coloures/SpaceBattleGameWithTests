using App;
using App.Scopes;
using Moq;
namespace SpaceBattle.Tests;
public class AddElemRepositoryCommandTests : IDisposable
{
    public AddElemRepositoryCommandTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");

        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }
    [Fact]
    public void TestPositive()
    {
        var dict = new Mock<IDictionary<string, string>>();
        var key = "first";
        var value = "value";

        var test = new Lib.AddElemRepositoryCommand<string>(dict.Object, key, value);
        test.Execute();

        dict.Verify(x => x.Add(key, value), Times.Once());
    }
    [Fact]
    public void TestNegativeAddingSameKey()
    {
        var dict = new Dictionary<string, string>();
        var key = "first";
        var value = "value";
        var value2 = "value2";

        new Lib.AddElemRepositoryCommand<string>(dict, key, value).Execute();
        Assert.ThrowsAny<Exception>(() => new Lib.AddElemRepositoryCommand<string>(dict, key, value2).Execute());
    }
    [Fact]
    public void TestNegative()
    {
        var dict = new Mock<IDictionary<string, string>>();
        dict.Setup(x => x.Add(It.IsAny<string>(), It.IsAny<string>())).Throws<Exception>();
        var key = "first";
        var value = "value";

        Assert.ThrowsAny<Exception>(() => new Lib.AddElemRepositoryCommand<string>(dict.Object, key, value).Execute());
        dict.Verify(x => x.Add(key, It.Is<string>(x => x == value)), Times.Once());
    }
    public void Dispose()
    {
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}
