using System;

namespace ShipFleet.Models
{
    public class Tank
    {
        public double Capacity { get; }
        public double CurrentVolume { get; set; }
        public FuelType FuelType { get; }

        public Tank(double capacity, FuelType fuelType)
        {
            if (capacity <= 0)
                throw new ArgumentException("Invalid capacity");

            Capacity = capacity;
            FuelType = fuelType;
            CurrentVolume = 0;
        }

        public void SetCurrentVolume(double volume)
        {
            if (volume < 0)
                throw new ArgumentException("Volume must not be negative");
            if (volume > Capacity)
                throw new ArgumentException("Volume exceeds tank capacity");

            CurrentVolume = volume;
        }
    }
}
