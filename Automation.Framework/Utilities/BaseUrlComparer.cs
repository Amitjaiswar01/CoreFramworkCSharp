using System.Collections.Generic;

namespace Automation.Framework.Utilities
{
    /// <summary>
    /// Contains logic for comparing image URLs.
    /// </summary>
    /// <remarks>
    /// Excludes the query string values in each URL from the comparison.
    /// </remarks>
    public class BaseUrlComparer : IEqualityComparer<string>
    {
        /// <inheritdoc />
        public bool Equals(string x, string y)
        {
            return GetBaseUrl(x).Equals(GetBaseUrl(y));
        }

        /// <inheritdoc />
        public int GetHashCode(string obj)
        {
            return -1;
        }

        private string GetBaseUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) { return string.Empty; }

            var index = url.IndexOf('?');
            return index == -1 ? url : url.Substring(0, index).ToLower().Trim();
        }
    }
}
