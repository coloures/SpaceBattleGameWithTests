namespace SpaceBattle.Tests;
using SpaceBattle.Lib;
using Moq;
public class StopMoveCommandTest
{
    [Fact]
    public void TestWithoutError()
    {
        // Arrange
        var _gameobject = new Mock<IDictionary<string, object>>();
        var _bridgeCommand = new BridgeCommand(new Mock<ICommand>().Object);
        _gameobject.Setup(x => x["Movement"]).Returns(_bridgeCommand);

        var StopMoveCommand = new StopMoveCommand(_gameobject.Object);

        // Act
        StopMoveCommand.Execute();

        // Assert

        Assert.IsType<EmptyCommand>(_bridgeCommand.command);
    }
    [Fact]
    public void TestWithErrorInRefferenceToGameObject()
    {
        //arrange
        var _gameobject = new Mock<IDictionary<string, object>>();
        _gameobject.Setup(x => x["Movement"]).Throws<Exception>();

        var StopMoveCommand = new StopMoveCommand(_gameobject.Object);

        Assert.Throws<Exception>( //Assert
            () => StopMoveCommand.Execute()//Act
        );
    }
}