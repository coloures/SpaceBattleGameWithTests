using App;
namespace SpaceBattle.Lib;
public class RegisterIocDependencyTree : ICommand
{
    public readonly string obj1_type;
    public readonly string obj2_type;
    public RegisterIocDependencyTree(string obj1_type, string obj2_type) // ДОБАВИТЬ УГЛЫ ДЛЯ 1 И 2 ОБЪЕКТОВ
    {
        this.obj1_type = obj1_type;
        this.obj2_type = obj2_type;
    }
    public void Execute()
    {
        var Tree = new Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>>();
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            $"{obj1_type}and{obj2_type}TreeAddSet",
            (object[] args) =>
            {
                return new TreeAddSetCommand(Tree, (int)args[0], (int)args[1], (int)args[2], (int)args[3]);
            }
        ).Execute();
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            $"{obj1_type}and{obj2_type}TreeCheckSet",
            (object[] args) =>
            {
                return new TreeCheckSetCommand(Tree, (int)args[0], (int)args[1], (int)args[2], (int)args[3]);
            }
        ).Execute();
    }
}
