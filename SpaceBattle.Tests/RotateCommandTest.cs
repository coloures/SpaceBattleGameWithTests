namespace SpaceBattle.Tests;
using Moq;
using SpaceBattle.Lib;

public class RotateCommandTest
{
    [Fact]
    public void TestPositive()
    {
        //arrange
        var RotatingObject = new Mock<IRotatingObject>();
        RotatingObject.SetupGet(m => m.angle).Returns(new Angle(45)).Verifiable();
        RotatingObject.SetupGet(m => m.Velocity).Returns(new Angle(90)).Verifiable();
        var cmd = new Rotate(RotatingObject.Object);

        //act
        cmd.Execute();

        //post
        //rotateble // Angle == (135, 360)

        RotatingObject.VerifySet(m => m.angle = new Angle(135), Times.Once);

        RotatingObject.VerifyAll();
    }
    [Fact]
    public void TestLocationRaisesException()
    {
        var RotatingObject = new Mock<IRotatingObject>();
        RotatingObject.SetupGet(m => m.angle).Throws<Exception>();
        RotatingObject.SetupGet(m => m.Velocity).Returns(new Angle(90)).Verifiable();
        var cmd = new Rotate(RotatingObject.Object);

        Assert.Throws<Exception>( //Assert
            () => cmd.Execute() //Act
        );
    }
    [Fact]
    public void TestVelocityRaisesException()
    {
        var RotatingObject = new Mock<IRotatingObject>();
        RotatingObject.SetupGet(m => m.angle).Returns(new Angle(45)).Verifiable();
        RotatingObject.SetupGet(m => m.Velocity).Throws<Exception>();
        var cmd = new Rotate(RotatingObject.Object);

        Assert.Throws<Exception>( //Assert
            () => cmd.Execute() //Act
        );
    }
    [Fact]
    public void TestExecutingException()
    {
        var RotatingObject = new Mock<IRotatingObject>();
        RotatingObject.SetupGet(m => m.angle).Returns(new Angle(45)).Verifiable();
        RotatingObject.SetupGet(m => m.Velocity).Returns(new Angle(90)).Verifiable();
        RotatingObject.SetupSet(m => m.angle = It.IsAny<Angle>()).Throws<Exception>();
        var cmd = new Rotate(RotatingObject.Object);

        Assert.Throws<Exception>( //Assert
            () => cmd.Execute() //Act
        );
    }
}
