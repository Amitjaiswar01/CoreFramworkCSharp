using System.Net;
using Xunit;
using Xunit.Abstractions;

using Automation.FrameworkTests.Tests.Framework.Utilities;
using Automation.FrameworkTests.Utilities;

namespace Automation.FrameworkTests.Tests.Framework.Server_Error
{
    /// <summary>
    /// Test the Server error detection feature. By forcing a faux server error page, the test should automatically skip.
    /// </summary>
    [Trait(Traits.Category, Traits.Unit), Trait(Traits.Feature, "Server Error Skip")]
    public class ServerErrorSkipIntegration : BrowserBase
    {
        /// <inheritdoc />
        public ServerErrorSkipIntegration(ITestOutputHelper output) : base(output, "ServerErrorSkipIntegration") { }

        /// <summary>
        /// Check that test is skipped if 404 page is loaded.
        /// Tests a failing browser wait method when on a server error page.
        /// </summary>
        [SkippableFact]
        public void CheckTestSkippedOn404()
        {
            // In context, this would be a click to the Wishlist button - HeaderFooter.WishListElement.Click();
            FauxServerError(HttpStatusCode.NotFound);
            
            Browser.Wait.ForPage(LampsPlusHomePageUrl);
        }

        /// <summary>
        /// Check that test is skipped if 500 page is loaded.
        /// Tests a failing verify statement when on a server error page.
        /// </summary>
        [SkippableFact]
        public void CheckTestSkippedOn500()
        {
            // Some visit to a page
            FauxServerError(HttpStatusCode.InternalServerError);
            
            Assert.True(false, "Definite failure.");

            Assert.True(false, "Never Reached");
        }

        /// <summary>
        /// Check that test is skipped if 503 page is loaded.
        /// Tests a failing browser locate when on a server error page.
        /// </summary>
        [SkippableFact]
        public void CheckTestSkippedOn503()
        {
            // Some visit to a page
            FauxServerError(HttpStatusCode.ServiceUnavailable);

            // Assumption of successful page load is made, then an attempt at accessing an element.
            Browser.Locate.ElementImmediately(".testing");
        }

        /// <summary>
        /// Check that test fails, not skipped, even if '503' string is in page.
        /// </summary>
        [SkippableFact]
        public void CheckTestFailsIf503StringPresentOnPage()
        {
            // Insert 503 string on page
            Browser.ExecuteJs($@"document.querySelector('p').innerText = '{(int)HttpStatusCode.ServiceUnavailable}'");

            // Should not skip test because of 503 string on page. Test should fail.
            Assert.True(false, "Definite failure.");
        }

        private void FauxServerError(HttpStatusCode serverError)
        {
            Browser.Navigate($"file:///{System.IO.Directory.GetCurrentDirectory()}/Tests/Framework/Server%20Error/{(int)serverError}.html");
        }
    }
}
