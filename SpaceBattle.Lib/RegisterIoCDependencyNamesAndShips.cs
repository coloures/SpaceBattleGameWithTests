using App;
namespace SpaceBattle.Lib;

public class RegisterIocDependencyNamesAndShips : ICommand
{
    public void Execute()
    {
        var Names_and_Ships = new Dictionary<string, IObject>();

        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "NamesAndShipsRepositoryAddShip", // IdОбъекта - Объект
            (object[] args) =>
            {
                Names_and_Ships.Add((string)args[0], (IObject)args[1]);
                return new object();
            }
        ).Execute();

        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "NamesAndShipsRepositoryRemoveShip",
            (object[] args) =>
            {
                if (Names_and_Ships.Remove((string)args[0]))
                {
                    return new object();
                }
                else
                {
                    throw new Exception();
                }
            }
        ).Execute();

        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "NamesAndShipsRepositoryGetShip",
            (object[] args) =>
            {
                if (Names_and_Ships.TryGetValue((string)args[0], out var result))
                {
                    return result;
                }
                else
                {
                    throw new Exception();
                }
            }
        ).Execute();

    }
}
