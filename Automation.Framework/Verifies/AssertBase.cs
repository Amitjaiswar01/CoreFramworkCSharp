using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;

namespace Automation.Framework.Verifies
{
    public abstract class AssertBase : IAssert
    {
        internal IBrowser Browser { get; }

        public AssertBase(IBrowser browser)
        {
            Browser = browser;
        }

        public bool DoesElementExistInDom { get; set; }

        public abstract void Equals(object expected, object actual, string message, bool skipMessage = false);

        public abstract void Equals<T>(T expected, T actual, IEqualityComparer<T> comparer, string message, bool skipMessage = false);

        public abstract void True(bool expression, string message, bool skipMessage = false);

        public abstract void False(bool expression, string message, bool skipMessage = false);

        public abstract void InRange(int value, int minimum, int maximum, string message);

        public abstract void Condition(Func<bool> method, string message);

        public abstract void ThrowsNotImplementedException(Expression<Func<IElement>> element);

        public abstract void ThrowsNotImplementedException(Expression<Func<ReadOnlyCollection<IElement>>> element);

        public EventHandler<WebElementEventArgs> ReadyToMoveToEventHandler { get; set; }

        public EventHandler<StringEventArgs> CheckElementImmediatelyEventHandler { get; set; }

        public VerifyType LastVerify { get; set; }

        public string Type { get; set; }

        public void DatabaseObject(object response, string databaseMethod)
        {
            try
            {
                LastVerify = VerifyType.DatabaseObject;

                var outputMessage = $"Database {databaseMethod} did not return any results";
                var message = string.IsNullOrEmpty(databaseMethod) ? string.Empty : outputMessage;

                Browser.Log.AttemptToVerify(Type, VerifyType.DatabaseObject, databaseMethod);
                True(!string.IsNullOrEmpty(databaseMethod), "A failure message is required.", true);

                NotNull(response, message, true);
                if (Browser.IsMobileCloud) { Browser.ExecuteJs(Browser.CloudTestStatusPassedJs);}
            }
            catch
            {
                if (Browser.IsMobileCloud) { Browser.ExecuteJs(Browser.CloudTestStatusFailedJs);}
            }
        }

        public void Displayed(IElement element, string message)
        {
            try
            {
                LastVerify = VerifyType.Displayed;

                Browser.Log.AttemptToVerify(Type, VerifyType.Displayed, "");
                Browser.Wait.ForDisplayedElement(element);
                True(element != null && element.Displayed, message, true);
                if (Browser.IsMobileCloud) { Browser.ExecuteJs(Browser.CloudTestStatusPassedJs); }
            }
            catch
            {
                if (Browser.IsMobileCloud) { Browser.ExecuteJs(Browser.CloudTestStatusFailedJs); }
            }
        }

        public void NotDisplayed(IElement element, string message)
        {
            try
            {
                LastVerify = VerifyType.NotDisplayed;

                Browser.Log.AttemptToVerify(Type, VerifyType.NotDisplayed, "");

                True(!element.Displayed, message, true);
                if (Browser.IsMobileCloud) { Browser.ExecuteJs(Browser.CloudTestStatusPassedJs); }
            }
            catch
            {
                True(false, message, true);
                if (Browser.IsMobileCloud) { Browser.ExecuteJs(Browser.CloudTestStatusFailedJs); }
            }
        }

        public void DoesNotExist(string cssElementLocator, string message)
        {
            try
            {
                LastVerify = VerifyType.DoesNotExist;
                DoesElementExistInDom = true;

                Browser.Log.AttemptToVerify(Type, VerifyType.DoesNotExist, $" => CSS Element Locator: {cssElementLocator} ");
                OnCheckElementImmediately(new StringEventArgs(cssElementLocator));

                // NOTE: There is a dependency here. This relies on DoesElementExistInDom being updated correctly before this is called.
                False(DoesElementExistInDom, $"One or more elements with this CSS locator {cssElementLocator} exist on the page.", true);
                if (Browser.IsMobileCloud) { Browser.ExecuteJs(Browser.CloudTestStatusPassedJs); }
            }
            catch
            {
                False(true, "The requested element was found but not expected.", true);
                if (Browser.IsMobileCloud) { Browser.ExecuteJs(Browser.CloudTestStatusFailedJs); }
            }
        }

        public void StringContains(string baseString, string subString, string message)
        {
            try
            {
                LastVerify = VerifyType.StringContains;

                Browser.Log.AttemptToVerify(Type, VerifyType.StringContains,
                    $" => BaseString: {baseString} || SubString: {subString} ");
                True(baseString.Contains(subString), $"{message}: {subString} was not found in {baseString}", true);
                if (Browser.IsMobileCloud) { Browser.ExecuteJs(Browser.CloudTestStatusPassedJs); }
            }
            catch
            {
                if (Browser.IsMobileCloud) { Browser.ExecuteJs(Browser.CloudTestStatusFailedJs); }
            }
        }

        public void PageUrl(string expectedUrl, string actualUrl, string message)
        {
            try
            {
                LastVerify = VerifyType.PageUrl;

                Browser.Log.AttemptToVerify(Type, VerifyType.PageUrl, $"=> Expected URL: {expectedUrl} || Actual URL: {actualUrl}");
                Equals(expectedUrl, actualUrl, message, true);
                if (Browser.IsMobileCloud) { Browser.ExecuteJs(Browser.CloudTestStatusPassedJs); }
            }
            catch
            {
                if (Browser.IsMobileCloud) { Browser.ExecuteJs(Browser.CloudTestStatusFailedJs); }
            }
        }

        public void TextLink(string expectedText, string actualText, string message)
        {
            try
            {
                LastVerify = VerifyType.TextLink;

                Browser.Log.AttemptToVerify(Type, VerifyType.TextLink, $" => Expected Text: {expectedText} || Actual Text: {actualText}");
                Equals(expectedText, actualText, message, true);
                if (Browser.IsMobileCloud) { Browser.ExecuteJs(Browser.CloudTestStatusPassedJs); }
            }
            catch
            {
                if (Browser.IsMobileCloud) { Browser.ExecuteJs(Browser.CloudTestStatusFailedJs); }
            }
        }

        public void NotNull(object itemToVerify, string message, bool skipMessage = false)
        {
            try
            {
                if (!skipMessage)
                {
                    LastVerify = VerifyType.NotNull;

                    Browser.Log.AttemptToVerify(Type, VerifyType.NotNull, "");
                }

                True(itemToVerify != null, message, true);
                if (Browser.IsMobileCloud) { Browser.ExecuteJs(Browser.CloudTestStatusPassedJs); }
            }
            catch
            {
                if (Browser.IsMobileCloud) { Browser.ExecuteJs(Browser.CloudTestStatusFailedJs); }
            }
        }

        public virtual void Dispose() { }

        public class WebElementEventArgs : EventArgs
        {
            public IElement Element { get; set; }

            public WebElementEventArgs(IElement element) { Element = element; }
        }

        public class BoolEventArgs : EventArgs
        {
            public bool Condition { get; set; }

            public BoolEventArgs(bool condition) { Condition = condition; }
        }

        public class StringEventArgs : EventArgs
        {
            public string Value { get; set; }

            public StringEventArgs(string value) { Value = value; }
        }

        protected void OnReadyToMoveToElement(WebElementEventArgs e)
        {
            var handler = ReadyToMoveToEventHandler;
            handler?.Invoke(this, e);
        }

        protected void OnCheckElementImmediately(StringEventArgs e)
        {
            var handler = CheckElementImmediatelyEventHandler;
            handler?.Invoke(this, e);
        }
    }
}

