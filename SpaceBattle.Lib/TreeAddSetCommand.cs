namespace SpaceBattle.Lib;
public class TreeAddSetCommand : ICommand
{
    public readonly Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>> Tree;
    public readonly int x;
    public readonly int y;
    public readonly int vel_x;
    public readonly int vel_y;
    public TreeAddSetCommand(Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>> Tree, int x, int y, int vel_x, int vel_y)
    {
        this.Tree = Tree;
        this.x = x;
        this.y = y;
        this.vel_x = vel_x;
        this.vel_y = vel_y;
    }
    public void Execute()
    {
        if (!Tree.ContainsKey(x))
        {
            Tree.Add(x, new Dictionary<int, Dictionary<int, HashSet<int>>>());
        }

        if (!Tree[x].ContainsKey(y))
        {
            Tree[x].Add(y, new Dictionary<int, HashSet<int>>());
        }

        if (!Tree[x][y].ContainsKey(vel_x))
        {
            Tree[x][y].Add(vel_x, new HashSet<int>());
        }

        Tree[x][y][vel_x].Add(vel_y);
    }
}
