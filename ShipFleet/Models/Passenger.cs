namespace ShipFleet.Models
{
    public class Passenger
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string PassportNumber { get; }

        public Passenger(string firstName, string lastName, string passportNumber)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be empty.");
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty.");
            if (string.IsNullOrWhiteSpace(passportNumber))
                throw new ArgumentException("Passport number cannot be empty.");

            FirstName = firstName;
            LastName = lastName;
            PassportNumber = passportNumber;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, Passport: {PassportNumber}";
        }
    }
}
