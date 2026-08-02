using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ProductDetail.T7944_T7946_VerifyNeedHelpLinkInStoreAreaAndInChatHr
{
    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7944_Windows_VerifyNeedHelpLinkInStoreAreaAndInChatHr : T7944_DesktopBase
    {
        public T7944_Windows_VerifyNeedHelpLinkInStoreAreaAndInChatHr(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7944. Rework - ACD-10738")]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void NeedHelpLinkInStoreAreaAndInChatHr(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7944_Windows_Pros_VerifyNeedHelpLinkInStoreAreaAndInChatHr : T7944_DesktopBase
    {
        public T7944_Windows_Pros_VerifyNeedHelpLinkInStoreAreaAndInChatHr(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7944. Rework - ACD-10738")]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_PCSI)]
        public void NeedHelpLinkInStoreAreaAndInChatHr(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7944_Mac_VerifyNeedHelpLinkInStoreAreaAndInChatHr : T7944_DesktopBase
    {
        public T7944_Mac_VerifyNeedHelpLinkInStoreAreaAndInChatHr(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void NeedHelpLinkInStoreAreaAndInChatHr(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7944_iPad_VerifyNeedHelpLinkInStoreAreaAndInChatHr : T7944_DesktopBase
    {
        public T7944_iPad_VerifyNeedHelpLinkInStoreAreaAndInChatHr(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void NeedHelpLinkInStoreAreaAndInChatHr(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7944_TabletEmulator_VerifyNeedHelpLinkInStoreAreaAndInChatHr : T7944_DesktopBase
    {
        public T7944_TabletEmulator_VerifyNeedHelpLinkInStoreAreaAndInChatHr(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void NeedHelpLinkInStoreAreaAndInChatHr(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that Need Help with This Product? link In Store Area and During Chat Hours
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10667
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7944
    /// </summary>
    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10667"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7944")]
    public abstract class T7944_DesktopBase : TestsBaseDesktop
    {
        protected T7944_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange : Navigate to any product detail page
            InitializeFunctionalTest(config);
            var shortSku = ProductActions.GetAnySkuWithProductDetailPage;
            Assert.DatabaseObject(shortSku, "ProductActions.GetAnySkuWithProductDetailPage()");

            Browser.NavigateToPdp(shortSku);
            Assert.True(ProductDetail.IsCurrentPage, "Current page is not Pdp page");

            //Act : Click on the "Need help with this product?" link
            ProductDetail.OpenProductHelpAndStoreAvailabilityModal();

            //Assert: Verify the "Need help with this product?" modal open
            Assert.True(ProductDetail.IsNeedHelpModalVisible, "Need help with this product modal is not visible");
            
            if (ProductDetail.IsChatIconEnabled())
            {
                //Assert: Verify the Live Chat is visible within the "Need help with this product?" modal
                Assert.True(ProductDetail.IsNeedHelpModalChatVisible, "Live Chat is not visible within the Need help with this product? modal");
            }
            else
            {
                Log.Message("Chat is outside business hours");
            }
        }
    }
}
