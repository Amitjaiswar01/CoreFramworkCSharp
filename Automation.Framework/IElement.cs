using System.Collections.ObjectModel;
using System.Drawing;

using Automation.Framework.Enums;

using OpenQA.Selenium;

namespace Automation.Framework
{
    /// <summary>
    /// Provide access to information and behavior to automate websites and mobile apps.
    /// </summary>
    public interface IElement
    {
        /// <summary>
        /// Selenium IWebElement used to interact with Selenium.
        /// </summary>
        IWebElement InternalElement { get; }

        /// <summary>
        /// String used to locate the requested element per the selected <see cref="LocatorStrategy"/>
        /// </summary>
        string LocatorString { get; }

        /// <summary>
        /// How an element will be found. This is used in conjunction with <see cref="LocatorString"/>
        /// </summary>
        LocatorStrategy LocatorStrategy { get; }

        /// <summary>
        /// Flag to determine if the element was properly created.
        /// </summary>
        bool IsInitialized { get; }

        #region Selenium Behavior
        bool Displayed { get; }
        bool Enabled { get; }
        Point Location { get; }
        bool Selected { get; }
        Size Size { get; }
        string TagName { get; }
        string Text { get; }

        void Clear();
        void Click();

        IElement FindElement(By by);
        ReadOnlyCollection<IElement> FindElements(By by);

        string GetAttribute(string attributeName);
        string GetCssValue(string propertyName);
        string GetProperty(string propertyName);
        void SendKeys(string text);
        void SendKeys(string text, bool clearFieldText);
        void Submit();
        #endregion
    }
}
