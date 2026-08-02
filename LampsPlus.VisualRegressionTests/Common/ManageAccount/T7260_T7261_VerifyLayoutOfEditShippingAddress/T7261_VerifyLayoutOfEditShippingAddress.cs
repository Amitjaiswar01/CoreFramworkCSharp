using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using System.Collections.Generic;
using Automation.Framework;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ManageAccount.T7260_T7261_VerifyLayoutOfEditShippingAddress
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7261_iPhone_VerifyTheLayoutOfEditShippingAddress : T7261_MobileBase
    {
        public T7261_iPhone_VerifyTheLayoutOfEditShippingAddress(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutOfEditShippingAddress(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7261_AndroidPhone_VerifyTheLayoutOfEditShippingAddress : T7261_MobileBase
    {
        public T7261_AndroidPhone_VerifyTheLayoutOfEditShippingAddress(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI)]
        public void LayoutOfEditShippingAddress(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7261_Emulator_VerifyTheLayoutOfEditShippingAddress : T7261_MobileBase
    {
        public T7261_Emulator_VerifyTheLayoutOfEditShippingAddress(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void LayoutOfEditShippingAddress(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Edit Shipping Address modal and Shipping Addresses page after adding and editing addresses.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9774
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7261
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9774"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7261")]
    public abstract class T7261_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7261_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrange: User is on the Manage Account Shipping Address page.
            InitializeVisualTest(config);
            ManageAccount.Navigate();
            Assert.True(ManageAccount.IsCurrentPage, "Current page is not ManageAccount page");

            /*Act:
            On the Manage Account page, click the Manage link in the Preferred Shipping Address section.
            Click on the ADD SHIPPING ADDRESS button.
             */
            ManageAccount.OpenShippingAddressForm();

            //Act: Capture a screenshot of the entire page.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true);

            /*Act:
            Fill out the Edit Shipping Address form.
            Click the SAVE button.
            */
            Address.State = StateCodeListUnitedStates.NV;
            ManageAccount.AddNewShippingAddressToModal(Address);
            ManageAccount.SaveShippingAddress();

            //Act: Capture a screenshot of the entire page.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true);

            //Act: Click the Edit link for the Default saved address.
            ManageAccount.OpenEditShippingAddressModal();

            //Act: Capture a screenshot of the entire page.
            ScreenCapturer.CaptureWholeOverlayModal(Browser.PageUrl, ManageAccount.GetShippingAddressScrollableOverlay(), true, true, new List<IElement> { ManageAccount.IgnoreFirstNameElement(), ManageAccount.IgnoreLastNameElement(), ManageAccount.IgnoreAddress2Element() }, ManageAccount.IgnoreAddress2Element(), 45);

            //Act: Edit the Shipping Address phone number and save.
            ManageAccount.ChangeShippingPhoneNumber();

            //Act: Capture a screenshot of the entire page.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true);
        }
    }
}
