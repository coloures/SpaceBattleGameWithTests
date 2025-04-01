using System.Diagnostics;
namespace SpaceBattle.Lib;
public class Game : SpaceBattle.Lib.ICommand
{
    public readonly IQueue? outer_queue;
    public readonly Stopwatch timer;
    public Game(object outer_queue)
    {
        this.outer_queue = (IQueue)outer_queue;
        timer = new Stopwatch();
    }
    public void Execute()
    {
        timer.Start();
        while (timer.ElapsedMilliseconds < 50 && outer_queue!.Count() > 0)
        {
            var cmd = outer_queue!.Get();
            try
            {
                cmd.Execute();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        timer.Reset();
    }
}
