namespace SpaceBattle.Tests;
using Moq;
using SpaceBattle.Lib;
public class StopCommandTest
{
    [Fact]
    public void TestWithoutError()
    {
        // Arrange
        var _gameobject = new Mock<IDictionary<string, object>>();
        var _isender = new Mock<ISender>();
        var _label = "Movement";

        var StopCommand = new StopCommand(_isender.Object, _gameobject.Object, _label);

        // Act
        StopCommand.Execute();

        // Assert
        _gameobject.VerifySet(x => x[_label] = It.IsAny<EmptyCommand>(), Times.Once);
        _isender.Verify(x => x.Send(It.IsAny<EmptyCommand>()), Times.Once);
    }
    [Fact]
    public void TestWithErrorInRefferenceToGameObjectInDictionary()
    {
        // Arrange
        var _gameobject = new Mock<IDictionary<string, object>>();
        var _label = "Movement";
        _gameobject.SetupSet(x => x[_label] = It.IsAny<ICommand>()).Throws<Exception>();
        var _isender = new Mock<ISender>();

        var StopCommand = new StopCommand(_isender.Object, _gameobject.Object, _label);

        // Act
        Assert.Throws<Exception>(() => StopCommand.Execute());
        _isender.Verify(x => x.Send(It.IsAny<EmptyCommand>()), Times.Never);
    }
    [Fact]
    public void TestWithErrorInRefferenceToGameObjectInSender()
    {
        // Arrange
        var _gameobject = new Mock<IDictionary<string, object>>();
        var _isender = new Mock<ISender>();
        _isender.Setup(x => x.Send(It.IsAny<ICommand>())).Throws<Exception>();
        var _label = "Movement";

        var StopCommand = new StopCommand(_isender.Object, _gameobject.Object, _label);

        // Act
        Assert.Throws<Exception>(() => StopCommand.Execute());

        _gameobject.VerifySet(x => x[_label] = It.IsAny<EmptyCommand>(), Times.Once);
        _isender.Verify(x => x.Send(It.IsAny<EmptyCommand>()), Times.Once);
    }
}
