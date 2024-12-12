namespace SpaceBattle.Tests;
using Moq;
using SpaceBattle.Lib;
public class StopMoveCommandTest
{
    [Fact]
    public void TestWithoutError()
    {
        //arrange
        var _isender = new Mock<ISender>();
        var _gameobject = new Mock<IDictionary<string, object>>();
        _gameobject.SetupGet(m => m["Movement"]).Returns(new Mock<ICommand>()).Verifiable();
        var StopMoveCommand = new StopMoveCommand(_isender.Object, _gameobject.Object);

        //act
        StopMoveCommand.Execute();

        //post
        _gameobject.VerifySet(m => m["Movement"] = It.IsAny<EmptyCommand>(), Times.Once);
        _isender.Verify(sender => sender.Send(It.IsAny<EmptyCommand>()), Times.Once);
    }
    [Fact]
    public void TestWithErrorInRefferenceToGameObject()
    {
        //arrange
        var _isender = new Mock<ISender>();
        var _gameobject = new Mock<IDictionary<string, object>>();
        _gameobject.SetupSet(m => m["Movement"] = It.IsAny<ICommand>()).Throws<Exception>();
        var StopMoveCommand = new StopMoveCommand(_isender.Object, _gameobject.Object);

        Assert.Throws<Exception>( //Assert
            () => StopMoveCommand.Execute()//Act
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
        var StopMoveCommand = new StopMoveCommand(_isender.Object, _gameobject.Object);

        Assert.Throws<Exception>( //Assert
            () => StopMoveCommand.Execute()//Act
        );

        //post
        _gameobject.VerifySet(m => m["Movement"] = It.IsAny<EmptyCommand>(), Times.Once);
        _isender.Verify(sender => sender.Send(It.IsAny<EmptyCommand>()), Times.Once);
    }
}
