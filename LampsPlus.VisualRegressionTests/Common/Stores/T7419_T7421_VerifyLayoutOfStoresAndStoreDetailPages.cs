using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using OpenQA.Selenium;
using Automation.Framework.Core;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.Common.Stores
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7419_Windows_VerifyLayoutOfStoresAndStoreDetailsPage : T7419_DesktopBase
    {
        public T7419_Windows_VerifyLayoutOfStoresAndStoreDetailsPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfStoresAndStoreDetails(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7419_Mac_VerifyLayoutOfStoresAndStoreDetailsPage : T7419_DesktopBase
    {
        public T7419_Mac_VerifyLayoutOfStoresAndStoreDetailsPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfStoresAndStoreDetails(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7419_iPad_VerifyLayoutOfStoresAndStoreDetailsPage : T7419_DesktopBase
    {
        public T7419_iPad_VerifyLayoutOfStoresAndStoreDetailsPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfStoresAndStoreDetails(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7419_TabletEmulator_VerifyLayoutOfStoresAndStoreDetailsPage : T7419_DesktopBase
    {
        public T7419_TabletEmulator_VerifyLayoutOfStoresAndStoreDetailsPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfStoresAndStoreDetails(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7421_iPhone_VerifyLayoutOfStoresAndStoreDetailsPage : T7421_MobileBase
    {
        public T7421_iPhone_VerifyLayoutOfStoresAndStoreDetailsPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutOfStoresAndStoreDetails(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7421_Android_VerifyLayoutOfStoresAndStoreDetailsPage : T7421_MobileBase
    {
        public T7421_Android_VerifyLayoutOfStoresAndStoreDetailsPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfStoresAndStoreDetails(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7421_Emulator_VerifyLayoutOfStoresAndStoreDetailsPage : T7421_MobileBase
    {
        public T7421_Emulator_VerifyLayoutOfStoresAndStoreDetailsPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfStoresAndStoreDetails(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Stores and Store Detail Page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7590
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7419
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7590"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7419")]
    public abstract class T7419_DesktopBase : T7419_T7421_Base
    {
        protected T7419_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected override void StoresPageCapture()
        {
            Browser.Wait.ForDomReady();
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true);
            Browser.Wait.ForDomReady();
        }

        protected override void StorePagesWorkflow()
        {
            Browser.Wait.WaitForAjaxComplete();
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, useStitchMode: true);

            //Click 'Store Details' link and screenshot entire page
            Stores.SelectedStoreElement = Stores.RandomStoreElement;

            Stores.StoreDetailsRegionLinks[0].Click();
            Browser.Wait.WaitForAjaxComplete();
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, useStitchMode: true);

            //Click 'Make this my store' button and screenshot visible screen
            Stores.ClickMakeThisMyStoreButton();
            Browser.Wait.ForDomReady();
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }


    /// <summary>
    /// Verify the layout of the Stores and Store Detail Page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7590
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7421
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7590"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7421")]
    public abstract class T7421_MobileBase : T7419_T7421_Base
    {
        protected T7421_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected override void StoresPageCapture()
        {
            //Allow iOS location usage once
            if (Browser.Device != null)
            {
                if (Browser.Device.IsIphone)
                {
                    ((IphoneBrowser)Browser).AllowLocationOnce();
                }
            }

            //NOTE: Finds stores by zipcode to have identical baseline and target captures
            Browser.Locate.ElementBySelector(Stores.StoreZipCodeInputId.ToCssIdSelector()).SendKeys(ZipCodeList.Chatsworth);
            Stores.StorePickerSubmitElement.Click();
            Browser.Wait.ForDomReady();
          
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true, true);
            Browser.Wait.ForDomReady();
        }

        protected override void StorePagesWorkflow()
        {
            Browser.Wait.IsVisibleElement(By.XPath(Stores.ScottsdaleStoreXpath));
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true);

            //On the Lighting Stores in 'Region' page, click on the DETAILS icon for any Store location.
            Stores.LpIconDetailsButton.Click();
            Browser.Wait.IsVisibleElement(By.XPath(Stores.StoresOptionsXpath));
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true);

            //Click 'Make this my store' button and screenshot visible screen
            Stores.MakeThisMyStoreButton.Click();
            Browser.Wait.ForDomReady();
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }


    public abstract class T7419_T7421_Base : VisualTestsBase, IClassFixture<FixtureBase>
    {
        protected T7419_T7421_Base(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        /// <summary> 
        /// Verify the layout of Stores and Store Detail pages.
        /// </summary>
        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            //Stores Page loads and screenshot entire page
            Browser.Navigate(Urls.StoresPageUrl);

            StoresPageCapture();

            //Click on Region and screenshot entire page
            Browser.ScrollIntoView(Stores.LampsPlusStoreRegionLinks[0],alignToBottom:true);
            Stores.LampsPlusStoreRegionLinks[0].Click();

            StorePagesWorkflow();
        }

        protected abstract void StorePagesWorkflow();

        protected abstract void StoresPageCapture();
    }
}
