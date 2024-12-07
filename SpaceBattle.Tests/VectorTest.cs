namespace SpaceBattle.Tests;
using SpaceBattle.Lib;

public class VectorTest
{
    [Fact]
    public void TestSameVectorsAreEqual()
    {
        var vector1 = new Vector(12, 5);
        var vector2 = new Vector(12, 5);
        Assert.NotSame(vector1, vector2);
        Assert.Equal(vector1, vector2);

    }
    [Fact]
    public void TestComparingVectorWithNotVectorType()
    {
        var vector1 = new Vector(12, 5);
        var Equality = vector1.Equals(new int[] { 12, 5 });
        Assert.False(Equality);
    }
    [Fact]
    public void TestSameVectorsAreWithDifferentDimentions()
    {
        var vector1 = new Vector(12, 5, 10);
        var vector2 = new Vector(12, 5);
        Assert.NotEqual(vector1, vector2);

    }
    [Fact]
    public void TestSameVectorsAreNotEqual()
    {
        var vector1 = new Vector(10, 5);
        var vector2 = new Vector(12, 5);
        Assert.NotEqual(vector1, vector2);
    }
    [Fact]
    public void TestSummationPositive()
    {
        var vector1 = new Vector(10, 5);
        var vector2 = new Vector(12, 5);
        var vector3 = vector1 + vector2;
        Assert.Equal(vector3, new Vector(22, 10));

    }
    [Fact]
    public void TestSummationNegative()
    {
        var vector1 = new Vector(10, 5);
        var vector2 = new Vector(-500, 5);
        var vector3 = vector1 + vector2;
        Assert.Equal(vector3, new Vector(-490, 10));

    }
    [Fact]
    public void TestSummationWithDifferentDimentions()
    {
        var vector1 = new Vector(10, 5);
        var vector2 = new Vector(-500, 5, 0);
        Assert.Throws<Exception>(() =>
        {
            var vector3 = vector1 + vector2;
        });
    }
    [Fact]
    public void TestMultiplicationVector()
    {
        var vector1 = new Vector(10, 5);
        var vector2 = vector1 * 100;
        Assert.Equal(new Vector(1000, 500), vector2);
    }
    [Fact]
    public void TestGetHashCode()
    {
        var vector1 = new Vector(10, 5);
        Assert.Equal(vector1.GetHashCode(), (1 * 31 + 10.GetHashCode()) * 31 + 5.GetHashCode());
    }
}