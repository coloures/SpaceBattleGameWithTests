namespace SpaceBattle.Tests;
using Moq;
using SpaceBattle.Lib;
public class SetupTorpedoCommandTest
{
    [Fact]
    public void TestWithoutError()
    {
        var _angle = new Angle(30);
        var _velocity = new Vector(10, 10);
        var _location = new Vector(10, 10);
        var _IShootable = new Mock<IShootable>();
        var _ITorpedo = new Mock<ITorpedo>();
        _IShootable.Setup(x => x.GetAngle()).Returns(_angle);
        _IShootable.Setup(x => x.GetLocation()).Returns(_location);
        _IShootable.Setup(x => x.GetVelocity()).Returns(_velocity);
        _ITorpedo.Setup(x => x.SetAngle(It.IsAny<Angle>())).Verifiable();
        _ITorpedo.Setup(x => x.SetLocation(It.IsAny<Vector>())).Verifiable();
        _ITorpedo.Setup(x => x.SetVelocity(It.IsAny<Vector>())).Verifiable();
        var cmd = new SetupTorpedoCommand(_ITorpedo.Object, _IShootable.Object);

        cmd.Execute();

        _ITorpedo.Verify(x => x.SetAngle(_angle), Times.Once);
        _ITorpedo.Verify(x => x.SetLocation(_location), Times.Once);
        _ITorpedo.Verify(x => x.SetVelocity(_velocity), Times.Once);
    }
    [Fact]
    public void TestWithErrorAngleGetter()
    {
        var _angle = new Angle(30);
        var _velocity = new Vector(10, 10);
        var _location = new Vector(10, 10);
        var _IShootable = new Mock<IShootable>();
        var _ITorpedo = new Mock<ITorpedo>();
        _IShootable.Setup(x => x.GetAngle()).Throws<Exception>();
        _IShootable.Setup(x => x.GetLocation()).Returns(_location);
        _IShootable.Setup(x => x.GetVelocity()).Returns(_velocity);
        _ITorpedo.Setup(x => x.SetAngle(It.IsAny<Angle>())).Verifiable();
        _ITorpedo.Setup(x => x.SetLocation(It.IsAny<Vector>())).Verifiable();
        _ITorpedo.Setup(x => x.SetVelocity(It.IsAny<Vector>())).Verifiable();
        var cmd = new SetupTorpedoCommand(_ITorpedo.Object, _IShootable.Object);

        Assert.ThrowsAny<Exception>(() => cmd.Execute());

        _ITorpedo.Verify(x => x.SetAngle(_angle), Times.Never);
        _ITorpedo.Verify(x => x.SetLocation(_location), Times.Never);
        _ITorpedo.Verify(x => x.SetVelocity(_velocity), Times.Never);
    }
    [Fact]
    public void TestWithErrorAngleSetter()
    {
        var _angle = new Angle(30);
        var _velocity = new Vector(10, 10);
        var _location = new Vector(10, 10);
        var _IShootable = new Mock<IShootable>();
        var _ITorpedo = new Mock<ITorpedo>();
        _IShootable.Setup(x => x.GetAngle()).Returns(_angle);
        _IShootable.Setup(x => x.GetLocation()).Returns(_location);
        _IShootable.Setup(x => x.GetVelocity()).Returns(_velocity);
        _ITorpedo.Setup(x => x.SetAngle(It.IsAny<Angle>())).Throws<Exception>().Verifiable();
        _ITorpedo.Setup(x => x.SetLocation(It.IsAny<Vector>())).Verifiable();
        _ITorpedo.Setup(x => x.SetVelocity(It.IsAny<Vector>())).Verifiable();
        var cmd = new SetupTorpedoCommand(_ITorpedo.Object, _IShootable.Object);

        Assert.ThrowsAny<Exception>(() => cmd.Execute());

        _ITorpedo.Verify(x => x.SetAngle(_angle), Times.Once);
        _ITorpedo.Verify(x => x.SetLocation(_location), Times.Never);
        _ITorpedo.Verify(x => x.SetVelocity(_velocity), Times.Never);
    }
    [Fact]
    public void TestWithErrorLocationGetter()
    {
        var _angle = new Angle(30);
        var _velocity = new Vector(10, 10);
        var _location = new Vector(10, 10);
        var _IShootable = new Mock<IShootable>();
        var _ITorpedo = new Mock<ITorpedo>();
        _IShootable.Setup(x => x.GetAngle()).Returns(_angle);
        _IShootable.Setup(x => x.GetLocation()).Throws<Exception>();
        _IShootable.Setup(x => x.GetVelocity()).Returns(_velocity);
        _ITorpedo.Setup(x => x.SetAngle(It.IsAny<Angle>())).Verifiable();
        _ITorpedo.Setup(x => x.SetLocation(It.IsAny<Vector>())).Verifiable();
        _ITorpedo.Setup(x => x.SetVelocity(It.IsAny<Vector>())).Verifiable();
        var cmd = new SetupTorpedoCommand(_ITorpedo.Object, _IShootable.Object);

        Assert.ThrowsAny<Exception>(() => cmd.Execute());

        _ITorpedo.Verify(x => x.SetAngle(_angle), Times.Once);
        _ITorpedo.Verify(x => x.SetLocation(_location), Times.Never);
        _ITorpedo.Verify(x => x.SetVelocity(_velocity), Times.Never);
    }
    [Fact]
    public void TestWithErrorLocationSetter()
    {
        var _angle = new Angle(30);
        var _velocity = new Vector(10, 10);
        var _location = new Vector(10, 10);
        var _IShootable = new Mock<IShootable>();
        var _ITorpedo = new Mock<ITorpedo>();
        _IShootable.Setup(x => x.GetAngle()).Returns(_angle);
        _IShootable.Setup(x => x.GetLocation()).Returns(_location);
        _IShootable.Setup(x => x.GetVelocity()).Returns(_velocity);
        _ITorpedo.Setup(x => x.SetAngle(It.IsAny<Angle>())).Verifiable();
        _ITorpedo.Setup(x => x.SetLocation(It.IsAny<Vector>())).Throws<Exception>().Verifiable();
        _ITorpedo.Setup(x => x.SetVelocity(It.IsAny<Vector>())).Verifiable();
        var cmd = new SetupTorpedoCommand(_ITorpedo.Object, _IShootable.Object);

        Assert.ThrowsAny<Exception>(() => cmd.Execute());

        _ITorpedo.Verify(x => x.SetAngle(_angle), Times.Once);
        _ITorpedo.Verify(x => x.SetLocation(_location), Times.Once);
        _ITorpedo.Verify(x => x.SetVelocity(_velocity), Times.Never);
    }
    [Fact]
    public void TestWithErrorVelocityGetter()
    {
        var _angle = new Angle(30);
        var _velocity = new Vector(10, 10);
        var _location = new Vector(10, 10);
        var _IShootable = new Mock<IShootable>();
        var _ITorpedo = new Mock<ITorpedo>();
        _IShootable.Setup(x => x.GetAngle()).Returns(_angle);
        _IShootable.Setup(x => x.GetLocation()).Returns(_location);
        _IShootable.Setup(x => x.GetVelocity()).Throws<Exception>();
        _ITorpedo.Setup(x => x.SetAngle(It.IsAny<Angle>())).Verifiable();
        _ITorpedo.Setup(x => x.SetLocation(It.IsAny<Vector>())).Verifiable();
        _ITorpedo.Setup(x => x.SetVelocity(It.IsAny<Vector>())).Verifiable();
        var cmd = new SetupTorpedoCommand(_ITorpedo.Object, _IShootable.Object);

        Assert.ThrowsAny<Exception>(() => cmd.Execute());

        _ITorpedo.Verify(x => x.SetAngle(_angle), Times.Once);
        _ITorpedo.Verify(x => x.SetLocation(_location), Times.Once);
        _ITorpedo.Verify(x => x.SetVelocity(_velocity), Times.Never);
    }
    [Fact]
    public void TestWithErrorVelocitySetter()
    {
        var _angle = new Angle(30);
        var _velocity = new Vector(10, 10);
        var _location = new Vector(10, 10);
        var _IShootable = new Mock<IShootable>();
        var _ITorpedo = new Mock<ITorpedo>();
        _IShootable.Setup(x => x.GetAngle()).Returns(_angle);
        _IShootable.Setup(x => x.GetLocation()).Returns(_location);
        _IShootable.Setup(x => x.GetVelocity()).Returns(_velocity);
        _ITorpedo.Setup(x => x.SetAngle(It.IsAny<Angle>())).Verifiable();
        _ITorpedo.Setup(x => x.SetLocation(It.IsAny<Vector>())).Verifiable();
        _ITorpedo.Setup(x => x.SetVelocity(It.IsAny<Vector>())).Throws<Exception>().Verifiable();
        var cmd = new SetupTorpedoCommand(_ITorpedo.Object, _IShootable.Object);

        Assert.ThrowsAny<Exception>(() => cmd.Execute());

        _ITorpedo.Verify(x => x.SetAngle(_angle), Times.Once);
        _ITorpedo.Verify(x => x.SetLocation(_location), Times.Once);
        _ITorpedo.Verify(x => x.SetVelocity(_velocity), Times.Once);
    }
}
