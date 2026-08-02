using System.Collections.Generic;
using System.Web.UI;

using Automation.Framework.Enums;

namespace Automation.Framework.Utilities
{
    /// <summary>
    /// String extensions
    /// </summary>
    public static class LocatorExtensions
    {
        /// <summary>
        /// Prepends an HTML ID string with '#' character to convert it into a CSS ID selector.
        /// </summary>
        /// <param name="id">The HTML ID attribute</param>
        /// <returns>A CSS ID selector string.</returns>
        public static string ToCssIdSelector(this string id)
        {
            return $"#{id}";
        }

        /// <summary>
        /// Prepends an HTML CSS class name with '.' character to convert it into a CSS class selector.
        /// </summary>
        /// <param name="className">The HTML CSS class name.</param>
        /// <returns>A CSS class name selector string.</returns>
        public static string ToCssClassSelector(this string className)
        {
            return $".{className}";
        }

        /// <summary>
        /// Prepends an HTML CSS class name with '> .' characters to convert it into a CSS class selector as a direct child of its parent.
        /// </summary>
        /// <param name="className">The HTML CSS class name.</param>
        /// <returns>A CSS class name selector string prepended by ">".</returns>
        public static string ToDirectChildCssClassSelector(this string className)
        {
            return $"> .{className}";
        }

        /// <summary>
        /// Prepends a CSS selector with '>' character to indicate it as a direct child of its parent.
        /// </summary>
        /// <param name="selector">The HTML CSS selector.</param>
        /// <returns>A CSS selector string prepended by ">".</returns>
        public static string ToDirectChildCssSelector(this string selector)
        {
            return $"> {selector}";
		}

		/// <summary>
		/// Prepends a tag selector with '>' character to indicate it as a direct child of its parent.
		/// </summary>
		/// <param name="tag">The HTML tag.</param>
		/// <returns>A CSS selector string prepended by ">".</returns>
		public static string ToDirectChildSelector(this HtmlTextWriterTag tag)
		{
			return $"> {tag}";
		}

		/// <summary>
		/// Prepends an HTML CSS class name with '+ .' characters to convert it into a CSS class selector that is an immediate next sibling of another element.
		/// </summary>
		/// <param name="className">The HTML CSS class name.</param>
		/// <returns>A CSS class name selector string prepended by "+".</returns>
		public static string ToAdjacentSiblingCssClassSelector(this string className)
        {
            return $"+ .{className}";
        }

        /// <summary>
        /// Prepends an HTML CSS class name with '~ .' characters to convert it into a CSS class selector that is a next sibling of another element.
        /// </summary>
        /// <param name="className">The HTML CSS class name.</param>
        /// <returns>A CSS class name selector string prepended by "~".</returns>
        public static string ToGeneralSiblingCssClassSelector(this string className)
        {
            return $"~ .{className}";
        }

        /// <summary>
        /// Prepends an HTML tag name with '>' character to convert it into a CSS tag selector as a direct child of its parent.
        /// </summary>
        /// <param name="tag">The HTML tag name.</param>
        /// <returns>A CSS tag name selector string prepended by ">".</returns>
        public static string ToDirectChildCssTagSelector(this string tag)
        {
            return $"> {tag}";
        }

        /// <summary>
        /// Prepends an HTML CSS tag name with '+' character to convert it into a CSS tag selector that is an immediate next sibling of another element.
        /// </summary>
        /// <param name="tag">The HTML tag name.</param>
        /// <returns>A CSS tag name selector string prepended by "+".</returns>
        public static string ToAdjacentSiblingTagClassSelector(this string tag)
        {
            return $"+ {tag}";
		}

		/// <summary>
		/// Prepends an HTML CSS tag name with '+' character to convert it into a CSS tag selector that is an immediate next sibling of another element.
		/// </summary>
		/// <param name="tag">The HTML tag name.</param>
		/// <returns>A CSS tag name selector string prepended by "+".</returns>
		public static string ToAdjacentSiblingTagClassSelector(this HtmlTextWriterTag tag)
		{
			return $"+ {tag}";
		}

