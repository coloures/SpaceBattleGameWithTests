using Moq;
using SpaceBattle.Lib;
namespace SpaceBattle.Tests;

public class UpdateGridCommandTest
{
    [Fact]
    public void TestPositive()
    {
        var obj = new Mock<IGridObject>();
        obj.SetupSet(x => x.GridLocation = It.IsAny<Vector>()).Verifiable();
        obj.SetupGet(x => x.AbsoluteLocation).Verifiable();
        var makerGrid = new Mock<IGetterGrid>();
        makerGrid.Setup(x => x.GetGridLocation(It.IsAny<Vector>())).Verifiable();
        var cmd = new UpdateGridCommand(obj.Object, makerGrid.Object);

        cmd.Execute();

        obj.VerifySet(x => x.GridLocation = It.IsAny<Vector>(), Times.Once);
        obj.VerifyGet(x => x.AbsoluteLocation, Times.Once);
        makerGrid.Verify(x => x.GetGridLocation(It.IsAny<Vector>()), Times.Once);
    }
    [Fact]
    public void TestSetterNegative()
    {
        var obj = new Mock<IGridObject>();
        obj.SetupSet(x => x.GridLocation = It.IsAny<Vector>()).Throws<Exception>().Verifiable();
        obj.SetupGet(x => x.AbsoluteLocation).Verifiable();
        var makerGrid = new Mock<IGetterGrid>();
        makerGrid.Setup(x => x.GetGridLocation(It.IsAny<Vector>())).Verifiable();
        var cmd = new UpdateGridCommand(obj.Object, makerGrid.Object);

        Assert.ThrowsAny<Exception>(() => cmd.Execute());

        obj.VerifySet(x => x.GridLocation = It.IsAny<Vector>(), Times.Once);
        obj.VerifyGet(x => x.AbsoluteLocation, Times.Once);
        makerGrid.Verify(x => x.GetGridLocation(It.IsAny<Vector>()), Times.Once);
    }
    [Fact]
    public void TestGetterNegative()
    {
        var obj = new Mock<IGridObject>();
        obj.SetupSet(x => x.GridLocation = It.IsAny<Vector>()).Verifiable();
        obj.SetupGet(x => x.AbsoluteLocation).Throws<Exception>().Verifiable();
        var makerGrid = new Mock<IGetterGrid>();
        makerGrid.Setup(x => x.GetGridLocation(It.IsAny<Vector>())).Verifiable();
        var cmd = new UpdateGridCommand(obj.Object, makerGrid.Object);

        Assert.ThrowsAny<Exception>(() => cmd.Execute());

        obj.VerifySet(x => x.GridLocation = It.IsAny<Vector>(), Times.Never);
        obj.VerifyGet(x => x.AbsoluteLocation, Times.Once);
        makerGrid.Verify(x => x.GetGridLocation(It.IsAny<Vector>()), Times.Never);
    }
    [Fact]
    public void TestMakerGridNegative()
    {
        var obj = new Mock<IGridObject>();
        obj.SetupSet(x => x.GridLocation = It.IsAny<Vector>()).Verifiable();
        obj.SetupGet(x => x.AbsoluteLocation).Verifiable();
        var makerGrid = new Mock<IGetterGrid>();
        makerGrid.Setup(x => x.GetGridLocation(It.IsAny<Vector>())).Throws<Exception>()
        .Verifiable();
        var cmd = new UpdateGridCommand(obj.Object, makerGrid.Object);

        Assert.ThrowsAny<Exception>(() => cmd.Execute());

        obj.VerifySet(x => x.GridLocation = It.IsAny<Vector>(), Times.Never);
        obj.VerifyGet(x => x.AbsoluteLocation, Times.Once);
        makerGrid.Verify(x => x.GetGridLocation(It.IsAny<Vector>()), Times.Once);
    }
}
