using System.Collections.Generic;
using System.Web;

using Automation.Framework.Utilities;

namespace Automation.Framework.Core
{
    /// <summary>
    /// Provides access to page specific details such as Title and URL.
    /// </summary>
    public class Page
    {
        public static char SingleSpaceChart => ' ';
        public static string ActiveClass => "active";
        public static string ContentString => "content";
        public static string InnerHtmlAttribute => "innerHTML";
        public static string InvestigateCaps => "INVESTIGATE";
        public static string ItemPropAttribute => "itemprop";
        public static string NewLineSequenceString => "\r\n";
        public static string TrueString => "true";

        /// <summary>
        /// Log class to update log messages.
        /// </summary>
        public Log Log => Browser.Log;

        /// <summary>
        /// Instance of a Browser to enable browser specific UI testing.
        /// </summary>
        public IBrowser Browser { get; }

        /// <summary>
        /// Decode the given string for HTTP.
        /// </summary>
        /// <returns>HTML decoded string.</returns>
        public static string DecodeHtmlString(string value) => HttpUtility.HtmlDecode(value);

        /// <summary>
        /// Provides access to page specific details such as Title and URL.
        /// </summary>
        /// <param name="browser">Browser to use for testing.</param>
        protected Page(IBrowser browser)
        {
            Browser = browser;
        }

        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        /// <param name="url">URL to navigate to. This must be begin with http:// or https://.</param>
        public void Navigate(string url) { Browser.Navigate(url); }

        /// <summary>
        /// Removes the "." or "#" from the specified CSS selector.
        /// </summary>
        /// <returns>The CSS class name or id</returns>
        public static string GetClassNameOrId(string cssSelector)
        {
            return cssSelector.Substring(1);
		}

		/// <summary>
		/// Gets an element from a list, checking if the index exists.
		/// </summary>
		/// <param name="elements"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		protected IElement GetElement(List<IElement> elements, int index) => elements.Count > index ? elements[index] : null;
	}
}