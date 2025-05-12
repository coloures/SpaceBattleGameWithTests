namespace SpaceBattle.Lib;
using App;

public class TreeChecker : ITreeChecker
{
    public void Check(int x, int y, int vel_x, int vel_y)
    {
        Ioc.Resolve<ICommand>("shipandtorpedoTreeCheckSet", x, y, vel_x, vel_y).Execute();
    }
}
