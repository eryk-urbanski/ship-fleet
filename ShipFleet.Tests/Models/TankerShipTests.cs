using ShipFleet.Models;
using Xunit;
using System;
using System.Collections.Generic;

namespace ShipFleet.Tests.Models
{
    public class TankerShipTests
    {
        [Fact]
        public void Constructor_ValidInputs_InitializesTankerShip()
        {
            // Arrange
            var imo = "IMO 9321483";
            var name = "Emma Maersk";
            double length = 397.0;
            double width = 56.0;
            int numDieselTanks = 2;
            var dieselCapacities = new List<double> { 1000.0, 1500.0 };
            int numHeavyFuelTanks = 1;
            var heavyFuelCapacities = new List<double> { 2000.0 };
            double maxWeight = 5000.0;

            // Act
            var tankerShip = new TankerShipBuilder()
                                .WithIMO(imo)
                                .WithName(name)
                                .WithDimensions(length, width)
                                .WithDieselTanks(numDieselTanks, dieselCapacities)
                                .WithHeavyFuelTanks(numHeavyFuelTanks, heavyFuelCapacities)
                                .WithMaxWeight(maxWeight)
                                .Build();

            // Assert
            Assert.Equal(numDieselTanks, tankerShip.NumDieselTanks);
            Assert.Equal(numHeavyFuelTanks, tankerShip.NumHeavyFuelTanks);
            Assert.Equal(dieselCapacities.Count + heavyFuelCapacities.Count, tankerShip.Tanks.Count);
            Assert.Equal(maxWeight, tankerShip.MaxWeight);
            Assert.Equal(0, tankerShip.CurrentWeight);
        }

        [Fact]
        public void Constructor_InvalidInputs_ThrowsArgumentException()
        {
            // Arrange
            var imo = "IMO 9321483";
            var name = "Emma Maersk";
            double length = 397.0;
            double width = 56.0;
            var dieselCapacities = new List<double> { 2000.0 };
            var heavyFuelCapacities = new List<double> { 2000.0 };
            double maxWeight = 5000.0;

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new TankerShipBuilder().WithIMO(imo)
                                       .WithName(name)
                                       .WithDimensions(length, width)
                                       .WithDieselTanks(-1, dieselCapacities)
                                       .WithHeavyFuelTanks(1, heavyFuelCapacities)
                                       .WithMaxWeight(maxWeight)
                                       .Build());

            Assert.Throws<ArgumentException>(() =>
                new TankerShipBuilder().WithIMO(imo)
                                       .WithName(name)
                                       .WithDimensions(length, width)
                                       .WithDieselTanks(1, dieselCapacities)
                                       .WithHeavyFuelTanks(-1, heavyFuelCapacities)
                                       .WithMaxWeight(maxWeight)
                                       .Build());

            Assert.Throws<ArgumentException>(() =>
                new TankerShipBuilder().WithIMO(imo)
                                       .WithName(name)
                                       .WithDimensions(length, width)
                                       .WithDieselTanks(0, new List<double>())
                                       .WithHeavyFuelTanks(0, new List<double>())
                                       .WithMaxWeight(maxWeight)
                                       .Build());

            Assert.Throws<ArgumentException>(() =>
                new TankerShipBuilder().WithIMO(imo)
                                       .WithName(name)
                                       .WithDimensions(length, width)
                                       .WithDieselTanks(7, dieselCapacities)
                                       .WithHeavyFuelTanks(1, heavyFuelCapacities)
                                       .WithMaxWeight(maxWeight)
                                       .Build());
        }

        [Fact]
        public void Refuel_ValidTankID_UpdatesWeight()
        {
            // Arrange
            var imo = "IMO 9321483";
            var name = "Emma Maersk";
            var tankerShip = new TankerShipBuilder()
                                .WithIMO(imo)
                                .WithName(name)
                                .WithDimensions(397.0, 56.0)
                                .WithDieselTanks(1, new List<double> { 1000.0 })
                                .WithHeavyFuelTanks(1, new List<double> { 2000.0 })
                                .WithMaxWeight(156907.0)
                                .Build();

            // Act
            tankerShip.RefuelTank(1, 500.0);

            // Assert
            Assert.Equal((500.0 * FuelDensity.Values[FuelType.Diesel]) / 1000, tankerShip.CurrentWeight);
        }

        [Fact]
        public void Refuel_InvalidTankID_ThrowsArgumentException()
        {
            // Arrange
            var imo = "IMO 9321483";
            var name = "Emma Maersk";
            var tankerShip = new TankerShipBuilder()
                                .WithIMO(imo)
                                .WithName(name)
                                .WithDimensions(397.0, 56.0)
                                .WithDieselTanks(1, new List<double> { 1000.0 })
                                .WithHeavyFuelTanks(1, new List<double> { 2000.0 })
                                .WithMaxWeight(156907.0)
                                .Build();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => tankerShip.RefuelTank(3, 100.0));
        }

        [Fact]
        public void Refuel_ExceedMaxCapacity_ThrowsArgumentException()
        {
            // Arrange
            var imo = "IMO 9321483";
            var name = "Emma Maersk";
            var tankerShip = new TankerShipBuilder()
                                .WithIMO(imo)
                                .WithName(name)
                                .WithDimensions(397.0, 56.0)
                                .WithDieselTanks(1, new List<double> { 1000.0 })
                                .WithHeavyFuelTanks(1, new List<double> { 2000.0 })
                                .WithMaxWeight(156907.0)
                                .Build();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => tankerShip.RefuelTank(1, 6000.0));
        }

        [Fact]
        public void EmptyTank_ValidTank_EmptiesTank()
        {
            // Arrange
            var imo = "IMO 9321483";
            var name = "Emma Maersk";
            var tankerShip = new TankerShipBuilder()
                                .WithIMO(imo)
                                .WithName(name)
                                .WithDimensions(397.0, 56.0)
                                .WithDieselTanks(1, new List<double> { 1000.0 })
                                .WithHeavyFuelTanks(1, new List<double> { 2000.0 })
                                .WithMaxWeight(156907.0)
                                .Build();

            tankerShip.RefuelTank(1, 500.0);

            // Act
            tankerShip.EmptyTank(1);

            // Assert
            Assert.Equal(0.0, tankerShip.CurrentWeight);
        }

        [Fact]
        public void EmptyTank_InvalidTankID_ThrowsArgumentException()
        {
            // Arrange
            var imo = "IMO 9321483";
            var name = "Emma Maersk";
            var tankerShip = new TankerShipBuilder()
                                .WithIMO(imo)
                                .WithName(name)
                                .WithDimensions(397.0, 56.0)
                                .WithDieselTanks(1, new List<double> { 1000.0 })
                                .WithHeavyFuelTanks(1, new List<double> { 2000.0 })
                                .WithMaxWeight(156907.0)
                                .Build();

            tankerShip.RefuelTank(1, 500.0);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => tankerShip.EmptyTank(3));
        }
    }
}
