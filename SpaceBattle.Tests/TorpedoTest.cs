namespace SpaceBattle.Tests;
using Moq;
using SpaceBattle.Lib;
public class TorpedoTest
{
    [Fact]
    public void TestWithoutError()
    {
        var _angle = new Angle(30);
        var _location = new Vector(10, 10);
        var _velocity = new Vector(5, 5);
        var _angle1 = new Angle(45);
        var _location1 = new Vector(15, 15);
        var _velocity1 = new Vector(7, 7);
        var any_dict1 = new Dictionary<string, object>();
        var any_dict2 = new Dictionary<string, object>();
        any_dict2.Add("Angle", _angle);
        any_dict2.Add("Location", _location);
        any_dict2.Add("Velocity", _velocity);

        var torpedo1 = new Torpedo(any_dict1);
        var torpedo2 = new Torpedo(any_dict2);

        torpedo1.SetAngle(_angle1);
        torpedo1.SetLocation(_location1);
        torpedo1.SetVelocity(_velocity1);

        torpedo2.SetAngle(_angle1);
        torpedo2.SetLocation(_location1);
        torpedo2.SetVelocity(_velocity1);

        Assert.Equal(_angle1, any_dict1["Angle"]);
        Assert.Equal(_angle1, any_dict2["Angle"]);
        Assert.Equal(_location1, any_dict1["Location"]);
        Assert.Equal(_location1, any_dict2["Location"]);
        Assert.Equal(_velocity1, any_dict1["Velocity"]);
        Assert.Equal(_velocity1, any_dict2["Velocity"]);
    }
    [Fact]
    public void TestWithErrorOfCreating()
    {
        var dict = new Mock<IDictionary<string, object>>();
        dict.Setup(x => x.ContainsKey(It.IsAny<string>())).Returns(false);
        dict.Setup(x => x.Add(It.IsAny<string>(), It.IsAny<object>())).Throws<Exception>().Verifiable();
        var _angle1 = new Angle(45);
        var _location1 = new Vector(15, 15);
        var _velocity1 = new Vector(7, 7);

        var torpedo1 = new Torpedo(dict.Object);

        Assert.ThrowsAny<Exception>(() => torpedo1.SetAngle(_angle1));
        Assert.ThrowsAny<Exception>(() => torpedo1.SetLocation(_location1));
        Assert.ThrowsAny<Exception>(() => torpedo1.SetVelocity(_velocity1));

        dict.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<object>()), Times.AtLeast(3));
        dict.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<object>()), Times.AtMost(3));
    }
    [Fact]
    public void TestWithErrorOfChanging()
    {
        var dict = new Mock<IDictionary<string, object>>();
        dict.Setup(x => x.ContainsKey(It.IsAny<string>())).Returns(true);
        dict.SetupSet(x => x["Angle"] = It.IsAny<object>()).Throws<Exception>().Verifiable();
        dict.SetupSet(x => x["Location"] = It.IsAny<object>()).Throws<Exception>().Verifiable();
        dict.SetupSet(x => x["Velocity"] = It.IsAny<object>()).Throws<Exception>().Verifiable();
        var _angle1 = new Angle(45);
        var _location1 = new Vector(15, 15);
        var _velocity1 = new Vector(7, 7);

        var torpedo1 = new Torpedo(dict.Object);

        Assert.ThrowsAny<Exception>(() => torpedo1.SetAngle(_angle1));
        Assert.ThrowsAny<Exception>(() => torpedo1.SetLocation(_location1));
        Assert.ThrowsAny<Exception>(() => torpedo1.SetVelocity(_velocity1));
        dict.VerifySet(x => x["Angle"] = It.IsAny<object>(), Times.Once);
        dict.VerifySet(x => x["Location"] = It.IsAny<object>(), Times.Once);
        dict.VerifySet(x => x["Velocity"] = It.IsAny<object>(), Times.Once);
    }
    [Fact]
    public void TestWithErrorOfAskingForContaining()
    {
        var dict = new Mock<IDictionary<string, object>>();
        dict.Setup(x => x.ContainsKey(It.IsAny<string>())).Throws<Exception>().Verifiable();
        dict.SetupSet(x => x["Angle"] = It.IsAny<object>()).Verifiable();
        dict.SetupSet(x => x["Location"] = It.IsAny<object>()).Verifiable();
        dict.SetupSet(x => x["Velocity"] = It.IsAny<object>()).Verifiable();
        dict.Setup(x => x.Add(It.IsAny<string>(), It.IsAny<object>())).Throws<Exception>().Verifiable();
        var _angle1 = new Angle(45);
        var _location1 = new Vector(15, 15);
        var _velocity1 = new Vector(7, 7);

        var torpedo1 = new Torpedo(dict.Object);

        Assert.ThrowsAny<Exception>(() => torpedo1.SetAngle(_angle1));
        Assert.ThrowsAny<Exception>(() => torpedo1.SetLocation(_location1));
        Assert.ThrowsAny<Exception>(() => torpedo1.SetVelocity(_velocity1));
        dict.Verify(x => x.ContainsKey(It.IsAny<string>()), Times.AtLeast(3));
        dict.Verify(x => x.ContainsKey(It.IsAny<string>()), Times.AtMost(3));
        dict.VerifySet(x => x["Angle"] = It.IsAny<object>(), Times.Never);
        dict.VerifySet(x => x["Location"] = It.IsAny<object>(), Times.Never);
        dict.VerifySet(x => x["Velocity"] = It.IsAny<object>(), Times.Never);
        dict.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<object>()), Times.Never());
    }
}
