using System;
using System.Collections.Generic;

namespace ShipFleet.Models
{
    public class TankerShip : Ship
    {
        private readonly List<Tank> tanks;
        private double currentWeight;

        public int NumDieselTanks { get; }
        public int NumHeavyFuelTanks { get; }
        public double MaxWeight { get; }
        public double CurrentWeight => currentWeight;
        public IReadOnlyList<Tank> Tanks => tanks.AsReadOnly();

        internal TankerShip(
            string imo,
            string name,
            double length,
            double width,
            int numDieselTanks,
            int numHeavyFuelTanks,
            double maxWeight,
            List<Tank> tanks
        ) : base(imo, name, length, width)
        {
            NumDieselTanks = numDieselTanks;
            NumHeavyFuelTanks = numHeavyFuelTanks;
            MaxWeight = maxWeight;
            currentWeight = 0;
            this.tanks = tanks ?? throw new ArgumentNullException(nameof(tanks));

            Console.WriteLine($"Diesel tanks have IDs in range: <1, {numDieselTanks}>");
            Console.WriteLine($"Heavy fuel tanks have IDs in range: <{numDieselTanks + 1}, {numDieselTanks + numHeavyFuelTanks}>");
        }

        public void RefuelTank(int tankID)
        {
            VerifyTankID(tankID);
            var tank = Tanks[tankID - 1];
            double refillAmount = tank.Capacity - tank.CurrentVolume;
            RefuelTank(tankID, refillAmount);
        }

        public void RefuelTank(int tankID, double volume)
        {
            VerifyTankID(tankID);
            var tank = Tanks[tankID - 1];
            double weightToAdd = (volume * FuelDensity.Values[tank.FuelType]) / 1000.0;

            if (CurrentWeight + weightToAdd > MaxWeight)
                throw new ArgumentException("Exceeds ship's maximum total weight");

            tank.CurrentVolume += volume;
            currentWeight += weightToAdd;

            Console.WriteLine($"Tank {tankID} refueled with {volume} liters of {tank.FuelType}");
        }

        public void EmptyTank(int tankID)
        {
            VerifyTankID(tankID);
            double volume = Tanks[tankID - 1].CurrentVolume;
            EmptyTank(tankID, volume);
        }

        public void EmptyTank(int tankID, double volume)
        {
            VerifyTankID(tankID);
            var tank = Tanks[tankID - 1];

            if (volume > tank.CurrentVolume)
                throw new ArgumentException("Provided volume is larger than the tank's current volume");

            tank.CurrentVolume -= volume;
            currentWeight -= (volume * FuelDensity.Values[tank.FuelType]) / 1000.0;

            Console.WriteLine($"Tank {tankID} emptied to {tank.CurrentVolume} litres");
        }

        private void VerifyTankID(int tankID)
        {
            if (!IsValidTankID(tankID))
                throw new ArgumentException("Invalid tank ID");
        }

        private bool IsValidTankID(int tankID)
        {
            return tankID > 0 && tankID <= Tanks.Count;
        }
    }
}
