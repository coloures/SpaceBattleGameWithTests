namespace SpaceBattle.Tests;

public class VectorTest
{
    [Fact]
    public void TestSummationPositive()
    {
        var vector1 = new Vector(1, -1, 2);
        var vector2 = new Vector(-1, 1, -2);
        var vector3 = vector1 + vector2;
        Assert.Equal(vector3, new Vector(0, 0, 0));
    }

    [Fact]
    public void TestArgumentExceptionOne()
    {
        var vector1 = new Vector(1, 2, 3);
        var vector2 = new Vector(1, 2);
        var exception = Assert.Throws<ArgumentException>(() => vector1 + vector2);
    }

    [Fact]
    public void TestArgumentExceptionTwo()
    {
        var vector2 = new Vector(1, 2, 3);
        var vector1 = new Vector(1, 2);
        var exception = Assert.Throws<ArgumentException>(() => vector1 + vector2);
    }

    [Fact]
    public void TestSameVectorsAreEqualMethod()
    {
        var vector1 = new Vector(12, 5);
        var vector2 = new Vector(12, 5);
        Assert.NotSame(vector1, vector2);
        Assert.Equal(vector1, vector2);
    }

    [Fact]
    public void TestSameVectorsAreEqualUsingOperator()
    {
        var vector1 = new Vector(12, 5);
        var vector2 = new Vector(12, 5);
        Assert.NotSame(vector1, vector2);
        Assert.True(vector1 == vector2);
    }

    [Fact]
    public void TestSameVectorsAreNotEqualMethod()
    {
        var vector1 = new Vector(12, 5);
        var vector2 = new Vector(12, 6);
        Assert.NotSame(vector1, vector2);
        Assert.NotEqual(vector1, vector2);
    }

    [Fact]
    public void TestSameVectorsAreNotEqualUsingOperator()
    {
        var vector1 = new Vector(12, 5);
        var vector2 = new Vector(12, 6);
        Assert.NotSame(vector1, vector2);
        Assert.True(vector1 != vector2);
    }

    [Fact]
    public void TestGetHashCode()
    {
        var vector1 = new Vector(10, 5);
        Assert.Equal(vector1.GetHashCode(), (1 * 31 + 10.GetHashCode()) * 31 + 5.GetHashCode());
    }

    [Fact]
    public void TestOperationEqualityNullTwo()
    {
        var vector2 = new Vector(10, 5);
        Vector? vector1 = null;
        Assert.True(vector1! != vector2);
    }

    [Fact]
    public void TestEqualityMethodWithNotVector()
    {
        var vector1 = new Vector(10, 5);
        var vector2 = new int[10, 5];
        var notequal = vector1.Equals(vector2);
        Assert.True(!notequal);
    }

    [Fact]
    public void TestEqualityMethodWithOtherDimensity()
    {
        var vector1 = new Vector(10, 5);
        var vector2 = new Vector(10, 5, 5);
        var notequal = vector1.Equals(vector2);
        Assert.True(!notequal);
    }
}
