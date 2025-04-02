using SpaceBattle.Lib;
namespace SpaceBattle.Tests;

public class CheckForPermissionsToUseShipCommandTest
{
    [Fact]
    public void TestPositive()
    {
        var PlayerShipsIds = new List<string>() { "A11", "B14", "C19" };
        var SpaceShipId = "A11";

        var cmd = new CheckForPermissionsToUseShipCommand(PlayerShipsIds, SpaceShipId);
        cmd.Execute();
    }
    [Fact]
    public void TestNegative()
    {
        var PlayerShipsIds = new List<string>() { "A11", "B14", "C19" };
        var SpaceShipId = "A12";

        var cmd = new CheckForPermissionsToUseShipCommand(PlayerShipsIds, SpaceShipId);
        Assert.ThrowsAny<Exception>(() => cmd.Execute());
    }
}
