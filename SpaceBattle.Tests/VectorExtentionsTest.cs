using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class VectorExtentionsTest
{
    [Fact]
    public void TestPositive()
    {
        int[] nums = { 2, 3 };
        var VectorObj = new Vector(2, 3);
        Assert.Equal(nums, VectorObj.GetCoordinates());
    }
    [Fact]
    public void TestPositiveEmpty()
    {
        var VectorObj = new Vector();
        Assert.Equal([], VectorObj.GetCoordinates());
    }
}
