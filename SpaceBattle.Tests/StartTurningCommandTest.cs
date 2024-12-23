namespace SpaceBattle.Tests;
using Moq;
using SpaceBattle.Lib;

public class StartTurningCommandTest
{
    [Fact]
    public void TestWithoutError()
    {
        //arrange
        var _isender = new Mock<ISender>();
        var _gameobject = new Mock<IDictionary<string, object>>();
        var _macrocommand = new Mock<ICommand>();
        _gameobject.SetupGet(m => m["Turn"]).Returns(new Mock<ICommand>()).Verifiable();
        var StartTurningCommand = new StartTurningCommand(_macrocommand.Object, _isender.Object, _gameobject.Object);

        //act
        StartTurningCommand.Execute();

        //post
        _gameobject.VerifySet(m => m["Turn"] = _macrocommand.Object, Times.Once);
        _isender.Verify(sender => sender.Send(It.IsAny<ICommand>()), Times.Once);
    }

    [Fact]
    public void TestWithErrorInRefferenceToGameObject()
    {
        //arrange
        var _isender = new Mock<ISender>();
        var _gameobject = new Mock<IDictionary<string, object>>();
        var _macrocommand = new Mock<ICommand>();
        _gameobject.SetupSet(m => m["Turn"] = It.IsAny<ICommand>()).Throws<Exception>();
        var StartTurningCommand = new StartTurningCommand(_macrocommand.Object, _isender.Object, _gameobject.Object);

        Assert.Throws<Exception>( //Assert
            () => StartTurningCommand.Execute()//Act
        );

        //post
        _isender.Verify(sender => sender.Send(It.IsAny<ICommand>()), Times.Never);
    }

    [Fact]
    public void TestWithErrorInRefferenceToISender()
    {
        //arrange
        var _isender = new Mock<ISender>();
        var _gameobject = new Mock<IDictionary<string, object>>();
        var _macrocommand = new Mock<ICommand>();
        _isender.Setup(m => m.Send(It.IsAny<ICommand>())).Throws<Exception>();
        var StartTurningCommand = new StartTurningCommand(_macrocommand.Object, _isender.Object, _gameobject.Object);

        Assert.Throws<Exception>( //Assert
            () => StartTurningCommand.Execute()//Act
        );

        //post
        _gameobject.VerifySet(m => m["Turn"] = _macrocommand.Object, Times.Once);
        _isender.Verify(sender => sender.Send(It.IsAny<ICommand>()), Times.Once);
    }
}
