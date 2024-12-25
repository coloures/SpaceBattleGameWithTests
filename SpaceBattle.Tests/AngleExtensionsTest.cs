namespace SpaceBattle.Tests;
using SpaceBattle.Lib;

public class AngleExtensionsTests
{
    [Fact]
    public void ToDouble_ValidAngle_ReturnsCorrectDoubleValue()
    {
        // Arrange
        var angle = new Angle(90);

        // Act
        var result = angle.ToDouble();

        // Assert
        Assert.Equal(90.0, result);
    }

    [Fact]
    public void ToDouble_NullAngle_ThrowsArgumentNullException()
    {
        // Arrange
        Angle angle = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => angle.ToDouble());
    }
}
