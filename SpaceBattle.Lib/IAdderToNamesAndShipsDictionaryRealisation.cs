using App;

namespace SpaceBattle.Lib;
public class IAdderToNamesAndShipsDictionaryRealisation : IAdderToNamesAndShipsDictionary
{
    public void Add(IDictionary<string, object> obj, string Id)
    {
        Ioc.Resolve<ICommand>("NamesAndShipsRepositoryAddShip", Id, obj).Execute();
    }
}