		/// <summary>
		/// Prepends an HTML CSS tag name with '~' character to convert it into a CSS tag selector that is a next sibling of another element.
		/// </summary>
		/// <param name="tag">The HTML CSS class name.</param>
		/// <returns>A CSS class name selector string prepended by "~".</returns>
		public static string ToGeneralSiblingCssTagSelector(this string tag)
        {
            return $"~ {tag}";
        }

        /// <summary>
        /// Converts an HTML attribute value into an HTML tag name and attribute CSS selector. E.g. tagName[attr="value"]
        /// </summary>
        /// <param name="attributeValue">The current string which is an HTML attribute value.</param>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the HTML tag name that forms the output CSS selector.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the HTML attribute name that forms the output CSS selector.</param>
        /// <returns>An HTML tag name and attribute selector in the format: tagName[attr="value"]</returns>
        public static string ToTagNameAndAttributeCssSelector(this string attributeValue, HtmlTextWriterTag tagName, HtmlTextWriterAttribute attributeName)
        {
            return $"{tagName}[{attributeName}=\"{attributeValue}\"]";
        }

        /// <summary>
        /// Converts an HTML attribute value into an HTML tag name and attribute CSS selector. E.g. tagName[attr="value"]
        /// </summary>
        /// <param name="attributeValue">The current string which is an HTML attribute value.</param>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the HTML tag name that forms the output CSS selector.</param>
        /// <param name="attributeName">The HTML attribute name that forms the output CSS selector.</param>
        /// <returns>An HTML tag name and attribute selector in the format: tagName[attr="value"]</returns>
        public static string ToTagNameAndAttributeCssSelector(this string attributeValue, HtmlTextWriterTag tagName, string attributeName)
        {
            return $"{tagName}[{attributeName}=\"{attributeValue}\"]";
        }

        /// <summary>
        /// Converts an HTML attribute value into an HTML input tag name and value attribute CSS selector. E.g. input[value="value"]
        /// </summary>
        /// <param name="attributeValue">The current string which is an HTML attribute value.</param>
        /// <returns>An HTML input tag name and value attribute selector in the format: input[value="value"]</returns>
        public static string ToInputValueCssSelector(this string attributeValue)
        {
            return $"{HtmlTextWriterTag.Input}[{HtmlTextWriterAttribute.Value}=\"{attributeValue}\"]";
        }

        /// <summary>
        /// Converts an HTML attribute type into an HTML input tag name and type attribute CSS selector. E.g. input[type="value"]
        /// </summary>
        /// <param name="attributeValue">The current string which is an HTML attribute value.</param>
        /// <returns>An HTML input tag name and type attribute selector in the format: input[type="value"]</returns>
        public static string ToInputTypeCssSelector(this string attributeValue)
        {
            return $"{HtmlTextWriterTag.Input}[{HtmlTextWriterAttribute.Type}=\"{attributeValue}\"]";
        }

        /// <summary>
        /// Appends :nth-child CSS selector to the current selector string. E.g. selector:nth-child(2)
        /// </summary>
        /// <param name="selector">The current CSS class or ID selector string.</param>
        /// <param name="nth">The numeric position of the element.</param>
        /// <returns>A CSS selector string with :nth-child selector.</returns>
        public static string ToNthChildSelector(this string selector, int nth)
        {
            return $"{selector}:nth-child({nth})";
        }

        /// <summary>
        /// Appends :nth-child CSS selector to the current selector string. E.g. selector:nth-child(odd) or selector:nth-child(2n+1)
        /// </summary>
        /// <param name="selector">The current CSS class or ID selector string.</param>
        /// <param name="keywordOrFormula">The keyword (e.g. "even" or "odd") or formula (e.g. "n+6") representing the position of the element.</param>
        /// <returns>A CSS selector string with :nth-child selector.</returns>
        public static string ToNthChildSelector(this string selector, string keywordOrFormula)
        {
            return $"{selector}:nth-child({keywordOrFormula})";
        }

