namespace SpaceBattle.Lib;

public interface IOwner
{
    List<string> GetRights();
}

public interface IObject
{
    List<string> GetAbilities();
}
