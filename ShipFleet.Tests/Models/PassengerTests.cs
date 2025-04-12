using System;
using ShipFleet.Models;
using Xunit;

namespace ShipFleet.Tests
{
    public class PassengerTests
    {
        [Fact]
        public void Constructor_ValidData_CreatesPassenger()
        {
            // Arrange & Act
            var passenger = new Passenger("Alice", "Johnson", "XYZ123456");

            // Assert
            Assert.Equal("Alice", passenger.FirstName);
            Assert.Equal("Johnson", passenger.LastName);
            Assert.Equal("XYZ123456", passenger.PassportNumber);
        }

        [Theory]
        [InlineData("", "Smith", "A123456")]
        [InlineData("John", "", "A123456")]
        [InlineData("John", "Smith", "")]
        [InlineData(null, "Smith", "A123456")]
        [InlineData("John", null, "A123456")]
        [InlineData("John", "Smith", null)]
        public void Constructor_InvalidData_ThrowsArgumentException(string firstName, string lastName, string passportNumber)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Passenger(firstName, lastName, passportNumber));
        }

        [Fact]
        public void ToString_ReturnsCorrectFormat()
        {
            // Arrange
            var passenger = new Passenger("Maria", "Garcia", "P987654");

            // Act
            var result = passenger.ToString();

            // Assert
            Assert.Equal("Maria Garcia, Passport: P987654", result);
        }
    }
}
