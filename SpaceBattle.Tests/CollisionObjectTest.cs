using Moq;
using SpaceBattle.Lib;

public class CollisionObjectTests
{
    [Fact]
    public void Check_Positive()
    {
        var expectedCoordinates = new[] { 5, 6 };
        var obj = new Dictionary<string, object>
        {
            ["Location"] = new Vector(10, 20),
            ["Velocity"] = new Vector(1, 2)
        };
        var Grid = new Mock<IGetterGrid>();
        Grid.Setup(g => g.GetGridLocation(It.IsAny<Vector>()))
                .Returns(new Vector(expectedCoordinates));

        var collisionObject = new CollisionObject(obj, Grid.Object);

        var result = collisionObject.ObjectItself;
        var gridLocation = collisionObject.GridLocation;
        var absoluteLocation = collisionObject.AbsoluteLocation;
        var velocityCoords = collisionObject.Velocity;

        Assert.Equal(obj, result);
        Assert.Equal(expectedCoordinates, gridLocation);
        Assert.Equal(new Vector(10, 20).GetCoordinates(), absoluteLocation);
        Assert.Equal(new Vector(1, 2).GetCoordinates(), velocityCoords);

        Grid.Verify(g => g.GetGridLocation(It.IsAny<Vector>()), Times.Once);
    }
    [Fact]
    public void Check_ThrowsException()
    {
        var obj = new Mock<IDictionary<string, object>>();
        obj.SetupGet(x => x["Location"]).Throws<Exception>().Verifiable();
        obj.SetupGet(x => x["Velocity"]).Throws<Exception>().Verifiable();
        var Grid = new Mock<IGetterGrid>();
        Grid.Setup(g => g.GetGridLocation(It.IsAny<Vector>()))
                .Throws<Exception>().Verifiable();

        var collisionObject = new CollisionObject(obj.Object, Grid.Object);

        Assert.Same(obj.Object, collisionObject.ObjectItself);
        Assert.ThrowsAny<Exception>(() => collisionObject.AbsoluteLocation);
        Assert.ThrowsAny<Exception>(() => collisionObject.Velocity);
        Assert.ThrowsAny<Exception>(() => collisionObject.GridLocation);
        Grid.Verify(g => g.GetGridLocation(It.IsAny<Vector>()), Times.Never);
        obj.VerifyGet(x => x["Location"], Times.Exactly(2));
        obj.VerifyGet(x => x["Velocity"], Times.Once);
    }
}
