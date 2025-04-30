namespace SpaceBattle.Tests;
using SpaceBattle.Lib;
using Moq;

public class AddToGridCommandTest
{
    [Fact]
    public void TestPositive()
    {
        var Object = new Mock<IDictionary<string, object>>();
        var adder = new Mock<IGridAdderElem>();
        adder.Setup(x => x.Add(It.IsAny<int>(),It.IsAny<int>(), 
        It.IsAny<IDictionary<string, object>>(), It.IsAny<string>())).Verifiable();
        var gridinfo = new Mock<IGridInfoObject>();
        gridinfo.SetupGet(x => x.GridLocation).Returns([5, 10]);
        gridinfo.SetupGet(x => x.ObjectItself).Returns(Object.Object);

        var cmd = new AddToGridCommand("name", gridinfo.Object, adder.Object);

        cmd.Execute();

        adder.Verify(x => x.Add(4, 9, Object.Object, "name"), Times.Once);
        adder.Verify(x => x.Add(6, 9, Object.Object, "name"), Times.Once);
        adder.Verify(x => x.Add(4, 11, Object.Object, "name"), Times.Once);
        adder.Verify(x => x.Add(6, 11, Object.Object, "name"), Times.Once);
        adder.Verify(x => x.Add(It.IsAny<int>(),It.IsAny<int>(), 
        It.IsAny<IDictionary<string, object>>(), It.IsAny<string>()), Times.Exactly(9));
    }
    [Fact]
    public void TestNegativeAdder()
    {
        var Object = new Mock<IDictionary<string, object>>();
        var adder = new Mock<IGridAdderElem>();
        adder.Setup(x => x.Add(It.IsAny<int>(),It.IsAny<int>(), 
        It.IsAny<IDictionary<string, object>>(), It.IsAny<string>())).Throws<Exception>().Verifiable();
        var gridinfo = new Mock<IGridInfoObject>();
        gridinfo.SetupGet(x => x.GridLocation).Returns([5, 10]);
        gridinfo.SetupGet(x => x.ObjectItself).Returns(Object.Object);

        var cmd = new AddToGridCommand("name", gridinfo.Object, adder.Object);

        Assert.ThrowsAny<Exception>(() =>cmd.Execute());
        adder.Verify(x => x.Add(It.IsAny<int>(),It.IsAny<int>(), 
        It.IsAny<IDictionary<string, object>>(), It.IsAny<string>()), Times.Once);
    }
    [Fact]
    public void TestNegativeGetterLocation()
    {
        var Object = new Mock<IDictionary<string, object>>();
        var adder = new Mock<IGridAdderElem>();
        adder.Setup(x => x.Add(It.IsAny<int>(),It.IsAny<int>(), 
        It.IsAny<IDictionary<string, object>>(), It.IsAny<string>())).Verifiable();
        var gridinfo = new Mock<IGridInfoObject>();
        gridinfo.SetupGet(x => x.GridLocation).Throws<Exception>();
        gridinfo.SetupGet(x => x.ObjectItself).Returns(Object.Object);

        var cmd = new AddToGridCommand("name", gridinfo.Object, adder.Object);

        Assert.ThrowsAny<Exception>(() =>cmd.Execute());
        adder.Verify(x => x.Add(It.IsAny<int>(),It.IsAny<int>(), 
        It.IsAny<IDictionary<string, object>>(), It.IsAny<string>()), Times.Exactly(0));
    }
    [Fact]
    public void TestNegativeGetterObject()
    {
        var Object = new Mock<IDictionary<string, object>>();
        var adder = new Mock<IGridAdderElem>();
        adder.Setup(x => x.Add(It.IsAny<int>(),It.IsAny<int>(), 
        It.IsAny<IDictionary<string, object>>(), It.IsAny<string>())).Verifiable();
        var gridinfo = new Mock<IGridInfoObject>();
        gridinfo.SetupGet(x => x.GridLocation).Returns([5, 10]);
        gridinfo.SetupGet(x => x.ObjectItself).Throws<Exception>();

        var cmd = new AddToGridCommand("name", gridinfo.Object, adder.Object);

        Assert.ThrowsAny<Exception>(() =>cmd.Execute());
        adder.Verify(x => x.Add(It.IsAny<int>(),It.IsAny<int>(), 
        It.IsAny<IDictionary<string, object>>(), It.IsAny<string>()), Times.Exactly(0));
    }
    [Fact]
    public void TestNegativeGetterLocationDimention()
    {
        var Object = new Mock<IDictionary<string, object>>();
        var adder = new Mock<IGridAdderElem>();
        adder.Setup(x => x.Add(It.IsAny<int>(),It.IsAny<int>(), 
        It.IsAny<IDictionary<string, object>>(), It.IsAny<string>())).Verifiable();
        var gridinfo = new Mock<IGridInfoObject>();
        gridinfo.SetupGet(x => x.GridLocation).Returns([5]);
        gridinfo.SetupGet(x => x.ObjectItself).Returns(Object.Object);

        var cmd = new AddToGridCommand("name", gridinfo.Object, adder.Object);

        Assert.ThrowsAny<Exception>(() =>cmd.Execute());
        adder.Verify(x => x.Add(It.IsAny<int>(),It.IsAny<int>(), 
        It.IsAny<IDictionary<string, object>>(), It.IsAny<string>()), Times.Exactly(0));
    }
    [Fact]
    public void TestNegativeInMiddle()
    {
        var Object = new Mock<IDictionary<string, object>>();
        var adder = new Mock<IGridAdderElem>();
        adder.Setup(x => x.Add(It.IsAny<int>(),It.IsAny<int>(), 
        It.IsAny<IDictionary<string, object>>(), It.IsAny<string>())).Verifiable();
        adder.Setup(x => x.Add(5,10, It.IsAny<IDictionary<string, object>>(), 
        It.IsAny<string>())).Throws<Exception>();
        var gridinfo = new Mock<IGridInfoObject>();
        gridinfo.SetupGet(x => x.GridLocation).Returns([5, 10]);
        gridinfo.SetupGet(x => x.ObjectItself).Returns(Object.Object);

        var cmd = new AddToGridCommand("name", gridinfo.Object, adder.Object);

        Assert.ThrowsAny<Exception>(() =>cmd.Execute());

        adder.Verify(x => x.Add(4, 9, Object.Object, "name"), Times.Once);
        adder.Verify(x => x.Add(6, 9, Object.Object, "name"), Times.Never);
        adder.Verify(x => x.Add(4, 11, Object.Object, "name"), Times.Once);
        adder.Verify(x => x.Add(6, 11, Object.Object, "name"), Times.Never);
        adder.Verify(x => x.Add(It.IsAny<int>(),It.IsAny<int>(), 
        It.IsAny<IDictionary<string, object>>(), It.IsAny<string>()), Times.Exactly(5));
    }
}