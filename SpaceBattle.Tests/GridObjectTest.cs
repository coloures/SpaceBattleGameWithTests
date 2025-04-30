using Moq;
using SpaceBattle.Lib;
namespace SpaceBattle.Tests;
public class GridObjectTest
{
    [Fact]
    public void TestPositive()
    {
        var obj = new Mock<Vector>();
        var ship = new Dictionary<string, object>();
        ship.Add("Location", obj.Object);

        var gridobject = new GridObject(ship);
        gridobject.GridLocation = new Vector(2,5);

        Assert.Equal(gridobject.AbsoluteLocation, obj.Object);
        Assert.Equal(new Vector(2,5), gridobject.GridLocationVector);
    }
    [Fact]
    public void TestNegative()
    {
        var obj = new Mock<Vector>();
        var ship = new Dictionary<string, object>();

        var gridobject = new GridObject(ship);
        gridobject.GridLocation = new Vector(2,5);

        Assert.ThrowsAny<Exception>(() => gridobject.AbsoluteLocation);
        Assert.Equal(new Vector(2,5), gridobject.GridLocationVector);
    }
}
