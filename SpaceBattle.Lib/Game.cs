using System.Diagnostics;
namespace SpaceBattle.Lib;
public class Game : SpaceBattle.Lib.ICommand
{
    public readonly IQueue? outer_queue;
    public readonly Stopwatch timer;
    public readonly int time;
    public Game(object outer_queue, int time)
    {
        this.outer_queue = (IQueue)outer_queue;
        timer = new Stopwatch();
        this.time = time;
    }
    public void Execute()
    {
        timer.Start();
        while (timer.ElapsedMilliseconds < time && outer_queue!.Count() > 0)
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
