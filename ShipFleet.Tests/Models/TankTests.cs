using System;
using Xunit;
using ShipFleet.Models;

public class TankTests
{
    [Fact]
    public void Constructor_ValidInputs_InitializesTank()
    {
        // Act
        var tank = new Tank(500.0, FuelType.Diesel);

        // Assert
        Assert.Equal(500.0, tank.Capacity);
        Assert.Equal(0.0, tank.CurrentVolume);
        Assert.Equal(FuelType.Diesel, tank.FuelType);
    }

    [Fact]
    public void Constructor_InvalidCapacity_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Tank(-1.0, FuelType.Diesel));
        Assert.Throws<ArgumentException>(() => new Tank(0.0, FuelType.Diesel));
    }

    [Fact]
    public void SetCurrentVolume_ValidVolume_SetsCurrentVolume()
    {
        // Arrange
        var tank = new Tank(500.0, FuelType.Diesel);

        // Act
        tank.SetCurrentVolume(50.0);

        // Assert
        Assert.Equal(50.0, tank.CurrentVolume);
    }

    [Fact]
    public void SetCurrentVolume_NegativeVolume_ThrowsArgumentException()
    {
        // Arrange
        var tank = new Tank(500.0, FuelType.Diesel);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => tank.SetCurrentVolume(-1.0));
    }

    [Fact]
    public void SetCurrentVolume_ExceedsCapacity_ThrowsArgumentException()
    {
        // Arrange
        var tank = new Tank(500.0, FuelType.Diesel);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => tank.SetCurrentVolume(1500.0));
    }
}
