using System;

using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Desktop.CreateAccount
{
    /// <summary>
    /// See <see cref="Test"/> for details.
    /// </summary>
    public class T496VerifyConnectUsingFacebookButtonRedirectsToFacebook : CreateAccountTestsBase
    {
        /// <summary>
        /// See <see cref="Test"/> for details.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public T496VerifyConnectUsingFacebookButtonRedirectsToFacebook(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify that clicking on the 'Connect using Facebook' button re-directs user to Facebook login.
        /// JIRA Task Link for Desktop: https://lampstrack.lampsplus.com:8443/browse/ACD-5253
        /// Test Case Link for Desktop: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T296
        /// JIRA Task Link for Mobile: https://lampstrack.lampsplus.com:8443/browse/ACD-5217
        /// Test Case Link for Mobile: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T497
        /// </summary>
        [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5253"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T296")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows10_Chrome_SNIS_UNSI)]
        [InlineData(TestConfiguration.Windows10_ChromeMobileView_SNIS_UNSI)]
        public void Test(string config)
        {
            InitializeFramework(config, Urls.CreateAccountPageUrl);

            ManageAccount.ClickFacebookButtonAndWait();
            var formattedUrl = new Uri(Browser.PageUrl).GetLeftPart(UriPartial.Path);

            if (!Settings.IsMobileView)
            {
                Verify.Equals("https://www.facebook.com/login.php", formattedUrl,
                    "Facebook connect button didn't redirect to facebook login page.");
            }
            else
            {
                Verify.Equals("https://m.facebook.com/login.php", formattedUrl,
                    "Facebook connect button didn't redirect to facebook login page.");
            }
        }
    }
}