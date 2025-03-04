using App;
using App.Scopes;
using Moq;
namespace SpaceBattle.Tests;
public class RegisterIocDependencyNamesAndShipsTests : IDisposable
{
    public RegisterIocDependencyNamesAndShipsTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");

        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }
    [Fact]
    public void NamesAndShipsIsRegistered()
    {
        var iobj_ = new Mock<Lib.IObject>();
        var temp = new Lib.RegisterIocDependencyNamesAndShips();
        temp.Execute();
        Ioc.Resolve<object>("NamesAndShipsRepositoryAddShip", "any_obj", iobj_.Object);

        var ship = Ioc.Resolve<Lib.IObject>("NamesAndShipsRepositoryGetShip", "any_obj");
        Assert.Equal(iobj_.Object, ship);

        Ioc.Resolve<object>("NamesAndShipsRepositoryRemoveShip", "any_obj");

        Assert.ThrowsAny<Exception>(() =>
        {
            var ship_ = Ioc.Resolve<Lib.IObject>("NamesAndShipsRepositoryGetShip", "any_obj");
        });
        Assert.ThrowsAny<Exception>(() =>
        {
            var ship_ = Ioc.Resolve<Lib.IObject>("NamesAndShipsRepositoryRemoveShip", "any_obj");
        });
    }
    [Fact]
    public void NamesAndShipsIsNotRegisteredAdding()
    {
        var iobj_ = new Mock<Lib.IObject>();
        Assert.ThrowsAny<Exception>(() =>
        {
            Ioc.Resolve<object>("NamesAndShipsRepositoryAddShip", "any_obj", iobj_.Object);
        });
    }
    [Fact]
    public void NamesAndShipsIsNotRegisteredRemoving()
    {
        Assert.ThrowsAny<Exception>(() =>
        {
            Ioc.Resolve<object>("NamesAndShipsRepositoryRemoveShip", "any_obj");
        });
    }
    [Fact]
    public void NamesAndShipsIsNotRegisteredGetting()
    {
        Assert.ThrowsAny<Exception>(() =>
        {
            var ship_ = Ioc.Resolve<Lib.IObject>("NamesAndShipsRepositoryGetShip", "any_obj");
        });
    }
    public void Dispose()
    {
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}
