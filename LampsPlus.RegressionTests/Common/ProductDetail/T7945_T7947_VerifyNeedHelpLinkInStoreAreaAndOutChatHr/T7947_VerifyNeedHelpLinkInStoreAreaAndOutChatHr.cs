using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ProductDetail.T7945_T7947_VerifyNeedHelpLinkInStoreAreaAndOutChatHr
{
    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
    public class T7947_iPhone_VerifyNeedHelpLinkInStoreAreaAndOutChatHr : T7947_MobileBase
    {
        public T7947_iPhone_VerifyNeedHelpLinkInStoreAreaAndOutChatHr(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void NeedHelpLinkInStoreAreaAndOutChatHr(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
    public class T7947_iPhone_Pros_VerifyNeedHelpLinkInStoreAreaAndOutChatHr : T7947_MobileBase
    {
        public T7947_iPhone_Pros_VerifyNeedHelpLinkInStoreAreaAndOutChatHr(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI)]
        public void NeedHelpLinkInStoreAreaAndOutChatHr(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7947_Emulator_VerifyNeedHelpLinkInStoreAreaAndOutChatHr : T7947_MobileBase
    {
        public T7947_Emulator_VerifyNeedHelpLinkInStoreAreaAndOutChatHr(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void NeedHelpLinkInStoreAreaAndOutChatHr(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7947_Emulator_Pros_VerifyNeedHelpLinkInStoreAreaAndOutChatHr : T7947_MobileBase
    {
        public T7947_Emulator_Pros_VerifyNeedHelpLinkInStoreAreaAndOutChatHr(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_PCSI)]
        public void NeedHelpLinkInStoreAreaAndOutChatHr(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that Need Help with This Product? link In Store Area and Outside Chat Hours
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10669
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7947
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10669"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7947")]
    public abstract class T7947_MobileBase : TestsBaseMobile
    {
        protected T7947_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange : Navigate to any product detail page
            InitializeFunctionalTest(config);
            var shortSku = ProductActions.GetAnySkuWithProductDetailPage;
            Assert.DatabaseObject(shortSku, "ProductActions.GetAnySkuWithProductDetailPage()");

            Browser.NavigateToPdp(shortSku);
            Assert.True(ProductDetail.IsCurrentPage, "Current page is not Pdp page");

            /* Act:
            Scroll upto the Need help with this product? link
            Click on the Need help with this product? link
            */
            ProductDetail.DisplayProductHelpLink();
            ProductDetail.OpenProductHelpAndStoreAvailabilityModal();

            //Assert: Verify the "Need help with this product?" modal open
            Assert.True(ProductDetail.IsNeedHelpModalVisible, "Need help with this product modal is not visible");

            if (!ProductDetail.IsChatIconEnabled())
            {
                //Assert: Verify the Live Chat is not visible within the modal after business hours
                Assert.False(ProductDetail.IsNeedHelpModalChatVisible, "Live Chat is visible within the 'Need help with this product?' modal after business hours");
            }
            else
            {
                Log.Message("Chat is in business hours");
            }
        }
    }
}