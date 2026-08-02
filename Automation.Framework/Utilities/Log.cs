using System;
using System.IO;
using System.Linq;
using System.Text;
using Xunit.Abstractions;

namespace Automation.Framework.Utilities
{
    /// <summary>
    /// Provides logging ability compatible with xUnit.net.
    /// </summary>
    public class Log
    {
        private readonly ITestOutputHelper _output;
        private static string _logExtension => ".html";

        private string _testCaseName { get; }
        private string _logsPath { get; }
        private string _pageSourcePath { get; }
        private string _elementValidityPath { get; }
        private StringBuilder _builder { get; set; }
        private DateTime _startTime { get; set; }
        private int _pageSourceCount { get; set; }

        public ElementValidity ElementValidity { get; set; }

        /// <summary>
        /// Format the given DateTime yyyy MM dd HH:mm:ss:ffff.
        /// </summary>
        /// <param name="dateTime">DateTime object to convert the display format of.</param>
        /// <returns></returns>
        public string FormatDateTime(DateTime dateTime) => dateTime.ToString("yyyy MM dd HH:mm:ss:ffff");

        /// <summary>
        /// Current log output.
        /// </summary>
        public string Output => _builder.ToString();

        /// <summary>
        /// Is logging enabled?
        /// </summary>
        public bool IsLogEnabled { get; set; }

        /// <summary>
        /// Log messages will be written to a file when they happen when true.
        /// When false logs will be written on the test dispose.
        /// Typically this should only be enabled for debugging purposes.
        /// </summary>
        public bool IsRealTimeLoggingEnabled { get; }

        /// <summary>
        /// Fully qualified path of the method under test.
        /// Note a log file will be created with a timestamp of the year month day and hour. This is to keep logs from growing and becoming a memory issue.
        /// Basically this means a new log will be created every hour for each test.
        /// </summary>
        public string TestLogPath => $@"{_logsPath}\{DateTime.Now:yyyy-MM-dd-HH-mm-ss-fff}-{_testCaseName}{_logExtension}";

        /// <summary>
        /// Provides logging ability compatible with xUnit.net.
        /// </summary>
        /// <param name="output">xUnit output helper class to log test execution.</param>
        /// <param name="testName">Name of the method under test.</param>
        /// <param name="enableRealTimeLogging">When true logs will not be written to the console. Logs in the Logs folder will be updated on disk in real time.</param>
        public Log(ITestOutputHelper output, string testName, bool enableRealTimeLogging = false)
        {
            _testCaseName = GetShortTestCaseName(testName);
            IsLogEnabled = true;
            IsRealTimeLoggingEnabled = enableRealTimeLogging;
            ElementValidity = new ElementValidity(_testCaseName);

            _output = output;
            _builder = new StringBuilder();
            _logsPath = BuildLogPath("Logs");
            _pageSourcePath = BuildLogPath("Page Source");
            _elementValidityPath = BuildLogPath(ElementValidity.ElementValidityFolderName);

            InitializeLogDirectory(_logsPath);
            InitializeLogDirectory(BuildLogPath("Results"));
            InitializeLogDirectory(_pageSourcePath);
            InitializeLogDirectory(_elementValidityPath);
        }

        private static string GetShortTestCaseName(string testName)
        {
            Func<char, bool> FilterForCapitalLetters() => c => c.ToString().ToUpper() == c.ToString();

            return string.Join(string.Empty, testName.ToCharArray().Where(FilterForCapitalLetters()));
        }


        /// <summary>
        /// Log message that the test has started.
        /// </summary>
        /// <param name="testName">Name of the test being executed.</param>
        public void TestStarted(string testName)
        {
            _startTime = DateTime.Now;

            Message($"Test Started: {testName}");
        }

        /// <summary>
        /// Log a message that the test has completed.
        /// </summary>
        public void TestCompleted()
        {
            var endTime = DateTime.Now;
            Footer($"Total test time: { endTime - _startTime }");
        }

        /// <summary>
        /// Add a line with no timestamp and "----" as a visual separation between blocks in the log.
        /// </summary>
        public void Header(string message = "")
        {
            Message("--------------------------------------------------------------------------", false);
            if (!string.IsNullOrEmpty(message)) { Message(message, false); }
        }

        /// <summary>
        /// Add a line with no timestamp and "----" as a visual separation between blocks in the log.
        /// </summary>
        public void Footer(string message = "")
        {
            if (!string.IsNullOrEmpty(message)) { Message(message, false); }
            Message("--------------------------------------------------------------------------", false);
        }

