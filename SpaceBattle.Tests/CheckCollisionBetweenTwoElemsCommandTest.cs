using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests
{
    public class CheckCollisionBetweenTwoElemsCommandTests
    {
        [Fact]
        public void Execute_CollisionDetected()
        {
            var FirstShip = new Mock<ICollisionObject>();
            var SecondShip = new Mock<ICollisionObject>();
            var Checker = new Mock<ICheckerNeighbourhood>();
            var Tree = new Mock<ITreeChecker>();
            Tree.Setup(x => x.Check(It.IsAny<int>(),It.IsAny<int>(),It.IsAny<int>(),It.IsAny<int>())).Verifiable();

            FirstShip.Setup(s => s.AbsoluteLocation).Returns(new int[] { 0, 0 });
            SecondShip.Setup(s => s.AbsoluteLocation).Returns(new int[] { 1, 1 });
            FirstShip.Setup(s => s.Velocity).Returns(new int[] { 0, 0 });
            SecondShip.Setup(s => s.Velocity).Returns(new int[] { 1, 1 });

            Checker.Setup(c => c.Check(It.IsAny<ICollisionObject>(), It.IsAny<ICollisionObject>()))
                      .Returns(true);

            var command = new CheckCollisionBetweenTwoElemsCommand(
                FirstShip.Object,
                SecondShip.Object,
                Checker.Object,
                Tree.Object);
            command.Execute();
            Tree.Verify(t => t.Check(1, 1, 1, 1), Times.Once);
        }

        [Fact]
        public void Execute_NoCollisionDetected()
        {
            var FirstShip = new Mock<ICollisionObject>();
            var SecondShip = new Mock<ICollisionObject>();
            var Checker = new Mock<ICheckerNeighbourhood>();
            var Tree = new Mock<ITreeChecker>();
            Tree.Setup(x => x.Check(It.IsAny<int>(),It.IsAny<int>(),It.IsAny<int>(),It.IsAny<int>())).Verifiable();

            Checker.Setup(c => c.Check(It.IsAny<ICollisionObject>(), It.IsAny<ICollisionObject>()))
                      .Returns(false);

            var command = new CheckCollisionBetweenTwoElemsCommand(
                FirstShip.Object,
                SecondShip.Object,
                Checker.Object,
                Tree.Object);
            command.Execute();
            Tree.Verify(t => t.Check(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }
        [Fact]
        public void Execute_GetterException()
        {
            var FirstShip = new Mock<ICollisionObject>();
            var SecondShip = new Mock<ICollisionObject>();
            var Checker = new Mock<ICheckerNeighbourhood>();
            var Tree = new Mock<ITreeChecker>();
                        Tree.Setup(x => x.Check(It.IsAny<int>(),It.IsAny<int>(),It.IsAny<int>(),It.IsAny<int>())).Verifiable();

            FirstShip.Setup(s => s.AbsoluteLocation).Throws<Exception>();
            SecondShip.Setup(s => s.AbsoluteLocation).Returns(new int[] { 1, 1 });
            FirstShip.Setup(s => s.Velocity).Returns(new int[] { 0, 0 });
            SecondShip.Setup(s => s.Velocity).Returns(new int[] { 1, 1 });

            Checker.Setup(c => c.Check(It.IsAny<ICollisionObject>(), It.IsAny<ICollisionObject>()))
                      .Returns(true);

            var command = new CheckCollisionBetweenTwoElemsCommand(
                FirstShip.Object,
                SecondShip.Object,
                Checker.Object,
                Tree.Object);
            Assert.ThrowsAny<Exception>(() =>command.Execute());
            Tree.Verify(t => t.Check(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }
        [Fact]
        public void Execute_CheckerException()
        {
            var FirstShip = new Mock<ICollisionObject>();
            var SecondShip = new Mock<ICollisionObject>();
            var Checker = new Mock<ICheckerNeighbourhood>();
            var Tree = new Mock<ITreeChecker>();
                        Tree.Setup(x => x.Check(It.IsAny<int>(),It.IsAny<int>(),It.IsAny<int>(),It.IsAny<int>())).Verifiable();

            Checker.Setup(c => c.Check(It.IsAny<ICollisionObject>(), It.IsAny<ICollisionObject>()))
                      .Throws<Exception>();

            var command = new CheckCollisionBetweenTwoElemsCommand(
                FirstShip.Object,
                SecondShip.Object,
                Checker.Object,
                Tree.Object);
            Assert.ThrowsAny<Exception>(() =>command.Execute());
        }
        [Fact]
        public void Execute_TreeException()
        {
            var FirstShip = new Mock<ICollisionObject>();
            var SecondShip = new Mock<ICollisionObject>();
            var Checker = new Mock<ICheckerNeighbourhood>();
            var Tree = new Mock<ITreeChecker>();
            Tree.Setup(x => x.Check(It.IsAny<int>(),It.IsAny<int>(),It.IsAny<int>(),It.IsAny<int>())).Throws<Exception>().Verifiable();

            FirstShip.Setup(s => s.AbsoluteLocation).Returns(new int[] { 0, 0 });
            SecondShip.Setup(s => s.AbsoluteLocation).Returns(new int[] { 1, 1 });
            FirstShip.Setup(s => s.Velocity).Returns(new int[] { 0, 0 });
            SecondShip.Setup(s => s.Velocity).Returns(new int[] { 1, 1 });

            Checker.Setup(c => c.Check(It.IsAny<ICollisionObject>(), It.IsAny<ICollisionObject>()))
                      .Returns(true);

            var command = new CheckCollisionBetweenTwoElemsCommand(
                FirstShip.Object,
                SecondShip.Object,
                Checker.Object,
                Tree.Object);
            Assert.ThrowsAny<Exception>(() =>command.Execute());
            Tree.Verify(t => t.Check(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }
    }
}
