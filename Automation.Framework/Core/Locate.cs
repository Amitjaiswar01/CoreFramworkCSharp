
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Web.UI;

using Automation.Framework.Enums;
using Automation.Framework.Utilities;

using OpenQA.Selenium;

namespace Automation.Framework.Core
{
#pragma warning disable CS3001, CS3002
    /// <summary>
    /// Provides support for finding elements on the screen.
    /// </summary>
    public class Locate
    {
        protected Browser Browser { get; }
        protected Log Log => Browser.Log;
        protected string _selector;
        protected LocatorStrategy _locatorStrategy;

        /// <summary>
        /// Provides support for finding elements on the screen.
        /// </summary>.
        /// <param name="browser">Provide access to the  WebDriver and Framework specific classes.</param>
        public Locate(Browser browser) { Browser = browser; }

        /// <summary>
        /// Locate an element by id attribute. Returns an empty Element if not found and no exception is thrown.
        /// </summary>
        /// <param name="id">The id of the element to locate excluding the hash (#) character.</param>
        /// <returns>Matching IElement.</returns>
        public virtual IElement ElementById(string id)
        {
            var elements = new List<IElement>();
            _selector = id;
            _locatorStrategy = LocatorStrategy.Id;

            Log.Message($"Locate an element by id {_selector}");

                foreach (var element in Browser.Driver.FindElements(By.Id(_selector)))
                {
                    elements.Add(new Element(element, Log, _selector, _locatorStrategy));
                }
                
            return CheckServerError(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by CSS class name. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate excluding the dot (.) character.</param>
        /// <returns>Matching IElement.</returns>
        public virtual IElement ElementByClassName(string className)
        {
             var elements = new List<IElement>();
            _selector = className;
            _locatorStrategy = LocatorStrategy.Class;

            Log.Message($"Locate an element by className {_selector}");

            foreach (var element in Browser.Driver.FindElements(By.ClassName(_selector)))
            {
                elements.Add(new Element(element, Log, _selector, _locatorStrategy));
            }
                
            return CheckServerError(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element contained in the specified parent element by CSS class name. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate excluding the dot (.) character.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>Matching IElement.</returns>
        public virtual IElement ElementByClassName(string className, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();
            _selector = className;
            _locatorStrategy = LocatorStrategy.Class;

            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return parentElement;
            }

            Log.Message($"Locate an element by className {_selector} with parent {parentElement.Stringify()}");

                var by = isDirectChild ? By.XPath(_selector.ToXPathCssClass(true)) : By.ClassName(_selector);

                foreach (var element in parentElement.InternalElement.FindElements(by))
                {
                    elements.Add(new Element(element, Log, _selector, _locatorStrategy));
                }
          
            return CheckServerError(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by CSS class name.
        /// </summary>
        /// <param name="className">The CSS class name of the elements to locate excluding the dot (.) character.</param>
        /// <returns>List of matching IElements.</returns>
        public virtual ReadOnlyCollection<IElement> ElementsByClassName(string className)
        {
            var elements = new List<IElement>();

            Log.Message($"Locate elements by className {className}");

           
                foreach (var element in Browser.Driver.FindElements(By.CssSelector(className.ToCssClassSelector())))
                {
                    elements.Add(new Element(element, Log, _selector, _locatorStrategy));
                }

                return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements contained in the specified parent element by CSS class name.
        /// </summary>
        /// <param name="className">The CSS class name of the elements to locate excluding the dot (.) character.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByClassName(string className, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            Log.Message($"Locate elements by className {className} with parent {parentElement.Stringify()}");

            var elementsList = isDirectChild ?
                parentElement.FindElements(By.XPath(className.ToXPathCssClass(true))) :
                parentElement.FindElements(By.ClassName(className));

            return CheckServerErrorForElements(elementsList);
        }

        /// <summary>
        /// Locate an element by multiple CSS class names (.className1.className2).
        /// </summary>
        /// <param name="classNames">The CSS class names of the element to locate excluding the dot (.) character. Specify as many class names as needed.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByClassNames(params string[] classNames)
        {
            var elements = new List<IElement>();
            var selectorBuilder = new StringBuilder();
            _locatorStrategy = LocatorStrategy.Css;

            foreach (var className in classNames)
            {
                selectorBuilder.Append(className.ToCssClassSelector());
            }

            _selector = selectorBuilder.ToString();

            Log.Message($"Locate an element by multiple classNames {_selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(_selector)))
            {
                elements.Add(new Element(element, Log, _selector, _locatorStrategy));
            }

            return CheckServerError(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by multiple CSS class names (.className1.className2).
        /// </summary>
        /// <param name="classNames">The CSS class names of the element to locate excluding the dot (.) character. Specify as many class names as needed.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByClassNames(IElement parentElement, bool isDirectChild = false, params string[] classNames)
        {
            var elements = new List<IElement>();
            var selector = new StringBuilder();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = classNames.ToXPathCssClasses(true);
                Log.Message($"Locate a direct child element by multiple class names {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            foreach (var className in classNames)
            {
                selector.Append(className.ToCssClassSelector());
            }

            Log.Message($"Locate an element by multiple class names {selector} with parent {parentElement.Stringify()}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector.ToString())))
            {
                elements.Add(new Element(element, Log, selector.ToString(), LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by multiple CSS class names (.className1.className2).
        /// </summary>
        /// <param name="classNames">The CSS class names of the elements to locate excluding the dot (.) character. Specify as many class names as needed.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByClassNames(params string[] classNames)
        {
            var selector = new StringBuilder();
            var elements = new List<IElement>();

            foreach (var className in classNames)
            {
                selector.Append(className.ToCssClassSelector());
            }

            Log.Message($"Locate elements by multiple classNames {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector.ToString())))
            {
                elements.Add(new Element(element, Log, selector.ToString(), LocatorStrategy.Class));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by multiple CSS class names (.className1.className2).
        /// </summary>
        /// <param name="classNames">The CSS class names of the elements to locate excluding the dot (.) character. Specify as many class names as needed.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>Matching IElement.</returns>
        public ReadOnlyCollection<IElement> ElementsByClassNames(IElement parentElement, bool isDirectChild = false, params string[] classNames)
        {
            var elements = new List<IElement>();
            ReadOnlyCollection<IElement> elementsList;

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(new List<IElement>());
            }

            if (isDirectChild)
            {
                var xPath = classNames.ToXPathCssClasses(true);
                Log.Message($"Locate direct child elements by multiple class names {xPath} with parent {parentElement.Stringify()}");

                elementsList = CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));

                foreach (IWebElement element in elementsList)
                {
                    elements.Add(new Element(element, Log, parentElement.Stringify(), LocatorStrategy.Xpath));
                }

                return new ReadOnlyCollection<IElement>(elements);
            }

            var selector = new StringBuilder();
            foreach (var className in classNames)
            {
                selector.Append(className.ToCssClassSelector());
            }

            Log.Message($"Locate elements by multiple class names {selector} with parent {parentElement.Stringify()}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector.ToString())))
            {
                elements.Add(new Element(element, Log, selector.ToString(), LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by the specified tag name. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByTagName(HtmlTextWriterTag tagName)
        {
            var elements = new List<IElement>();
            _selector = tagName.ToString();
            _locatorStrategy = LocatorStrategy.TagName;

            Log.Message($"Locate an element by tagName {_selector}");

            foreach (var element in Browser.Driver.FindElements(By.TagName(_selector)))
            {
                elements.Add(new Element(element, Log, _selector, _locatorStrategy));
            }

            return CheckServerError(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element contained in the parent element by the specified tag name. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByTagName(HtmlTextWriterTag tagName, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return parentElement;
            }

            _selector = tagName.ToString();
            _locatorStrategy = LocatorStrategy.TagName;

            Log.Message($"Locate an element by tagName {_selector} with parent {parentElement.Stringify()}");

            var by = isDirectChild ? By.XPath(tagName.ToXPathTagName(true)) : By.TagName(_selector);

            foreach (var element in parentElement.InternalElement.FindElements(by))
            {
                elements.Add(new Element(element, Log, _selector, _locatorStrategy));
            }

            return CheckServerError(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by the specified tag name.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of xthe element to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByTagName(HtmlTextWriterTag tagName)
        {
            var elements = new List<IElement>();

            Log.Message($"Locate elements by tagName {tagName}");

            foreach (var element in Browser.Driver.FindElements(By.TagName(tagName.ToString())))
            {
                elements.Add(new Element(element, Log, tagName.ToString(), LocatorStrategy.TagName));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements contained in the parent element by the specified tag name.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByTagName(HtmlTextWriterTag tagName, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            Log.Message($"Locate elements by tagName {tagName} with parent {parentElement.Stringify()}");

            var elementsList = isDirectChild ?
                parentElement.FindElements(By.XPath(tagName.ToXPathTagName(true))) :
                parentElement.FindElements(By.TagName(tagName.ToString()));

            foreach (var element in elementsList)
            {
                elements.Add(new Element(element.InternalElement, Log, tagName.ToString(), LocatorStrategy.TagName));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by the specified name attribute value. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="name">The name attribute value of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByName(string name)
        {
            var elements = new List<IElement>();
            _selector = name;
            _locatorStrategy = LocatorStrategy.Name;

            Log.Message($"Locate an element by attribute name {_selector}");

            elements.Add(new Element(Browser.Driver.FindElement(By.Name(_selector)), Log, _selector, _locatorStrategy));

            return CheckServerError(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element contained in the parent element by the specified name attribute value. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="name">The name attribute value of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByName(string name, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            Log.Message($"Locate an element by attribute name {name} with parent {parentElement.Stringify()}");

            var by = isDirectChild ? By.XPath(name.ToXPathNameAttribute(true)) : By.Name(name);

            return CheckServerErrorForElement(parentElement.FindElements(by));
        }

        /// <summary>
        /// Locate the elements by the specified name attribute.
        /// </summary>
        /// <param name="name">The name attribute of the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByName(string name)
        {
            var elements = new List<IElement>();

            Log.Message($"Locate elements by attribute name {name}");

            foreach (var element in Browser.Driver.FindElements(By.Name(name)))
            {
                elements.Add(new Element(element, Log, name, LocatorStrategy.Name));
            }

            return new ReadOnlyCollection<IElement>(elements);
        }

        /// <summary>
        /// Locate the elements contained in the parent element by the specified name attribute value.
        /// </summary>
        /// <param name="name">The name attribute value of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate is a direct child of the specified parent element or not.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByName(string name, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            Log.Message($"Locate elements by name attribute {name} with parent {parentElement.Stringify()}");

            var elementsList = isDirectChild ?
                parentElement.FindElements(By.XPath(name.ToXPathNameAttribute(true))) :
                parentElement.FindElements(By.Name(name));

            return CheckServerErrorForElements(elementsList);
        }

        /// <summary>
        /// Locate an element by the specified CSS selector. This should be last option to use when all other methods cannot be used.
        /// Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="selector">The CSS selector of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementBySelector(string selector)
        {
            var elements = new List<IElement>();
            _selector = selector;
            _locatorStrategy = LocatorStrategy.Css;

            Log.Message($"Locate an element by selector {_selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(_selector)))
            {
                elements.Add(new Element(element, Log, _selector, _locatorStrategy));
            }

            return CheckServerError(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by the XPATH selector. This should be last option to use when all other methods cannot be used.
        /// Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="selector">The XPATH selector of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public virtual IElement ElementByXpath(string selector, bool nativeContext = false)
        {
            _selector = selector;
            _locatorStrategy = LocatorStrategy.Css;

            Log.Message($"Locate an element by selector {_selector}");

            //TODO Injected explicit wait to find IWebElement until it is presented, GetDefaultWait made public for access.
            var wait = Browser.Wait.GetDefaultWait(60);
            int tryCounter = 0;
            IElement element = null;
            for (; tryCounter < 2; tryCounter++)
            {
                bool successWait;
                try
                {
                    element = new Element(wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(_selector))), Log, _selector, _locatorStrategy);
                    successWait = true;
                }
                catch
                {
                    Log.Message($"Element {_selector} not located, trying again");
                    successWait = false;
                    if (tryCounter > 0)
                    {
                        throw;
                    }
                }
                if (successWait)
                {
                    break;
                }
            }
            return element;
        }

        /// <summary>
        /// Locate elements by the XPATH selector. 
        /// Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="selector">The XPATH selector of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public ReadOnlyCollection<IElement> ElementsByXpath(string selector)
        {
            _selector = selector;
            _locatorStrategy = LocatorStrategy.Xpath;

            var elements = new List<IElement>();

            Log.Message($"Locate an element by selector {_selector}");


            foreach (var element in Browser.Driver.FindElements(By.XPath(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Xpath));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element contained in the parent element by the specified CSS selector. This should be last option to use when all other methods cannot be used.
        /// Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="selector">The CSS selector of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.  Note: Direct child matching not possible.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementBySelector(string selector, IElement parentElement)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            Log.Message($"Locate an element by selector {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by the specified CSS selector. This should be last option to use when all other methods cannot be used.
        /// </summary>
        /// <param name="selector">The CSS selector of the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsBySelector(string selector)
        {
            var elements = new List<IElement>();

            Log.Message($"Locate elements by selector {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements contained in the parent element by the specified CSS selector. This should be last option to use when all other methods cannot be used.
        /// </summary>
        /// <param name="selector">The CSS selector of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate. Note: Direct child matching not possible.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsBySelector(string selector, IElement parentElement)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            Log.Message($"Locate elements by selector {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by attribute name and value or just by the presence of an attribute name. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate. Optional if AttributeSelectorType.HasAttribute is specified.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByAttribute(AttributeSelectorType attSelectorType, HtmlTextWriterAttribute attributeName, string attributeValue = "")
        {
            var elements = new List<IElement>();
            _selector = GetAttributeSelector(attSelectorType, attributeName, attributeValue);
            _locatorStrategy = LocatorStrategy.Css;

            Log.Message($"Locate an element by attribute {_selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(_selector)))
            {
                elements.Add(new Element(element, Log, _selector, _locatorStrategy));
            }

            return CheckServerError(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by attribute name and value from a list of parent elements. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="parentElements">Parent element list to find the given element by.</param>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate. Optional if AttributeSelectorType.HasAttribute is specified.</param>
        /// <returns>Matching IElement</returns>
        public IElement ElementByAttribute(ReadOnlyCollection<IElement> parentElements, AttributeSelectorType attSelectorType, HtmlTextWriterAttribute attributeName, string attributeValue = "")
        {
            var elements = new List<IWebElement>();
            var elementsList = new List<IElement>();

            var selector = GetAttributeSelector(attSelectorType, attributeName, attributeValue);
            Log.Message($"Locate an element by attribute {selector}");

            if (parentElements.Count > 0)
            {
                foreach (var element in parentElements)
                {
                    elements.AddRange(element.InternalElement.FindElements(By.CssSelector(selector)));
                }

                foreach (var element in elements)
                {
                    elementsList.Add(new Element(element, Log, selector, LocatorStrategy.Css));
                }

                return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elementsList));
            }

            Log.Message("WARNING: an initialized parent element was not provided");

            return new Element();
        }

        /// <summary>
        /// Locate an element by attribute name and value or just by the presence of an attribute name. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate. Optional if AttributeSelectorType.HasAttribute is specified.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByAttribute(AttributeSelectorType attSelectorType, string attributeName, string attributeValue = "", int elementIndex = 0)
        {
            var elements = new List<IElement>();
            _selector = GetAttributeSelector(attSelectorType, attributeName, attributeValue, string.Empty);
            _locatorStrategy = LocatorStrategy.Css;

            Log.Message($"Locate an element by attribute {_selector}");

            var element = Browser.Driver.FindElements(By.CssSelector(_selector))[elementIndex];

            elements.Add(new Element(element, Log, _selector, _locatorStrategy));

            return CheckServerError(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element contained in the parent element by attribute name and value or just by the presence of an attribute name. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate. Specify null or empty if AttributeSelectorType.HasAttribute is specified.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByAttribute(AttributeSelectorType attSelectorType, HtmlTextWriterAttribute attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = attributeName.ToXPathAttribute(attributeValue, attSelectorType, true);
                Log.Message($"Locate a direct child element by attribute {xPath} with parent {parentElement.Stringify()}");

                var elementsByXPath = parentElement.FindElements(By.XPath(xPath));

                return CheckServerErrorForElement(elementsByXPath);
            }

            var selector = GetAttributeSelector(attSelectorType, attributeName, attributeValue);
            Log.Message($"Locate an element by attribute {selector} with parent {parentElement.Stringify()}");

            return parentElement.FindElement(By.CssSelector(selector));
        }

        /// <summary>
        /// Locate an element contained in the parent element by attribute name and value or just by the presence of an attribute name. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate. Specify null or empty if AttributeSelectorType.HasAttribute is specified.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByAttribute(AttributeSelectorType attSelectorType, string attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = attributeName.ToXPathAttribute(attributeValue, attSelectorType, true);
                Log.Message($"Locate a direct child element by attribute {xPath} with parent {parentElement.Stringify()}");

                var elementsByXPath = parentElement.FindElements(By.XPath(xPath));

                return CheckServerErrorForElement(elementsByXPath);

            }

            var selector = GetAttributeSelector(attSelectorType, attributeName, attributeValue, string.Empty);
            Log.Message($"Locate an element by attribute {selector} with parent {parentElement.Stringify()}");

            return parentElement.FindElement(By.CssSelector(selector));
        }

        /// <summary>
        /// Locate the elements by attribute name and value or just by the presence of an attribute name.
        /// </summary>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate. Optional if AttributeSelectorType.HasAttribute is specified.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByAttribute(AttributeSelectorType attSelectorType, HtmlTextWriterAttribute attributeName, string attributeValue = "")
        {
            var elements = new List<IElement>();
            var selector = GetAttributeSelector(attSelectorType, attributeName, attributeValue);

            Log.Message($"Locate elements by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return new ReadOnlyCollection<IElement>(elements);
        }

        /// <summary>
        /// Locate elements by attribute name and value from a list of parent elements.
        /// </summary>
        /// <param name="parentElements">Parent element list to find the given element by.</param>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate. Optional if AttributeSelectorType.HasAttribute is specified.</param>
        /// <returns>Matching IElement</returns>
        public ReadOnlyCollection<IElement> ElementsByAttribute(ReadOnlyCollection<IElement> parentElements, AttributeSelectorType attSelectorType, HtmlTextWriterAttribute attributeName, string attributeValue = "")
        {
            var elements = new List<IWebElement>();
            var elementsList = new List<IElement>();

            var selector = GetAttributeSelector(attSelectorType, attributeName, attributeValue);
            Log.Message($"Locate an element by attribute {selector}");

            Log.Message($"WARNING: Parent element should always be present, otherwise no elements would be returned");
            if (parentElements.Count > 0)
            {
                foreach (var element in parentElements)
                {
                    elements.AddRange(element.InternalElement.FindElements(By.CssSelector(selector)));
                }

                foreach (var element in elements)
                {
                    elementsList.Add(new Element(element, Log, selector, LocatorStrategy.Css));
                }
            }

            return new ReadOnlyCollection<IElement>(elementsList);
        }

        /// <summary>
        /// Locate the elements by attribute name and value or just by the presence of an attribute name.
        /// </summary>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate. Optional if AttributeSelectorType.HasAttribute is specified.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByAttribute(AttributeSelectorType attSelectorType, string attributeName, string attributeValue = "")
        {
            var elements = new List<IElement>();
            var selector = GetAttributeSelector(attSelectorType, attributeName, attributeValue, string.Empty);

            Log.Message($"Locate elements by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return new ReadOnlyCollection<IElement>(elements);
        }

        /// <summary>
        /// Locate the elements contained in the parent element by attribute name and value or just by the presence of an attribute name.
        /// </summary>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate. Specify null or empty if AttributeSelectorType.HasAttribute is specified.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate is a direct child of the specified parent element or not.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByAttribute(AttributeSelectorType attSelectorType, HtmlTextWriterAttribute attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = attributeName.ToXPathAttribute(attributeValue, attSelectorType, true);
                Log.Message($"Locate direct child elements by attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(attSelectorType, attributeName, attributeValue);
            Log.Message($"Locate elements by attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements contained in the parent element by attribute name and value or just by the presence of an attribute name.
        /// </summary>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate. Specify null or empty if AttributeSelectorType.HasAttribute is specified.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate is a direct child of the specified parent element or not.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByAttribute(AttributeSelectorType attSelectorType, string attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = attributeName.ToXPathAttribute(attributeValue, attSelectorType, true);
                Log.Message($"Locate direct child elements by attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(attSelectorType, attributeName, attributeValue, string.Empty);
            Log.Message($"Locate elements by attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by attribute name and exact value ([attr="value"]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByAttributeEquals(HtmlTextWriterAttribute attributeName, string attributeValue)
        {
            var elements = new List<IElement>();
            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributeName, attributeValue);

            Log.Message($"Locate an element by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by attribute name and exact value ([attr="value"]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByAttributeEquals(string attributeName, string attributeValue)
        {
            var elements = new List<IElement>();
            _selector = GetAttributeSelector(AttributeSelectorType.Equals, attributeName, attributeValue, string.Empty);
            _locatorStrategy = LocatorStrategy.Css;

            Log.Message($"Locate an element by attribute {_selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(_selector)))
            {
                elements.Add(new Element(element, Log, _selector, _locatorStrategy));
            }

            return CheckServerError(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by attribute name and exact value ([attr="value"]) contained in the specified parent element. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByAttributeEquals(HtmlTextWriterAttribute attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = attributeName.ToXPathAttribute(attributeValue, AttributeSelectorType.Equals, true);
                Log.Message($"Locate a direct child element by attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributeName, attributeValue);
            Log.Message($"Locate an element by attribute {selector} with parent {parentElement.Stringify()}");

            return parentElement.FindElement(By.CssSelector(selector));
        }

        /// <summary>
        /// Locate an element by attribute name and exact value ([attr="value"]) contained in the specified parent element. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByAttributeEquals(string attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = attributeName.ToXPathAttribute(attributeValue, AttributeSelectorType.Equals, true);
                Log.Message($"Locate a direct child element by attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributeName, attributeValue, string.Empty);
            Log.Message($"Locate an element by attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by attribute name and exact value ([attr="value"]).
        /// </summary>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByAttributeEquals(HtmlTextWriterAttribute attributeName, string attributeValue)
        {
            var elements = new List<IElement>();
            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributeName, attributeValue);

            Log.Message($"Locate elements by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by attribute name and exact value ([attr="value"]).
        /// </summary>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByAttributeEquals(string attributeName, string attributeValue)
        {
            var elements = new List<IElement>();
            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributeName, attributeValue, string.Empty);

            Log.Message($"Locate elements by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by attribute name and exact value ([attr="value"]) contained in the specified parent element.
        /// </summary>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not. Optional, defaults to false.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByAttributeEquals(HtmlTextWriterAttribute attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = attributeName.ToXPathAttribute(attributeValue, AttributeSelectorType.Equals, true);
                Log.Message($"Locate direct child elements by attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributeName, attributeValue);
            Log.Message($"Locate elements by attribute {selector} with parent {parentElement.Stringify()}");

            foreach (var element in CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector))))
            {
                elements.Add(new Element(element.InternalElement, Log, selector, LocatorStrategy.Css));
            }

            return new ReadOnlyCollection<IElement>(elements);
        }

        /// <summary>
        /// Locate the elements by attribute name and exact value ([attr="value"]) contained in the specified parent element.
        /// </summary>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByAttributeEquals(string attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = attributeName.ToXPathAttribute(attributeValue, AttributeSelectorType.Equals, true);
                Log.Message($"Locate direct child elements by attribute {xPath} with parent {parentElement.Stringify()}");

                foreach (var element in CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath))))
                {
                    elements.Add(new Element(element.InternalElement, Log, xPath, LocatorStrategy.Xpath));
                }

                return new ReadOnlyCollection<IElement>(elements);

            }

            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributeName, attributeValue, string.Empty);
            Log.Message($"Locate elements by attribute {selector} with parent {parentElement.Stringify()}");

            foreach (var element in CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector))))
            {
                elements.Add(new Element(element.InternalElement, Log, selector, LocatorStrategy.Css));
            }

            return new ReadOnlyCollection<IElement>(elements);
        }

        /// <summary>
        /// Locate an element by attribute name whose value starts with the specified attribute value ([attr^="value"]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByAttributeStartsWith(HtmlTextWriterAttribute attributeName, string attributeValue)
        {
            var elements = new List<IElement>();
            var selector = GetAttributeSelector(AttributeSelectorType.StartsWith, attributeName, attributeValue);

            Log.Message($"Locate an element by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by attribute name whose value starts with the specified attribute value ([attr^="value"]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByAttributeStartsWith(string attributeName, string attributeValue)
        {
            var elements = new List<IElement>();
            var selector = GetAttributeSelector(AttributeSelectorType.StartsWith, attributeName, attributeValue, string.Empty);

            Log.Message($"Locate an element by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by attribute name whose value starts with the specified attribute value ([attr^="value"]) contained in the specified parent element. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByAttributeStartsWith(HtmlTextWriterAttribute attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = attributeName.ToXPathAttribute(attributeValue, AttributeSelectorType.StartsWith, true);
                Log.Message($"Locate a direct child element by attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.StartsWith, attributeName, attributeValue);
            Log.Message($"Locate an element by attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by attribute name whose value starts with the specified attribute value ([attr^="value"]) contained in the specified parent element. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByAttributeStartsWith(string attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = attributeName.ToXPathAttribute(attributeValue, AttributeSelectorType.StartsWith, true);
                Log.Message($"Locate a direct child element by attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.StartsWith, attributeName, attributeValue, string.Empty);
            Log.Message($"Locate an element by attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by attribute name whose value starts with the specified attribute value ([attr^="value"]).
        /// </summary>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByAttributeStartsWith(HtmlTextWriterAttribute attributeName, string attributeValue)
        {
            var elements = new List<IElement>();
            var selector = GetAttributeSelector(AttributeSelectorType.StartsWith, attributeName, attributeValue);

            Log.Message($"Locate elements by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by attribute name whose value starts with the specified attribute value ([attr^="value"]).
        /// </summary>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByAttributeStartsWith(string attributeName, string attributeValue)
        {
            var elements = new List<IElement>();
            var selector = GetAttributeSelector(AttributeSelectorType.StartsWith, attributeName, attributeValue, string.Empty);

            Log.Message($"Locate elements by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by attribute name whose value starts with the specified attribute value ([attr^="value"]) contained in the specified parent element.
        /// </summary>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByAttributeStartsWith(HtmlTextWriterAttribute attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = attributeName.ToXPathAttribute(attributeValue, AttributeSelectorType.StartsWith, true);
                Log.Message($"Locate a direct child element by attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.StartsWith, attributeName, attributeValue);
            Log.Message($"Locate an element by attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by attribute name whose value starts with the specified attribute value ([attr^="value"]) contained in the specified parent element.
        /// </summary>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByAttributeStartsWith(string attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = attributeName.ToXPathAttribute(attributeValue, AttributeSelectorType.StartsWith, true);
                Log.Message($"Locate a direct child element by attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.StartsWith, attributeName, attributeValue, string.Empty);
            Log.Message($"Locate an element by attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by attribute name whose value ends with the specified attribute value ([attr$="value"]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByAttributeEndsWith(HtmlTextWriterAttribute attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.EndsWith, attributeName, attributeValue);
            Log.Message($"Locate an element by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by attribute name whose value ends with the specified attribute value ([attr$="value"]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByAttributeEndsWith(string attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.EndsWith, attributeName, attributeValue, string.Empty);
            Log.Message($"Locate an element by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by attribute name whose value ends with the specified attribute value ([attr$="value"]) contained in the specified parent element. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByAttributeEndsWith(HtmlTextWriterAttribute attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = attributeName.ToXPathAttribute(attributeValue, AttributeSelectorType.EndsWith, true);
                Log.Message($"Locate a direct child element by attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.EndsWith, attributeName, attributeValue);
            Log.Message($"Locate an element by attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by attribute name whose value ends with the specified attribute value ([attr$="value"]) contained in the specified parent element. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByAttributeEndsWith(string attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = attributeName.ToXPathAttribute(attributeValue, AttributeSelectorType.EndsWith, true);
                Log.Message($"Locate a direct child element by attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.EndsWith, attributeName, attributeValue, string.Empty);
            Log.Message($"Locate an element by attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by attribute name whose value ends with the specified attribute value ([attr$="value"]).
        /// </summary>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByAttributeEndsWith(HtmlTextWriterAttribute attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.EndsWith, attributeName, attributeValue);
            Log.Message($"Locate elements by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by attribute name whose value ends with the specified attribute value ([attr$="value"]).
        /// </summary>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByAttributeEndsWith(string attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.EndsWith, attributeName, attributeValue, string.Empty);
            Log.Message($"Locate elements by attribute {selector}");

            foreach (var element in (Browser.Driver.FindElements(By.CssSelector(selector))))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by attribute name whose value ends with the specified attribute value ([attr$="value"]) contained in the specified parent element.
        /// </summary>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByAttributeEndsWith(HtmlTextWriterAttribute attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = attributeName.ToXPathAttribute(attributeValue, AttributeSelectorType.EndsWith, true);
                Log.Message($"Locate a direct child element by attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.EndsWith, attributeName, attributeValue);
            Log.Message($"Locate an element by attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by attribute name whose value ends with the specified attribute value ([attr$="value"]) contained in the specified parent element.
        /// </summary>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByAttributeEndsWith(string attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = attributeName.ToXPathAttribute(attributeValue, AttributeSelectorType.EndsWith, true);
                Log.Message($"Locate a direct child element by attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.EndsWith, attributeName, attributeValue, string.Empty);
            Log.Message($"Locate an element by attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by attribute name ([attr]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByAttributeName(HtmlTextWriterAttribute attributeName)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName);
            Log.Message($"Locate an element by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by attribute name ([attr]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByAttributeName(string attributeName)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName);
            Log.Message($"Locate an element by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by attribute name contained in the specified parent element. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByAttributeName(HtmlTextWriterAttribute attributeName, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = attributeName.ToXPathAttribute(string.Empty, AttributeSelectorType.HasAttribute, true);
                Log.Message($"Locate a direct child element by attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName);
            Log.Message($"Locate an element by attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by attribute name contained in the specified parent element. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByAttributeName(string attributeName, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return null;
            }

            if (isDirectChild)
            {
                var xPath = attributeName.ToXPathAttribute(string.Empty, AttributeSelectorType.HasAttribute, true);
                Log.Message($"Locate a direct child element by attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName);
            Log.Message($"Locate an element by attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by attribute name ([attr]).
        /// </summary>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByAttributeName(HtmlTextWriterAttribute attributeName)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName);
            Log.Message($"Locate elements by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by attribute name ([attr]).
        /// </summary>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByAttributeName(string attributeName)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName);
            Log.Message($"Locate elements by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by attribute name ([attr]) contained in the specified parent element.
        /// </summary>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByAttributeName(HtmlTextWriterAttribute attributeName, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = attributeName.ToXPathAttribute(string.Empty, AttributeSelectorType.HasAttribute, true);
                Log.Message($"Locate a direct child element by attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName);
            Log.Message($"Locate an element by attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by attribute name ([attr]) contained in the specified parent element.
        /// </summary>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByAttributeName(string attributeName, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = attributeName.ToXPathAttribute(string.Empty, AttributeSelectorType.HasAttribute, true);
                Log.Message($"Locate a direct child element by attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName);
            Log.Message($"Locate an element by attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by multiple attribute names ([attr1][attr2]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="attributeNames">The HtmlTextWriterAttribute that indicates the attribute names of the element to locate. Pass any number of attribute names as needed.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByMultipleAttributeNames(params HtmlTextWriterAttribute[] attributeNames)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(attributeNames);
            Log.Message($"Locate an element by attribute {selector}");

            foreach (var element in (Browser.Driver.FindElements(By.CssSelector(selector))))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by multiple attribute names ([attr1][attr2]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="attributeNames">The attribute names of the element to locate. Pass any number of attribute names as needed.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByMultipleAttributeNames(params string[] attributeNames)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(attributeNames);
            Log.Message($"Locate an element by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by multiple attribute names ([attr1][attr2]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="attributeNames">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate. Pass any number of attributes as needed.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByMultipleAttributeNames(IElement parentElement, params HtmlTextWriterAttribute[] attributeNames)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            var selector = GetAttributeSelector(attributeNames);
            Log.Message($"Locate an element by attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by multiple attribute names ([attr1][attr2]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="attributeNames">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate. Pass any number of attributes as needed.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByMultipleAttributeNames(IElement parentElement, params string[] attributeNames)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            var selector = GetAttributeSelector(attributeNames);
            Log.Message($"Locate an element by attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by multiple attribute names ([attr1][attr2]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not.</param>
        /// <param name="attributeNames">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate. Pass any number of attributes as needed.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByMultipleAttributeNames(IElement parentElement, bool isDirectChild = false, params string[] attributeNames)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = attributeNames.ToXPathAttributeNames(true);
                Log.Message($"Locate a direct child element by attribute {xPath} with parent {parentElement.Stringify()}");

                return (CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath))));
            }

            var selector = GetAttributeSelector(attributeNames);
            Log.Message($"Locate an element by attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by multiple attribute names ([attr1][attr2]).
        /// </summary>
        /// <param name="attributeNames">The HtmlTextWriterAttribute that indicates the attribute names of the elements to locate. Pass any number of attribute names as needed.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByMultipleAttributeNames(params HtmlTextWriterAttribute[] attributeNames)
        {
            var elements = new List<IElement>();
            var selector = GetAttributeSelector(attributeNames);

            Log.Message($"Locate elements by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by multiple attribute names ([attr1][attr2]).
        /// </summary>
        /// <param name="attributeNames">The attribute names of the elements to locate. Pass any number of attribute names as needed.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByMultipleAttributeNames(params string[] attributeNames)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(attributeNames);
            Log.Message($"Locate elements by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by multiple attribute names ([attr1][attr2]) with the specified parent element.
        /// </summary>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="attributeNames">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate. Pass any number of attributes as needed.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByMultipleAttributeNames(IElement parentElement, params HtmlTextWriterAttribute[] attributeNames)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            var selector = GetAttributeSelector(attributeNames);
            Log.Message($"Locate elements by attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by multiple attribute names ([attr1][attr2]) with the specified parent element.
        /// </summary>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="attributeNames">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate. Pass any number of attributes as needed.</param>
        /// <returns>Matching IElement.</returns>
        public ReadOnlyCollection<IElement> ElementsByMultipleAttributeNames(IElement parentElement, params string[] attributeNames)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            var selector = GetAttributeSelector(attributeNames);
            Log.Message($"Locate elements by attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by multiple attribute names and exact values ([attr1="value"][attr2="value"]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="attributes">The attribute name with type HtmlTextWriterAttribute and attribute value key value pairs of the element to locate. Pass any number of attributes as needed.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByMultipleAttributesEquals(params KeyValuePair<HtmlTextWriterAttribute, string>[] attributes)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributes);
            Log.Message($"Locate an element by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by multiple attribute names and exact values ([attr1="value"][attr2="value"]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="attributes">The attribute name and attribute value key value pairs of the element to locate. Pass any number of attributes as needed.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByMultipleAttributesEquals(params KeyValuePair<string, string>[] attributes)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributes);
            Log.Message($"Locate an element by attribute {selector}");

            foreach (var element in (Browser.Driver.FindElements(By.CssSelector(selector))))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by multiple attribute names and exact values ([attr1="value"][attr2="value"]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not. Optional, defaults to false.</param>
        /// <param name="attributes">The attribute name with type HtmlTextWriterAttribute and attribute value key value pairs of the element to locate. Pass any number of attributes as needed.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByMultipleAttributesEquals(IElement parentElement, bool isDirectChild = false, params KeyValuePair<HtmlTextWriterAttribute, string>[] attributes)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = attributes.ToXPathAttributes(true);
                Log.Message($"Locate a direct child element by attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributes);
            Log.Message($"Locate an element by attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by multiple attribute names and exact values ([attr1="value"][attr2="value"]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not. Optional, defaults to false.</param>
        /// <param name="attributes">The attribute name and attribute value key value pairs of the element to locate. Pass any number of attributes as needed.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByMultipleAttributesEquals(IElement parentElement, bool isDirectChild = false, params KeyValuePair<string, string>[] attributes)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = attributes.ToXPathAttributes(true);
                Log.Message($"Locate a direct child element by attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributes);
            Log.Message($"Locate an element by attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by multiple attribute names and exact values ([attr1="value"][attr2="value"]).
        /// </summary>
        /// <param name="attributes">The attribute name with type HtmlTextWriterAttribute and attribute value key value pairs of the elements to locate. Pass any number of attributes as needed.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByMultipleAttributesEquals(params KeyValuePair<HtmlTextWriterAttribute, string>[] attributes)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributes);
            Log.Message($"Locate elements by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by multiple attribute names and exact values ([attr1="value"][attr2="value"]).
        /// </summary>
        /// <param name="attributes">The attribute name and attribute value key value pairs of the elements to locate. Pass any number of attributes as needed.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByMultipleAttributesEquals(params KeyValuePair<string, string>[] attributes)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributes);
            Log.Message($"Locate elements by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by multiple attribute names and exact values ([attr1="value"][attr2="value"]).
        /// </summary>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not. Optional, defaults to false.</param>
        /// <param name="attributes">The attribute name with type HtmlTextWriterAttribute and attribute value key value pairs of the elements to locate. Pass any number of attributes as needed.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByMultipleAttributesEquals(IElement parentElement, bool isDirectChild = false, params KeyValuePair<HtmlTextWriterAttribute, string>[] attributes)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = attributes.ToXPathAttributes(true);
                Log.Message($"Locate direct child elements by attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributes);
            Log.Message($"Locate elements by attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by multiple attribute names and exact values ([attr1="value"][attr2="value"]).
        /// </summary>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not. Optional, defaults to false.</param>
        /// <param name="attributes">The attribute name and attribute value key value pairs of the elements to locate. Pass any number of attributes as needed.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByMultipleAttributesEquals(IElement parentElement, bool isDirectChild = false, params KeyValuePair<string, string>[] attributes)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = attributes.ToXPathAttributes(true);
                Log.Message($"Locate direct child elements by attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributes);
            Log.Message($"Locate elements by attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by tag name and attribute. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the element to locate.</param>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate. Optional if AttributeSelectorType.HasAttribute is specified.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByTagNameAndAttribute(HtmlTextWriterTag tagName, AttributeSelectorType attSelectorType, HtmlTextWriterAttribute attributeName, string attributeValue = "")
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(attSelectorType, attributeName, attributeValue, tagName);
            Log.Message($"Locate an element by tag name and attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by tag name and attribute. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the element to locate.</param>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate. Optional if AttributeSelectorType.HasAttribute is specified.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByTagNameAndAttribute(HtmlTextWriterTag tagName, AttributeSelectorType attSelectorType, string attributeName, string attributeValue = "")
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(attSelectorType, attributeName, attributeValue, tagName);
            Log.Message($"Locate an element by tag name and attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element contained in the parent element by tag name and attribute. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the element to locate.</param>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate. Specify null or empty if AttributeSelectorType.HasAttribute is specified.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByTagNameAndAttribute(HtmlTextWriterTag tagName, AttributeSelectorType attSelectorType, HtmlTextWriterAttribute attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = tagName.ToXPathTagNameAndAttribute(attributeName, attributeValue, attSelectorType, true);
                Log.Message($"Locate a direct child element by tag name and attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(attSelectorType, attributeName, attributeValue, tagName);
            Log.Message($"Locate an element by tag name and attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element contained in the parent element by tag name and attribute. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the element to locate.</param>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate. Specify null or empty if AttributeSelectorType.HasAttribute is specified.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByTagNameAndAttribute(HtmlTextWriterTag tagName, AttributeSelectorType attSelectorType, string attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = tagName.ToXPathTagNameAndAttribute(attributeName, attributeValue, attSelectorType, true);
                Log.Message($"Locate a direct child element by tag name and attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(attSelectorType, attributeName, attributeValue, tagName);
            Log.Message($"Locate an element by tag name and attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by tag name and attribute.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the elements to locate.</param>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate. Optional if AttributeSelectorType.HasAttribute is specified.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByTagNameAndAttribute(HtmlTextWriterTag tagName, AttributeSelectorType attSelectorType, HtmlTextWriterAttribute attributeName, string attributeValue = "")
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(attSelectorType, attributeName, attributeValue, tagName);
            Log.Message($"Locate elements by tag name and attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by tag name and attribute.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the elements to locate.</param>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate. Optional if AttributeSelectorType.HasAttribute is specified.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByTagNameAndAttribute(HtmlTextWriterTag tagName, AttributeSelectorType attSelectorType, string attributeName, string attributeValue = "")
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(attSelectorType, attributeName, attributeValue, tagName);
            Log.Message($"Locate elements by tag name and attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements contained in the parent element by tag name and attribute.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the elements to locate.</param>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate. Specify null or empty if AttributeSelectorType.HasAttribute is specified.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByTagNameAndAttribute(HtmlTextWriterTag tagName, AttributeSelectorType attSelectorType, HtmlTextWriterAttribute attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = tagName.ToXPathTagNameAndAttribute(attributeName, attributeValue, attSelectorType, true);
                Log.Message($"Locate direct child elements by tag name and attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(attSelectorType, attributeName, attributeValue, tagName);
            Log.Message($"Locate elements by tag name and attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements contained in the parent element by tag name and attribute.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the elements to locate.</param>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate. Specify null or empty if AttributeSelectorType.HasAttribute is specified.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByTagNameAndAttribute(HtmlTextWriterTag tagName, AttributeSelectorType attSelectorType, string attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = tagName.ToXPathTagNameAndAttribute(attributeName, attributeValue, attSelectorType, true);
                Log.Message($"Locate direct child elements by tag name and attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(attSelectorType, attributeName, attributeValue, tagName);
            Log.Message($"Locate elements by tag name and attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by tag name and attribute name whose value exactly equals the specified attribute value (tagName[attr="value"]).
        /// Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the element to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByTagNameAndAttributeEquals(HtmlTextWriterTag tagName, HtmlTextWriterAttribute attributeName, string attributeValue)
        {
            var elements = new List<IElement>();
            _selector = GetAttributeSelector(AttributeSelectorType.Equals, attributeName, attributeValue, tagName);
            _locatorStrategy = LocatorStrategy.Css;

            Log.Message($"Locate an element by tag name and attribute {_selector}");

            foreach (var element in (Browser.Driver.FindElements(By.CssSelector(_selector))))
            {
                elements.Add(new Element(element, Log, _selector, _locatorStrategy));
            }

            return CheckServerError(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by tag name and attribute name whose value exactly equals the specified attribute value (tagName[attr="value"]).
        /// Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the element to locate.</param>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByTagNameAndAttributeEquals(HtmlTextWriterTag tagName, string attributeName, string attributeValue)
        {
            var elements = new List<IElement>();
            _selector = GetAttributeSelector(AttributeSelectorType.Equals, attributeName, attributeValue, tagName);
            _locatorStrategy = LocatorStrategy.Css;

            Log.Message($"Locate an element by tag name and attribute {_selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(_selector)))
            {
                elements.Add(new Element(element, Log, _selector, _locatorStrategy));
            }

            return CheckServerError(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by tag name and attribute name whose value exactly equals the specified attribute value (tagName[attr="value"]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the element to locate.</param>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The HtmlTextWriterAttribute that indicates the attribute value of the specified attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByTagNameAndAttributeEquals(HtmlTextWriterTag tagName, HtmlTextWriterAttribute attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = tagName.ToXPathTagNameAndAttribute(attributeName, attributeValue, AttributeSelectorType.Equals, true);
                Log.Message($"Locate a direct child element by tag name and attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributeName, attributeValue, tagName);
            Log.Message($"Locate an element by tag name and attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by tag name and attribute name whose value exactly equals the specified attribute value (tagName[attr="value"]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the element to locate.</param>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByTagNameAndAttributeEquals(HtmlTextWriterTag tagName, string attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return parentElement;
            }

            _selector = isDirectChild
                ? tagName.ToXPathTagNameAndAttribute(attributeName, attributeValue, AttributeSelectorType.Equals, true)
                : GetAttributeSelector(AttributeSelectorType.Equals, attributeName, attributeValue, tagName);

            _locatorStrategy = isDirectChild ? LocatorStrategy.Xpath : LocatorStrategy.Css;

            var by = isDirectChild ? By.XPath(_selector) : By.CssSelector(_selector);

            foreach (var element in parentElement.InternalElement.FindElements(by))
            {
                elements.Add(new Element(element, Log, _selector, _locatorStrategy));
            }

            Log.Message(isDirectChild
                ? $"Locate a direct child element by tag name and attribute {_selector} with parent {parentElement.Stringify()}"
                : $"Locate an element by tag name and attribute {_selector} with parent {parentElement.Stringify()}");

            return CheckServerError(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by tag name and attribute name whose value exactly equals the specified attribute value (tagName[attr="value"]).
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the elements to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute value of the attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByTagNameAndAttributeEquals(HtmlTextWriterTag tagName, HtmlTextWriterAttribute attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributeName, attributeValue, tagName);
            Log.Message($"Locate elements by tag name and attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by tag name and attribute name whose value exactly equals the specified attribute value (tagName[attr="value"]).
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the elements to locate.</param>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByTagNameAndAttributeEquals(HtmlTextWriterTag tagName, string attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributeName, attributeValue, tagName);
            Log.Message($"Locate elements by tag name and attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by tag name and attribute name whose value exactly equals the specified attribute value (tagName[attr="value"]).
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the elements to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByTagNameAndAttributeEquals(HtmlTextWriterTag tagName, HtmlTextWriterAttribute attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = tagName.ToXPathTagNameAndAttribute(attributeName, attributeValue, AttributeSelectorType.Equals, true);
                Log.Message($"Locate direct child elements by tag name and attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributeName, attributeValue, tagName);
            Log.Message($"Locate elements by tag name and attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by tag name and attribute name whose value exactly equals the specified attribute value (tagName[attr="value"]).
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the elements to locate.</param>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByTagNameAndAttributeEquals(HtmlTextWriterTag tagName, string attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = tagName.ToXPathTagNameAndAttribute(attributeName, attributeValue, AttributeSelectorType.Equals, true);
                Log.Message($"Locate direct child elements by tag name and attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributeName, attributeValue, tagName);
            Log.Message($"Locate elements by tag name and attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by tag name and attribute name whose value starts with the specified attribute value (tagName[attr^="value"]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the element to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag tagName, HtmlTextWriterAttribute attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.StartsWith, attributeName, attributeValue, tagName);
            Log.Message($"Locate an element by tag name and attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by tag name and attribute name whose value starts with the specified attribute value (tagName[attr^="value"]). Returns null if not found and no exception is thrown.
        /// </summary>a
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the element to locate.</param>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag tagName, string attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.StartsWith, attributeName, attributeValue, tagName);
            Log.Message($"Locate an element by tag name and attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by tag name and attribute name whose value starts with the specified attribute value (tagName[attr^="value"]) contained in the specified parent element.
        /// Returns null if not found and no exception is thrown. 
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the element to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag tagName, HtmlTextWriterAttribute attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = tagName.ToXPathTagNameAndAttribute(attributeName, attributeValue, AttributeSelectorType.StartsWith, true);
                Log.Message($"Locate a direct child element by tag name and attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.StartsWith, attributeName, attributeValue, tagName);
            Log.Message($"Locate an element by tag name and attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by tag name and attribute name whose value starts with the specified attribute value (tagName[attr^="value"]) contained in the specified parent element.
        /// Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the element to locate.</param>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag tagName, string attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = tagName.ToXPathTagNameAndAttribute(attributeName, attributeValue, AttributeSelectorType.StartsWith, true);
                Log.Message($"Locate a direct child element by tag name and attribute {xPath} with parent {parentElement.Stringify()}");

                return (CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath))));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.StartsWith, attributeName, attributeValue, tagName);
            Log.Message($"Locate an element by tag name and attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by tag name and attribute name whose value starts with the specified attribute value ([attr^="value"]).
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the elements to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByTagNameAndAttributeStartsWith(HtmlTextWriterTag tagName, HtmlTextWriterAttribute attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.StartsWith, attributeName, attributeValue, tagName);
            Log.Message($"Locate elements by tag name and attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by tag name and attribute name whose value starts with the specified attribute value ([attr^="value"]).
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the elements to locate.</param>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByTagNameAndAttributeStartsWith(HtmlTextWriterTag tagName, string attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.StartsWith, attributeName, attributeValue, tagName);
            Log.Message($"Locate elements by tag name and attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by tag name and attribute name whose value starts with the specified attribute value ([attr^="value"]) contained in the specified parent element.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the elements to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByTagNameAndAttributeStartsWith(HtmlTextWriterTag tagName, HtmlTextWriterAttribute attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = tagName.ToXPathTagNameAndAttribute(attributeName, attributeValue, AttributeSelectorType.StartsWith, true);
                Log.Message($"Locate direct child elements by tag name and attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.StartsWith, attributeName, attributeValue, tagName);
            Log.Message($"Locate elements by tag name and attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by tag name and attribute name whose value starts with the specified attribute value ([attr^="value"]) contained in the specified parent element.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the elements to locate.</param>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByTagNameAndAttributeStartsWith(HtmlTextWriterTag tagName, string attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = tagName.ToXPathTagNameAndAttribute(attributeName, attributeValue, AttributeSelectorType.StartsWith, true);
                Log.Message($"Locate direct child elements by tag name and attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.StartsWith, attributeName, attributeValue, tagName);
            Log.Message($"Locate elements by tag name and attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by tag name and attribute name whose value ends with the specified attribute value (tagName[attr$="value"]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the element to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByTagNameAndAttributeEndsWith(HtmlTextWriterTag tagName, HtmlTextWriterAttribute attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.EndsWith, attributeName, attributeValue, tagName);
            Log.Message($"Locate an element by tag name and attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by tag name and attribute name whose value ends with the specified attribute value (tagName[attr$="value"]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the element to locate.</param>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByTagNameAndAttributeEndsWith(HtmlTextWriterTag tagName, string attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.EndsWith, attributeName, attributeValue, tagName);
            Log.Message($"Locate an element by tag name and attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by tag name and attribute name whose value ends with the specified attribute value (tagName[attr$="value"]) contained in the specified parent element.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the element to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByTagNameAndAttributeEndsWith(HtmlTextWriterTag tagName, HtmlTextWriterAttribute attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = tagName.ToXPathTagNameAndAttribute(attributeName, attributeValue, AttributeSelectorType.EndsWith, true);
                Log.Message($"Locate a direct child element by tag name and attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.EndsWith, attributeName, attributeValue, tagName);
            Log.Message($"Locate an element by tag name and attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by tag name and attribute name whose value ends with the specified attribute value (tagName[attr$="value"]) contained in the specified parent element.
        /// Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the element to locate.</param>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByTagNameAndAttributeEndsWith(HtmlTextWriterTag tagName, string attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = tagName.ToXPathTagNameAndAttribute(attributeName, attributeValue, AttributeSelectorType.EndsWith, true);
                Log.Message($"Locate a direct child element by tag name and attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.EndsWith, attributeName, attributeValue, tagName);
            Log.Message($"Locate an element by tag name and attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by tag name and attribute name whose value ends with the specified attribute value ([attr$="value"]).
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the elements to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByTagNameAndAttributeEndsWith(HtmlTextWriterTag tagName, HtmlTextWriterAttribute attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.EndsWith, attributeName, attributeValue, tagName);
            Log.Message($"Locate elements by tag name and attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by tag name and attribute name whose value ends with the specified attribute value ([attr$="value"]).
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the elements to locate.</param>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByTagNameAndAttributeEndsWith(HtmlTextWriterTag tagName, string attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.EndsWith, attributeName, attributeValue, tagName);
            Log.Message($"Locate elements by tag name and attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by tag name and attribute name whose value ends with the specified attribute value ([attr$="value"]) contained in the specified parent element.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the elements to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByTagNameAndAttributeEndsWith(HtmlTextWriterTag tagName, HtmlTextWriterAttribute attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = tagName.ToXPathTagNameAndAttribute(attributeName, attributeValue, AttributeSelectorType.EndsWith, true);
                Log.Message($"Locate direct child elements by tag name and attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.EndsWith, attributeName, attributeValue, tagName);
            Log.Message($"Locate elements by tag name and attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by tag name and attribute name whose value ends with the specified attribute value ([attr$="value"]) contained in the specified parent element.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the elements to locate.</param>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByTagNameAndAttributeEndsWith(HtmlTextWriterTag tagName, string attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = tagName.ToXPathTagNameAndAttribute(attributeName, attributeValue, AttributeSelectorType.EndsWith, true);
                Log.Message($"Locate direct child elements by tag name and attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.EndsWith, attributeName, attributeValue, tagName);
            Log.Message($"Locate elements by tag name and attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by tag name and attribute name (tagName[attr]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the element to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByTagNameAndAttributeName(HtmlTextWriterTag tagName, HtmlTextWriterAttribute attributeName)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName, string.Empty, tagName);
            Log.Message($"Locate an element by tag name and attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by tag name and attribute name (tagName[attr]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the element to locate.</param>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByTagNameAndAttributeName(HtmlTextWriterTag tagName, string attributeName)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName, string.Empty, tagName);
            Log.Message($"Locate an element by tag name and attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by tag name and attribute name (tagName[attr]) contained in the specified parent element. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the element to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByTagNameAndAttributeName(HtmlTextWriterTag tagName, HtmlTextWriterAttribute attributeName, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = tagName.ToXPathTagNameAndAttribute(attributeName, string.Empty, AttributeSelectorType.HasAttribute, true);
                Log.Message($"Locate a direct child element by tag name and attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName, string.Empty, tagName);
            Log.Message($"Locate an element by tag name and attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by tag name and attribute name (tagName[attr]) contained in the specified parent element. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the element to locate.</param>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByTagNameAndAttributeName(HtmlTextWriterTag tagName, string attributeName, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = tagName.ToXPathTagNameAndAttribute(attributeName, string.Empty, AttributeSelectorType.HasAttribute, true);
                Log.Message($"Locate a direct child element by tag name and attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName, string.Empty, tagName);
            Log.Message($"Locate an element by tag name and attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by tag name and attribute name (tagName[attr]).
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the elements to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByTagNameAndAttributeName(HtmlTextWriterTag tagName, HtmlTextWriterAttribute attributeName)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName, string.Empty, tagName);
            Log.Message($"Locate elements by tag name and attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by tag name and attribute name (tagName[attr]).
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the elements to locate.</param>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByTagNameAndAttributeName(HtmlTextWriterTag tagName, string attributeName)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName, string.Empty, tagName);
            Log.Message($"Locate elements by tag name and attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by tag name and attribute name (tagName[attr]) contained in the specified parent element.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the elements to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByTagNameAndAttributeName(HtmlTextWriterTag tagName, HtmlTextWriterAttribute attributeName, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = tagName.ToXPathTagNameAndAttribute(attributeName, string.Empty, AttributeSelectorType.HasAttribute, true);
                Log.Message($"Locate a direct child element by tag name and attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName, string.Empty, tagName);
            Log.Message($"Locate an element by tag name and attribute {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by tag name and attribute name (tagName[attr]) contained in the specified parent element.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the elements to locate.</param>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByTagNameAndAttributeName(HtmlTextWriterTag tagName, string attributeName, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = tagName.ToXPathTagNameAndAttribute(attributeName, string.Empty, AttributeSelectorType.HasAttribute, true);
                Log.Message($"Locate a direct child element by tag name and attribute {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName, string.Empty, tagName);
            Log.Message($"Locate an element by tag name and attribute {selector} with parent {parentElement.Stringify()}");

            foreach (var element in CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector))))
            {
                elements.Add(new Element(element.InternalElement, Log, selector, LocatorStrategy.Css));
            }

            return new ReadOnlyCollection<IElement>(elements);
        }

        /// <summary>
        /// Locate an element by tag name and class name. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the element to locate.</param>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByTagNameAndClassName(HtmlTextWriterTag tagName, string className)
        {
            var elements = new List<IElement>();
            _selector = $"{tagName}.{className}";
            _locatorStrategy = LocatorStrategy.Css;

            Log.Message($"Locate an element by tag name and class name {_selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(_selector)))
            {
                elements.Add(new Element(element, Log, _selector, _locatorStrategy));
            }

            return CheckServerError(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by tag name and class name with the specified parent element. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the element to locate.</param>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not. Optional, defaults to false.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByTagNameAndClassName(HtmlTextWriterTag tagName, string className, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = tagName.ToXPathTagNameAndAttribute(HtmlTextWriterAttribute.Class, className, AttributeSelectorType.ContainsWord, true);
                Log.Message($"Locate a direct child element by tag name and class name {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = $"{tagName}.{className}";
            Log.Message($"Locate an element by tag name and class name {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by tag name and class name.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the elements to locate.</param>
        /// <param name="className">The CSS class n ame of the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByTagNameAndClassName(HtmlTextWriterTag tagName, string className)
        {
            var elements = new List<IElement>();

            var selector = $"{tagName}.{className}";
            Log.Message($"Locate elements by tag name and class name {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by tag name and class name with the specified parent element.
        /// </summary>
        /// <param name="tagName">The HtmlTextWriterTag that indicates the tag name of the elements to locate.</param>
        /// <param name="className">The CSS class name of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate is a direct child of the specified parent element or not.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByTagNameAndClassName(HtmlTextWriterTag tagName, string className, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = tagName.ToXPathTagNameAndAttribute(HtmlTextWriterAttribute.Class, className, AttributeSelectorType.ContainsWord, true);
                Log.Message($"Locate direct child elements by tag name and class name {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = $"{tagName}.{className}";
            Log.Message($"Locate elements by tag name and class name {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by class name and attribute. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate. Optional if AttributeSelectorType.HasAttribute is specified.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByClassNameAndAttribute(string className, AttributeSelectorType attSelectorType, HtmlTextWriterAttribute attributeName, string attributeValue = "")
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(attSelectorType, attributeName.ToString(), attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate an element by class name and attribute name {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by class name and attribute. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate. Optional if AttributeSelectorType.HasAttribute is specified.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByClassNameAndAttribute(string className, AttributeSelectorType attSelectorType, string attributeName, string attributeValue = "")
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(attSelectorType, attributeName, attributeValue, className);
            Log.Message($"Locate an element by class name and attribute name {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by class name and attribute contained in the parent element. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate. Specify null or empty if AttributeSelectorType.HasAttribute is specified.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByClassNameAndAttribute(string className, AttributeSelectorType attSelectorType, HtmlTextWriterAttribute attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = className.ToXPathClassNameAndAttribute(attributeName, attributeValue, attSelectorType, true);
                Log.Message($"Locate a direct child element by class name and attribute name {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(attSelectorType, attributeName.ToString(), attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate an element by class name name and attribute name {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by class name and attribute contained in the parent element. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate. Specify null or empty if AttributeSelectorType.HasAttribute is specified.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByClassNameAndAttribute(string className, AttributeSelectorType attSelectorType, string attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = className.ToXPathClassNameAndAttribute(attributeName, attributeValue, attSelectorType, true);
                Log.Message($"Locate a direct child element by class name and attribute name {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(attSelectorType, attributeName, attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate an element by class name name and attribute name {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by class name and attribute.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate. Optional if AttributeSelectorType.HasAttribute is specified.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByClassNameAndAttribute(string className, AttributeSelectorType attSelectorType, HtmlTextWriterAttribute attributeName, string attributeValue = "")
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(attSelectorType, attributeName.ToString(), attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate elements by class name and attribute name {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by class name and attribute.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate. Optional if AttributeSelectorType.HasAttribute is specified.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByClassNameAndAttribute(string className, AttributeSelectorType attSelectorType, string attributeName, string attributeValue = "")
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(attSelectorType, attributeName, attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate elements by class name and attribute name {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by class name and attribute contained in the parent element.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate. Specify null or empty if AttributeSelectorType.HasAttribute is specified.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByClassNameAndAttribute(string className, AttributeSelectorType attSelectorType, HtmlTextWriterAttribute attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = className.ToXPathClassNameAndAttribute(attributeName, attributeValue, attSelectorType, true);
                Log.Message($"Locate a direct child element by class name and attribute name {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(attSelectorType, attributeName.ToString(), attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate an element by class name name and attribute name {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by class name and attribute contained in the parent element.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attSelectorType">The AttributeSelectorType that indicates how the specified attribute should be matched.</param>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate. Specify null or empty if AttributeSelectorType.HasAttribute is specified.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByClassNameAndAttribute(string className, AttributeSelectorType attSelectorType, string attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = className.ToXPathClassNameAndAttribute(attributeName, attributeValue, attSelectorType, true);
                Log.Message($"Locate a direct child element by class name and attribute name {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(attSelectorType, attributeName, attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate an element by class name name and attribute name {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by class name and attribute name whose value exactly equals the specified attribute value (.className[attr="value"]).
        /// Returns null if not found and no exception is thrown. 
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByClassNameAndAttributeEquals(string className, HtmlTextWriterAttribute attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributeName.ToString(), attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate an element by class name and attribute name {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by class name and attribute name whose value exactly equals the specified attribute value (.className[attr="value"]).
        /// Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByClassNameAndAttributeEquals(string className, string attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributeName, attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate an element by class name and attribute name {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by class name and attribute name whose value exactly equals the specified attribute value (.className[attr="value"]) contained in the specified parent element.
        /// Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByClassNameAndAttributeEquals(string className, HtmlTextWriterAttribute attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = className.ToXPathClassNameAndAttribute(attributeName, attributeValue, AttributeSelectorType.Equals, true);
                Log.Message($"Locate a direct child element by class name and attribute name {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributeName.ToString(), attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate an element by class name and attribute name {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by class name and attribute name whose value exactly equals the specified attribute value (.className[attr="value"]) contained in the specified parent element.
        /// Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByClassNameAndAttributeEquals(string className, string attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = className.ToXPathClassNameAndAttribute(attributeName, attributeValue, AttributeSelectorType.Equals, true);
                Log.Message($"Locate a direct child element by class name and attribute name {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributeName, attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate an element by class name and attribute name {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by class name and attribute name whose value exactly equals the specified attribute value (.className[attr="value"]).
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public ReadOnlyCollection<IElement> ElementsByClassNameAndAttributeEquals(string className, HtmlTextWriterAttribute attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributeName.ToString(), attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate elements by class name and attribute name {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by class name and attribute name whose value exactly equals the specified attribute value (.className[attr="value"]).
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public ReadOnlyCollection<IElement> ElementsByClassNameAndAttributeEquals(string className, string attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributeName, attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate elements by class name and attribute name {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by class name and attribute name whose value exactly equals the specified attribute value (.className[attr="value"]) contained in the specified parent element.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not.</param>
        /// <returns>Matching IElement.</returns>
        public ReadOnlyCollection<IElement> ElementsByClassNameAndAttributeEquals(string className, HtmlTextWriterAttribute attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = className.ToXPathClassNameAndAttribute(attributeName, attributeValue, AttributeSelectorType.Equals, true);
                Log.Message($"Locate direct child elements by class name and attribute name {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributeName.ToString(), attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate elements by class name and attribute name {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by class name and attribute name whose value exactly equals the specified attribute value (.className[attr="value"]) contained in the specified parent element.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not.</param>
        /// <returns>Matching IElement.</returns>
        public ReadOnlyCollection<IElement> ElementsByClassNameAndAttributeEquals(string className, string attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = className.ToXPathClassNameAndAttribute(attributeName, attributeValue, AttributeSelectorType.Equals, true);
                Log.Message($"Locate direct child elements by class name and attribute name {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.Equals, attributeName, attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate elements by class name and attribute name {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by class name and attribute name whose value starts with the specified attribute value (.className[attr^="value"]).
        /// Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByClassNameAndAttributeStartsWith(string className, HtmlTextWriterAttribute attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.StartsWith, attributeName.ToString(), attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate an element by class name and attribute name {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by class name and attribute name whose value starts with the specified attribute value (.className[attr^="value"]).
        /// Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByClassNameAndAttributeStartsWith(string className, string attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.StartsWith, attributeName, attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate an element by class name and attribute name {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by class name and attribute name whose value starts with the specified attribute value (.className[attr^="value"]).
        /// Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild"></param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByClassNameAndAttributeStartsWith(string className, HtmlTextWriterAttribute attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            var selector = GetAttributeSelector(AttributeSelectorType.StartsWith, attributeName.ToString(), attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate an element by class name attribute name {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by class name and attribute name whose value starts with the specified attribute value (.className[attr^="value"]).
        /// Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByClassNameAndAttributeStartsWith(string className, string attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = className.ToXPathClassNameAndAttribute(attributeName, attributeValue, AttributeSelectorType.StartsWith, true);
                Log.Message($"Locate a direct child element by class name and attribute name {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.StartsWith, attributeName, attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate an element by class name and attribute name {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by class name and attribute name whose value starts with the specified attribute value (.className[attr^="value"]).
        /// </summary>
        /// <param name="className">The CSS class name of the elements to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByClassNameAndAttributeStartsWith(string className, HtmlTextWriterAttribute attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.StartsWith, attributeName.ToString(), attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate elements by class name and attribute name {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements)); 
        }

        /// <summary>
        /// Locate the elements by class name and attribute name whose value starts with the specified attribute value (.className[attr^="value"]).
        /// </summary>
        /// <param name="className">The CSS class name of the elements to locate.</param>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByClassNameAndAttributeStartsWith(string className, string attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.StartsWith, attributeName, attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate elements by class name and attribute name {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by class name and attribute name whose value starts with the specified attribute value (.className[attr^="value"]) contained in the specified parent element.
        /// </summary>
        /// <param name="className">The CSS class name of the elements to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByClassNameAndAttributeStartsWith(string className, HtmlTextWriterAttribute attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = className.ToXPathClassNameAndAttribute(attributeName, attributeValue, AttributeSelectorType.StartsWith, true);
                Log.Message($"Locate direct child elements by class name and attribute name {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.StartsWith, attributeName.ToString(), attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate elements by class name and attribute name {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by class name and attribute name whose value starts with the specified attribute value (.className[attr^="value"]) contained in the specified parent element.
        /// </summary>
        /// <param name="className">The CSS class name of the elements to locate.</param>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByClassNameAndAttributeStartsWith(string className, string attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = className.ToXPathClassNameAndAttribute(attributeName, attributeValue, AttributeSelectorType.StartsWith, true);
                Log.Message($"Locate direct child elements by class name and attribute name {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.StartsWith, attributeName, attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate elements by class name and attribute name {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by class name and attribute name whose value ends with the specified attribute value (.className[attr$="value"]).
        /// Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByClassNameAndAttributeEndsWith(string className, HtmlTextWriterAttribute attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.EndsWith, attributeName.ToString(), attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate an element by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by class name and attribute name whose value ends with the specified attribute value (.className[attr$="value"]).
        /// Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByClassNameAndAttributeEndsWith(string className, string attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.EndsWith, attributeName, attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate an element by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by class name and attribute name whose value ends with the specified attribute value (.className[attr$="value"]).
        /// Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByClassNameAndAttributeEndsWith(string className, HtmlTextWriterAttribute attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = className.ToXPathClassNameAndAttribute(attributeName, attributeValue, AttributeSelectorType.EndsWith, true);
                Log.Message($"Locate a direct child element by class name and attribute name {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.EndsWith, attributeName.ToString(), attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate an element by class name and attribute name {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by class name and attribute name whose value ends with the specified attribute value (.className[attr$="value"]).
        /// Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByClassNameAndAttributeEndsWith(string className, string attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = className.ToXPathClassNameAndAttribute(attributeName, attributeValue, AttributeSelectorType.EndsWith, true);
                Log.Message($"Locate a direct child element by class name and attribute name {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.EndsWith, attributeName, attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate an element by class name and attribute name {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by class name and attribute name whose value ends with the specified attribute value (.className[attr$="value"]).
        /// </summary>
        /// <param name="className">The CSS class name of the elements to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByClassNameAndAttributeEndsWith(string className, HtmlTextWriterAttribute attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.EndsWith, attributeName.ToString(), attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate elements by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by class name and attribute name whose value ends with the specified attribute value (.className[attr$="value"]).
        /// </summary>
        /// <param name="className">The CSS class name of the elements to locate.</param>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByClassNameAndAttributeEndsWith(string className, string attributeName, string attributeValue)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.EndsWith, attributeName, attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate elements by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by class name and attribute name whose value ends with the specified attribute value (.className[attr$="value"]) contained in the specified parent element.
        /// </summary>
        /// <param name="className">The CSS class name of the elements to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByClassNameAndAttributeEndsWith(string className, HtmlTextWriterAttribute attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = className.ToXPathClassNameAndAttribute(attributeName, attributeValue, AttributeSelectorType.EndsWith, true);
                Log.Message($"Locate direct child elements by class name and attribute name {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.EndsWith, attributeName.ToString(), attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate elements by class name and attribute name {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by class name and attribute name whose value ends with the specified attribute value (.className[attr$="value"]) contained in the specified parent element.
        /// </summary>
        /// <param name="className">The CSS class name of the elements to locate.</param>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="attributeValue">The attribute value of the specified attribute name of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByClassNameAndAttributeEndsWith(string className, string attributeName, string attributeValue, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = className.ToXPathClassNameAndAttribute(attributeName, attributeValue, AttributeSelectorType.EndsWith, true);
                Log.Message($"Locate direct child elements by class name and attribute name {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.EndsWith, attributeName, attributeValue, className.ToCssClassSelector());
            Log.Message($"Locate elements by class name and attribute name {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by class name and attribute name (.className[attr]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByClassNameAndAttributeName(string className, HtmlTextWriterAttribute attributeName)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName.ToString(), string.Empty, className.ToCssClassSelector());
            Log.Message($"Locate an element by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by class name and attribute name (.className[attr]). Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByClassNameAndAttributeName(string className, string attributeName)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName, string.Empty, className.ToCssClassSelector());
            Log.Message($"Locate an element by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by class name and attribute name (.className[attr]) contained in the specified parent element. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByClassNameAndAttributeName(string className, HtmlTextWriterAttribute attributeName, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = className.ToXPathClassNameAndAttribute(attributeName, string.Empty, AttributeSelectorType.HasAttribute, true);
                Log.Message($"Locate a direct child element by class name and attribute name {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName.ToString(), string.Empty, className.ToCssClassSelector());
            Log.Message($"Locate an element by class name and attribute name {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an element by class name and attribute name (.className[attr]) contained in the specified parent element. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate.</param>
        /// <param name="attributeName">The attribute name of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByClassNameAndAttributeName(string className, string attributeName, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = className.ToXPathClassNameAndAttribute(attributeName, string.Empty, AttributeSelectorType.HasAttribute, true);
                Log.Message($"Locate a direct child element by class name and attribute name {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName, string.Empty, className.ToCssClassSelector());
            Log.Message($"Locate an element by class name and attribute name {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by class name and attribute name (.className[attr]).
        /// </summary>
        /// <param name="className">The CSS class name of the elements to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByClassNameAndAttributeName(string className, HtmlTextWriterAttribute attributeName)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName.ToString(), string.Empty, className.ToCssClassSelector());
            Log.Message($"Locate elements by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by class name and attribute name (.className[attr]).
        /// </summary>
        /// <param name="className">The CSS class name of the elements to locate.</param>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByClassNameAndAttributeName(string className, string attributeName)
        {
            var elements = new List<IElement>();

            var selector = GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName, string.Empty, className.ToCssClassSelector());
            Log.Message($"Locate elements by attribute {selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(selector)))
            {
                elements.Add(new Element(element, Log, selector, LocatorStrategy.Css));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by class name and attribute name (.className[attr]) contained in the specified parent element.
        /// </summary>
        /// <param name="className">The CSS class name of the elements to locate.</param>
        /// <param name="attributeName">The HtmlTextWriterAttribute that indicates the attribute name of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByClassNameAndAttributeName(string className, HtmlTextWriterAttribute attributeName, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = className.ToXPathClassNameAndAttribute(attributeName, string.Empty, AttributeSelectorType.HasAttribute, true);
                Log.Message($"Locate direct child elements by class name and attribute name {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName.ToString(), string.Empty, className.ToCssClassSelector());
            Log.Message($"Locate elements by class name and attribute name {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate the elements by class name and attribute name (.className[attr]) contained in the specified parent element.
        /// </summary>
        /// <param name="className">The CSS class name of the elements to locate.</param>
        /// <param name="attributeName">The attribute name of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not.</param>
        /// <returns>List of matching IElements.</returns>
        public ReadOnlyCollection<IElement> ElementsByClassNameAndAttributeName(string className, string attributeName, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = className.ToXPathClassNameAndAttribute(attributeName, string.Empty, AttributeSelectorType.HasAttribute, true);
                Log.Message($"Locate direct child elements by class name and attribute name {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            var selector = GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName, string.Empty, className.ToCssClassSelector());
            Log.Message($"Locate elements by class name and attribute name {selector} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.CssSelector(selector)));
        }

        /// <summary>
        /// Locate an first element in the given list of elements with the expected text.
        /// </summary>
        /// <param name="elements">Elements to find the given text of.</param>
        /// <param name="comparisonType">What type of text comparison will be performed.</param>
        /// <param name="elementText">Expected text for an element.</param>
        /// <returns></returns>
        public IElement ElementWithText(ReadOnlyCollection<IElement> elements, AttributeSelectorType comparisonType = AttributeSelectorType.Equals, params string[] elementText)
        {
            var elementsList = ElementsWithTextElements(elements, comparisonType, elementText);
            _selector = string.Join(", ", elementText);
            _locatorStrategy = LocatorStrategy.Text;

            return CheckServerError(new ReadOnlyCollection<IElement>(elementsList));
        }

        /// <summary>
        /// Locate a first element in the given list of elements with the expected text..
        /// </summary>
        /// <param name="elements">Elements to find the given text of.</param>
        /// <param name="comparisonType">What type of text comparison will be performed.</param>
        /// <param name="elementText">Expected text for an element.</param>
        /// <returns></returns>
        public ReadOnlyCollection<IElement> ElementsWithText(ReadOnlyCollection<IElement> elements, AttributeSelectorType comparisonType = AttributeSelectorType.Equals, params string[] elementText)
        {
            var elementsList = ElementsWithTextElements(elements, comparisonType, elementText);

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elementsList));
        }

        /// <summary>
        /// Return a list of displayed elements from the given list of elements.
        /// </summary>
        /// <param name="elements">Check the Displayed property of the given list of elements.</param>
        /// <returns></returns>
        public ReadOnlyCollection<IElement> DisplayedElements(ReadOnlyCollection<IElement> elements)
        {
            var elementsList = new List<IElement>();

            foreach (var element in elements)
            {
                if (element.InternalElement.Displayed)
                {
                    elementsList.Add(element);
                }
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elementsList));
        }

        /// <summary>
        /// Return a list of selected elements from the given list of elements.
        /// </summary>
        /// <param name="elements">Check the Selected property of the given list of elements.</param>
        /// <returns></returns>
        public ReadOnlyCollection<IElement> SelectedElements(ReadOnlyCollection<IElement> elements)
        {
            var elementsList = new List<IElement>();

            foreach (var element in elements)
            {
                if (element.InternalElement.Selected)
                {
                    elementsList.Add(element);
                }
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elementsList));
        }

        /// <summary>
        /// Locate an element by link text. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="text">The link text of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByLinkText(string text)
        {
            var elements = new List<IElement>();
            _selector = text;
            _locatorStrategy = LocatorStrategy.Text;

            Log.Message($"Locate an element by link text {_selector}");

            foreach (var element in Browser.Driver.FindElements(By.LinkText(_selector)))
            {
                elements.Add(new Element(element, Log, _selector, _locatorStrategy));
            }

            return CheckServerError(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element contained in the parent element by link text. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="text">The link text of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByLinkText(string text, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return parentElement;
            }

            _selector = isDirectChild ? text.ToXPathText(HtmlTextWriterTag.A.ToString(), true) : text;
            _locatorStrategy = isDirectChild ? LocatorStrategy.Xpath : LocatorStrategy.Text;

            var by = isDirectChild ? By.XPath(_selector) : By.LinkText(text);

            foreach (var element in parentElement.InternalElement.FindElements(by))
            {
                elements.Add(new Element(element, Log, _selector, _locatorStrategy));
            }

            Log.Message(isDirectChild
                ? $"Locate a direct child element by link text {_selector} with parent {parentElement.Stringify()}"
                : $"Locate an element by link text {text} with parent {parentElement.Stringify()}");

            return CheckServerError(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements by link text.
        /// </summary>
        /// <param name="text">The link text of the elements to locate.</param>
        /// <returns>List of matching IElement.</returns>
        public ReadOnlyCollection<IElement> ElementsByLinkText(string text)
        {
            var elements = new List<IElement>();

            Log.Message($"Locate elements by link text {text}");

            foreach (var element in Browser.Driver.FindElements(By.LinkText(text)))
            {
                elements.Add(new Element(element, Log, text, LocatorStrategy.Text));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements contained in the parent element by link text.
        /// </summary>
        /// <param name="text">The link text of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not.</param>
        /// <returns>List of matching IElement.</returns>
        public ReadOnlyCollection<IElement> ElementsByLinkText(string text, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = text.ToXPathText(HtmlTextWriterTag.A.ToString(), true);
                Log.Message($"Locate direct child elements by link text {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            Log.Message($"Locate elements by link text {text} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.LinkText(text)));
        }

        /// <summary>
        /// Locate an element by partial link text. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="text">The partial link text of the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByPartialLinkText(string text)
        {
            var elements = new List<IElement>();

            Log.Message($"Locate elements by partial link text {text}");

            foreach (var element in (Browser.Driver.FindElements(By.PartialLinkText(text))))
            {
                elements.Add(new Element(element, Log, text, LocatorStrategy.PartialText));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element contained in the parent element by partial link text. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="text">The partial link text of the element to locate.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the element to locate is a direct child of the specified parent element or not.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementByPartialLinkText(string text, IElement parentElement, bool isDirectChild = false)
        {
            if (!parentElement.IsInitialized)
            {
                Log.Message("Element not located because specified parent element is null.");

                return new Element();
            }

            if (isDirectChild)
            {
                var xPath = text.ToXPathPartialText(HtmlTextWriterTag.A.ToString(), true);
                Log.Message($"Locate a direct child element by partial link text {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElement(parentElement.FindElements(By.XPath(xPath)));
            }

            Log.Message($"Locate an element by partial link text {text} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElement(parentElement.FindElements(By.PartialLinkText(text)));
        }

        /// <summary>
        /// Locate the elements by partial link text.
        /// </summary>
        /// <param name="text">The partial link text of the elements to locate.</param>
        /// <returns>List of matching IElement.</returns>
        public ReadOnlyCollection<IElement> ElementsByPartialLinkText(string text)
        {
            var elements = new List<IElement>();

            Log.Message($"Locate elements by partial link text {text}");

            foreach (var element in Browser.Driver.FindElements(By.PartialLinkText(text)))
            {
                elements.Add(new Element(element, Log, text, LocatorStrategy.PartialText));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate the elements contained in the parent element by partial link text.
        /// </summary>
        /// <param name="text">The partial link text of the elements to locate.</param>
        /// <param name="parentElement">The parent element containing the elements to locate.</param>
        /// <param name="isDirectChild">The flag to indicate if the elements to locate are direct children of the specified parent element or not.</param>
        /// <returns>List of matching IElement.</returns>
        public ReadOnlyCollection<IElement> ElementsByPartialLinkText(string text, IElement parentElement, bool isDirectChild = false)
        {
            var elements = new List<IElement>();

            if (!parentElement.IsInitialized)
            {
                Log.Message("Elements not located because specified parent element is null.");

                return new ReadOnlyCollection<IElement>(elements);
            }

            if (isDirectChild)
            {
                var xPath = text.ToXPathPartialText(HtmlTextWriterTag.A.ToString(), true);
                Log.Message($"Locate direct child elements by partial link text {xPath} with parent {parentElement.Stringify()}");

                return CheckServerErrorForElements(parentElement.FindElements(By.XPath(xPath)));
            }

            Log.Message($"Locate elements by partial link text {text} with parent {parentElement.Stringify()}");

            return CheckServerErrorForElements(parentElement.FindElements(By.PartialLinkText(text)));
        }

        /// <summary>
        /// Locate the immediate next sibling relative to the specified element.
        /// </summary>
        /// <param name="element">The element to check for next sibling.</param>
        /// <returns>A matching IElement.</returns>
        public IElement NextSiblingElement(IElement element)
        {
            var jsCode = "return arguments[0].nextElementSibling";

            return CheckServerErrorForElement(CreateCollection(new Element((IWebElement)Browser.ExecuteJs(jsCode, element.InternalElement), Log, jsCode, LocatorStrategy.Js)));
        }

        /// <summary>
        /// Locate the immediate previous sibling relative to the specified element.
        /// </summary>
        /// <param name="element">The element to check for previous sibling.</param>
        /// <returns>A matching IElement.</returns>
        public IElement PreviousSiblingElement(IElement element)
        {
            var jsCode = "return arguments[0].previousElementSibling";

            return CheckServerErrorForElement(CreateCollection(new Element((IWebElement)Browser.ExecuteJs(jsCode, element.InternalElement), Log, jsCode, LocatorStrategy.Js)));
        }

        /// <summary>
        /// Get the parent DOM element of a given element.
        /// </summary>
        /// <param name="element">Element to find the parent of.</param>
        /// <returns></returns>
        public IElement ParentElement(IElement element) => CheckServerErrorForElement(element.FindElements(By.XPath("..")));

        /// <summary>
        /// Locate an element immediately by the given valid CSS Selector syntax.
        /// </summary>
        /// <param name="selector">CSS selector string to locate an element by. Class or ID must contain respective selector character.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementImmediately(string selector, IElement parentElement = null)
        {
            IWebElement element;
            _locatorStrategy = LocatorStrategy.Js;

            if (parentElement == null)
            {
                _selector = $"return document.querySelector('{selector}')";
                element = (IWebElement)Browser.ExecuteJs(_selector);
            }
            else
            {
                _selector = $"return arguments[0].querySelector('{selector}')";
                element = (IWebElement)Browser.ExecuteJs(_selector, parentElement);
            }

            return CheckServerError(CreateCollection(new Element(element, Log, _selector, LocatorStrategy.Js)));
        }

        /// <summary>
        /// Is the given element valid at the point of this request?
        /// </summary>
        /// <param name="identifier">Element to check validity on (not null). Class or ID must contain respective selector character.</param>
        /// <param name="parentElement">The parent element containing the element to locate.</param>
        /// <returns>True if the requested element is valid.</returns>
        public bool DoesElementExistImmediately(string selector, IElement parentElement = null)
        {
            var doesElementExist = ElementImmediately(selector, parentElement).IsInitialized;

            return doesElementExist;
        }

        /// <summary>
        /// Is the given element visible in the viewport
        /// </summary>
        /// <param name="element">Element to identify if exists in viewport.</param>
        /// <returns>True if the requested element is in the viewport.</returns>
        public bool IsVisibleInViewport(IElement element)
        {
            var jsCode =
                "var elem = arguments[0], box = elem.getBoundingClientRect(), cx = box.left + box.width / 2, cy = box.top + box.height / 2, e = document.elementFromPoint(cx, cy); for (; e; e = e.parentElement) { if (e === elem) return true; } return false; ";

            return (bool) Browser.ExecuteJs(jsCode, element.InternalElement);
        }

        /// <summary>
        /// Is the given image element visible based on naturalWidth
        /// </summary>
        /// <param name="element">Element to identify if visible based on naturalWidth.</param>
        /// <returns>True if the requested element is visible based on naturalWidth.</returns>
        public bool IsImageVisible(IElement element)
        {
            var jsCode =
                "return arguments[0].complete && typeof arguments[0].naturalWidth != \"undefined\" && arguments[0].naturalWidth > 1";

            return (bool)Browser.ExecuteJs(jsCode, element.InternalElement);
        }

        /// <summary>
        /// Get the number of elements that can be located with the given CSS Selector string.
        /// </summary>
        /// <param name="selector">CSS selector string to locate an element by.</param>
        /// <returns>Number of elements found matching the given CSS Selector string.</returns>
        public int ElementCount(string selector)
        {
            Log.Message($"Get the element count for {selector} element by CSS");

            return ElementsBySelector(selector).Count;
        }

        /// <summary>
        /// Locate elements under the given parent element by the given CSS selector string.
        /// </summary>
        /// <param name="parentElement">Parent element to locate and find information on a child element.</param>
        /// <param name="selector">CSS selector string to locate a sub element of the parent element.</param>
        /// <returns>Matching IElement.</returns>
        public IElement ElementFromAncestor(IElement parentElement, string selector)
        {
            var element = CheckServerErrorForElement(CreateCollection(parentElement.FindElement(By.CssSelector(selector))));

            return CheckServerErrorForElement(CreateCollection(new Element(element.InternalElement, Log, selector, LocatorStrategy.Css)));
        }

        /// <summary>
        /// Locate an element that is an ancestor element of the specified descendant element by id attribute.
        /// </summary>
        /// <param name="descendantElement">Descendant element to use as reference for locating its ancestor element.</param>
        /// <param name="ancestorId">The ID attribute of the ancestor element to locate.</param>
        /// <returns>Matching IElement</returns>
        public IElement AncestorElementById(IElement descendantElement, string ancestorId)
        {
            var jsCode = $"return arguments[0].closest('{ancestorId.ToCssIdSelector()}')";

            return CheckServerErrorForElement(CreateCollection(new Element((IWebElement)Browser.ExecuteJs(jsCode, descendantElement.InternalElement), Log, jsCode, LocatorStrategy.Js)));
        }

        /// <summary>
        /// Locate an element that is an ancestor element of the specified descendant element by CSS class name.
        /// </summary>
        /// <param name="descendantElement">Descendant element to use as reference for locating its ancestor element.</param>
        /// <param name="ancestorClassName">The CSS class name of the ancestor element to locate.</param>
        /// <returns>Matching IElement</returns>
        public IElement AncestorElementByClassName(IElement descendantElement, string ancestorClassName)
        {
            var jsCode = $"return arguments[0].closest('{ancestorClassName.ToCssClassSelector()}')";

            return CheckServerErrorForElement(CreateCollection(new Element((IWebElement)Browser.ExecuteJs(jsCode, descendantElement.InternalElement), Log, jsCode, LocatorStrategy.Js)));
        }

        /// <summary>
        /// Locate an element that is an ancestor element of the specified descendant element by HTML tag name.
        /// </summary>
        /// <param name="descendantElement">Descendant element to use as reference for locating its ancestor element.</param>
        /// <param name="ancestorTagName">The HTML tag name of the ancestor element to locate.</param>
        /// <returns>Matching IElement</returns>
        public IElement AncestorElementByTagName(IElement descendantElement, string ancestorTagName)
        {
            var jsCode = $"return arguments[0].closest('{ancestorTagName}')";

            return CheckServerErrorForElement(CreateCollection(new Element((IWebElement)Browser.ExecuteJs(jsCode, descendantElement.InternalElement), Log, jsCode, LocatorStrategy.Js)));
        }

        /// <summary>
        /// Locate an element that is an ancestor element of the specified descendant element by HTML tag name.
        /// </summary>
        /// <param name="descendantElement">Descendant element to use as reference for locating its ancestor element.</param>
        /// <param name="ancestorTagName">The HTML tag name of the ancestor element to locate.</param>
        /// <returns>Matching IElement</returns>
        public IElement AncestorElementByTagName(IElement descendantElement, HtmlTextWriterTag ancestorTagName)
        {
            var jsCode = $"return arguments[0].closest('{ancestorTagName.ToString()}')";

            return CheckServerErrorForElement(CreateCollection(new Element((IWebElement)Browser.ExecuteJs(jsCode, descendantElement.InternalElement), Log, jsCode, LocatorStrategy.Js)));
        }

        /// <summary>
        /// Locate an element that is an ancestor element of the specified descendant element by CSS selector.
        /// </summary>
        /// <param name="descendantElement">Descendant element to use as reference for locating its ancestor element.</param>
        /// <param name="ancestorSelector">The CSS selector of the ancestor element to locate.</param>
        /// <returns>Matching IElement</returns>
        public IElement AncestorElementBySelector(IElement descendantElement, string ancestorSelector)
        {
            var jsCode = $"return arguments[0].closest('{ancestorSelector}')";

            return CheckServerErrorForElement(CreateCollection(new Element((IWebElement)Browser.ExecuteJs(jsCode, descendantElement.InternalElement), Log, jsCode, LocatorStrategy.Js)));
        }

        /// <summary>
        /// Locate an element by Javascript code
        /// </summary>
        /// <param name="baseElement">Base element to use as reference for locating by Javascript.</param>
        /// <param name="jsCode">The javascript code to locate by.</param>
        /// <returns>Matching IElement</returns>
        public IElement ElementByJavascript(IElement baseElement, string jsCode)
        {
            return CheckServerErrorForElement(CreateCollection(new Element((IWebElement)Browser.ExecuteJs(jsCode, baseElement.InternalElement), Log, jsCode, LocatorStrategy.Js)));
        }

        /// <summary>
        /// Selects an option in a dropdown by matching the option value attribute with the given optionValue.
        /// </summary>
        /// <param name="element">Dropdown element to select an option from</param>
        /// <param name="optionValue">Value of desired option to be selected</param>
        public void ClickDropdownByValue(IElement element, string optionValue)
        {
            element.Click();
            ElementByAttributeEquals(HtmlTextWriterAttribute.Value, optionValue, element).Click();
        }

        private IElement CheckServerErrorForElement(ReadOnlyCollection<IElement> elements)
        {
            if (elements.Count < 1) { Browser.SkipTestIfServerError(); }

            return elements.Count > 0 ? elements[0] : new Element();
        }

        protected IElement CheckServerError(ReadOnlyCollection<IElement> elements)
        {
            if (elements.Count < 1) { Browser.SkipTestIfServerError(); }

            return elements.Count > 0 ? elements[0] : new Element(Log, _selector, _locatorStrategy);
        }

        protected ReadOnlyCollection<IElement> CheckServerErrorForElements(ReadOnlyCollection<IElement> elements)
        {
            if (elements.Count < 1) { Browser.SkipTestIfServerError(); }

            return elements;
        }

        private ReadOnlyCollection<IElement> CreateCollection(IElement element)
        {
            var list = new List<IElement>();

            if (element.IsInitialized) { list.Add(element); }

            return new ReadOnlyCollection<IElement>(list);
        }

        /// <summary>
        /// Locate an element by the given Selenium By syntax. Note: Use this only if no other methods are available.
        /// </summary>
        /// <param name="by">Selenium By object to locate the requested element.</param>
        /// <returns>Located IElement.</returns>
        private IElement Element(By by)
        {
            var elements = new List<IElement>();

            Log.Message($"Locate an element by {by} By statement");

            foreach (var element in Browser.Driver.FindElements(by))
            {
                elements.Add(new Element(element, Log, string.Empty, LocatorStrategy.By));
            }

            return CheckServerErrorForElement(new ReadOnlyCollection<IElement>(elements));
        }

        private ReadOnlyCollection<IElement> ElementsWithTextElements(ReadOnlyCollection<IElement> elements, AttributeSelectorType comparisonType = AttributeSelectorType.Equals, params string[] elementText)
        {
            var returnElementsList = new List<IElement>();
            var elementsList = new List<IWebElement>();
            var list = string.Join(",", elementText);

            foreach (var ele in elements)
            {
                elementsList.Add(ele.InternalElement);
            }

            switch (comparisonType)
            {
                case AttributeSelectorType.Contains:
                {
                    foreach (var param in elementText)
                    {
                        elementsList = elementsList.FindAll(x => x.Text.ToLower().Contains(param.ToLower()));
                    }

                    break;
                }
                case AttributeSelectorType.Equals:
                {
                    if (elementText.Length > 1)
                    {
                        var ignoreList = list.Substring(1); // Get ignored text values.
                        Log.Message($"WARNING: ChildElementWithText will only honor the {elementText[0]}, {ignoreList} values are not used in this comparison");
                    }
                    elementsList = elementsList.FindAll(x => x.Text.ToLower().Equals(elementText[0].ToLower()));

                    break;
                }
                default:
                {
                    Log.Message($"WARNING: {comparisonType} is not currently a supported option.");

                    break;
                }
            }

            foreach (var element in elementsList)
            {
                returnElementsList.Add(new Element(element, Log, list, LocatorStrategy.Text));
            }

            return new ReadOnlyCollection<IElement>(returnElementsList);
        }

        private static string GetAttributeSelector(AttributeSelectorType attSelectorType, HtmlTextWriterAttribute attributeName, string attributeValue = "", HtmlTextWriterTag tagNameSelector = HtmlTextWriterTag.Unknown)
        {
            var elementTagName = tagNameSelector == HtmlTextWriterTag.Unknown ? string.Empty : tagNameSelector.ToString().ToLower();
            return GetAttributeSelector(attSelectorType, attributeName.ToString().ToLower(), attributeValue, elementTagName);
        }

        private static string GetAttributeSelector(AttributeSelectorType attSelectorType, string attributeName, string attributeValue = "", HtmlTextWriterTag tagNameSelector = HtmlTextWriterTag.Unknown)
        {
            var elementTagName = tagNameSelector == HtmlTextWriterTag.Unknown ? string.Empty : tagNameSelector.ToString().ToLower();
            return GetAttributeSelector(attSelectorType, attributeName, attributeValue, elementTagName);
        }

        private static string GetAttributeSelector(params HtmlTextWriterAttribute[] attributeNames)
        {
            var selector = new StringBuilder();
            foreach (var attributeName in attributeNames)
            {
                selector.Append(GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName));
            }

            return selector.ToString();
        }

        private static string GetAttributeSelector(params string[] attributeNames)
        {
            var selector = new StringBuilder();
            foreach (var attributeName in attributeNames)
            {
                selector.Append(GetAttributeSelector(AttributeSelectorType.HasAttribute, attributeName));
            }

            return selector.ToString();
        }

        private static string GetAttributeSelector(AttributeSelectorType attSelectorType, params KeyValuePair<HtmlTextWriterAttribute, string>[] attributes)
        {
            var selector = new StringBuilder();
            foreach (var attribute in attributes)
            {
                selector.Append(GetAttributeSelector(attSelectorType, attribute.Key, attribute.Value));
            }

            return selector.ToString();
        }

        private static string GetAttributeSelector(AttributeSelectorType attSelectorType, params KeyValuePair<string, string>[] attributes)
        {
            var selector = new StringBuilder();
            foreach (var attribute in attributes)
            {
                selector.Append(
                    GetAttributeSelector(
                        string.IsNullOrEmpty(attribute.Value) ? AttributeSelectorType.HasAttribute : attSelectorType,
                        attribute.Key,
                        attribute.Value,
                        string.Empty
                    )
                );
            }

            return selector.ToString();
        }

        private static string GetAttributeSelector(AttributeSelectorType attSelectorType, string attributeName, string attributeValue, string elementSelector = "")
        {
            string selector;

            switch (attSelectorType)
            {
                case AttributeSelectorType.Equals:
                    {
                        selector = $"{elementSelector}[{attributeName.ToLower()}=\"{attributeValue}\"]";
                        break;
                    }

                case AttributeSelectorType.HasAttribute:
                    {
                        selector = $"{elementSelector}[{attributeName.ToLower()}]";
                        break;
                    }

                case AttributeSelectorType.StartsWith:
                    {
                        selector = $"{elementSelector}[{attributeName.ToLower()}^=\"{attributeValue}\"]";
                        break;
                    }

                case AttributeSelectorType.EndsWith:
                    {
                        selector = $"{elementSelector}[{attributeName.ToLower()}$=\"{attributeValue}\"]";
                        break;
                    }

                case AttributeSelectorType.Contains:
                    {
                        selector = $"{elementSelector}[{attributeName.ToLower()}*=\"{attributeValue}\"]";
                        break;
                    }

                case AttributeSelectorType.ContainsPrefix:
                    {
                        selector = $"{elementSelector}[{attributeName.ToLower()}|=\"{attributeValue}\"]";
                        break;
                    }

                case AttributeSelectorType.ContainsWord:
                    {
                        selector = $"{elementSelector}[{attributeName.ToLower()}~=\"{attributeValue}\"]";
                        break;
                    }

                default:
                    {
                        // defaults to Equals
                        selector = $"{elementSelector}[{attributeName}=\"{attributeValue}\"]";
                        break;
                    }
            }

            return selector;
        }
#pragma warning restore CS3001, CS3002
    }
}