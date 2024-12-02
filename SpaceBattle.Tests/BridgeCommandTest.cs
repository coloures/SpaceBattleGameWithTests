namespace SpaceBattle.Tests;
using SpaceBattle.Lib;
using Moq;

public class BridgeCommandTest
{
    [Fact]
    public void TestPositive()
    {
        var command = new Mock<ICommand>();
        var BridgeCommand = new BridgeCommand(command.Object);
        BridgeCommand.Execute();

        command.Verify(x => x.Execute(), Times.Once);
    }
    [Fact]
    public void TestNegative()
    {
        var command = new Mock<ICommand>();
        command.Setup(x => x.Execute()).Throws<Exception>();
        var BridgeCommand = new BridgeCommand(command.Object);

        Assert.Throws<Exception>(() => BridgeCommand.Execute());
    }
    [Fact]
    public void TestInjectingPositive()
    {
        var command = new Mock<ICommand>();
        var BridgeCommand = new BridgeCommand(command.Object);
        var InjectedCommand = new Mock<ICommand>();

        BridgeCommand.Inject(InjectedCommand.Object);
        BridgeCommand.Execute();

        InjectedCommand.Verify(x => x.Execute(), Times.Once);
    }
    [Fact]
    public void TestInjectingNegative()
    {
        var command = new Mock<ICommand>();
        var BridgeCommand = new Mock<IBridgeCommand>();
        BridgeCommand.SetupSet(x => x.command = command.Object).Verifiable();
        BridgeCommand.Setup(x => x.Inject(It.IsAny<ICommand>())).Throws<Exception>();
        var InjectedCommand = new Mock<ICommand>();

        Assert.Throws<Exception>(() => BridgeCommand.Object.Inject(InjectedCommand.Object));
    }
}