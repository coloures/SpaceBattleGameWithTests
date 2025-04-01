namespace SpaceBattle.Lib;

public class CreatingCommand : ICommand
{
    public readonly IAdderToNamesAndShipsDictionary? Adder;
    public readonly IdGenerator? GeneratorId;
    public CreatingCommand(object Adder, object GeneratorId)
    {
        this.Adder = (IAdderToNamesAndShipsDictionary)Adder;
        this.GeneratorId = (IdGenerator)GeneratorId;
    }
    public void Execute()
    {
        var init = new Dictionary<string, object>();
        init["Id"] = GeneratorId!.Generate();
        Adder!.Add(init, (string)init["Id"]);
    }
}