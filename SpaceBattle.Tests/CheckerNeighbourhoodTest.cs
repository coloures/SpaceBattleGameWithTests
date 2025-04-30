using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests
{
    public class CheckerNeighbourhoodTests
    {
        [Fact]
        public void Check_ReturnsCorrectResult1()
        {
            var checker = new CheckerNeighbourhood();
            var Obj1 = new Mock<ICollisionObject>();
            var Obj2 = new Mock<ICollisionObject>();
            Obj1.SetupGet(o => o.GridLocation).Returns(new int[] { 0, 1 });
            Obj2.SetupGet(o => o.GridLocation).Returns(new int[] { 1, 0 });
            var result = checker.Check(Obj1.Object, Obj2.Object);
            Assert.True(result);
        }
        [Fact]
        public void Check_ReturnsCorrectResult2()
        {
            var checker = new CheckerNeighbourhood();
            var Obj1 = new Mock<ICollisionObject>();
            var Obj2 = new Mock<ICollisionObject>();
            Obj1.SetupGet(o => o.GridLocation).Returns(new int[] { 0, 2 });
            Obj2.SetupGet(o => o.GridLocation).Returns(new int[] { 2, 0 });
            var result = checker.Check(Obj1.Object, Obj2.Object);
            Assert.False(result);
        }
        [Fact]
        public void Check_DifferentDimentions()
        {
            var checker = new CheckerNeighbourhood();
            var Obj1 = new Mock<ICollisionObject>();
            var Obj2 = new Mock<ICollisionObject>();
            Obj1.SetupGet(o => o.GridLocation).Returns(new int[] { 0 });
            Obj2.SetupGet(o => o.GridLocation).Returns(new int[] { 0, 0 });
            Assert.ThrowsAny<Exception>(() => checker.Check(Obj1.Object, Obj2.Object));
        }
        [Fact]
        public void Check_NoGridLocation()
        {
            var checker = new CheckerNeighbourhood();
            var Obj1 = new Mock<ICollisionObject>();
            var Obj2 = new Mock<ICollisionObject>();
            Obj1.SetupGet(o => o.GridLocation).Returns(new int[] { 0, 2 });
            Assert.ThrowsAny<Exception>(() => checker.Check(Obj1.Object, Obj2.Object));
        }
    }
}
