using System.Diagnostics;
using Moq;
using SpaceBattle.Lib;
namespace SpaceBattle.Tests;
public class GameTest
{
    [Fact]
    public void TestPositive()
    {
        var cmd = new Mock<ICommand>();
        cmd.Setup(x => x.Execute()).Verifiable();
        var queue = new Mock<IQueue>();
        queue.Setup(x => x.Get()).Returns(cmd.Object);
        queue.Setup(x => x.Count()).Returns(3);
        var Game = new Game(queue.Object);
        var timer = new Stopwatch();
        timer.Start();
        Game.Execute();
        timer.Stop();
        cmd.Verify(x => x.Execute(), Times.AtLeast(1));
        Assert.True(timer.ElapsedMilliseconds < 58); // при точных 50 милисекундах выдает ошибку
    }
    [Fact]
    public void TestPositiveWithNoElemsInQueue()
    {
        var cmd = new Mock<ICommand>();
        cmd.Setup(x => x.Execute()).Verifiable();
        var queue = new Mock<IQueue>();
        queue.Setup(x => x.Get()).Returns(cmd.Object);
        queue.Setup(x => x.Count()).Returns(0);
        var Game = new Game(queue.Object);
        var timer = new Stopwatch();
        timer.Start();
        Game.Execute();
        timer.Stop();
        cmd.Verify(x => x.Execute(), Times.Never);
        Assert.True(timer.ElapsedMilliseconds < 5);
    }
    [Fact]
    public void TestNegativeCommand()
    {
        var cmd = new Mock<ICommand>();
        cmd.Setup(x => x.Execute()).Throws<Exception>().Verifiable();
        var queue = new Mock<IQueue>();
        queue.Setup(x => x.Get()).Returns(cmd.Object);
        queue.Setup(x => x.Count()).Returns(3);
        var Game = new Game(queue.Object);
        Assert.ThrowsAny<Exception>(() => Game.Execute());
        cmd.Verify(x => x.Execute(), Times.AtMost(1));
    }
    [Fact]
    public void TestNegativeQueue1()
    {
        var queue = new Mock<IQueue>();
        queue.Setup(x => x.Get()).Throws<Exception>();
        queue.Setup(x => x.Count()).Returns(3);
        var Game = new Game(queue.Object);
        Assert.ThrowsAny<Exception>(() => Game.Execute());
    }
    [Fact]
    public void TestNegativeQueue2()
    {
        var cmd = new Mock<ICommand>();
        cmd.Setup(x => x.Execute()).Verifiable();
        var queue = new Mock<IQueue>();
        queue.Setup(x => x.Get()).Returns(cmd.Object);
        queue.Setup(x => x.Count()).Throws<Exception>();
        var Game = new Game(queue.Object);
        Assert.ThrowsAny<Exception>(() => Game.Execute());
    }
}
