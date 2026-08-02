using System;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Utilities
{
    /// <summary>
    /// Helper utility to generate addresses
    /// </summary>
    public class AddressGenerator
    {
        public static Address BillingAddressCaliforniaUniqueEmail() => new Address("Billing")
        {
            ZipCode = ZipCodeList.Chatsworth,
            State = StateCodeListUnitedStates.CA,
            Email = "testautomation" + new Random().Next(1000) + "@mailinator.com"
        };

        public static Address ShippingAddressCaliforniaUniqueEmail () => new Address ("Shipping")
        {
            ZipCode = ZipCodeList.Chatsworth,
            State = StateCodeListUnitedStates.CA,
            Email = "testautomation" + DateTime.Now.Ticks.ToString() + "@mailinator.com"
        };

        public static Address RandomUsAddress ()
        {
            var rnd = new Random();
            var (line1, line2) = RandomStreetName("US");
            var states = typeof(StateCodeListUnitedStates).GetFields();
            var state = states[rnd.Next(0, states.Length)].GetValue(null) as string;

            return new Address()
            {
                FirstName = NamesGenerator.FirstName(),
                LastName = NamesGenerator.LastName(),
                AddressLine1 = line1,
                AddressLine2 = line2,
                City = CityNames[rnd.Next(0, CityNames.Length)],
                Country = CountryCodeList.US,
                State = state,
                ZipCode = ((long)(rnd.NextDouble() * 99999) + 1).ToString("D5"),
                Phone = ((long)(rnd.NextDouble() * 9000000000) + 1000000000).ToString(),
                Email = $"{Guid.NewGuid().ToString("n").Substring(0, 8)}@sharklasers.com"
            };
        }

        public static (string line1, string line2) RandomStreetName(string countrycode)
        {
            switch (countrycode)
            {
                case "US":
                    var rnd = new Random();
                    var home = ((long)(rnd.NextDouble() * 9999) + 1).ToString();
                    var prefix = rnd.Next(0, 10) < 3 ? $"{StreetPrefixes[rnd.Next(0, StreetPrefixes.Length)]} " : "";
                    var postfix = StreetPostfixes[rnd.Next(0, StreetPostfixes.Length)];
                    var line1 = $"{home} {prefix}{StreetNames[rnd.Next(0, StreetNames.Length)]} {postfix}";
                    var line2 = rnd.Next(0, 1) < 1 ? $"{Line2Prefixes[rnd.Next(0, Line2Prefixes.Length)]}{rnd.Next(1, 999)}" : "";
                    return (line1, line2);
                default:
                    throw new ArgumentException($"Country code {countrycode} is not supported");
            }
        }

        private static readonly string[] StreetNames = { "Baker", "Florida", "Elchel", "Morgan", "Benson", "Rudd", "Mead", "Las Virgenes", "High", "Belleville", "76th" };
        private static readonly string[] StreetPrefixes = { "West", "W", "South", "S", "East", "E", "North", "N" };
        private static readonly string[] StreetPostfixes = { "Street", "St", "Road", "Rd", "Drive", "Dr", "Boulevard", "Blvd", "Parkway", "Pkwy", "Avenue", "Ave" };
        private static readonly string[] Line2Prefixes = { "Apt ", "#", "Suite ", "STE ", "Ste ", "Floor " };
        private static readonly string[] CityNames = { "Los Angeles", "LA", "San Francisco", "Phenix City", "New York City", "New York", "Fort Payne", "Austin", "San Jose", "Dallas" };
    }
}
