namespace SpaceBattle.Tests;
using SpaceBattle.Lib;
using Moq;

public class DeleteFromGridCommandTest
{
    [Fact]
    public void TestPositive()
    {
        var Object = new Mock<IDictionary<string, object>>();
        var deleter = new Mock<IGridDeleterElem>();
        deleter.Setup(x => x.Delete(It.IsAny<int>(),It.IsAny<int>(), 
        It.IsAny<IDictionary<string, object>>(), It.IsAny<string>())).Verifiable();
        var gridinfo = new Mock<IGridInfoObject>();
        gridinfo.SetupGet(x => x.GridLocation).Returns([5, 10]);
        gridinfo.SetupGet(x => x.ObjectItself).Returns(Object.Object);

        var cmd = new DeleteFromGridCommand("name", gridinfo.Object, deleter.Object);

        cmd.Execute();

        deleter.Verify(x => x.Delete(4, 9, Object.Object, "name"), Times.Once);
        deleter.Verify(x => x.Delete(6, 9, Object.Object, "name"), Times.Once);
        deleter.Verify(x => x.Delete(4, 11, Object.Object, "name"), Times.Once);
        deleter.Verify(x => x.Delete(6, 11, Object.Object, "name"), Times.Once);
        deleter.Verify(x => x.Delete(It.IsAny<int>(),It.IsAny<int>(), 
        It.IsAny<IDictionary<string, object>>(), It.IsAny<string>()), Times.Exactly(9));
    }
    [Fact]
    public void TestNegativeDeleter()
    {
        var Object = new Mock<IDictionary<string, object>>();
        var deleter = new Mock<IGridDeleterElem>();
        deleter.Setup(x => x.Delete(It.IsAny<int>(),It.IsAny<int>(), 
        It.IsAny<IDictionary<string, object>>(), It.IsAny<string>())).Throws<Exception>().Verifiable();
        var gridinfo = new Mock<IGridInfoObject>();
        gridinfo.SetupGet(x => x.GridLocation).Returns([5, 10]);
        gridinfo.SetupGet(x => x.ObjectItself).Returns(Object.Object);

        var cmd = new DeleteFromGridCommand("name", gridinfo.Object, deleter.Object);

        Assert.ThrowsAny<Exception>(() =>cmd.Execute());
        deleter.Verify(x => x.Delete(It.IsAny<int>(),It.IsAny<int>(), 
        It.IsAny<IDictionary<string, object>>(), It.IsAny<string>()), Times.Once);
    }
    [Fact]
    public void TestNegativeGetterLocation()
    {
        var Object = new Mock<IDictionary<string, object>>();
        var deleter = new Mock<IGridDeleterElem>();
        deleter.Setup(x => x.Delete(It.IsAny<int>(),It.IsAny<int>(), 
        It.IsAny<IDictionary<string, object>>(), It.IsAny<string>())).Verifiable();
        var gridinfo = new Mock<IGridInfoObject>();
        gridinfo.SetupGet(x => x.GridLocation).Throws<Exception>();
        gridinfo.SetupGet(x => x.ObjectItself).Returns(Object.Object);

        var cmd = new DeleteFromGridCommand("name", gridinfo.Object, deleter.Object);

        Assert.ThrowsAny<Exception>(() =>cmd.Execute());
        deleter.Verify(x => x.Delete(It.IsAny<int>(),It.IsAny<int>(), 
        It.IsAny<IDictionary<string, object>>(), It.IsAny<string>()), Times.Exactly(0));
    }
    [Fact]
    public void TestNegativeGetterObject()
    {
        var Object = new Mock<IDictionary<string, object>>();
        var deleter = new Mock<IGridDeleterElem>();
        deleter.Setup(x => x.Delete(It.IsAny<int>(),It.IsAny<int>(), 
        It.IsAny<IDictionary<string, object>>(), It.IsAny<string>())).Verifiable();
        var gridinfo = new Mock<IGridInfoObject>();
        gridinfo.SetupGet(x => x.GridLocation).Returns([5, 10]);
        gridinfo.SetupGet(x => x.ObjectItself).Throws<Exception>();

        var cmd = new DeleteFromGridCommand("name", gridinfo.Object, deleter.Object);

        Assert.ThrowsAny<Exception>(() =>cmd.Execute());
        deleter.Verify(x => x.Delete(It.IsAny<int>(),It.IsAny<int>(), 
        It.IsAny<IDictionary<string, object>>(), It.IsAny<string>()), Times.Exactly(0));
    }
    [Fact]
    public void TestNegativeGetterLocationDimention()
    {
        var Object = new Mock<IDictionary<string, object>>();
        var deleter = new Mock<IGridDeleterElem>();
        deleter.Setup(x => x.Delete(It.IsAny<int>(),It.IsAny<int>(), 
        It.IsAny<IDictionary<string, object>>(), It.IsAny<string>())).Verifiable();
        var gridinfo = new Mock<IGridInfoObject>();
        gridinfo.SetupGet(x => x.GridLocation).Returns([5]);
        gridinfo.SetupGet(x => x.ObjectItself).Returns(Object.Object);

        var cmd = new DeleteFromGridCommand("name", gridinfo.Object, deleter.Object);

        Assert.ThrowsAny<Exception>(() =>cmd.Execute());
        deleter.Verify(x => x.Delete(It.IsAny<int>(),It.IsAny<int>(), 
        It.IsAny<IDictionary<string, object>>(), It.IsAny<string>()), Times.Exactly(0));
    }
    [Fact]
    public void TestNegativeInMiddle()
    {
        var Object = new Mock<IDictionary<string, object>>();
        var deleter = new Mock<IGridDeleterElem>();
        deleter.Setup(x => x.Delete(It.IsAny<int>(),It.IsAny<int>(), 
        It.IsAny<IDictionary<string, object>>(), It.IsAny<string>())).Verifiable();
        deleter.Setup(x => x.Delete(5,10, It.IsAny<IDictionary<string, object>>(), 
        It.IsAny<string>())).Throws<Exception>();
        var gridinfo = new Mock<IGridInfoObject>();
        gridinfo.SetupGet(x => x.GridLocation).Returns([5, 10]);
        gridinfo.SetupGet(x => x.ObjectItself).Returns(Object.Object);

        var cmd = new DeleteFromGridCommand("name", gridinfo.Object, deleter.Object);

        Assert.ThrowsAny<Exception>(() =>cmd.Execute());

        deleter.Verify(x => x.Delete(4, 9, Object.Object, "name"), Times.Once);
        deleter.Verify(x => x.Delete(6, 9, Object.Object, "name"), Times.Never);
        deleter.Verify(x => x.Delete(4, 11, Object.Object, "name"), Times.Once);
        deleter.Verify(x => x.Delete(6, 11, Object.Object, "name"), Times.Never);
        deleter.Verify(x => x.Delete(It.IsAny<int>(),It.IsAny<int>(), 
        It.IsAny<IDictionary<string, object>>(), It.IsAny<string>()), Times.Exactly(5));
    }
}