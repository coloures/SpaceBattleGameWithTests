namespace SpaceBattle.Tests;
using Moq;
using SpaceBattle.Lib;

public class MacroCommandTest
{
    [Fact]
    public void TestPositive()
    {
        var command1 = new Mock<ICommand>();
        var command2 = new Mock<ICommand>();
        var MacroCommand = new MacroCommand(new ICommand[] { command1.Object, command2.Object });

        MacroCommand.Execute();
    }
    [Fact]
    public void TestNegativeFirstCommand()
    {
        var command1 = new Mock<ICommand>();
        command1.Setup(x => x.Execute()).Throws<Exception>();
        var command2 = new Mock<ICommand>();
        var MacroCommand = new MacroCommand(new ICommand[] { command1.Object, command2.Object });

        Assert.Throws<Exception>(() => MacroCommand.Execute());

        command1.Verify(cmd => cmd.Execute(), Times.Once);
        command2.Verify(cmd => cmd.Execute(), Times.Never);
    }
    [Fact]
    public void TestNegativeMiddleCommand()
    {
        var command1 = new Mock<ICommand>();
        var command2 = new Mock<ICommand>();
        command2.Setup(x => x.Execute()).Throws<Exception>();
        var command3 = new Mock<ICommand>();
        var MacroCommand = new MacroCommand(new ICommand[] { command1.Object, command2.Object, command3.Object });

        Assert.Throws<Exception>(() => MacroCommand.Execute());

        command1.Verify(cmd => cmd.Execute(), Times.Once);
        command2.Verify(cmd => cmd.Execute(), Times.Once);
        command3.Verify(cmd => cmd.Execute(), Times.Never);
    }
}
