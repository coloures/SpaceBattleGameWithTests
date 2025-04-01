namespace SpaceBattle.Tests;

public class IdGeneratorRealisationTest
{
    [Fact]
    public void TestPositive()
    {
        var Generator = new SpaceBattle.Lib.IdGeneratorRealisation();
        Assert.Equal(typeof(string), Generator.Generate().GetType());
    }
}
