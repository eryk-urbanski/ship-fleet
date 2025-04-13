using System;
using System.Collections.Generic;

namespace ShipFleet.Models
{
    public class PassengerShipBuilder
    {
        private string imo;
        private string name;
        private double length;
        private double width;
        private List<Passenger> passengers = new();

        public PassengerShipBuilder WithIMO(string imo)
        {
            this.imo = imo ?? throw new ArgumentNullException(nameof(imo), "IMO cannot be null");
            return this;
        }

        public PassengerShipBuilder WithName(string name)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name), "Name cannot be null");
            return this;
        }

        public PassengerShipBuilder WithDimensions(double length, double width)
        {
            this.length = length;
            this.width = width;
            return this;
        }

        public PassengerShipBuilder WithPassengers(List<Passenger> passengers)
        {
            this.passengers = passengers;
            return this;
        }

        public PassengerShip Build()
        {
            return new PassengerShip(imo, name, length, width, passengers);
        }
    }
}
