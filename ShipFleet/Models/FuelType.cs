namespace ShipFleet.Models
{
    public enum FuelType
    {
        Diesel,
        HeavyFuel
    }

    public static class FuelDensity
    {
        // Densities in kg/litre
        public static readonly Dictionary<FuelType, double> Values = new()
        {
            { FuelType.Diesel, 0.85 },
            { FuelType.HeavyFuel, 0.96 }
        };

        public static string ToString(FuelType type)
        {
            return type switch
            {
                FuelType.Diesel => "Diesel",
                FuelType.HeavyFuel => "Heavy Fuel",
                _ => "Unknown Fuel Type"
            };
        }
    }
}
