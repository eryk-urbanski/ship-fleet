using ShipFleet.Models;
using Xunit;
using System;
using System.Collections.Generic;

namespace ShipFleet.Tests.Models
{
    public class PassengerShipTests
    {
        private readonly string imo = "IMO 9321483";
        private readonly string name = "Emma Maersk";
        private readonly double length = 397.0;
        private readonly double width = 56.0;

        [Fact]
        public void Constructor_ValidInputs_InitializesPassengerShip()
        {
            // Arrange
            var passengers = new List<Passenger>
            {
                new Passenger("Alice", "Smith", "P001"),
                new Passenger("Bob", "Jones", "P002")
            };

            // Act
            var ship = new PassengerShipBuilder()
                        .WithIMO(imo)
                        .WithName(name)
                        .WithDimensions(length, width)
                        .WithPassengers(passengers)
                        .Build();

            // Assert
            Assert.Equal(name, ship.Name);
            Assert.Equal(length, ship.Length);
            Assert.Equal(width, ship.Width);
            Assert.Equal(2, ship.Passengers.Count);
            Assert.Contains(ship.Passengers, p => p.FirstName == "Alice" && p.LastName == "Smith");
            Assert.Contains(ship.Passengers, p => p.PassportNumber == "P002");
        }

        [Fact]
        public void Constructor_NullPassengerList_InitializesWithEmptyList()
        {
            // Arrange
            // No passengers provided

            // Act
            var ship = new PassengerShipBuilder()
                        .WithIMO(imo)
                        .WithName(name)
                        .WithDimensions(length, width)
                        .WithPassengers(null)
                        .Build();

            // Assert
            Assert.Empty(ship.Passengers);
        }

        [Fact]
        public void UpdatePassengerList_ValidList_ReplacesPassengers()
        {
            // Arrange
            var originalPassengers = new List<Passenger>
            {
                new Passenger("John", "Doe", "P003")
            };

            var newPassengers = new List<Passenger>
            {
                new Passenger("Eva", "Green", "P004"),
                new Passenger("Charlie", "Black", "P005")
            };

            var ship = new PassengerShipBuilder()
                        .WithIMO(imo)
                        .WithName(name)
                        .WithDimensions(length, width)
                        .WithPassengers(originalPassengers)
                        .Build();

            // Act
            ship.UpdatePassengerList(newPassengers);

            // Assert
            Assert.Equal(2, ship.Passengers.Count);
            Assert.DoesNotContain(ship.Passengers, p => p.FirstName == "John" && p.LastName == "Doe");
            Assert.Contains(ship.Passengers, p => p.FirstName == "Eva" && p.LastName == "Green");
        }

        [Fact]
        public void UpdatePassengerList_Null_ThrowsArgumentNullException()
        {
            // Arrange
            var ship = new PassengerShipBuilder()
                        .WithIMO(imo)
                        .WithName(name)
                        .WithDimensions(length, width)
                        .Build();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => ship.UpdatePassengerList(null));
        }
    }
}
