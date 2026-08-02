using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

using Automation.Framework.Enums;
using Automation.Framework.Utilities;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Automation.Framework.Core
{
    /// <summary>
    /// Wrapper to interact with elements but hide Selenium specific details.
    /// </summary>
    public class Element : IElement
    {
        private Log Log { get; }

        /// <inheritdoc />
        public IWebElement InternalElement { get; }

        /// <inheritdoc />
        public string LocatorString { get; }

        /// <inheritdoc />
        public LocatorStrategy LocatorStrategy { get; }

        /// <inheritdoc />
        public bool IsInitialized { get; }
        
        /// <summary>
        /// Construct an Element object based on the provided <see cref="LocatorString"/> and <see cref="LocatorStrategy"/>.
        /// </summary>
        /// <param name="element">IWebElement to wrap.</param>
        /// <param name="log"><see cref="Utilities.Log"/></param>
        /// <param name="locatorString"><see cref="LocatorString"/></param>
        /// <param name="locatorStrategy"><see cref="LocatorStrategy"/></param>
        public Element(IWebElement element, Log log, string locatorString, LocatorStrategy locatorStrategy)
        {
            InternalElement = element;
            Log = log;

            LocatorString = locatorString;
            LocatorStrategy = locatorStrategy;

            if (element != null)
            {
                IsInitialized = true;
            }
        }

        /// <summary>
        /// This constructor should only be used to return "null" objects.
        /// This will help with graceful handling of null elements.
        /// </summary>
        public Element(Log log, string locatorString, LocatorStrategy locatorStrategy)
        {
            Log = log;
            LocatorString = locatorString;
            LocatorStrategy = locatorStrategy;
        }

        /// <summary>
        /// For null element returns in Locate class that aren't converted to use Element Validity checks.
        /// </summary>
        public Element() { }

        /// <inheritdoc />
        public IElement FindElement(By by)
        {
            try
            {
                return new Element(InternalElement.FindElement(by), Log, LocatorString, LocatorStrategy);
            }
            finally
            {
                LogToValidityLog();
            }
        }

        /// <inheritdoc />
        public ReadOnlyCollection<IElement> FindElements(By by)
        {
            try
            {
                var elements = new List<IElement>();

                foreach (var element in InternalElement.FindElements(by))
                {
                    elements.Add(new Element(element, Log, LocatorString, LocatorStrategy));
                }

                return elements.AsReadOnly();
            }
            finally
            {
                LogToValidityLog();
            }
        }

        /// <inheritdoc cref="IWebElement.Clear" />
        public void Clear()
        {
            try
            {
                InternalElement.Clear();
            }
            finally
            {
                LogToValidityLog();
            }

        }

        /// <inheritdoc cref="IWebElement.SendKeys" />
        public void SendKeys(string text)
        {
                try
                {
                    InternalElement.SendKeys(text);
                }

                finally
                {
                    LogToValidityLog();
                }
        }

        /// <inheritdoc cref="IWebElement.SendKeys" />
        public void SendKeys(string text, bool clearFieldText)
        {
            if (clearFieldText) { Clear(); }

            SendKeys(text);
        }

        /// <inheritdoc cref="IWebElement.Submit" />
        public void Submit()
        {
            try
            {
                InternalElement.Submit();
            }
            finally
            {
                LogToValidityLog();
            }
        }

        /// <inheritdoc cref="IWebElement.Click" />
        public void Click()
        {
            try
            {
                InternalElement.Click();
            }
            finally
            {
                LogToValidityLog();
            }
        }

        /// <inheritdoc cref="IWebElement.GetAttribute" />
        public string GetAttribute(string attributeName)
        {
            try
            {
                return InternalElement.GetAttribute(attributeName);
            }
            finally
            {
                LogToValidityLog();
            }
        }

        /// <inheritdoc cref="IWebElement.GetProperty"/>
        public string GetProperty(string propertyName)
        {
            try
            {
                return InternalElement.GetProperty(propertyName);
            }
            finally
            {
                LogToValidityLog();
            }
        }

        /// <inheritdoc cref="IWebElement.GetCssValue" />
        public string GetCssValue(string propertyName)
        {
            try
            {
                return InternalElement.GetCssValue(propertyName);
            }
            finally
            {
                LogToValidityLog();
            }
        }

        /// <inheritdoc cref="IWebElement.TagName" />
        public string TagName
        {
            get
            {
                try
                {
                    return InternalElement.TagName;
                }
                finally
                {
                    LogToValidityLog();
                }
            }
        }

        /// <inheritdoc cref="IWebElement.Text" />
        public string Text
        {
            get
            {
                try
                {
                    return InternalElement.Text;
                }
                finally
                {
                    LogToValidityLog();
                }
            }
        }

        /// <inheritdoc cref="IWebElement.Enabled" />
        public bool Enabled
        {
            get
            {
                try
                {
                    return InternalElement.Enabled;
                }
                finally
                {
                    LogToValidityLog();
                }
            }
        }

        /// <inheritdoc cref="IWebElement.Selected" />
        public bool Selected
        {
            get
            {
                try
                {
                    return InternalElement.Selected;
                }
                finally
                {
                    LogToValidityLog();
                }
            }
        }

        /// <inheritdoc cref="IWebElement.Location" />
        public Point Location
        {
            get
            {
                try
                {
                    return InternalElement.Location;
                }
                finally
                {
                    LogToValidityLog();
                }
            }
        }

        /// <inheritdoc cref="IWebElement.Size" />
        public Size Size
        {
            get
            {
                try
                {
                    return InternalElement.Size;
                }
                finally
                {
                    LogToValidityLog();
                }
            }
        }

        /// <inheritdoc cref="IWebElement.Displayed" />
        public bool Displayed
        {
            get
            {
                try
                {
                    return InternalElement.Displayed;
                }
                finally
                {
                    LogToValidityLog();
                }
            }
        }

        private void LogToValidityLog()
        {
            Log.ElementValidity.Log(LocatorStrategy, LocatorString, IsInitialized);
        }
    }
}
