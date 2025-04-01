namespace SpaceBattle.Tests;
using Moq;
using SpaceBattle.Lib;

public class CreatingCommandTest
{
    [Fact]
    public void TestPositive()
    {
        var adder = new Mock<IAdderToNamesAndShipsDictionary>();
        adder.Setup(x => x.Add(It.IsAny<IDictionary<string, object>>(), It.IsAny<string>())).Verifiable();
        var generator = new Mock<IdGenerator>();
        generator.Setup(x => x.Generate()).Returns("obj").Verifiable();
        var CreatingCommand = new CreatingCommand(adder.Object, generator.Object);
        CreatingCommand.Execute();
        adder.Verify(x => x.Add(It.IsAny<IDictionary<string, object>>(), "obj"), Times.Once);
        generator.Verify(x => x.Generate(), Times.Once);
    }
    [Fact]
    public void TestNegativeGenerator()
    {
        var adder = new Mock<IAdderToNamesAndShipsDictionary>();
        adder.Setup(x => x.Add(It.IsAny<IDictionary<string, object>>(), It.IsAny<string>())).Verifiable();
        var generator = new Mock<IdGenerator>();
        generator.Setup(x => x.Generate()).Throws<Exception>().Verifiable();
        var CreatingCommand = new CreatingCommand(adder.Object, generator.Object);
        Assert.ThrowsAny<Exception>(() => CreatingCommand.Execute());
        generator.Verify(x => x.Generate(), Times.Once);
        adder.Verify(x => x.Add(It.IsAny<IDictionary<string, object>>(), "obj"), Times.Never);
    }
    [Fact]
    public void TestNegativeAdder()
    {
        var adder = new Mock<IAdderToNamesAndShipsDictionary>();
        adder.Setup(x => x.Add(It.IsAny<IDictionary<string, object>>(), It.IsAny<string>())).Throws<Exception>().Verifiable();
        var generator = new Mock<IdGenerator>();
        generator.Setup(x => x.Generate()).Returns("obj").Verifiable();
        var CreatingCommand = new CreatingCommand(adder.Object, generator.Object);
        Assert.ThrowsAny<Exception>(() => CreatingCommand.Execute());
        generator.Verify(x => x.Generate(), Times.Once);
        adder.Verify(x => x.Add(It.IsAny<IDictionary<string, object>>(), "obj"), Times.Once);
    }
}
