namespace SpaceBattle.Tests;
using SpaceBattle.Lib;

public class EmptyCommandTest
{
    [Fact]
    public void TestPositive()
    {
        var EmptyCommand = new EmptyCommand();
        EmptyCommand.Execute();
    }
}