        /// <summary>
        /// Appends :nth-child CSS selector to the current selector string. E.g. tagName:nth-child(2)
        /// </summary>
        /// <param name="tag">The current HtmlTextWriterTag selector.</param>
        /// <param name="nth">The numeric position of the element.</param>
        /// <returns>A CSS selector string with :nth-child selector.</returns>
        public static string ToNthChildSelector(this HtmlTextWriterTag tag, int nth)
        {
            return $"{tag}:nth-child({nth})";
        }

        /// <summary>
        /// Appends :nth-child CSS selector to the current selector string. E.g. tagName:nth-child(odd) or tagName:nth-child(2n+1)
        /// </summary>
        /// <param name="tag">The current HtmlTextWriterTag selector.</param>
        /// <param name="keywordOrFormula">The keyword (e.g. "even" or "odd") or formula (e.g. "n+6") representing the position of the element.</param>
        /// <returns>A CSS selector string with :nth-child selector.</returns>
        public static string ToNthChildSelector(this HtmlTextWriterTag tag, string keywordOrFormula)
        {
            return $"{tag}:nth-child({keywordOrFormula})";
        }

        /// <summary>
        /// Appends :first-child CSS selector to the current selector string. E.g. selector:first-child
        /// </summary>
        /// <param name="selector">The current CSS class or ID selector string.</param>
        /// <returns>A CSS selector string with :first-child selector.</returns>
        public static string ToFirstChildSelector(this string selector)
        {
            return $"{selector}:first-child";
        }

        /// <summary>
        /// Appends :first-child CSS selector to the current tag name selector string. E.g. tagName:first-child
        /// </summary>
        /// <param name="tag">The current HtmlTextWriterTag selector.</param>
        /// <returns>A CSS selector string with :first-child selector.</returns>
        public static string ToFirstChildSelector(this HtmlTextWriterTag tag)
        {
            return $"{tag}:first-child";
        }

        /// <summary>
        /// Appends :last-child CSS selector to the current selector string. E.g. selector:last-child
        /// </summary>
        /// <param name="selector">The current CSS class or ID selector string.</param>
        /// <returns>A CSS selector string with :last-child selector.</returns>
        public static string ToLastChildSelector(this string selector)
        {
            return $"{selector}:last-child";
        }

        /// <summary>
        /// Appends :last-child CSS selector to the current tag name. E.g. tagName:last-child
        /// </summary>
        /// <param name="tag">The current HtmlTextWriterTag selector.</param>
        /// <returns>A CSS selector string with :last-child selector.</returns>
        public static string ToLastChildSelector(this HtmlTextWriterTag tag)
        {
            return $"{tag}:last-child";
        }

        /// <summary>
        /// Converts an HTML attribute into a :not CSS attribute selector. E.g. :not([attr])
        /// </summary>
        /// <param name="attributeToBeNegated">The HTML attribute name to be negated.</param>
        /// <returns>A CSS selector string with :not selector.</returns>
        public static string ToNotAttributeSelector(this string attributeToBeNegated)
        {
            return $":not([{attributeToBeNegated}])";
        }

        /// <summary>
        /// Converts an HTML attribute into a :not CSS attribute selector. E.g. :not([attr])
        /// </summary>
        /// <param name="attributeToBeNegated">The HTML attribute name to be negated.</param>
        /// <returns>A CSS selector string with :not selector.</returns>
        public static string ToNotAttributeSelector(this HtmlTextWriterAttribute attributeToBeNegated)
        {
            return $":not([{attributeToBeNegated}])";
        }

        /// <summary>
        /// Appends :not CSS selector to the current tag name. E.g. tagName:not(.className)
        /// </summary>
        /// <param name="tag">The current HtmlTextWriterTag selector.</param>
        /// <param name="classNameToBeNegated">The CSS class name to be negated.</param>
        /// <returns>A CSS selector string with :not selector.</returns>
        public static string ToTagNotClassSelector(this HtmlTextWriterTag tag, string classNameToBeNegated)
        {
            return $"{tag}:not(.{classNameToBeNegated})";
        }

        /// <summary>
        /// Appends :not CSS selector to the current class name. E.g. .className1:not(.className2)
        /// </summary>
        /// <param name="className">The current CSS class name string.</param>
        /// <param name="classNameToBeNegated">The CSS class name to be negated.</param>
        /// <returns>A CSS selector string with :not selector.</returns>
        public static string ToClassNotClassSelector(this string className, string classNameToBeNegated)
        {
            return $".{className}:not(.{classNameToBeNegated})";
		}

