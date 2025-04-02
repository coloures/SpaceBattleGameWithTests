using Moq;
using SpaceBattle.Lib;
namespace SpaceBattle.Tests;

public class CheckForAbilitiesCommandTest
{
    [Fact]
    public void TestPositive()
    {
        var abilities = new List<string>() { "moving", "shooting" };
        var Ship = new Mock<IObject>();
        Ship.Setup(x => x.GetAbilities()).Returns(abilities).Verifiable();
        var order = new Dictionary<string, object>();
        order["Command"] = "moving";
        var cmd = new CheckForAbilitiesCommand(Ship.Object, order);
        cmd.Execute();
        Ship.Verify(x => x.GetAbilities(), Times.Once);
    }
    [Fact]
    public void TestNegative()
    {
        var abilities = new List<string>() { "moving", "shooting" };
        var Ship = new Mock<IObject>();
        Ship.Setup(x => x.GetAbilities()).Returns(abilities).Verifiable();
        var order = new Dictionary<string, object>();
        order["Command"] = "rotating";
        var cmd = new CheckForAbilitiesCommand(Ship.Object, order);
        Assert.ThrowsAny<Exception>(() => cmd.Execute());
        Ship.Verify(x => x.GetAbilities(), Times.Once);
    }
    [Fact]
    public void TestNoElemInDict()
    {
        var abilities = new List<string>() { "moving", "shooting" };
        var Ship = new Mock<IObject>();
        Ship.Setup(x => x.GetAbilities()).Returns(abilities).Verifiable();
        var order = new Dictionary<string, object>();
        var cmd = new CheckForAbilitiesCommand(Ship.Object, order);
        Assert.ThrowsAny<Exception>(() => cmd.Execute());
        Ship.Verify(x => x.GetAbilities(), Times.Once);
    }
    [Fact]
    public void TestExceptionInGettingAbilities()
    {
        var Ship = new Mock<IObject>();
        Ship.Setup(x => x.GetAbilities()).Throws<NullReferenceException>().Verifiable();
        var order = new Dictionary<string, object>();
        order["Command"] = "rotating";
        var cmd = new CheckForAbilitiesCommand(Ship.Object, order);
        Assert.Throws<NullReferenceException>(() => cmd.Execute());
        Ship.Verify(x => x.GetAbilities(), Times.Once);
    }
}
