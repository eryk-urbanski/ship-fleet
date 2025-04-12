using ShipFleet.Models;
using Xunit;
using System;

namespace ShipFleet.Tests.Models
{
    public class ShipTests
    {
        [Fact]
        public void Constructor_ValidInputs_InitializesShip()
        {
            // Arrange
            var imo = "IMO 9074729";
            var name = "Black Pearl";
            double length = 300.5;
            double width = 40.0;

            // Act
            var ship = new TestShip(imo, name, length, width);

            // Assert
            Assert.Equal(imo, ship.IMONumber);
            Assert.Equal(name, ship.Name);
            Assert.Equal(length, ship.Length);
            Assert.Equal(width, ship.Width);
        }

        [Fact]
        public void Constructor_InvalidIMONumberFormat_ThrowsArgumentException()
        {
            // Arrange
            var invalidIMO = "12345";

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new TestShip(invalidIMO, "Black Pearl", 300.5, 40.0));
        }

        [Fact]
        public void Constructor_InvalidIMONumberChecksum_ThrowsArgumentException()
        {
            var invalidIMO = "IMO 9074725"; // Invalid checksum

            Assert.Throws<ArgumentException>(() =>
                new TestShip(invalidIMO, "Black Pearl", 300.5, 40.0));
        }

        [Theory]
        [InlineData(-300.5, 40.0)]
        [InlineData(300.5, -40.0)]
        [InlineData(0.0, 40.0)]
        [InlineData(300.5, 0.0)]
        public void Constructor_InvalidLengthOrWidth_ThrowsArgumentException(double length, double width)
        {
            var imo = "IMO 9074729";

            Assert.Throws<ArgumentException>(() =>
                new TestShip(imo, "Black Pearl", length, width));
        }

        [Fact]
        public void UpdatePosition_ValidPosition_AddsToHistory()
        {
            var ship = new TestShip("IMO 9074729", "Black Pearl", 300.5, 40.0);

            var position = new Position
            {
                Latitude = 12.34,
                Longitude = 56.78,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            };

            ship.UpdatePosition(position);

            Assert.Single(ship.PositionHistory);
            Assert.Equal(position.Latitude, ship.PositionHistory[0].Latitude);
            Assert.Equal(position.Longitude, ship.PositionHistory[0].Longitude);
        }

        [Fact]
        public void UpdatePosition_InvalidLatitude_ThrowsOutOfRangeException()
        {
            var ship = new TestShip("IMO 9074729", "Black Pearl", 300.5, 40.0);

            var invalidPos = new Position
            {
                Latitude = 100.0,
                Longitude = 56.78,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => ship.UpdatePosition(invalidPos));
        }

        [Fact]
        public void UpdatePosition_InvalidLongitude_ThrowsOutOfRangeException()
        {
            var ship = new TestShip("IMO 9074729", "Black Pearl", 300.5, 40.0);

            var invalidPos = new Position
            {
                Latitude = 12.34,
                Longitude = 200.0,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => ship.UpdatePosition(invalidPos));
        }
    }
}