		/// <summary>
		/// Appends :not CSS selector to the current class name. E.g. .className:not(#id)
		/// </summary>
		/// <param name="className">The current CSS class name string.</param>
		/// <param name="idToBeNegated">The CSS id to be negated.</param>
		/// <returns>A CSS selector string with :not selector.</returns>
		public static string ToClassNotIdSelector(this string className, string idToBeNegated)
		{
			return $".{className}:not(#{idToBeNegated})";
		}

		/// <summary>
		/// Appends :first-of-type CSS selector to the current tag name. E.g. tagName:first-of-type
		/// </summary>
		/// <param name="tag">The current HtmlTextWriterTag selector.</param>
		/// <returns>A CSS selector string with :first-child selector.</returns>
		public static string ToTagFirstOfTypeSelector(this HtmlTextWriterTag tag)
        {
            return $"{tag}:first-of-type";
        }

        /// <summary>
        /// Appends :first-of-type CSS selector to the current CSS class name. E.g. tagName:first-of-type
        /// </summary>
        /// <param name="className">The current class name.</param>
        /// <returns>A CSS selector string with :first-child selector.</returns>
        public static string ToClassFirstOfTypeSelector(this string className)
        {
            return $".{className}:first-of-type";
        }

        /// <summary>
        /// Converts the current CSS class name string into an equivalent XPath expression.
        /// Usually used for direct child matching relative to the parent element.
        /// </summary>
        /// <param name="className">The current CSS class name string</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child or not.</param>
        /// <returns>An XPath string</returns>
        public static string ToXPathCssClass(this string className, bool isDirectChild = false)
        {
            var xPath = GetXPath(HtmlTextWriterAttribute.Class.ToString(), className, AttributeSelectorType.Contains);
            return isDirectChild ? $"./{xPath}" : $"//{xPath}";
        }

        /// <summary>
        /// Converts the current CSS class name string into an equivalent XPath expression.
        /// Usually used for direct child matching relative to the parent element.
        /// </summary>
        /// <param name="classNames">The current CSS class names array of string.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child or not.</param>
        /// <returns>An XPath string</returns>
        public static string ToXPathCssClasses(this string[] classNames, bool isDirectChild = false)
        {
            var xPathExpressions = new List<string>();
            foreach (var className in classNames)
            {
                xPathExpressions.Add(GetXPathAttributeExpression(HtmlTextWriterAttribute.Class.ToString(), className, AttributeSelectorType.ContainsWord));
            }

            var xPath = $"({string.Join(") and (", xPathExpressions)})";

            return $"{(isDirectChild ? "." : string.Empty)}//{xPath}";
        }

        /// <summary>
        /// Converts the current HtmlTextWriteTag tag name into an equivalent XPath expression.
        /// Usually used for direct child matching relative to the parent element.
        /// </summary>
        /// <param name="tag">The current HTML tag name with HtmlTextWriterTag type.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child or not.</param>
        /// <returns>An XPath string</returns>
        public static string ToXPathTagName(this HtmlTextWriterTag tag, bool isDirectChild = false)
        {
            var tagName = (tag == HtmlTextWriterTag.Unknown) ? "*" : tag.ToString().ToLower();
            return isDirectChild ? $"./{tagName}" : $"//{tagName}";
        }

        /// <summary>
        /// Converts the current HtmlTextWriteTag tag name and CSS class name into an equivalent XPath expression.
        /// Usually used for direct child matching relative to the parent element.
        /// </summary>
        /// <param name="tag">The current HTML tag name with HtmlTextWriterTag type.</param>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child or not.</param>
        /// <returns>An XPath string</returns>
        public static string ToXPathTagNameAndClass(this HtmlTextWriterTag tag, string className, bool isDirectChild = false)
        {
            var tagName = (tag == HtmlTextWriterTag.Unknown) ? "*" : tag.ToString().ToLower();
            return isDirectChild ? $"./{tagName}[@class='{className}']" : $"//{tagName}[@class='{className}']";
        }

