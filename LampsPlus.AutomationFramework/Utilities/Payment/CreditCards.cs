using System;
using System.Collections.Generic;
using System.Linq;

namespace LampsPlus.AutomationFramework.Utilities.Payment
{
    /// <summary>
    /// Supported Lamps Plus Payment types.
    /// </summary>
    public class CreditCards
    {
        /// <summary>
        /// List of Testing Credit Cards
        /// </summary>
        private static IReadOnlyDictionary<CreditCardType, IReadOnlyList<string>> TestCards { get; } = new Dictionary<CreditCardType, IReadOnlyList<string>>()
            {
                {CreditCardType.Visa, new List<string>() { "4111111111111111", "4012888888881881"}},
                {CreditCardType.MasterCard, new List<string>() { "2222420000001113", "5555555555554444", "5105105105105100" }},
                {CreditCardType.Discovery, new List<string>() { "6011111111111117", "6011000990139424" }},
                {CreditCardType.AmericanExpress, new List<string>() { "378282246310005", "371449635398431" }}
            };

        /// <summary>
        /// This Credit Card only works on PPE
        /// </summary>
        public static CreditCard TestVisaCard { get; } =  new CreditCard { CardNumber = TestCards[CreditCardType.Visa].First(), ExpirationMonth = DateTime.Now.Month, ExpirationYear = DateTime.Now.AddYears(1).Year, NameOnCard = "Jack Daniels", CardType = "Visa", SecurityCode = "123" };
        public static CreditCard TestMasterCard { get; }  = new CreditCard { CardNumber = TestCards[CreditCardType.MasterCard].First(), ExpirationMonth = DateTime.Now.Month, ExpirationYear = DateTime.Now.AddYears(1).Year, NameOnCard = "Master Daniels", CardType = "MasterCard", SecurityCode = "123" };

        public static CreditCard RandomTestCard ()
        {
            var rnd = new Random();
            var cardType = TestCards.Keys.ElementAt(rnd.Next(0, TestCards.Keys.Count()));
            var cardNumber = TestCards[cardType].ElementAt(rnd.Next(0, TestCards[cardType].Count));

            return new CreditCard()
            {
                CardNumber = cardNumber,
                ExpirationMonth = rnd.Next(1, 13),
                ExpirationYear = DateTime.Now.AddYears(rnd.Next(1, 5)).Year,
                NameOnCard = $"{NamesGenerator.FirstName()} {NamesGenerator.LastName()}",
                CardType = cardType.ToString(),
                SecurityCode = rnd.Next(0, 1000).ToString("0:000")
            };
        }
    }
}
