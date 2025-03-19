namespace SpaceBattle.Lib;
public class AddElemRepositoryCommand<T> : ICommand
{
    private readonly IDictionary<string, T>? any_dict;
    private readonly string? key;

    private readonly T? value;
    public AddElemRepositoryCommand(IDictionary<string, T> any_dict, string key, T value)
    {
        this.any_dict = any_dict;
        this.key = key;
        this.value = value;
    }
    public void Execute()
    {
        any_dict!.Add(key!, value!);
    }
}
