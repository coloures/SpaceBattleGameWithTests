namespace SpaceBattle.Lib;
public class DeleteElemRepositoryCommand<T> : ICommand
{
    private readonly IDictionary<string, T>? any_dict;
    private readonly string? key;
    public DeleteElemRepositoryCommand(IDictionary<string, T> any_dict, string key)
    {
        this.any_dict = any_dict;
        this.key = key;
    }
    public void Execute()
    {
        if (!any_dict!.Remove(key!))
        {
            throw new Exception();
        }
    }
}
