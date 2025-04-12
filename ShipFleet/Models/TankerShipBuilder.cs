namespace ShipFleet.Models
{    
    public class TankerShipBuilder
    {
        private string imo;
        private string name;
        private double length;
        private double width;
        private int numDieselTanks;
        private List<double> dieselCapacities = new();
        private int numHeavyFuelTanks;
        private List<double> heavyFuelCapacities = new();
        private double maxWeight;

        public TankerShipBuilder WithIMO(string imo)
        {
            this.imo = imo ?? throw new ArgumentNullException(nameof(imo), "IMO cannot be null");
            return this;
        }

        public TankerShipBuilder WithName(string name)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name), "Name cannot be null");
            return this;
        }

        public TankerShipBuilder WithDimensions(double length, double width)
        {
            this.length = length;
            this.width = width;
            return this;
        }

        public TankerShipBuilder WithDieselTanks(int count, List<double> capacities)
        {
            this.numDieselTanks = count;
            this.dieselCapacities = capacities;
            return this;
        }

        public TankerShipBuilder WithHeavyFuelTanks(int count, List<double> capacities)
        {
            this.numHeavyFuelTanks = count;
            this.heavyFuelCapacities = capacities;
            return this;
        }

        public TankerShipBuilder WithMaxWeight(double maxWeight)
        {
            this.maxWeight = maxWeight;
            return this;
        }

        public TankerShip Build()
        {
            if (numDieselTanks < 0 || numHeavyFuelTanks < 0)
                throw new ArgumentException("Invalid tank numbers");

            if (numDieselTanks == 0 && numHeavyFuelTanks == 0)
                throw new ArgumentException("At least one tank is required");

            if (dieselCapacities == null || dieselCapacities.Count != numDieselTanks)
                throw new ArgumentException("Mismatch in diesel tank capacities");

            if (heavyFuelCapacities == null || heavyFuelCapacities.Count != numHeavyFuelTanks)
                throw new ArgumentException("Mismatch in heavy fuel tank capacities");

            var tanks = new List<Tank>();

            foreach (var cap in dieselCapacities)
            {
                if (cap <= 0)
                    throw new ArgumentException("Invalid diesel tank capacity");
                tanks.Add(new Tank(cap, FuelType.Diesel));
            }

            foreach (var cap in heavyFuelCapacities)
            {
                if (cap <= 0)
                    throw new ArgumentException("Invalid heavy fuel tank capacity");
                tanks.Add(new Tank(cap, FuelType.HeavyFuel));
            }

            return new TankerShip(
                imo,
                name,
                length,
                width,
                numDieselTanks,
                numHeavyFuelTanks,
                maxWeight,
                tanks
            );
        }
    }
}