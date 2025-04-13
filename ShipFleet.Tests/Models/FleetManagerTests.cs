using ShipFleet.Models;
using Xunit;
using System;
using System.Collections.Generic;

namespace ShipFleet.Tests.Models
{
    public class FleetManagerTests
    {
        private readonly string imo = "IMO 9321483";
        private readonly string name = "Emma Maersk";
        private readonly double length = 397.0;
        private readonly double width = 56.0;
        private readonly int numDieselTanks = 2;
        private readonly List<double> dieselCapacities = new List<double> { 1000.0, 1500.0 };
        private readonly int numHeavyFuelTanks = 1;
        private readonly List<double> heavyFuelCapacities = new List<double> { 2000.0 };
        private readonly double maxWeight = 5000.0;
        [Fact]
        public void AddShip_WithValidTankerAndPassenger_AddsBothToFleet()
        {
            var manager = new FleetManager();

            var tanker = new TankerShipBuilder()
                                .WithIMO(imo)
                                .WithName(name)
                                .WithDimensions(length, width)
                                .WithDieselTanks(numDieselTanks, dieselCapacities)
                                .WithHeavyFuelTanks(numHeavyFuelTanks, heavyFuelCapacities)
                                .WithMaxWeight(maxWeight)
                                .Build();
            var passenger = new PassengerShipBuilder()
                                    .WithIMO(imo)
                                    .WithName(name)
                                    .WithDimensions(length, width)
                                    .WithPassengers(new List<Passenger>())
                                    .Build();

            manager.AddShip(tanker);
            manager.AddShip(passenger);

            var allShips = manager.GetAllShips();
            Assert.Contains(tanker, allShips);
            Assert.Contains(passenger, allShips);
        }

        [Fact]
        public void RemoveShip_WithExistingIMO_RemovesShipFromFleet()
        {
            var manager = new FleetManager();
            var tanker = new TankerShipBuilder()
                                .WithIMO(imo)
                                .WithName(name)
                                .WithDimensions(length, width)
                                .WithDieselTanks(numDieselTanks, dieselCapacities)
                                .WithHeavyFuelTanks(numHeavyFuelTanks, heavyFuelCapacities)
                                .WithMaxWeight(maxWeight)
                                .Build();
            manager.AddShip(tanker);

            var result = manager.RemoveShip("IMO 9321483");

            Assert.True(result);
            Assert.Empty(manager.GetAllShips());
        }

        [Fact]
        public void RemoveShip_WithNonExistentIMO_ReturnsFalse()
        {
            var manager = new FleetManager();

            var result = manager.RemoveShip("IMO 9999999");

            Assert.False(result);
        }

        [Fact]
        public void RefuelTankerShip_WithValidInput_UpdatesFuelLevel()
        {
            var manager = new FleetManager();
            var tanker = new TankerShipBuilder()
                                .WithIMO(imo)
                                .WithName(name)
                                .WithDimensions(length, width)
                                .WithDieselTanks(numDieselTanks, dieselCapacities)
                                .WithHeavyFuelTanks(numHeavyFuelTanks, heavyFuelCapacities)
                                .WithMaxWeight(maxWeight)
                                .Build();
            manager.AddShip(tanker);

            manager.RefuelTankerShip("IMO 9321483", 1, 100.0);

            Assert.Equal((100.0 * FuelDensity.Values[FuelType.Diesel]) / 1000, tanker.CurrentWeight);
        }

        [Fact]
        public void RefuelTankerShip_WithNonExistentIMO_ThrowsInvalidOperationException()
        {
            var manager = new FleetManager();

            Assert.Throws<InvalidOperationException>(() =>
                manager.RefuelTankerShip("IMO 9321483", 1, 100.0));
        }

        [Fact]
        public void UpdatePassengerList_WithValidPassengerShip_UpdatesPassengerList()
        {
            var manager = new FleetManager();
            var ship = new PassengerShipBuilder()
                                    .WithIMO(imo)
                                    .WithName(name)
                                    .WithDimensions(length, width)
                                    .WithPassengers(new List<Passenger>())
                                    .Build();
            manager.AddShip(ship);

            var newPassengers = new List<Passenger>
            {
                new Passenger("Alice", "Smith", "A123456"),
                new Passenger("Bob", "Jones", "B654321")
            };

            manager.UpdatePassengerList("IMO 9321483", newPassengers);

            Assert.Equal(2, ship.Passengers.Count);
        }

        [Fact]
        public void UpdatePassengerList_WithNonExistentShip_ThrowsInvalidOperationException()
        {
            var manager = new FleetManager();

            Assert.Throws<InvalidOperationException>(() =>
                manager.UpdatePassengerList("IMO 9999999", new List<Passenger>()));
        }

        [Fact]
        public void GetShipByIMO_WithExistingIMO_ReturnsCorrectShip()
        {
            var manager = new FleetManager();
            var tanker = new TankerShipBuilder()
                                .WithIMO(imo)
                                .WithName(name)
                                .WithDimensions(length, width)
                                .WithDieselTanks(numDieselTanks, dieselCapacities)
                                .WithHeavyFuelTanks(numHeavyFuelTanks, heavyFuelCapacities)
                                .WithMaxWeight(maxWeight)
                                .Build();
            manager.AddShip(tanker);

            var ship = manager.GetShipByIMO("IMO 9321483");

            Assert.Equal(tanker, ship);
        }

        [Fact]
        public void GetShipByIMO_WithNonExistentIMO_ReturnsNull()
        {
            var manager = new FleetManager();

            var ship = manager.GetShipByIMO("IMO 9999999");

            Assert.Null(ship);
        }

        [Fact]
        public void UpdatePosition_WithExistingShip_UpdatesCurrentPositionAndHistory()
        {
            var manager = new FleetManager();
            var tanker = new TankerShipBuilder()
                                .WithIMO(imo)
                                .WithName(name)
                                .WithDimensions(length, width)
                                .WithDieselTanks(numDieselTanks, dieselCapacities)
                                .WithHeavyFuelTanks(numHeavyFuelTanks, heavyFuelCapacities)
                                .WithMaxWeight(maxWeight)
                                .Build();
            manager.AddShip(tanker);

            var position1 = new Position { Latitude = 34.0, Longitude = -120.0, Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds() };
            var position2 = new Position { Latitude = 35.0, Longitude = -121.0, Timestamp = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds() };

            manager.GetShipByIMO("IMO 9321483").UpdatePosition(position1);
            manager.GetShipByIMO("IMO 9321483").UpdatePosition(position2);

            Assert.Equal(position2.Latitude, tanker.GetCurrentPosition().Latitude);
            Assert.Equal(position2.Longitude, tanker.GetCurrentPosition().Longitude);
            Assert.Contains(position1, tanker.PositionHistory);
            Assert.Contains(position2, tanker.PositionHistory);
            Assert.Equal(2, tanker.PositionHistory.Count);
        }

        [Fact]
        public void UpdatePosition_WithExistingShipWrongPosition_ThrowsArgumentOutOfRangeException()
        {
            var manager = new FleetManager();
            var tanker = new TankerShipBuilder()
                                .WithIMO(imo)
                                .WithName(name)
                                .WithDimensions(length, width)
                                .WithDieselTanks(numDieselTanks, dieselCapacities)
                                .WithHeavyFuelTanks(numHeavyFuelTanks, heavyFuelCapacities)
                                .WithMaxWeight(maxWeight)
                                .Build();
            manager.AddShip(tanker);

            var position = new Position { Latitude = 134.0, Longitude = -120.0, Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds() };

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                manager.GetShipByIMO("IMO 9321483").UpdatePosition(position));
        }
    }
}
