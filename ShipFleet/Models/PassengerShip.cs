using System;
using System.Collections.Generic;

namespace ShipFleet.Models
{
    public class PassengerShip : Ship
    {
        private List<Passenger> passengers;

        public IReadOnlyList<Passenger> Passengers => passengers.AsReadOnly();

        internal PassengerShip(
            string imo, 
            string name,
            double length, 
            double width, 
            List<Passenger> passengers)
            : base(imo, name, length, width)
        {
            this.passengers = passengers != null
                ? new List<Passenger>(passengers)
                : new List<Passenger>();
        }

        public void UpdatePassengerList(List<Passenger> newPassengerList)
        {
            if (newPassengerList == null)
                throw new ArgumentNullException(nameof(newPassengerList));

            passengers = new List<Passenger>(newPassengerList);
        }

        public void ListPassengers()
        {
            Console.WriteLine($"Passengers on {Name}:");
            foreach (var p in passengers)
            {
                Console.WriteLine(p.ToString());
            }
        }
    }
}
