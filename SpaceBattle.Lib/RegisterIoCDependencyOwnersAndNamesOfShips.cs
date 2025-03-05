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
                return new AddMultipleElemsRepositoryCommand<string>(OwnersAndNames_of_Ships, (string)args[0], (string)args[1]);
            }
        ).Execute();

        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "OwnersAndNamesOfShipsRepositoryRemoveShip",
            (object[] args) =>
            {
                return new DeleteElemRepositoryCommand<List<string>>(OwnersAndNames_of_Ships, (string)args[0]);
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
