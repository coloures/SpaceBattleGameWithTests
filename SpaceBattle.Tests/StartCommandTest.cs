namespace SpaceBattle.Tests;
using Moq;
using SpaceBattle.Lib;
public class StartCommandTest
{
    [Fact]
    public void TestWithoutError()
    {
        // Arrange
        var _gameobject = new Mock<IDictionary<string, object>>();
        var _isender = new Mock<ISender>();
        var _bridgeCommand = new Mock<ICommand>();
        var _label = "Movement";

        var StartCommand = new StartCommand(_bridgeCommand.Object, _isender.Object, _gameobject.Object, _label);

        // Act
        StartCommand.Execute();

        // Assert

        _isender.Verify(x => x.Send(It.IsAny<ICommand>()), Times.Once);
        _gameobject.Verify(x => x.Remove(_label), Times.Never);
        _gameobject.VerifySet(x => x[_label] = _bridgeCommand.Object, Times.Once);
    }
    [Fact]
    public void TestWithErrorInRefferenceToGameObjectInDictionary()
    {
        // Arrange
        var _gameobject = new Mock<IDictionary<string, object>>();
        var _label = "Movement";
        _gameobject.SetupSet(x => x[_label] = It.IsAny<ICommand>()).Throws<Exception>();
        var _isender = new Mock<ISender>();
        var _bridgeCommand = new Mock<ICommand>();

        var StartCommand = new StartCommand(_bridgeCommand.Object, _isender.Object, _gameobject.Object, _label);

        // Act
        Assert.Throws<Exception>(() => StartCommand.Execute());

        _gameobject.VerifySet(x => x[_label] = _bridgeCommand.Object, Times.Once);
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
        var _label = "Movement";

        var StartCommand = new StartCommand(_bridgeCommand.Object, _isender.Object, _gameobject.Object, _label);

        // Act
        StartCommand.Execute();

        _gameobject.VerifySet(x => x[_label] = _bridgeCommand.Object, Times.Once);
        _gameobject.Verify(x => x.Remove(_label), Times.Once);
        _isender.Verify(x => x.Send(It.IsAny<ICommand>()), Times.Once);
    }
}
