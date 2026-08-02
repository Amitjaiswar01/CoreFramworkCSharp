using System;
using LampsPlus.AutomationFramework;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Common.Certona
{
    /// <summary>
    /// Base class for Certona specific tests.
    /// </summary>
    public class CertonaTestsBase : TestsBase, IDisposable
    {
        /// <summary>
        /// Set this to true for Certona tests that do not run the CertonaUtilities Verify statements in Dispose.
        /// </summary>
        public bool DontRunDefaultVerifies { get; set; }

        /// <summary>
        /// Test base for Certona tests to check if the tests are against the production database.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public CertonaTestsBase(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Initialize Certona setup.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="url"></param>
        public void InitializeCertonaFramework(string config, string url = "")
        {
            try
            {
                InitializeFramework(config, url);
            }
            catch
            {
                DontRunDefaultVerifies = true;
                throw;
            }
        }

        /// <summary>
        /// Initialize Certona setup.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="visitMultiplePages"></param>
        /// <param name="urlToNavigateTo"></param>
        public void InitializeCertonaFramework(string config, bool visitMultiplePages, string urlToNavigateTo)
        {
            try
            {
                InitializeFramework(config, urlToNavigateTo);

                if (visitMultiplePages) { CertonaUtilities.VisitMultiplePages(); }
            }
            catch
            {
                DontRunDefaultVerifies = true;
                throw;
            }
        }

        /// <summary>
        /// Run common CertonaUtilities verify methods when DontRunDefaultVerifies = false.
        /// </summary>
        // ReSharper disable once InheritdocConsiderUsage
        public new void Dispose()
        {
            try
            {
                if (!DontRunDefaultVerifies)
                {
                    CertonaUtilities.VerifySchemesExistInResponse();
                    CertonaUtilities.VerifyTitlesMatchResponse();
                    CertonaUtilities.VerifySkusMatchResponse();
                    CertonaUtilities.VerifySkuAmountsMatchDefinedQuantities();
                    CertonaUtilities.VerifyCertonaIsDisabledForEmployee();
                }
            }
            finally
            {
                base.Dispose();
            }
        }
    }
}
