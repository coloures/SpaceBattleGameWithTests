namespace SpaceBattle.Tests;
using Moq;
using SpaceBattle.Lib;
public class StopTurningCommandTest
{
    [Fact]
    public void TestWithoutError()
    {
        //arrange
        var _isender = new Mock<ISender>();
        var _gameobject = new Mock<IDictionary<string, object>>();
        _gameobject.SetupGet(m => m["Turn"]).Returns(new Mock<ICommand>()).Verifiable();
        var StopTurningCommand = new StopTurningCommand(_isender.Object, _gameobject.Object);

        //act
        StopTurningCommand.Execute();

        //post
        _gameobject.VerifySet(m => m["Turn"] = It.IsAny<EmptyCommand>(), Times.Once);
        _isender.Verify(sender => sender.Send(It.IsAny<EmptyCommand>()), Times.Once);
    }
    [Fact]
    public void TestWithErrorInRefferenceToGameObject()
    {
        //arrange
        var _isender = new Mock<ISender>();
        var _gameobject = new Mock<IDictionary<string, object>>();
        _gameobject.SetupSet(m => m["Turn"] = It.IsAny<ICommand>()).Throws<Exception>();
        var StopTurningCommand = new StopTurningCommand(_isender.Object, _gameobject.Object);

        Assert.Throws<Exception>( //Assert
            () => StopTurningCommand.Execute()//Act
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
        _isender.Setup(m => m.Send(It.IsAny<ICommand>())).Throws<Exception>();
        var StopTurningCommand = new StopTurningCommand(_isender.Object, _gameobject.Object);

        Assert.Throws<Exception>( //Assert
            () => StopTurningCommand.Execute()//Act
        );

        //post
        _gameobject.VerifySet(m => m["Turn"] = It.IsAny<EmptyCommand>(), Times.Once);
        _isender.Verify(sender => sender.Send(It.IsAny<EmptyCommand>()), Times.Once);
    }
}
