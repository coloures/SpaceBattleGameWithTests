namespace SpaceBattle.Tests;
using System.Collections.Generic;
using SpaceBattle.Lib;
using Xunit;

public class TreeAddSetCommandTest
{
    [Fact]
    public void Constructor_InitializesFieldsCorrectly()
    {
        // Arrange
        var tree = new Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>>();
        int x = 1, y = 2, vel_x = 3, vel_y = 4;

        // Act
        var command = new TreeAddSetCommand(tree, x, y, vel_x, vel_y);

        // Assert
        Assert.Same(tree, command.Tree);
        Assert.Equal(x, command.x);
        Assert.Equal(y, command.y);
        Assert.Equal(vel_x, command.vel_x);
        Assert.Equal(vel_y, command.vel_y);
    }

    [Fact]
    public void Execute_AddsToEmptyTree()
    {
        // Arrange
        var tree = new Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>>();
        int x = 1, y = 2, vel_x = 3, vel_y = 4;
        var command = new TreeAddSetCommand(tree, x, y, vel_x, vel_y);

        // Act
        command.Execute();

        // Assert
        Assert.True(tree.ContainsKey(x));
        Assert.True(tree[x].ContainsKey(y));
        Assert.True(tree[x][y].ContainsKey(vel_x));
        Assert.Contains(vel_y, tree[x][y][vel_x]);
        Assert.Equal(1, tree[x][y][vel_x].Count);
    }

    [Fact]
    public void Execute_AddsToExistingX()
    {
        // Arrange
        var tree = new Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>>();
        tree[1] = new Dictionary<int, Dictionary<int, HashSet<int>>>();
        int x = 1, y = 2, vel_x = 3, vel_y = 4;
        var command = new TreeAddSetCommand(tree, x, y, vel_x, vel_y);

        // Act
        command.Execute();

        // Assert
        Assert.True(tree.ContainsKey(x));
        Assert.True(tree[x].ContainsKey(y));
        Assert.True(tree[x][y].ContainsKey(vel_x));
        Assert.Contains(vel_y, tree[x][y][vel_x]);
        Assert.Equal(1, tree[x][y][vel_x].Count);
    }

    [Fact]
    public void Execute_AddsToExistingXAndY()
    {
        // Arrange
        var tree = new Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>>();
        tree[1] = new Dictionary<int, Dictionary<int, HashSet<int>>>();
        tree[1][2] = new Dictionary<int, HashSet<int>>();
        int x = 1, y = 2, vel_x = 3, vel_y = 4;
        var command = new TreeAddSetCommand(tree, x, y, vel_x, vel_y);

        // Act
        command.Execute();

        // Assert
        Assert.True(tree.ContainsKey(x));
        Assert.True(tree[x].ContainsKey(y));
        Assert.True(tree[x][y].ContainsKey(vel_x));
        Assert.Contains(vel_y, tree[x][y][vel_x]);
        Assert.Equal(1, tree[x][y][vel_x].Count);
    }

    [Fact]
    public void Execute_AddsToExistingXAndYAndVelX()
    {
        // Arrange
        var tree = new Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>>();
        tree[1] = new Dictionary<int, Dictionary<int, HashSet<int>>>();
        tree[1][2] = new Dictionary<int, HashSet<int>>();
        tree[1][2][3] = new HashSet<int> { 5 }; // Уже есть другое vel_y
        int x = 1, y = 2, vel_x = 3, vel_y = 4;
        var command = new TreeAddSetCommand(tree, x, y, vel_x, vel_y);

        // Act
        command.Execute();

        // Assert
        Assert.True(tree[x][y].ContainsKey(vel_x));
        Assert.Contains(5, tree[x][y][vel_x]);
        Assert.Contains(vel_y, tree[x][y][vel_x]);
        Assert.Equal(2, tree[x][y][vel_x].Count);
    }

    [Fact]
    public void Execute_DoesNotDuplicateVelY()
    {
        // Arrange
        var tree = new Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>>();
        tree[1] = new Dictionary<int, Dictionary<int, HashSet<int>>>();
        tree[1][2] = new Dictionary<int, HashSet<int>>();
        tree[1][2][3] = new HashSet<int> { 4 }; // vel_y уже существует
        int x = 1, y = 2, vel_x = 3, vel_y = 4;
        var command = new TreeAddSetCommand(tree, x, y, vel_x, vel_y);

        // Act
        command.Execute();
        command.Execute(); // Вызываем дважды, чтобы проверить отсутствие дублирования

        // Assert
        Assert.True(tree.ContainsKey(x));
        Assert.True(tree[x].ContainsKey(y));
        Assert.True(tree[x][y].ContainsKey(vel_x));
        Assert.Contains(vel_y, tree[x][y][vel_x]);
        Assert.Equal(1, tree[x][y][vel_x].Count);
    }
}