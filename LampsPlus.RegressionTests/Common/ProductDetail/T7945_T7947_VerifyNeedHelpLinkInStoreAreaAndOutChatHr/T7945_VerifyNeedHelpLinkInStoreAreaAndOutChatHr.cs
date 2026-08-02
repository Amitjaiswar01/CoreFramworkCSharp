using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ProductDetail.T7945_T7947_VerifyNeedHelpLinkInStoreAreaAndOutChatHr
{
    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7945_Windows_VerifyNeedHelpLinkInStoreAreaAndOutChatHr : T7945_DesktopBase
    {
        public T7945_Windows_VerifyNeedHelpLinkInStoreAreaAndOutChatHr(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void NeedHelpLinkInStoreAreaAndOutChatHr(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7945_Windows_Pros_VerifyNeedHelpLinkInStoreAreaAndOutChatHr : T7945_DesktopBase
    {
        public T7945_Windows_Pros_VerifyNeedHelpLinkInStoreAreaAndOutChatHr(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_PCSI)]
        public void NeedHelpLinkInStoreAreaAndOutChatHr(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that Need Help with This Product? link In Store Area and Outside Chat Hours
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10669
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7945
    /// </summary>
    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10669"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7945")]
    public abstract class T7945_DesktopBase : TestsBaseDesktop
    {
        protected T7945_DesktopBase(ITestOutputHelper output) : base(output) { }

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
            Assert.True(ProductDetail.IsNeedHelpModalVisible, "Product Help & Store Availability modal is not visible");

            if (!ProductDetail.IsChatIconEnabled())
            {
                //Assert: Verify the Live Chat is not visible within the modal after business hours
                Assert.False(ProductDetail.IsNeedHelpModalChatVisible, "Live Chat is visible within the Need help with this product? modal after business hours");
            }
            else
            {
                Log.Message("Chat is in business hours");
            }
        }
    }
}
