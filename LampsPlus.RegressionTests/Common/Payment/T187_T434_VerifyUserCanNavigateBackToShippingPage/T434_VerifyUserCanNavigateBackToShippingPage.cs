using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using xRetry;

namespace LampsPlus.RegressionTests.Common.Payment.T187_T434_VerifyUserCanNavigateBackToShippingPage
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Payment)]
    public class T434_iPhone_VerifyUserCanNavigateBackToShippingPage : T434_MobileBase
    {
        public T434_iPhone_VerifyUserCanNavigateBackToShippingPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void UserCanNavigateBackToShippingPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T434_Emulator_VerifyUserCanNavigateBackToShippingPage : T434_MobileBase
    {
        public T434_Emulator_VerifyUserCanNavigateBackToShippingPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void UserCanNavigateBackToShippingPage(string config) => Validate(config);
    }


    /// <summary>
    /// Verify a user can navigate back to the Shipping page.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10002
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T434
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10002"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T434")]
    public abstract class T434_MobileBase : TestsBaseMobile
    {
        protected T434_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: Add any item to the cart and proceed to the Payment page.
            InitializeFunctionalTest(config);
            ShoppingCartWorkflow.ProceedToPaymentWithSingleProduct();

            //Act: On the top of the Payment page, click on the Shipping breadcrumb in header.
            ShoppingCartWorkflow.NavigateBackToShippingPageFromPaymentPage();

            //Assert: The user is re-directed to the Shipping page.
            Assert.PageUrl(Urls.ShippingPageUrl, Browser.PageUrl, "Did not redirect to shipping page after clicking Shipping link in breadcrumb.");
        }
    }
}
