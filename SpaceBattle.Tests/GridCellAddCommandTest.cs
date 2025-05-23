﻿using SpaceBattle.Lib;
namespace SpaceBattle.Tests;

public class GridCellAddCommandTest
{
    [Fact]
    public void TestPositive()
    {
        var ship = new Dictionary<string, object>();
        var grid = new List<IDictionary<string, object>>[2];
        grid[0] = new List<IDictionary<string, object>>();

        var cmd = new GridCellAddCommand(grid, 0, 0, ship, 2, 1);

        cmd.Execute();

        Assert.Single(grid[0]);
        Assert.Equal(ship, grid[0][0]);
    }
    [Fact]
    public void TestPositivAlreadyInList()
    {
        var ship = new Dictionary<string, object>();
        var grid = new List<IDictionary<string, object>>[2];
        grid[0] = new List<IDictionary<string, object>>();
        grid[0].Add(ship);

        var cmd = new GridCellAddCommand(grid, 0, 0, ship, 2, 1);

        cmd.Execute();

        Assert.Single(grid[0]);
    }
    [Fact]
    public void TestNegativeFirstIf()
    {
        var ship = new Dictionary<string, object>();
        var grid = new List<IDictionary<string, object>>[2];
        grid[0] = new List<IDictionary<string, object>>();

        var cmd = new GridCellAddCommand(grid, 2, 1, ship, 2, 1);

        cmd.Execute();

        Assert.Empty(grid[0]);
    }
}
