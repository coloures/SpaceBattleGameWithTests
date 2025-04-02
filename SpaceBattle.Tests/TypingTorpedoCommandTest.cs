namespace SpaceBattle.Tests;
using Moq;
using SpaceBattle.Lib;
public class TypingTorpedoCommandTest
{
    [Fact]
    public void TestWithoutError()
    {
        var _ityping = new Mock<ITyping>();
        _ityping.SetupSet(x => x.Type = It.IsAny<string>()).Verifiable();
        var cmd = new TypingTorpedoCommand(_ityping.Object);
        cmd.Execute();
        _ityping.VerifySet(x => x.Type = "Torpedo");

    }
    [Fact]
    public void TestWithError()
    {
        var _ityping = new Mock<ITyping>();
        _ityping.SetupSet(x => x.Type = It.IsAny<string>()).Throws<Exception>().Verifiable();
        var cmd = new TypingTorpedoCommand(_ityping.Object);
        Assert.ThrowsAny<Exception>(() => cmd.Execute());
        _ityping.VerifySet(x => x.Type = "Torpedo");

    }
}
