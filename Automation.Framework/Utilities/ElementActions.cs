using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using OpenQA.Selenium;
using Page = Automation.Framework.Core.Page;

namespace Automation.Framework.Utilities
{
    /// <summary>
    /// Helper class for acting on IWebElements.
    /// </summary>
    public static class ElementActions
    {
        /// <summary>
        /// Get list of classes within a given element and class name.
        /// </summary>
        /// <param name="element">Element to locate and find sub classes of.</param>
        /// <param name="className">Class name to find in the parent element.</param>
        /// <returns>True when the given element contains the requested className.</returns>
        public static bool HasClass(IElement element, string className)
        {
            return Array.IndexOf(ClassList(element), className) > -1;
        }

        /// <summary>
        /// Is the given element disabled?
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool IsElementDisabled(IElement element)
        {
            return HasClass(element, "haslpt");
        }

        /// <summary>
        /// Get list of classes within a given element.
        /// </summary>
        /// <param name="element">Element to locate and find sub classes of.</param>
        /// <returns></returns>
        private static string[] ClassList(IElement element)
        {
            return element.GetAttribute(HtmlTextWriterAttribute.Class.ToString()).Split(Page.SingleSpaceChart);
        }

        /// <summary>
        /// Selects and returns a random element from a list of elements.
        /// </summary>
        /// <param name="Elements">List of Elements</param>
        /// <returns>Randomly selected element from list.</returns>
        public static IElement SelectRandom(IEnumerable<IElement> Elements)
        {
            return Elements.OrderBy(o => Guid.NewGuid()).First();
        }
    }
}
