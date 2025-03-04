using App;
namespace SpaceBattle.Lib;

public class RegisterIocDependencyOwnersAndNamesOfShips : ICommand
{
    public void Execute()
    {
        var OwnersAndNames_of_Ships = new Dictionary<string, List<string>>();

        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "OwnersAndNamesOfShipsRepositoryAddShip",
            (object[] args) =>
            {
                if (OwnersAndNames_of_Ships.ContainsKey((string)args[0]))
                {
                    OwnersAndNames_of_Ships[(string)args[0]].Add((string)args[1]);
                }
                else
                {
                    var temp = new List<string>() { (string)args[1] };
                    OwnersAndNames_of_Ships.Add((string)args[0], temp); // первое - код "обладателя", второе - код корабля
                }

                return new object();
            }
        ).Execute();

        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "OwnersAndNamesOfShipsRepositoryRemoveShip",
            (object[] args) =>
            {
                if (OwnersAndNames_of_Ships.Remove((string)args[0]))
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
            "OwnersAndNamesOfShipsRepositoryGetShips",
            (object[] args) =>
            {
                if (OwnersAndNames_of_Ships.TryGetValue((string)args[0], out var result))
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
