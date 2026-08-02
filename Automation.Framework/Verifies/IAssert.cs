using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace Automation.Framework.Verifies
{
    /// <summary>
    /// Base interface for all type of verification classes.
    /// </summary>
    public interface IAssert : IDisposable
    {
        /// <summary>
        /// Flag to know if the requested element is in the DOM.
        /// </summary>
        bool DoesElementExistInDom { get; set; }

        /// <summary>
        /// Event handler to communicate when an element should be visibly located.
        /// </summary>
        EventHandler<AssertBase.WebElementEventArgs> ReadyToMoveToEventHandler { get; set; }

        /// <summary>
        /// Event handler to communicate if an element can be immediately located.
        /// </summary>
        EventHandler<AssertBase.StringEventArgs> CheckElementImmediatelyEventHandler { get; set; }

#pragma warning disable CS3001
        /// <summary>
        /// Is the given objects have the same value?
        /// </summary>
        /// <param name="expected">Expected result</param>
        /// <param name="actual">Actual result</param>
        /// <param name="message">Message to show if the verify statement fails.</param>
        void Equals(object expected, object actual, string message, bool skipMessage = false);

        /// <summary>
        /// Do the given objects have the same value?
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="expected">Expected result.</param>
        /// <param name="actual">Actual result.</param>
        /// <param name="comparer">Contains custom logic to perform the comparison.</param>
        /// <param name="message">Message to show if the verify statement fails.</param>
        void Equals<T>(T expected, T actual, IEqualityComparer<T> comparer, string message, bool skipMessage = false);

        /// <summary>
        /// Is the given expression true?
        /// </summary>
        /// <param name="expression">Expression to evaluate in the test.</param>
        /// <param name="message">Message to show if the verify statement fails.</param>
        /// <param name="skipMessage"></param>
        void True(bool expression, string message, bool skipMessage = false);

        /// <summary>
        /// Is the given expression is false?
        /// </summary>
        /// <param name="expression">Expression to evaluate in the test.</param>
        /// <param name="message">Message to show if the verify statement fails.</param>
        void False(bool expression, string message, bool skipMessage = false);

        /// <summary>
        /// Is the given value in the provided range?
        /// </summary>
        /// <param name="value">Value to range check.</param>
        /// <param name="minimum">Minimum acceptable value.</param>
        /// <param name="maximum">Maximum acceptable value.</param>
        /// <param name="message">Message to show if the verify statement fails.</param>
        void InRange(int value, int minimum, int maximum, string message);

        /// <summary>
        /// Is the condition is true?
        /// </summary>
        /// <param name="method">Method that checks a condition and returns a bool</param>
        /// <param name="message">Message to show if the verify statement fails.</param>
        void Condition(Func<bool> method, string message);

        /// <summary>
        /// Does the database object return a result?
        /// </summary>
        /// <param name="response">Response object from the database.</param>
        /// <param name="databaseMethod">Message to display if the query does not return results.</param>
        void DatabaseObject(object response, string databaseMethod);

        /// <summary>
        /// Is the given element displayed on the screen?
        /// </summary>
        /// <param name="element">Element to locate and check the Displayed property.</param>
        /// <param name="message">Message to show if the verify statement fails.</param>
        void Displayed(IElement element, string message);

        /// <summary>
        /// Is the given element not displayed on the screen?
        /// </summary>
        /// <param name="element">Element to locate and check the Displayed property.</param>
        /// <param name="message">Message to show if the verify statement fails.</param>
        void NotDisplayed(IElement element, string message);

        /// <summary>
        /// Does the given element not exist on the page?
        /// </summary>
        /// <param name="cssElementLocator"></param>
        /// <param name="message"></param>
        void DoesNotExist(string cssElementLocator, string message);

        /// <summary>
        /// Is the given subString contained within the baseString?
        /// </summary>
        /// <param name="baseString">String expected to contain the given subString.</param>
        /// <param name="subString">Expected string within the given baseString.</param>
        /// <param name="message">Message to show if the verify statement fails.</param>
        void StringContains(string baseString, string subString, string message);

        /// <summary>
        /// Are the expectedUrl and actualUrl strings the same?
        /// </summary>
        /// <param name="expectedUrl">Expected Url string.</param>
        /// <param name="actualUrl">Actual Url string.</param>
        /// <param name="message">Message to show if the verify statement fails.</param>
        void PageUrl(string expectedUrl, string actualUrl, string message);

        /// <summary>
        /// Are the expectedUrl and actualUrl strings the same?
        /// </summary>
        /// <param name="expectedText">Expected Url string.</param>
        /// <param name="actualText">Actual Url string.</param>
        /// <param name="message">Message to show if the verify statement fails.</param>
        void TextLink(string expectedText, string actualText, string message);

        /// <summary>
        /// Is the given object null?
        /// </summary>
        /// <param name="itemToVerify">Check if the given object is null.</param>
        /// <param name="message">Message to show if the verify statement fails.</param>
        /// <param name="skipMessage">Skip the auto-generated message when true.</param>
        void NotNull(object itemToVerify, string message, bool skipMessage = false);

        /// <summary>
        /// Does the given element throw a NotImplementedException?
        /// </summary>
        /// <param name="element">Expected element to throw NotImplementedException.</param>
        void ThrowsNotImplementedException(Expression<Func<IElement>> element);

        /// <summary>
        /// Does the given element throw a NotImplementedException?
        /// </summary>
        /// <param name="element">Expected element to throw NotImplementedException.</param>
        void ThrowsNotImplementedException(Expression<Func<ReadOnlyCollection<IElement>>> element);
#pragma warning restore CS3001
    }
}
