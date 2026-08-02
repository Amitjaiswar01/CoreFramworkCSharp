using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ManageAccount.T7258_T7259_VerifyLayoutOfShippingAddressesPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7259_iPhone_VerifyLayoutOfShippingAddressesPage : T7259_MobileBase
    {
        public T7259_iPhone_VerifyLayoutOfShippingAddressesPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutOfShippingAddressesPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7259_AndroidPhone_VerifyLayoutOfShippingAddressesPage : T7259_MobileBase
    {
        public T7259_AndroidPhone_VerifyLayoutOfShippingAddressesPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI)]
        public void LayoutOfShippingAddressesPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7259_Emulator_VerifyLayoutOfShippingAddressesPage : T7259_MobileBase
    {
        public T7259_Emulator_VerifyLayoutOfShippingAddressesPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void LayoutOfShippingAddressesPage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the entire Shipping Addresses page in Manage Account.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9775
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7259
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9775"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7259")]
    public abstract class T7259_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7259_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrange: User is on the Manage Account page and has one saved address.
            InitializeVisualTest(config);
            ManageAccount.Navigate();
            Assert.True(ManageAccount.IsCurrentPage, "Current page is not ManageAccount page");
            ManageAccount.OpenShippingAddressForm();
            ManageAccount.AddNewShippingAddressToModal(Address);
            ManageAccount.SaveShippingAddress();

            //Act: Capture a screenshot of the entire page.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture, true);
        }
    }
}
