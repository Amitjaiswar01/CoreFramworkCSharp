using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Text;

using Automation.Framework.Enums;

namespace Automation.Framework.Utilities
{
    /// <summary>
    /// Logs out elements that caused test failures across a suite execution.
    /// </summary>
    public class ElementValidity
    {
        private static readonly object Lock = new object();
        private static readonly ConcurrentDictionary<string, ElementValidityLog> ElementLogs = new ConcurrentDictionary<string, ElementValidityLog>();

        private readonly string _testCaseName;

        internal static string ElementValidityFolderName = "ElementValidityLogs";

        public ElementValidity(string testCaseName)
        {
            _testCaseName = testCaseName;
        }

        public void Log(LocatorStrategy locatorStrategy, string locatorString, bool isUsageSuccessful)
        {
            var elementName = $"Locate {locatorStrategy}: '{locatorString}'";

            if (ElementLogs.TryGetValue(elementName, out var item))
            {
                AddUsageToList(item, isUsageSuccessful);
            }
            else
            {
                var newLog = new ElementValidityLog
                {
                    ElementName = elementName,
                };

                AddUsageToList(newLog, isUsageSuccessful);

                ElementLogs.TryAdd(elementName, newLog);
            }
        }

        public void ExportLogToFile()
        {
            lock (Lock)
            {
                var lines = new StringBuilder();
                lines.AppendLine("~~~Elements Validity Results~~~");
                lines.AppendLine();

                foreach (var log in ElementLogs.OrderByDescending(log => log.Value.FailedTests.Count))
                {
                    lines.AppendLine(log.Value.ToString());
                }

                lines.AppendLine("~~~~~~~~~~~~~~");

                File.WriteAllText($@"{AppDomain.CurrentDomain.BaseDirectory}\{ElementValidityFolderName}\ElementValidityLog.txt", lines.ToString());
            }
        }

        private void AddUsageToList(ElementValidityLog record, bool isSuccessful)
        {
            if (isSuccessful)
            {
                record.SuccessfulTests.AddOrUpdate(_testCaseName, 1, (key, oldValue) => oldValue + 1);
            }
            else
            {
                record.FailedTests.AddOrUpdate(_testCaseName, 1, (key, oldValue) => oldValue + 1);
            }
        }


        /// <summary>
        /// Single Log for a unique element
        /// </summary>
        private class ElementValidityLog
        {
            public string ElementName { private get; set; }
            public ConcurrentDictionary<string, int> FailedTests { get; }
            public ConcurrentDictionary<string, int> SuccessfulTests { get; }

            public ElementValidityLog()
            {
                FailedTests = new ConcurrentDictionary<string, int>();
                SuccessfulTests = new ConcurrentDictionary<string, int>();
            }

            private string BuildTestNamesString(ConcurrentDictionary<string, int> dictionary)
            {
                var testNamesString = new StringBuilder();

                dictionary.OrderBy(item => item.Key).ToList().ForEach(item =>
                {
                    var timesUsed = item.Value > 1 ? $" x{item.Value}" : string.Empty;
                    testNamesString.AppendLine($"{item.Key}{timesUsed}");
                });

                return testNamesString.ToString();
            }

            public override string ToString()
            {
                var builtString = new StringBuilder();
                builtString.Append(ElementName);
                builtString.AppendLine($" [Fail Test Count: {FailedTests.Count}][Success Test Count: {SuccessfulTests.Count}]");

                if (FailedTests.Count > 0)
                {
                    builtString.AppendLine("--Failed Test Names--");
                    builtString.AppendLine(BuildTestNamesString(FailedTests));
                }
                if (SuccessfulTests.Count > 0)
                {
                    builtString.AppendLine("--Successful Test Names--");
                    builtString.AppendLine(BuildTestNamesString(SuccessfulTests));
                }

                builtString.AppendLine("________________________________________________________________________________________");
                builtString.AppendLine();

                return builtString.ToString();
            }
        }
    }
}
