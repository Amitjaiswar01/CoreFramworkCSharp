using System;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;

namespace LampsPlus.AutomationFramework.Utilities
{
    /// <summary>
    /// Class for creating a Create Account object with default values used for populating create account form. 
    /// </summary>
    public class Account
    {
        /// <summary>
        /// First name for the account.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name for the account.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email address for the account.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Password for the account.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Security answer for the account.
        /// </summary>
        public string SecurityAnswer { get; set; }

        /// <summary>
        /// Zip code for the account.
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Application name for the account.
        /// </summary>
        public string ApplicationName { get; set; }

        /// <inheritdoc />
        public Account()
        {
            FirstName = "TestAccountFirstName";
            LastName = "TestAccountLastName";
            ApplicationName = "lampsplusautomation";
            EmailAddress = GenerateRandonEmailAddress();
            Password = "AutoTest123@";
            SecurityAnswer = "pizza";
            ZipCode = ZipCodeList.Chatsworth;
        }

        /// <summary>
        /// Append the date time to the given email account to create a new random email account.
        /// </summary>
        /// <returns></returns>
        private string GenerateRandonEmailAddress()
        {
            return $"{ApplicationName}{DateTime.Now:yyyyMMddHHmmss}@getnada.com";
        }
    }
}
