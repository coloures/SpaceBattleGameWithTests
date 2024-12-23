namespace SpaceBattle.Tests;
using Moq;
using SpaceBattle.Lib;

public class SendCommandTest
{
    [Fact]
    public void TestWithoutError()
    {
        var _isender = new Mock<ISender>();
        _isender.Setup(x => x.Send(It.IsAny<ICommand>())).Verifiable();
        var _cmd = new Mock<ICommand>();

        var SendCommand = new SendCommand(_cmd.Object, _isender.Object);
        SendCommand.Execute();

        _isender.Verify(x => x.Send(It.IsAny<ICommand>()), Times.Once);
    }
    [Fact]
    public void TestWithError()
    {
        var _isender = new Mock<ISender>();
        _isender.Setup(x => x.Send(It.IsAny<ICommand>())).Throws<Exception>();
        var _cmd = new Mock<ICommand>();

        var SendCommand = new SendCommand(_cmd.Object, _isender.Object);
        Assert.ThrowsAny<Exception>(() => SendCommand.Execute());
    }
}
