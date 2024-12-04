namespace SpaceBattle.Tests;
using Moq;
using SpaceBattle.Lib;
public class StartMoveCommandTest
{
    [Fact]
    public void TestWithoutError()
    {
        // Arrange
        var _gameobject = new Mock<IDictionary<string, object>>();
        var _isender = new Mock<ISender>();
        var _bridgeCommand = new Mock<ICommand>();

        var StartMoveCommand = new StartMoveCommand(_bridgeCommand.Object, _isender.Object, _gameobject.Object);

        // Act
        StartMoveCommand.Execute();

        // Assert

        _isender.Verify(x => x.Send(It.IsAny<ICommand>()), Times.Once);
        _gameobject.VerifySet(x => x["Movement"] = _bridgeCommand.Object, Times.Once);
    }
    [Fact]
    public void TestWithErrorInRefferenceToGameObjectInDictionary()
    {
        // Arrange
        var _gameobject = new Mock<IDictionary<string, object>>();
        _gameobject.SetupSet(x => x["Movement"] = It.IsAny<ICommand>()).Throws<Exception>();
        var _isender = new Mock<ISender>();
        var _bridgeCommand = new Mock<ICommand>();

        var StartMoveCommand = new StartMoveCommand(_bridgeCommand.Object, _isender.Object, _gameobject.Object);

        // Act
        Assert.Throws<Exception>(() => StartMoveCommand.Execute());

        _gameobject.VerifySet(x => x["Movement"] = _bridgeCommand.Object, Times.Once);
        _isender.Verify(x => x.Send(It.IsAny<ICommand>()), Times.Never);
    }
    [Fact]
    public void TestWithErrorInRefferenceToGameObjectInSender()
    {
        // Arrange
        var _gameobject = new Mock<IDictionary<string, object>>();
        var _isender = new Mock<ISender>();
        _isender.Setup(x => x.Send(It.IsAny<ICommand>())).Throws<Exception>();
        var _bridgeCommand = new Mock<ICommand>();

        var StartMoveCommand = new StartMoveCommand(_bridgeCommand.Object, _isender.Object, _gameobject.Object);

        // Act
        Assert.Throws<Exception>(() => StartMoveCommand.Execute());

        _gameobject.VerifySet(x => x["Movement"] = _bridgeCommand.Object, Times.Once);
        _isender.Verify(x => x.Send(It.IsAny<ICommand>()), Times.Once);
    }
}
