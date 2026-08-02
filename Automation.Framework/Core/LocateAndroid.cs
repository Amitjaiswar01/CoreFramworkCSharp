using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using OpenQA.Selenium;

namespace Automation.Framework.Core
{
    class LocateAndroid : Locate
    {
        /// <summary>
        /// Provides support for finding elements on the Android device screen.
        /// </summary>.
        public LocateAndroid(Browser browser) : base(browser)
        {
        }

        /// <summary>
        /// Locate an element by CSS class name. Returns null if not found and no exception is thrown.
        /// </summary>
        /// <param name="className">The CSS class name of the element to locate excluding the dot (.) character.</param>
        /// <returns>Matching IElement.</returns>
        public override IElement ElementByClassName(string className)
        {
            var elements = new List<IElement>();
            _selector = className;
            _locatorStrategy = LocatorStrategy.Class;

            Log.Message($"Locate an element by className {_selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(className.ToCssClassSelector())))
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
        public override IElement ElementByClassName(string className, IElement parentElement, bool isDirectChild = false)
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


            var byAndroid = isDirectChild ? By.XPath(_selector.ToXPathCssClass(true)) : By.CssSelector(_selector.ToCssClassSelector());

            foreach (var element in parentElement.InternalElement.FindElements(byAndroid))
            {
                elements.Add(new Element(element, Log, _selector, _locatorStrategy));
            }

            return CheckServerError(new ReadOnlyCollection<IElement>(elements));
        }

        /// <summary>
        /// Locate an element by id attribute. Returns an empty Element if not found and no exception is thrown.
        /// </summary>
        /// <param name="id">The id of the element to locate excluding the hash (#) character.</param>
        /// <returns>Matching IElement.</returns>
        public override IElement ElementById(string id)
        {
            var elements = new List<IElement>();
            _selector = id;
            _locatorStrategy = LocatorStrategy.Id;

            Log.Message($"Locate an element by id {_selector}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(_selector.ToCssIdSelector())))
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
        public override ReadOnlyCollection<IElement> ElementsByClassName(string className)
        {
            var elements = new List<IElement>();

            Log.Message($"Locate elements by className {className}");

            foreach (var element in Browser.Driver.FindElements(By.CssSelector(className.ToCssClassSelector())))
            {
                elements.Add(new Element(element, Log, _selector, _locatorStrategy));
            }

            return CheckServerErrorForElements(new ReadOnlyCollection<IElement>(elements));
        }
    }
}
