using System;

namespace ShipFleet.Models
{
    public struct Position
    {
        // Latitude in range [-90, 90]. Positive values indicate northern hemisphere, negative - southern.
        public double Latitude;

        // Longitude in range [-180, 180]. Positive values indicate eastern hemisphere, negative - western.
        public double Longitude;

        // Timestamp of the position update (Unix time).
        public long Timestamp;
    }
}
