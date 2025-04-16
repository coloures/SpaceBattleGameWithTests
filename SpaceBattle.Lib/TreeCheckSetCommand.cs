namespace SpaceBattle.Lib;
public class TreeCheckSetCommand : ICommand
{
    public readonly Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>> Tree;
    public readonly int x;
    public readonly int y;
    public readonly int vel_x;
    public readonly int vel_y;
    public TreeCheckSetCommand(Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>> Tree, int x, int y, int vel_x, int vel_y)
    {
        this.Tree = Tree;
        this.x = x;
        this.y = y;
        this.vel_x = vel_x;
        this.vel_y = vel_y;
    }
    public void Execute()
    {
        if (Tree.ContainsKey(x))
        {
            if (Tree[x].ContainsKey(y))
            {
                if (Tree[x][y].ContainsKey(vel_x))
                {
                    if (Tree[x][y][vel_x].Contains(vel_y))
                    {
                        return;
                    }
                }
            }
        }

        throw new Exception();
    }
}
