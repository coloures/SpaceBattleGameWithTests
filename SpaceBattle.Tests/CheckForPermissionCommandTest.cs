using Moq;
using SpaceBattle.Lib;
namespace SpaceBattle.Tests;

public class CheckForPermissionCommandTest
{
    [Fact]
    public void TestPositive()
    {
        var rigths = new List<string>() {"Shoot", "Create", "Move"};
        var owner = new Mock<IOwner>();
        owner.Setup(x => x.GetRights()).Returns(rigths).Verifiable();
        var order = new Dictionary<string, object>();
        order["Command"] = "Move";
        var cmd = new CheckForPermissionCommand(owner.Object, order);
        cmd.Execute();
        owner.Verify(x => x.GetRights(), Times.Once);
    }
    [Fact]
    public void TestNegative()
    {
        var rigths = new List<string>() {"Shoot", "Create"};
        var owner = new Mock<IOwner>();
        owner.Setup(x => x.GetRights()).Returns(rigths).Verifiable();
        var order = new Dictionary<string, object>();
        order["Command"] = "Move";
        var cmd = new CheckForPermissionCommand(owner.Object, order);
        Assert.ThrowsAny<Exception>(() => cmd.Execute());
        owner.Verify(x => x.GetRights(), Times.Once);
    }
    [Fact]
    public void TestNoElemInDict()
    {
        var rigths = new List<string>() {"Shoot", "Create"};
        var owner = new Mock<IOwner>();
        owner.Setup(x => x.GetRights()).Returns(rigths).Verifiable();
        var order = new Dictionary<string, object>();
        var cmd = new CheckForPermissionCommand(owner.Object, order);
        Assert.ThrowsAny<Exception>(() => cmd.Execute());
        owner.Verify(x => x.GetRights(), Times.Once);
    }
    [Fact]
    public void TestExceptionInGettingRigths()
    {
        var owner = new Mock<IOwner>();
        owner.Setup(x => x.GetRights()).Throws<NullReferenceException>().Verifiable();
        var order = new Dictionary<string, object>();
        order["Command"] = "Move";
        var cmd = new CheckForPermissionCommand(owner.Object, order);
        Assert.Throws<NullReferenceException>(() => cmd.Execute());
        owner.Verify(x => x.GetRights(), Times.Once);
    }
}