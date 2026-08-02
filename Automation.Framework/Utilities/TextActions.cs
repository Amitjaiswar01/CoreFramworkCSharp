using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Automation.Framework.Utilities
{
    /// <summary>
    /// Helper class for actions on text.
    /// </summary>
    public static class TextActions
    {
        /// <summary>
        /// Remove $ from the given string.
        /// </summary>
        /// <param name="textString">Text string to remove $ from.</param>
        /// <returns>Formatted string, removing $.</returns>
        public static string RemoveDollarSign(string textString) => textString.Replace("$", "");

        /// <summary>
        /// Converts string to 8-bit unassigned integer array
        /// </summary>
        /// <param name="base64ForUrlInput"></param>
        /// <returns></returns>
        public static byte[] FromBase64ForUrlString(string base64ForUrlInput)
        {
            var padChars = (base64ForUrlInput.Length % 4) == 0 ? 0 : (4 - (base64ForUrlInput.Length % 4));
            var result = new StringBuilder(base64ForUrlInput, base64ForUrlInput.Length + padChars);
            result.Append(string.Empty.PadRight(padChars, '='));
            result.Replace('-', '+').Replace('_', '/');
            return Convert.FromBase64String(result.ToString());
        }

        /// <summary>
        /// Convert decimal to string and format it to 2 numbers after the decimal point.
        /// </summary>
        /// <param name="number">Decimal to truncate</param>
        /// <returns>string</returns>
        public static string FormatToTwoDecimals(decimal number)
        {
            return $"{number:n2}";
        }

        /// <summary>
        /// Format the the given value as a price in $.
        /// </summary>
        /// <param name="price">Price to format "string.Format("{0:n}", price)}".</param>
        /// <returns>Formatted price string.</returns>
        public static string FormatPrice(decimal price) => $"${FormatToTwoDecimals(price)}";

        /// <summary>
        /// Remove $ and any empty spaces, this will retain the '-' in the case of negative numbers.
        /// </summary>
        /// <param name="price"></param>
        /// <returns>Decimal value of price</returns>
        public static decimal FormatPrice(string price) => decimal.Parse(price.Replace("$", string.Empty).Replace(" ", string.Empty));

        /// <summary>
        /// Adds a slash at the end of a url if it doesn't already contain one.
        /// </summary>
        /// <param name="url">Url to normalize.</param>
        /// <returns>Normalized Url.</returns>
        public static string NormalizeUrl(string url)
        {
            if (url.Last() != '/')
            {
                return $"{url}/";
            }

            return url;
        }

        /// <summary>
        /// Remove white space from a string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveWhitespace(string str)
        {
            return Regex.Replace(str, @"\s+", string.Empty);
        }

        /// <summary>
        /// Returns the string with only one space between words and whitespace trimmed from the ends.
        /// </summary>
        /// <param name="str">String to normalize.</param>
        /// <returns>Normalized string.</returns>
        public static string NormalizeWhitespace(string str)
        {
            return Regex.Replace(str, @"\s{2,}", " ").Trim();
        }

        /// <summary>
        /// Remove non-numerical digits from a phone number.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static string NormalizePhoneNumber(string phoneNumber)
        {
            var removeNonDigits = Regex.Replace(phoneNumber, @"[^\d]", string.Empty);
            return Regex.Replace(removeNonDigits, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
        }

        public static string RegexNoTabsAndNewLines(string valueToRegex)
        {
            Regex rgx = new Regex("\t|\\s+");
            return rgx.Replace(valueToRegex, " ");
        }

        public static int GetIntegerOnly(string original)
        {
            var str = string.Empty;
            var val = 0;

            for (var i = 0; i < original.Length; i++)
            {
                if (char.IsDigit(original[i]))
                {
                    str += original[i];
                }
            }

            if (str.Length > 0)
            {
                val = int.Parse(str);
            }

            return val;
        }

        public static string TrimUrlAfterDesignatedString(string originalUrl, string substring)
        {
            var index = originalUrl.IndexOf(substring, StringComparison.Ordinal);
            return index >= 0 ? originalUrl.Substring(0, index + substring.Length) : originalUrl;
        }

        public static string RemoveTextBeforeAndIncludingCharacter(string targetText, char character)
        {
            var index = targetText.IndexOf(character);
            targetText = targetText.Substring(index + 1).Trim();
            return targetText;
        }

        public static string GetPriceTextOnly(string targetText)
        {
            var pattern = @"\d+(\.\d+)?";//Regular expression pattern to match numbers(including decimals)
            var match = Regex.Match(targetText, pattern);
            targetText = match.Value;
            return targetText;
        }

        public static string GetOnlyPriceFromString(string targetText)
        {
            var pattern = @"\$\d+(\.\d+)?";//Regular expression pattern to match numbers(including decimals) and dollar sign.
            var match = Regex.Match(targetText, pattern);
            targetText = match.Value;
            return targetText;
        }
    }
}