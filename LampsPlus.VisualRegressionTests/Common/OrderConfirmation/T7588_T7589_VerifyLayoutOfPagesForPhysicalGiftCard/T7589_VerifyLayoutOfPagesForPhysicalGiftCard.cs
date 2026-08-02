using System.Collections.Generic;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.OrderConfirmation.T7588_T7589_VerifyLayoutOfPagesForPhysicalGiftCard
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7589_iPhone_VerifyLayoutOfPagesForPhysicalGiftCard : T7589_MobileBase
    {
        public T7589_iPhone_VerifyLayoutOfPagesForPhysicalGiftCard(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void VerifyLayoutOfPagesForPhysicalGiftCard(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7589_Android_VerifyLayoutOfPagesForPhysicalGiftCard : T7589_MobileBase
    {
        public T7589_Android_VerifyLayoutOfPagesForPhysicalGiftCard(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI)]
        public void VerifyLayoutOfPagesForPhysicalGiftCard(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7589_Emulator_VerifyLayoutOfPagesForPhysicalGiftCard : T7589_MobileBase
    {
        public T7589_Emulator_VerifyLayoutOfPagesForPhysicalGiftCard(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void VerifyLayoutOfPagesForPhysicalGiftCard(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the Layout of the Pages for a Physical Gift Card
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9809
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7589
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9809"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7589")]
    public abstract class T7589_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7589_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrange: User has Navigated to the Gift Card Page
            InitializeVisualTest(config);
            ShoppingCartWorkflow.EmptyCart();
            Browser.Navigate(Urls.GiftCardLandingPageUrl);
            Browser.Wait.ForDomReady();

            //Act : Take Screenshot of the Entire Page
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement>{ Sort.IgnoreRecentlyViewedContainer() });

            //Act : Click on "Shop Now" Button to Navigate to Gift Card PDP
            Sort.NavigateToGiftCardPdp();
            Assert.True(ProductDetail.IsCurrentPage, "User is not on PDP");

            //Act : Fill Out the Form on PDP
            ProductDetail.AddGiftCardDetails("LPQA Test");

            //Act : Take Screenshot of the Entire Page
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true, true);

            //Act : Add Gift Card to the Cart 
            ProductDetail.AddToCart();
            Assert.True(Cart.IsCurrentPage, "User is not on Cart Page");

            //Act : Take Screenshot of the Visible Screen
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, Cart.IgnoreCartIdAndMoreYouMayLike(), false, true, Cart.GetMoreYouMayLike(), 20);
        }
    }
}