using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Automation.Framework;
using Automation.Framework.Core;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages
{
    /// <summary>
    /// Base class for Order History / Order Detail specific tests.
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Integration.PageObjectModel)]
    public class PageObjectTestsBase : TestsBase, IDisposable
    {
        public List<string> ElementList;
        public List<string> ElementsList;
        public List<KeyValuePair<object, string>> NotFoundElements;

        /// <summary>
        /// Test base for Order History / Order Detail.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public PageObjectTestsBase(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Initialize the framework for Page Object integration testing.
        /// </summary>
        /// <param name="config"><see cref="TestConfiguration"/></param>
        /// <param name="url">Optional: Initial URL to navigate to.</param>
        public void InitializeFramework(string config, string url = "")
        {
            ElementList = new List<string>();
            ElementsList = new List<string>();
            NotFoundElements = new List<KeyValuePair<object, string>>();

            base.InitializeFramework(config, url);
        }

        /// <summary>
        /// Build lists for all page object IElement and list of IElements.
        /// Note: This must be ran after <see cref="InitializeFramework"/>
        /// </summary>
        /// <param name="pageUnderTest">Page object to get elements of.</param>
        /// <param name="includeInheritedProps">Flag to indicate if inherited properties from the base class is included or not. Defaults to true.</param>
        public void BuildElementsList(object pageUnderTest, bool includeInheritedProps = true)
        {
            var bindingFlags = includeInheritedProps
                ? BindingFlags.Instance | BindingFlags.Public
                : BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly;
            var props = pageUnderTest.GetType().GetProperties(bindingFlags).Where(x => x.PropertyType == typeof(IElement));
            foreach (var property in props)
            {
                var value = property.Name.Split(Page.SingleSpaceChart);
                ElementList.Add(value[0]);
            }

            props = pageUnderTest.GetType().GetProperties(bindingFlags).Where(x => x.PropertyType == typeof(ReadOnlyCollection<IElement>));

            foreach (var property in props)
            {
                var value = property.Name.Split(Page.SingleSpaceChart);
                ElementsList.Add(value[0]);
            }
        }

        /// <summary>
        /// Verify the first element is displayed.
        /// </summary>
        /// <param name="element">Verify element is displayed on the page..</param>
        public void VerifyElementDisplayed(Expression<Func<IElement>> element)
        {
            var methodInfo = element.Body.ToString().Split('.');
            var propName = methodInfo[methodInfo.Length - 1];

            if (!ElementList.Contains(propName))
            {
                SoftVerify.False(true, $"{propName} found but was not expected in the list of IElement properties for the given page object.");
            }
            else
            {
                SoftVerify.Displayed(element.Compile().Invoke(), $"The expected element \"{propName}\" was not visible.");
                ElementList.Remove(propName);
            }
        }

        /// <summary>
        /// Verify the first element is not displayed.
        /// </summary>
        /// <param name="propName">Verify element is not displayed on the page..</param>
        public void VerifyElementNotDisplayed(string propName)
        {

            if (ElementList.Contains(propName))
            {
                SoftVerify.True(true, $"The expected element \"{propName}\" was visible.");
                ElementList.Remove(propName);
            }
            else
            {
                SoftVerify.True(false, $"{propName} found and is expected in the list of IWebElement properties for the given page object.");
            }
        }

        /// <summary>
        /// Verify the given element throws a NotImplementedException.
        /// </summary>
        /// <param name="element"></param>
        public void VerifyElementNotImplemented(Expression<Func<IElement>> element)
        {
            var methodInfo = element.Body.ToString().Split('.');
            var propName = methodInfo[methodInfo.Length - 1];

            if (!ElementList.Contains(propName))
            {
                SoftVerify.False(true, $"{propName} found but was not expected in the list of IElement properties for the given page object.");
            }
            else
            {
                // The logic seems a bit strange here, but since an error is expected, the first assumption is to remove the prop, if the check fails it will be added back in the catch.
                try
                {
                    ElementList.Remove(propName);
                    SoftVerify.ThrowsNotImplementedException(element);
                }
                catch
                {
                    ElementList.Add(propName);
                }
            }
        }
        
        /// <summary>
        /// Verify the given list of elements throws a NotImplementedException.
        /// </summary>
        /// <param name="element"></param>
        public void VerifyElementNotImplemented(Expression<Func<ReadOnlyCollection<IElement>>> element)
        {
            var methodInfo = element.Body.ToString().Split('.');
            var propName = methodInfo[methodInfo.Length - 1];

            if (!ElementsList.Contains(propName))
            {
                SoftVerify.False(true, $"{propName} found but was not expected in the list of IElement properties for the given page object.");
            }
            else
            {
                // The logic seems a bit strange here, but since an error is expected, the first assumption is to remove the prop, if the check fails it will be added back in the catch.
                try
                {
                    ElementsList.Remove(propName);

                    SoftVerify.ThrowsNotImplementedException(() => element.Compile().Invoke()[0]);
                }
                catch
                {
                    ElementsList.Add(propName);
                }
            }
        }

        /// <summary>
        /// Verify the given list of elements throws a NotImplementedException.
        /// </summary>
        /// <param name="elements"></param>
        public void VerifyElementsNotImplemented(Expression<Func<ReadOnlyCollection<IElement>>> elements)
        {
            var methodInfo = elements.Body.ToString().Split('.');
            var propName = methodInfo[methodInfo.Length - 1];

            if (!ElementsList.Contains(propName))
            {
                SoftVerify.False(true, $"{propName} found but was not expected in the list of IWebElement properties for the given page object.");
            }
            else
            {
                // The logic seems a bit strange here, but since an error is expected, the first assumption is to remove the prop, if the check fails it will be added back in the catch.
                try
                {
                    ElementsList.Remove(propName);

                    SoftVerify.ThrowsNotImplementedException(() => elements.Compile().Invoke());
                }
                catch
                {
                    ElementsList.Add(propName);
                }
            }
        }

        /// <summary>
        /// Verify the first element in the element list is displayed.
        /// </summary>
        /// <param name="element">Verify element is displayed on the page..</param>
        public void VerifyElementDisplayed(Expression<Func<IReadOnlyCollection<IElement>>> element)
        {
            var methodInfo = element.Body.ToString().Split('.');
            var propName = methodInfo[methodInfo.Length - 1];

            if (!ElementsList.Contains(propName)) { SoftVerify.False(true, $"{propName} found but was not expected in the list of List<IElement> properties for the given page object."); }
            else
            {
                SoftVerify.Displayed(element.Compile().Invoke().FirstOrDefault(), $"The expected element \"{propName}\" was not visible.");
                ElementsList.Remove(propName);
            }
        }

        /// <summary>
        /// Verify the first element in the element list exists in the DOM (even if it's hidden or not displayed).
        /// </summary>
        /// <param name="element"></param>
        public void VerifyElementExists(Expression<Func<IReadOnlyCollection<IElement>>> element)
        {
            var methodInfo = element.Body.ToString().Split('.');
            var propName = methodInfo[methodInfo.Length - 1];

            if (!ElementsList.Contains(propName)) { SoftVerify.False(true, $"{propName} found but was not expected in the list of List<IElement> properties for the given page object."); }
            else
            {
                var resultElement = element.Compile().Invoke().FirstOrDefault();
                SoftVerify.True(resultElement != null && resultElement.IsInitialized, $"The expected element \"{propName}\" was not found.");
                ElementsList.Remove(propName);
            }
        }

        /// <summary>
        /// Verify the element exists in the DOM (even if it's hidden or not displayed).
        /// </summary>
        /// <param name="element"></param>
        public void VerifyElementExists(Expression<Func<IElement>> element)
        {
            var methodInfo = element.Body.ToString().Split('.');
            var propName = methodInfo[methodInfo.Length - 1];

            if (!ElementList.Contains(propName)) { SoftVerify.False(true, $"{propName} found but was not expected in the list of IElement properties for the given page object."); }
            else
            {
                var resultElement = element.Compile().Invoke();
                SoftVerify.True(resultElement != null && resultElement.IsInitialized, $"The expected element \"{propName}\" was not found.");
                ElementList.Remove(propName);
            }
        }

        /// <summary>
        /// Construct a URL for a PLA item.
        /// </summary>
        public void GetPlaForHomeLocatorTest()
        {
            var sku = ProductActions.GetPlaSkuWithStarsQAndA();
            var url = Urls.HomePageUrl;

            Browser.Navigate($"{url}/sfp/{sku}");
        }

        /// <summary>
        /// Handle Clicks for mobile devices
        /// </summary>
        /// <param name="element"></param>
        public void HandleClickForMobileElementClose(IElement element)
        {
            Browser.Wait.ForElementToStopAnimating(element);
            element.Click();
            Browser.Wait.ForElementToStopAnimating(element);
        }

        /// <summary>
        /// Handle clicks for desktop modal windows
        /// </summary>
        /// <param name="element"></param>
        public void HandleClickForDesktopModalClose(IElement element)
        {
            element.Click();
            Browser.Wait.UntilElementUnloads(element);
        }

        /// <summary>
        /// Run checks for missing element checks.
        /// </summary>
        public new void Dispose()
        {
            if (ElementList != null && ElementList.Count > 0)
            {
                var list = string.Join(", ", ElementList);

                SoftVerify.False(true, $"The following elements were not covered by one or more page object integration test: {list}");
            }

            if (ElementsList != null && ElementsList.Count > 0)
            {
                var list = string.Join(", ", ElementsList);

                SoftVerify.False(true, $"The following elements were not covered by one or more page object integration test: {list}");
            }

            base.Dispose();
        }
    }
}
