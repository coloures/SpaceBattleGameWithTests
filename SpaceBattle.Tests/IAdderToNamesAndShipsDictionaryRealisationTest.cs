using App;
using App.Scopes;
namespace SpaceBattle.Lib.Tests;

public class IAdderToNamesAndShipsDictionaryRealisationTest : IDisposable
{
    public IAdderToNamesAndShipsDictionaryRealisationTest()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void IAdderToNamesAndShipsDictionaryRealisationWorksWell()
    {
        var obj = new Dictionary<string, object>();
        var any_dict = new Dictionary<string, object>();
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "NamesAndShipsRepositoryAddShip", // IdОбъекта - Объект
            (object[] args) =>
            {
                return new SpaceBattle.Lib.AddElemRepositoryCommand<object>(any_dict, (string)args[0], (object)args[1]);
            }
        ).Execute();
        var adder = new SpaceBattle.Lib.IAdderToNamesAndShipsDictionaryRealisation();
        adder.Add(obj, "object1");
        Assert.Equal(obj, any_dict["object1"]);
    }
    [Fact]
    public void IAdderToNamesAndShipsDictionaryRealisationWorksBad()
    {
        var obj = new Dictionary<string, object>();
        var any_dict = new Dictionary<string, object>();
        var adder = new SpaceBattle.Lib.IAdderToNamesAndShipsDictionaryRealisation();
        Assert.ThrowsAny<Exception>(() =>adder.Add(obj, "object1"));
        Assert.False(any_dict.ContainsKey("object1"));
    }
    public void Dispose()
    {
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}
