namespace SpaceBattle.Tests;
using Moq;
using SpaceBattle.Lib;
public class ShootableTest
{
    [Fact]
    public void TestWithoutError()
    {
        var _angle = new Angle(30);
        var _location = new Vector(10, 10);
        var _velocity = new Vector(5, 5);
        var spaceship = new Dictionary<string, object>();
        spaceship.Add("Angle", _angle);
        spaceship.Add("Location", _location);
        spaceship.Add("Velocity", _velocity);

        var shootable = new Shootable(spaceship);

        Assert.Equal(_angle, shootable.GetAngle());
        Assert.Equal(_location, shootable.GetLocation());
        Assert.Equal(_velocity, shootable.GetVelocity());
    }
    [Fact]
    public void TestWithErrorFirst()
    {
        var _location = new Vector(10, 10);
        var _velocity = new Vector(5, 5);
        var spaceship = new Mock<IDictionary<string, object>>();
        spaceship.Setup(x => x["Angle"]).Throws<Exception>();
        spaceship.Setup(x => x["Location"]).Returns(_location);
        spaceship.Setup(x => x["Velocity"]).Returns(_velocity);
        var shootable = new Shootable(spaceship.Object);
        Assert.ThrowsAny<Exception>(() =>
        {
            shootable.GetAngle();
        });
        Assert.Equal(_location, shootable.GetLocation());
        Assert.Equal(_velocity, shootable.GetVelocity());
    }
    [Fact]
    public void TestWithErrorSecond()
    {
        var _angle = new Angle(30);
        var _velocity = new Vector(5, 5);
        var spaceship = new Mock<IDictionary<string, object>>();
        spaceship.Setup(x => x["Angle"]).Returns(_angle);
        spaceship.Setup(x => x["Location"]).Throws<Exception>();
        spaceship.Setup(x => x["Velocity"]).Returns(_velocity);
        var shootable = new Shootable(spaceship.Object);
        Assert.Equal(_angle, shootable.GetAngle());
        Assert.ThrowsAny<Exception>(() =>
        {
            shootable.GetLocation();
        });
        Assert.Equal(_velocity, shootable.GetVelocity());
    }
    [Fact]
    public void TestWithErrorLast()
    {
        var _angle = new Angle(30);
        var _location = new Vector(10, 10);
        var spaceship = new Mock<IDictionary<string, object>>();
        spaceship.Setup(x => x["Angle"]).Returns(_angle);
        spaceship.Setup(x => x["Location"]).Returns(_location);
        spaceship.Setup(x => x["Velocity"]).Throws<Exception>();
        var shootable = new Shootable(spaceship.Object);
        Assert.Equal(_angle, shootable.GetAngle());
        Assert.Equal(_location, shootable.GetLocation());
        Assert.ThrowsAny<Exception>(() =>
        {
            shootable.GetVelocity();
        });
    }
}
