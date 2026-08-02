using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Xunit;

namespace Automation.Framework.Verifies
{
    public class Assert : AssertBase
    {
        public Assert(IBrowser browser) : base(browser) { }

        public override void Equals(object expected, object actual, string message, bool skipMessage = false)
        {
            try
            {
                Xunit.Assert.Equal(expected, actual);
            }
            catch 
            {
                Browser.IsTestFailed = true;
                if (!string.IsNullOrEmpty(message)) { Browser.Log.Message(message); }
                throw;
            }
        }

        public override void Equals<T>(T expected, T actual, IEqualityComparer<T> comparer, string message, bool skipMessage = false)
        {
            try
            {
                Xunit.Assert.True(comparer.Equals(expected, actual));
            }
            catch 
            {
                Browser.IsTestFailed = true;
                throw;
            }
        }

        public override void True(bool expression, string message, bool skipMessage = false)
        {
            try
            {
                Xunit.Assert.True(expression, message);
            }
            catch
            {
                Browser.IsTestFailed = true;
                throw;
            }
        }

        public override void False(bool expression, string message, bool skipMessage = false)
        {
            try
            {
                Xunit.Assert.False(expression, message);
            }
            catch
            {
                Browser.IsTestFailed = true;
                throw;
            }
        }

        public override void InRange(int value, int minimum, int maximum, string message)
        {
            try
            {
                Xunit.Assert.InRange(value, minimum, maximum);
            }
            catch
            {
                Browser.IsTestFailed = true;
                throw;
            }
        }

        public override void Condition(Func<bool> method, string message)
        {
            try
            {
                var result = method();
                Xunit.Assert.True(result, message);
            }
            catch
            {
                Browser.IsTestFailed = true;
                throw;
            }
        }

        public override void ThrowsNotImplementedException(Expression<Func<IElement>> element)
        {
            Xunit.Assert.Throws<NotImplementedException>(() => element.Compile().Invoke());
        }

        public override void ThrowsNotImplementedException(Expression<Func<ReadOnlyCollection<IElement>>> element)
        {
            Xunit.Assert.Throws<NotImplementedException>(() => element.Compile().Invoke());
        }
    }
}
