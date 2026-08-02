using System;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Address
{
    public class Address : IAddress
    {
        private string EmailFormatted = string.Format("testautomation{0}@mailinator.com", DateTime.Now.ToString("yyyyMMddHHmmssFF"));

        private static (string line1, string line2) RandomStreetName(string countrycode)
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

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool SaveToProfile { get; set; }

        //Instance
        protected IBrowser Browser;

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl { get; }
        public bool IsCurrentPage { get; }

        /// <summary>
        /// Default values for entering in Shipping information.
        /// </summary>
        /// <param name="nameSuffix"></param>
        public Address(string nameSuffix = "")
        {
            // Set default values
            FirstName = $"lptest{nameSuffix}";
            LastName = $"lptest{nameSuffix}";
            AddressLine1 = "20250 Plummer St";
            AddressLine2 = "lptest";
            City = "Chatsworth";
            State = StateCodeListUnitedStates.CA; // Use State code not name
            Country = CountryCodeList.US; // Use Country code not name
            ZipCode = ZipCodeList.Chatsworth;
            Phone = "1234567890";
            Email = EmailFormatted;
            SaveToProfile = false;
        }

        /// <summary>
        /// Class for creating a Shipping address object with default values used for populating International Shipping address form. 
        /// Note: Use Country and State codes not names.
        /// </summary>
        public class IntAddress : Address, IIntAddress
        {
            /// <summary>
            /// Default values for entering in International Shipping information.
            /// </summary>
            public IntAddress()
            {
                AddressLine1 = "22 Baker Street";
                City = "London";
                Country = CountryCodeList.GB; // Use Country code not name
                ZipCode = "W1U3BW";
            }
        }

        public class RandomAddressGenerator : Address, IRandomAddressGenerator
        {
            public Address RandomUsAddress()
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
        }
    }
}
