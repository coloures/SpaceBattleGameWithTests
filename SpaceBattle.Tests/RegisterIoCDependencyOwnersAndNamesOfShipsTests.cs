using App;
using App.Scopes;
namespace SpaceBattle.Tests;
public class RegisterIocDependencyOwnerAndNamesOfShipsTests : IDisposable
{
    public RegisterIocDependencyOwnerAndNamesOfShipsTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");

        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }
    [Fact]
    public void OwnerAndNamesOfShipsIsRegistered()
    {
        var temp = new Lib.RegisterIocDependencyOwnersAndNamesOfShips();
        temp.Execute();
        Ioc.Resolve<Lib.ICommand>("OwnersAndNamesOfShipsRepositoryAddShip", "any_obj", "first_owner").Execute();

        var owner = Ioc.Resolve<List<string>>("OwnersAndNamesOfShipsRepositoryGetShips", "any_obj");
        Assert.Equal("first_owner", owner.Single());

        Ioc.Resolve<Lib.ICommand>("OwnersAndNamesOfShipsRepositoryAddShip", "any_obj", "second_owner").Execute();

        owner = Ioc.Resolve<List<string>>("OwnersAndNamesOfShipsRepositoryGetShips", "any_obj");
        Assert.Equal("second_owner", owner.Last());

        Ioc.Resolve<Lib.ICommand>("OwnersAndNamesOfShipsRepositoryRemoveShip", "any_obj").Execute();

        Assert.ThrowsAny<Exception>(() =>
        {
            var ship_ = Ioc.Resolve<List<string>>("OwnersAndNamesOfShipsRepositoryGetShips", "any_obj");
        });

        Assert.ThrowsAny<Exception>(() =>
        {
            Ioc.Resolve<Lib.ICommand>("OwnersAndNamesOfShipsRepositoryRemoveShip", "any_obj").Execute();
        });
    }
    [Fact]
    public void OwnerAndNamesOfShipsIsNotRegisteredAdding()
    {
        Assert.ThrowsAny<Exception>(() =>
        {
            Ioc.Resolve<Lib.ICommand>("OwnersAndNamesOfShipsRepositoryAddShip", "any_obj", "first_owner").Execute();
        });
    }
    [Fact]
    public void OwnerAndNamesOfShipsIsNotRegisteredRemoving()
    {
        Assert.ThrowsAny<Exception>(() =>
        {
            Ioc.Resolve<Lib.ICommand>("OwnersAndNamesOfShipsRepositoryRemoveShip", "any_obj").Execute();
        });
    }
    [Fact]
    public void OwnerAndNamesOfShipsIsNotRegisteredGetting()
    {
        Assert.ThrowsAny<Exception>(() =>
        {
            var owner = Ioc.Resolve<List<string>>("OwnersAndNamesOfShipsRepositoryGetShips", "any_obj");
        });
    }
    public void Dispose()
    {
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}
