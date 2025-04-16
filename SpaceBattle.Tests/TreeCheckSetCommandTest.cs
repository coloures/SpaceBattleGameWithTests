namespace SpaceBattle.Tests;

using System;
using System.Collections.Generic;
using SpaceBattle.Lib;
using Xunit;

public class TreeCheckSetCommandTest
{
    [Fact]
    public void Constructor_InitializesFieldsCorrectly()
    {
        // Arrange
        var tree = new Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>>();
        int x = 1, y = 2, vel_x = 3, vel_y = 4;

        // Act
        var command = new TreeCheckSetCommand(tree, x, y, vel_x, vel_y);

        // Assert
        Assert.Same(tree, command.Tree);
        Assert.Equal(x, command.x);
        Assert.Equal(y, command.y);
        Assert.Equal(vel_x, command.vel_x);
        Assert.Equal(vel_y, command.vel_y);
    }

    [Fact]
    public void Execute_DoesNotThrow_WhenValueExists()
    {
        // Arrange
        var tree = new Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>>();
        tree[1] = new Dictionary<int, Dictionary<int, HashSet<int>>>();
        tree[1][2] = new Dictionary<int, HashSet<int>>();
        tree[1][2][3] = new HashSet<int> { 4 };
        int x = 1, y = 2, vel_x = 3, vel_y = 4;
        var command = new TreeCheckSetCommand(tree, x, y, vel_x, vel_y);

        // Act & Assert
        var exception = Record.Exception(() => command.Execute());
        Assert.Null(exception);
    }

    [Fact]
    public void Execute_ThrowsException_WhenXDoesNotExist()
    {
        // Arrange
        var tree = new Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>>();
        int x = 1, y = 2, vel_x = 3, vel_y = 4;
        var command = new TreeCheckSetCommand(tree, x, y, vel_x, vel_y);

        // Act & Assert
        Assert.Throws<Exception>(() => command.Execute());
    }

    [Fact]
    public void Execute_ThrowsException_WhenYDoesNotExist()
    {
        // Arrange
        var tree = new Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>>();
        tree[1] = new Dictionary<int, Dictionary<int, HashSet<int>>>();
        int x = 1, y = 2, vel_x = 3, vel_y = 4;
        var command = new TreeCheckSetCommand(tree, x, y, vel_x, vel_y);

        // Act & Assert
        Assert.Throws<Exception>(() => command.Execute());
    }

    [Fact]
    public void Execute_ThrowsException_WhenVelXDoesNotExist()
    {
        // Arrange
        var tree = new Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>>();
        tree[1] = new Dictionary<int, Dictionary<int, HashSet<int>>>();
        tree[1][2] = new Dictionary<int, HashSet<int>>();
        int x = 1, y = 2, vel_x = 3, vel_y = 4;
        var command = new TreeCheckSetCommand(tree, x, y, vel_x, vel_y);

        // Act & Assert
        Assert.Throws<Exception>(() => command.Execute());
    }

    [Fact]
    public void Execute_ThrowsException_WhenVelYDoesNotExist()
    {
        // Arrange
        var tree = new Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>>();
        tree[1] = new Dictionary<int, Dictionary<int, HashSet<int>>>();
        tree[1][2] = new Dictionary<int, HashSet<int>>();
        tree[1][2][3] = new HashSet<int> { 5 }; // vel_y = 4 отсутствует
        int x = 1, y = 2, vel_x = 3, vel_y = 4;
        var command = new TreeCheckSetCommand(tree, x, y, vel_x, vel_y);

        // Act & Assert
        Assert.Throws<Exception>(() => command.Execute());
    }
}