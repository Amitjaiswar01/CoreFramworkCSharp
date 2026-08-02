using System;

namespace LampsPlus.AutomationFramework.Utilities
{
    /// <summary>
    /// Helper utility to generate names
    /// </summary>
    public class NamesGenerator
    {
        public static string FirstName () => FirstNames[new Random().Next(0, FirstNames.Length)];
        public static string LastName () => LastNames[new Random().Next(0, LastNames.Length)];

        private static readonly string[] FirstNames = { "Eddard", "Robert", "Jaime", "Catelyn", "Cersei", "Daenerys", "Jorah", "Viserys", "Jon", "Sansa", "Arya", "Joffrey", "Bronn", "Ramsay", "Theon" };
        private static readonly string[] LastNames = { "Stark", "Baratheon", "Lannister", "Targaryen", "Mormont", "Snow", "Greyjoy", "Clegane", "Tarly", "Tyrell" };
    }
}
