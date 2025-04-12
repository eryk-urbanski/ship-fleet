using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ShipFleet.Models
{
    public abstract partial class Ship
    {
        // Properties
        public string IMO { get; }
        public string Name { get; }
        public double Length { get; }
        public double Width { get; }
        public List<Position> PositionHistory { get; } = new List<Position>();

        // Constructor
        public Ship(string imo, string name, double length, double width)
        {
            if (!IsValidIMO(imo))
                throw new ArgumentException("Invalid IMO number");

            if (!IsValidDimensions(length, width))
                throw new ArgumentException("Invalid ship width or length (both should be positive)");

            IMO = imo;
            Name = name;
            Length = length;
            Width = width;
        }

        // Update position
        public void UpdatePosition(Position position)
        {
            if (position.Latitude < -90 || position.Latitude > 90)
                throw new ArgumentOutOfRangeException("Latitude must be between -90 and 90");

            if (position.Longitude < -180 || position.Longitude > 180)
                throw new ArgumentOutOfRangeException("Longitude must be between -180 and 180");

            PositionHistory.Add(position);
        }

        // Get current position
        public Position GetCurrentPosition()
        {
            if (PositionHistory.Count == 0)
                throw new InvalidOperationException("No position history available");

            return PositionHistory[^1];
        }

        // Private validation methods
        private static bool IsValidIMO(string imo)
        {
            var match = MyRegex().Match(imo);
            if (!match.Success)
                return false;

            string digits = match.Groups[1].Value; // Extract the 7 digits, which are in group 1 of the regex match.
            int sum = 0;

            for (int i = 0; i < 6; i++)
                sum += (digits[i] - '0') * (7 - i);

            return sum % 10 == (digits[6] - '0');
        }

        private static bool IsValidDimensions(double length, double width)
        {
            return length > 0 && width > 0;
        }

        [GeneratedRegex(@"^IMO (\d{7})$")]
        private static partial Regex MyRegex();
    }
}