        /// <summary>
        /// Decorate the a given link in HTML.
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public string GetHtmlLinkString(string link)
        {
            return $"<a href=\"{link}\">{link}</a>";
        }

        /// <summary>
        /// Update the log with the given message.
        /// </summary>
        /// <param name="message">Message to add to the log.</param>
        /// <param name="addTimestamp">Add a timestamp to the log message. True by default.</param>
        public void Message(string message, bool addTimestamp = true)
        {
            if (IsLogEnabled)
            {
                var outputMessage = addTimestamp ? $"{FormatDateTime(DateTime.Now)} {message}" : message;

                _builder.AppendLine($"{outputMessage} <br />");

                if (IsRealTimeLoggingEnabled) { LogToFile(false); }
                else { _output.WriteLine(outputMessage); } // We will not log out to the console when RealTimeLogging is enabled. This an cause a memory leak for long running tests.
            }
        }

        /// <summary>
        /// Update the log with given message.
        /// </summary>
        /// <param name="verify">Verify Name to add to the log.</param>
        /// <param name="message">Message to add to the log.</param>
        /// <param name="verifyType">Verify Type to add to the log.</param>
        public void AttemptToVerify(string verify, VerifyType verifyType, string message)
        {
            Header();
            Message($"Attempting verification for {verify}.{verifyType} {message}");
            Footer();
        }

        /// <summary>
        /// Update the log with given message.
        /// </summary>
        /// <param name="verify">Verify Name to add to the log.</param>
        /// <param name="message">Message to add to the log.</param>
        /// <param name="verifyType">Verify Type to add to the log.</param>
        public void Verify(string verify, VerifyType verifyType, string message)
        {
            Header();
            Message($"Attempt for {verify}.{verifyType} {message}");
            Footer();
        }

        /// <summary>
        /// Update the log with the given message. This message will have an empty line before and after the message to draw attention to the message.
        /// </summary>
        /// <param name="message"></param>
        public void BlockMessage(string message)
        {
            _output.WriteLine(string.Empty);
            Message(message);
            _output.WriteLine(string.Empty);
        }

        /// <summary>
        /// Log the page source (DOM) for the given page.
        /// NOTE: Do not use directly. Use TestsBase.LogPageSource().
        /// </summary>
        /// <param name="pageSource">DOM to log.</param>
        public void LogPageSource(string pageSource)
        {
            var path = $@"{_pageSourcePath}\{_testCaseName} {_pageSourceCount++}{_logExtension}";

            using (var writer = new StreamWriter(path))
            {
                writer.Write(pageSource);
            }
        }

        /// <summary>
        /// Write the current log out to a file. The log will be appended to the file with the test class name.
        /// </summary>
        public void LogToFile(bool logTestCompleted = true)
        {
            var data = string.Empty;

            if (logTestCompleted) { TestCompleted(); }

            if (File.Exists(TestLogPath)) { using (var reader = new StreamReader(TestLogPath)) { data = reader.ReadToEnd(); } }

            using (var writer = new StreamWriter(TestLogPath))
            {
                _builder.AppendLine("<br />");
                writer.WriteLine(_builder.ToString());
                writer.Write(data);

                _builder = new StringBuilder(); // Initialize a new _builder object once data is written to disk.
            }
        }

        /// <summary>
        /// Delete all files in the Logs directory.
        /// </summary>
        public void ClearLogs()
        {
            try
            {
                if (Directory.Exists(_logsPath))
                {
                    var files = new DirectoryInfo(_logsPath).GetFiles();

                    foreach (var file in files)
                    {
                        file.Delete();
                    }
                }
            }
            catch (Exception e)
            {
                Message($"Error deleting log files: {e.Message}");
                throw;
            }
        }

        private void InitializeLogDirectory(string path) { if (!Directory.Exists(path)) { Directory.CreateDirectory(path); } }

        private string BuildLogPath(string directory) { return $@"{AppDomain.CurrentDomain.BaseDirectory}\{directory}"; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum VerifyType
    {
        Condition,
        DatabaseObject,
        Displayed,
        DoesNotExist,
        Equals,
        False,
        InRange,
        NotDisplayed,
        NotImplemented,
        NotNull,
        PageUrl,
        StringContains,
        TextLink,
        True,
    }
}
