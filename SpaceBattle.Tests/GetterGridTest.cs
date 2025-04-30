namespace SpaceBattle.Tests;
using SpaceBattle.Lib;
public class GetterGridTest
{
    [Fact]
    public void TestPositive()
    {
        var Grid = new Dictionary<string, object>();
        Grid.Add("size_x", 10);
        Grid.Add("size_y", 10);
        Grid.Add("shift_x", 5);
        Grid.Add("shift_y", 5);
        var gettergrid = new GetterGrid(Grid);

        var vect = new Vector(10, 15);
        Assert.Equal(new Vector(0,1), gettergrid.GetGridLocation(vect));
    }
    [Fact]
    public void TestNegativeDivisionByZer1()
    {
        var Grid = new Dictionary<string, object>();
        Grid.Add("size_x", 10);
        Grid.Add("size_y", 0);
        Grid.Add("shift_x", 5);
        Grid.Add("shift_y", 5);
        var gettergrid = new GetterGrid(Grid);

        var vect = new Vector(10, 10);
        Assert.ThrowsAny<Exception>(() => gettergrid.GetGridLocation(vect));
    }
    [Fact]
    public void TestNegativeDivisionByZero2()
    {
        var Grid = new Dictionary<string, object>();
        Grid.Add("size_x", 0);
        Grid.Add("size_y", 10);
        Grid.Add("shift_x", 5);
        Grid.Add("shift_y", 5);
        var gettergrid = new GetterGrid(Grid);

        var vect = new Vector(10, 10);
        Assert.ThrowsAny<Exception>(() => gettergrid.GetGridLocation(vect));
    }
    [Fact]
    public void TestNegativeDifferentDimentions()
    {
        var Grid = new Dictionary<string, object>();
        Grid.Add("size_x", 10);
        Grid.Add("size_y", 10);
        Grid.Add("shift_x", 5);
        Grid.Add("shift_y", 5);
        var gettergrid = new GetterGrid(Grid);

        var vect = new Vector(10);
        Assert.ThrowsAny<Exception>(() => gettergrid.GetGridLocation(vect));
    }
    [Fact]
    public void TestNegativeSomethingWrongWithDictionary()
    {
        var Grid = new Dictionary<string, object>();
        Grid.Add("size_x", 10);
        Grid.Add("size_y", 10);
        Grid.Add("shift_x", 5);
        Assert.ThrowsAny<Exception>(() => new GetterGrid(Grid));
    }
}

