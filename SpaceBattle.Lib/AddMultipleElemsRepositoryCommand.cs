namespace SpaceBattle.Lib;
public class AddMultipleElemsRepositoryCommand<T> : ICommand
{
    private readonly IDictionary<string, List<T>>? any_dict;
    private readonly string? key;

    private readonly T? value;
    public AddMultipleElemsRepositoryCommand(IDictionary<string, List<T>> any_dict, string key, T value)
    {
        this.any_dict = any_dict;
        this.key = key;
        this.value = value;
    }
    public void Execute()
    {
        if (any_dict!.ContainsKey(key!))
        {
            any_dict[key!].Add(value!);
        }
        else
        {
            var temp = new List<T>() { value! };
            any_dict!.Add(key!, temp!);
        }
    }
}
