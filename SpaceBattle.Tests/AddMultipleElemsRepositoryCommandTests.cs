using App;
using App.Scopes;
using Moq;
namespace SpaceBattle.Tests;
public class AddMultipleElemsRepositoryCommandTests : IDisposable
{
    public AddMultipleElemsRepositoryCommandTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");

        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }
    [Fact]
    public void TestPositive()
    {
        var dict = new Mock<IDictionary<string, List<string>>>();
        var key = "first";
        var value = "value";
        var key2 = "second";
        var value2 = "value2";

        var test = new Lib.AddMultipleElemsRepositoryCommand<string>(dict.Object, key, value);
        test.Execute();

        dict.Verify(x => x.Add(key, It.Is<List<string>>(list => list.Count == 1 && list.Single() == value)), Times.Once());

        var dict1 = new Dictionary<string, List<string>>();
        var test1 = new Lib.AddMultipleElemsRepositoryCommand<string>(dict1, key2, value);
        test1.Execute();
        var test2 = new Lib.AddMultipleElemsRepositoryCommand<string>(dict1, key2, value2);
        test2.Execute();

        Assert.Equal(2, dict1[key2].Count());
        Assert.Equal(value, dict1[key2].First());
        Assert.Equal(value2, dict1[key2].Last());
    }
    [Fact]
    public void TestNegative()
    {
        var dict = new Mock<IDictionary<string, List<string>>>();
        dict.Setup(x => x.Add(It.IsAny<string>(), It.IsAny<List<string>>())).Throws<Exception>();
        var key = "first";
        var value = "value";

        Assert.ThrowsAny<Exception>(() => new Lib.AddMultipleElemsRepositoryCommand<string>(dict.Object, key, value).Execute());
    }
    [Fact]
    public void TestNegativeSecond()
    {
        var dict = new Mock<IDictionary<string, List<string>>>();
        var key = "first";
        var value = "value";
        var value2 = "value2";
        new Lib.AddMultipleElemsRepositoryCommand<string>(dict.Object, key, value).Execute();
        dict.Verify(x => x.Add(key, It.Is<List<string>>(list => list.Count() == 1 && list.Single() == value)), Times.Once());

        dict.Setup(x => x.Add(It.IsAny<string>(), It.IsAny<List<string>>())).Throws<Exception>();
        Assert.ThrowsAny<Exception>(() => new Lib.AddMultipleElemsRepositoryCommand<string>(dict.Object, key, value2).Execute());
    }
    public void Dispose()
    {
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}
