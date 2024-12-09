using Moq;
using SpaceBattle.Lib;
namespace SpaceBattle.Tests;

public class MoveCommandTest
{
    [Fact]
    public void TestPositive()
    {
        //arrange
        var MovingObject = new Mock<IMovingObject>();
        MovingObject.SetupGet(m => m.Location).Returns(new Vector(12, 5)).Verifiable();
        MovingObject.SetupGet(m => m.Velocity).Returns(new Vector(-5, 3)).Verifiable();
        var cmd = new Move(MovingObject.Object);

        //act
        cmd.Execute();

        //post
        //movable // pos == (7, 8)

        MovingObject.VerifySet(m => m.Location = new Vector(7, 8), Times.Once);
        MovingObject.VerifyAll();
    }
    [Fact]
    public void TestLocationRaisesException()
    {
        var MovingObject = new Mock<IMovingObject>();
        MovingObject.SetupGet(m => m.Location).Throws<Exception>();
        MovingObject.SetupGet(m => m.Velocity).Returns(new Vector(1, 1)).Verifiable();
        var cmd = new Move(MovingObject.Object);

        Assert.Throws<Exception>( //Assert
            () => cmd.Execute() //Act
        );
    }
    [Fact]
    public void TestVelocityRaisesException()
    {
        var MovingObject = new Mock<IMovingObject>();
        MovingObject.SetupGet(m => m.Location).Returns(new Vector(1, 1)).Verifiable();
        MovingObject.SetupGet(m => m.Velocity).Throws<Exception>();
        var cmd = new Move(MovingObject.Object);

        Assert.Throws<Exception>( //Assert
            () => cmd.Execute() //Act
        );
    }
    [Fact]
    public void TestExecutingException()
    {
        var MovingObject = new Mock<IMovingObject>();
        MovingObject.SetupGet(m => m.Location).Returns(new Vector(12, 5)).Verifiable();
        MovingObject.SetupGet(m => m.Velocity).Returns(new Vector(-5, 3)).Verifiable();
        MovingObject.SetupSet(m => m.Location = It.IsAny<Vector>()).Throws<Exception>();
        var cmd = new Move(MovingObject.Object);

        Assert.Throws<Exception>( //Assert
            () => cmd.Execute() //Act
        );
    }
}
