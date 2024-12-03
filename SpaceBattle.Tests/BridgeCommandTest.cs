namespace SpaceBattle.Tests;
using Moq;
using SpaceBattle.Lib;

public class BridgeCommandTest
{
    [Fact]
    public void TestPositive()
    {
        var command = new Mock<ICommand>();
        var BridgeCommand = new BridgeCommand();
        BridgeCommand.Inject(command.Object);
        BridgeCommand.Execute();

        command.Verify(x => x.Execute(), Times.Once);
    }
    [Fact]
    public void TestExecutingNegative()
    {
        var command = new Mock<ICommand>();
        command.Setup(x => x.Execute()).Throws<Exception>();
        var BridgeCommand = new BridgeCommand();
        BridgeCommand.Inject(command.Object);

        Assert.Throws<Exception>(() => BridgeCommand.Execute());
    }
    [Fact]
    public void TestInjectingNegative()
    {
        var BridgeCommand = new BridgeCommand();

        Assert.Throws<NullReferenceException>(() => BridgeCommand.Execute());
    }
}
