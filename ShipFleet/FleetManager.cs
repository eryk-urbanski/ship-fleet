using System;
using System.Collections.Generic;
using System.Linq;

namespace ShipFleet.Models
{
    public class FleetManager
    {
        private readonly List<Ship> fleet;

        public FleetManager()
        {
            fleet = new List<Ship>();
        }

        // Add a ship to the fleet
        public void AddShip(Ship ship)
        {
            ArgumentNullException.ThrowIfNull(ship);

            fleet.Add(ship);
        }

        // Remove a ship from the fleet by IMO
        public bool RemoveShip(string imo)
        {
            var shipToRemove = fleet.FirstOrDefault(ship => ship.IMO == imo);
            if (shipToRemove != null)
            {
                fleet.Remove(shipToRemove);
                return true;
            }

            return false; // Ship not found
        }

        // Perform operations specific to TankerShips (e.g., refuel)
        public void RefuelTankerShip(string imo, int tankId, double fuelAmount)
        {
            var ship = fleet.OfType<TankerShip>().FirstOrDefault(s => s.IMO == imo);
            if (ship == null)
                throw new InvalidOperationException("Tanker ship not found.");

            ship.RefuelTank(tankId, fuelAmount);
        }

        // Perform operations specific to PassengerShips (e.g., list passengers)
        public void ListPassengerShipPassengers(string imo)
        {
            var ship = fleet.OfType<PassengerShip>().FirstOrDefault(s => s.IMO == imo);
            if (ship == null)
                throw new InvalidOperationException("Passenger ship not found.");

            ship.ListPassengers();
        }

        // Update passenger list of a PassengerShip
        public void UpdatePassengerList(string imo, List<Passenger> newPassengerList)
        {
            var ship = fleet.OfType<PassengerShip>().FirstOrDefault(s => s.IMO == imo);
            if (ship == null)
                throw new InvalidOperationException("Passenger ship not found.");

            ship.UpdatePassengerList(newPassengerList);
        }

        // Get list of all ships
        public List<Ship> GetAllShips()
        {
            return fleet;
        }

        // Get a specific ship by IMO
        public Ship GetShipByIMO(string imo)
        {
            return fleet.FirstOrDefault(ship => ship.IMO == imo);
        }
    }
}