        /// <summary>
        /// Converts the current HTML attribute value of "name" attribute into an equivalent XPath expression.
        /// Usually used for direct child matching relative to the parent element.
        /// </summary>
        /// <param name="attributeValue">The current HTML attribute value string.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child or not.</param>
        /// <returns>An XPath string</returns>
        public static string ToXPathNameAttribute(this string attributeValue, bool isDirectChild = false)
        {
            return $"{(isDirectChild ? "." : string.Empty)}//*[@name='{attributeValue}']";
        }

        /// <summary>
        /// Converts the current HtmlTextWriterAttribute attribute name and value into an equivalent XPath expression.
        /// Usually used for direct child matching relative to the parent element.
        /// </summary>
        /// <param name="attributeName">The current attribute name with HtmlTextWriterAttribute type.</param>
        /// <param name="attributeValue">The HTML attribute value.</param>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child or not.</param>
        /// <returns>An XPath string</returns>
        public static string ToXPathAttribute(this HtmlTextWriterAttribute attributeName, string attributeValue, AttributeSelectorType attSelectorType, bool isDirectChild = false)
        {
            var xPath = GetXPath(attributeName.ToString(), attributeValue, attSelectorType);
            
            return $"{(isDirectChild ? "." : string.Empty)}//{xPath}";
        }

        /// <summary>
        /// Converts the current HtmlTextWriterAttribute attribute name and value into an equivalent XPath expression.
        /// Usually used for direct child matching relative to the parent element.
        /// </summary>
        /// <param name="attributeName">The current HTML attribute name string.</param>
        /// <param name="attributeValue">The HTML attribute value.</param>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child or not.</param>
        /// <returns>An XPath string</returns>
        public static string ToXPathAttribute(this string attributeName, string attributeValue, AttributeSelectorType attSelectorType, bool isDirectChild = false)
        {
            var xPath = GetXPath(attributeName, attributeValue, attSelectorType);

            return $"{(isDirectChild ? "." : string.Empty)}//{xPath}";
        }

        /// <summary>
        /// Converts the current HTML attribute names into an equivalent XPath expression.
        /// Usually used for direct child matching relative to the parent element.
        /// </summary>
        /// <param name="attributeNames">The current HTML attribute names string array.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child or not.</param>
        /// <returns>An XPath string</returns>
        public static string ToXPathAttributeNames(this string[] attributeNames, bool isDirectChild = false)
        {
            var xPaths = new List<string>();
            foreach (var attributeName in attributeNames)
            {
                xPaths.Add($"@{attributeName}");
            }

            var xPath = string.Join(" and ", xPaths);

            return $"{(isDirectChild ? "." : string.Empty)}//*[{xPath}]";
        }

        /// <summary>
        /// Converts the current HTML attribute name and value pairs into an equivalent XPath expression.
        /// Usually used for direct child matching relative to the parent element.
        /// </summary>
        /// <param name="attributes">The current HTML attribute name and value pairs.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child or not.</param>
        /// <returns>An XPath string</returns>
        public static string ToXPathAttributes(this KeyValuePair<HtmlTextWriterAttribute, string>[] attributes, bool isDirectChild = false)
        {
            var xPaths = new List<string>();
            foreach (var attribute in attributes)
            {
                xPaths.Add($"@{attribute.Key}='{attribute.Value}'");
            }

            var xPath = string.Join(" and ", xPaths);

            return $"{(isDirectChild ? "." : string.Empty)}//*[{xPath}]";
        }

        /// <summary>
        /// Converts the current HTML attribute name and value pairs into an equivalent XPath expression.
        /// Usually used for direct child matching relative to the parent element.
        /// </summary>
        /// <param name="attributes">The current HTML attribute name and value pairs.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child or not.</param>
        /// <returns>An XPath string</returns>
        public static string ToXPathAttributes(this KeyValuePair<string, string>[] attributes, bool isDirectChild = false)
        {
            var xPaths = new List<string>();
            foreach (var attribute in attributes)
            {
                xPaths.Add($"@{attribute.Key}='{attribute.Value}'");
            }

            var xPath = string.Join(" and ", xPaths);

            return $"{(isDirectChild ? "." : string.Empty)}//*[{xPath}]";
        }

