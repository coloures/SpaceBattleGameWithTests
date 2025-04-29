namespace SpaceBattle.Lib;
using App;

public class TreeAdderElem : ITreeAdderElem
{
    public void Add(string type1, string type2, int[] ints)
    {
        Ioc.Resolve<ICommand>($"{type1}and{type2}TreeAddSet", ints[0], ints[1], ints[2], ints[3]).Execute();
    }
}
