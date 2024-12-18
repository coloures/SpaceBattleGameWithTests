namespace SpaceBattle.Tests;
using SpaceBattle.Lib;

public class AngleTest
{
    [Fact]
    public void TestSameAnglesAreEqual()
    {
        var angl1 = new Angle(12);
        var angl2 = new Angle(12);
        Assert.NotSame(angl1, angl2);
        Assert.Equal(angl1, angl2);
    }

    [Fact]
    public void TestAdditionThrowsException()
    {
        Angle angl1 = null; 
        
        var angl2 = new Angle(12);
        Assert.Throws<ArgumentNullException>(() => angl1 + angl2);
    }

    [Fact]
    public void TestComparingAngleWithNotAngleType()
    {
        var angl1 = new Angle(12);
        var Equality = angl1.Equals(12);
        Assert.False(Equality);
    }

    [Fact]
    public void TestEqualsObjectNull()
    {
        var angl1 = new Angle(12);
        var Equality = angl1.Equals(null);
        Assert.False(Equality);
    }

    [Fact]
    public void TestSameAnglesAreNotEqual()
    {
        var angl1 = new Angle(10);
        var angl2 = new Angle(12);
        Assert.NotEqual(angl1, angl2);
    }

    [Fact]
    public void TestSummationPositive()
    {
        var angl1 = new Angle(10);
        var angl2 = new Angle(12);
        var angl3 = angl1 + angl2;
        Assert.Equal(angl3, new Angle(22));
    }

    [Fact]
    public void TestSummationNegative()
    {
        var angl1 = new Angle(10);
        var angl2 = new Angle(-30);
        var angl3 = angl1 + angl2;
        Assert.Equal(angl3, new Angle(-20));
    }
    [Fact]
    public void TestGetHashCode()
    {
        var angle1 = new Angle(54);
        var angle2 = new Angle(54);
        Assert.True(angle1.GetHashCode() == angle2.GetHashCode());
    }

    [Fact]
    public void TestNewImplication()
    {
        var angle1 = new Angle(12);
        angle1.A = 13;

        Assert.Equal(13, angle1.A);
    }
}