        /// <summary>
        /// Converts the current HTML tag name and attribute name and value into an equivalent XPath expression.
        /// Usually used for direct child matching relative to the parent element.
        /// </summary>
        /// <param name="tagName">The current HTML tag name with HtmlTextWriterTag type.</param>
        /// <param name="attributeName">The HTML attribute name with HtmlTextWriterAttribute type.</param>
        /// <param name="attributeValue">The HTML attribute value.</param>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child or not.</param>
        /// <returns>An XPath string</returns>
        public static string ToXPathTagNameAndAttribute(this HtmlTextWriterTag tagName, HtmlTextWriterAttribute attributeName, string attributeValue, AttributeSelectorType attSelectorType, bool isDirectChild = false)
        {
            var xPath = GetXPath(attributeName.ToString(), attributeValue, attSelectorType, tagName.ToString());

            return $"{(isDirectChild ? "." : string.Empty)}//{xPath}";
        }

        /// <summary>
        /// Converts the current HTML tag name and attribute name and value into an equivalent XPath expression.
        /// Usually used for direct child matching relative to the parent element.
        /// </summary>
        /// <param name="tagName">The current HTML tag name with HtmlTextWriterTag type.</param>
        /// <param name="attributeName">The HTML attribute name.</param>
        /// <param name="attributeValue">The HTML attribute value.</param>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child or not.</param>
        /// <returns>An XPath string</returns>
        public static string ToXPathTagNameAndAttribute(this HtmlTextWriterTag tagName, string attributeName, string attributeValue, AttributeSelectorType attSelectorType, bool isDirectChild = false)
        {
            var xPath = GetXPath(attributeName, attributeValue, attSelectorType, tagName.ToString());

            return $"{(isDirectChild ? "." : string.Empty)}//{xPath}";
        }

        /// <summary>
        /// Converts the current CSS class name and attribute name and value into an equivalent XPath expression.
        /// Usually used for direct child matching relative to the parent element.
        /// </summary>
        /// <param name="className">The current CSS class name.</param>
        /// <param name="attributeName">The HTML attribute name with HtmlTextWriterAttribute type.</param>
        /// <param name="attributeValue">The HTML attribute value.</param>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child or not.</param>
        /// <returns>An XPath string</returns>
        public static string ToXPathClassNameAndAttribute(this string className, HtmlTextWriterAttribute attributeName, string attributeValue, AttributeSelectorType attSelectorType, bool isDirectChild = false)
        {
            var xPathClassNameExpression = GetXPathAttributeExpression(HtmlTextWriterAttribute.Class.ToString(), className, AttributeSelectorType.ContainsWord);
            var xPathAttributeExpression = GetXPathAttributeExpression(attributeName.ToString(), attributeValue, attSelectorType);

            return $"{(isDirectChild ? "." : string.Empty)}//*[({xPathClassNameExpression}) and ({xPathAttributeExpression})]";
        }

        /// <summary>
        /// Converts the current CSS class name and attribute name and value into an equivalent XPath expression.
        /// Usually used for direct child matching relative to the parent element.
        /// </summary>
        /// <param name="className">The current CSS class name.</param>
        /// <param name="attributeName">The HTML attribute name.</param>
        /// <param name="attributeValue">The HTML attribute value.</param>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child or not.</param>
        /// <returns>An XPath string</returns>
        public static string ToXPathClassNameAndAttribute(this string className, string attributeName, string attributeValue, AttributeSelectorType attSelectorType, bool isDirectChild = false)
        {
            var xPathClassNameExpression = GetXPathAttributeExpression(HtmlTextWriterAttribute.Class.ToString(), className, AttributeSelectorType.ContainsWord);
            var xPathAttributeExpression = GetXPathAttributeExpression(attributeName, attributeValue, attSelectorType);

            return $"{(isDirectChild ? "." : string.Empty)}//*[({xPathClassNameExpression}) and ({xPathAttributeExpression})]";
        }

