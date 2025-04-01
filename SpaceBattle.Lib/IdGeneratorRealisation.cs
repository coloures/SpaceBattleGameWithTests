namespace SpaceBattle.Lib;

public class IdGeneratorRealisation : IdGenerator
{
    public string Generate()
    {
        return Guid.NewGuid().ToString();
    }
}