        /// <summary>
        /// Converts the current link text into an equivalent XPath expression.
        /// Usually used for direct child matching relative to the parent element.
        /// </summary>
        /// <param name="text">The current link text.</param>
        /// <param name="tagName">The HTML tag name.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child or not.</param>
        /// <returns>An XPath string</returns>
        public static string ToXPathText(this string text, string tagName = "*", bool isDirectChild = false)
        {
            return $"{(isDirectChild ? "." : string.Empty)}//{tagName.ToLower()}[text()={text}]";
        }

        /// <summary>
        /// Converts the current partial link text into an equivalent XPath expression.
        /// Usually used for direct child matching relative to the parent element.
        /// </summary>
        /// <param name="text">The current partial link text.</param>
        /// <param name="tagName">The HTML tag name.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child or not.</param>
        /// <returns>An XPath string</returns>
        public static string ToXPathPartialText(this string text, string tagName = "*", bool isDirectChild = false)
        {
            return $"{(isDirectChild ? "." : string.Empty)}//{tagName.ToLower()}[contains(text(), '{text}')]";
        }

        /// <summary>
        /// Converts the current IWebElement into a string representation showing the HTML tag name with id, class or name.
        /// Usually used for logging parent element object.
        /// </summary>
        /// <param name="element">The IWebElement element to convert into string.</param>
        /// <returns>A string representation showing the HTML tag name with id, class or name.</returns>
        public static string Stringify(this IElement element)
        {
            var tagName = element.TagName.ToLower();

            var id = element.GetAttribute(HtmlTextWriterAttribute.Id.ToString());
            if (!string.IsNullOrEmpty(id)) { return $"{tagName} id=\"{id}\""; }

            var classNames = element.GetAttribute(HtmlTextWriterAttribute.Class.ToString());
            if (!string.IsNullOrEmpty(classNames)) { return $"{tagName} class=\"{classNames}\""; }

            var name = element.GetAttribute(HtmlTextWriterAttribute.Name.ToString());

            return (!string.IsNullOrEmpty(name)) ? $"{tagName} name=\"{name}\"" : tagName;
        }

        private static string GetXPath(string attributeName, string attributeValue, AttributeSelectorType attSelectorType, string tagName = "*")
        {
            tagName = tagName.ToLower();
            attributeName = attributeName.ToLower();

            return $"{tagName}[{GetXPathAttributeExpression(attributeName, attributeValue, attSelectorType)}]";           
        }

        private static string GetXPathAttributeExpression(string attributeName, string attributeValue, AttributeSelectorType attSelectorType)
        {
            var xPath = string.Empty;
            attributeName = attributeName.ToLower();
            switch (attSelectorType)
            {
                case AttributeSelectorType.Contains:
                {
                    xPath = $"contains(@{attributeName}, '{attributeValue}')";
                    break;
                }

                case AttributeSelectorType.Equals:
                {
                    xPath = $"@{attributeName}='{attributeValue}'";
                    break;
                }

                case AttributeSelectorType.HasAttribute:
                {
                    xPath = $"@{attributeName}";
                    break;
                }

                case AttributeSelectorType.StartsWith:
                {
                    xPath = $"starts-with(@{attributeName}, '{attributeValue}')";
                    break;
                }

                case AttributeSelectorType.EndsWith:
                {
                    xPath = $"ends-with(@{attributeName}, '{attributeValue}')";
                    break;
                }

                case AttributeSelectorType.ContainsPrefix:
                {
                    xPath = $"@{attributeName}='{attributeValue}' or starts-with(@{attributeName}, '{attributeValue}-')";
                    break;
                }

                case AttributeSelectorType.ContainsWord:
                {
                    // Browsers don't support 'ends-with' XPath function which is part of XPath 2.0
                    // so let's do it using combination of contains() and not(contains()) expressions (3rd line below)
                    xPath = $@"contains(@{attributeName}, ' {attributeValue} ') or
                            starts-with(@{attributeName}, '{attributeValue} ') or
                            (contains(@{attributeName}, ' {attributeValue}') and not(contains(@{attributeName}, ' {attributeValue} ')))";
                    break;
                }
            }

            return xPath;
        }
    }
}
