using System;
using System.Linq;
using System.Web.UI;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using OpenQA.Selenium;
using Automation.Framework.Core;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.Stores;

namespace LampsPlus.RegressionTests.Common.Stores
{
    //[Collection(LpTraits.BatchGroup.Common.Stores)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Stores)]
    public class T341_Windows_VerifyStoresPagesUpdateCorrectly : T341_DesktopBase
    {
        public T341_Windows_VerifyStoresPagesUpdateCorrectly(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void StoresPagesUpdateCorrectly(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Stores)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Stores)]
    public class T341_Mac_VerifyStoresPagesUpdateCorrectly : T341_DesktopBase
    {
        public T341_Mac_VerifyStoresPagesUpdateCorrectly(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T341. Rework - CI-3360")]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void StoresPagesUpdateCorrectly(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Stores)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Stores)]
    public class T341_iPad_VerifyStoresPagesUpdateCorrectly : T341_DesktopBase
    {
        public T341_iPad_VerifyStoresPagesUpdateCorrectly(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void StoresPagesUpdateCorrectly(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Stores)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Stores)]
    public class T341_TabletEmulator_VerifyStoresPagesUpdateCorrectly : T341_DesktopBase
    {
        public T341_TabletEmulator_VerifyStoresPagesUpdateCorrectly(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void StoresPagesUpdateCorrectly(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.Stores)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Stores)]
    public class T532_iPhone_VerifyStoresPagesUpdateCorrectly : T532_MobileBase
    {
        public T532_iPhone_VerifyStoresPagesUpdateCorrectly(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void StoresPagesUpdateCorrectly(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Stores)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Stores)]
    public class T532_Emulator_VerifyStoresPagesUpdateCorrectly : T532_MobileBase
    {
        public T532_Emulator_VerifyStoresPagesUpdateCorrectly(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void StoresPagesUpdateCorrectly(string config) => Validate(config);
    }


    /// <summary>
    /// Verify when user selects different region/ZIP to search, the page updates correctly.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5510
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T341
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5510"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T341")]
    public abstract class T341_DesktopBase : T341_T532_Base
    {
        protected T341_DesktopBase(ITestOutputHelper output) : base(output) { }
        
        protected override void StoreResultDetails()
        {
            foreach (var store in Stores.StoreResults)
            {
                var storeDetailsText = Stores.GetLinkTextFromStoreResult(store);

                Assert.True(storeDetailsText[0].Equals("store details", StringComparison.OrdinalIgnoreCase), "Store listing doesn't contain Store Details link.");
                Assert.True(storeDetailsText[1].Equals("schedule appointment", StringComparison.OrdinalIgnoreCase), "Store listing doesn't contain Schedule Appointment link.");
                Assert.True(storeDetailsText[2].Equals("make this my store", StringComparison.OrdinalIgnoreCase), "Store listing doesn't contain Make This My Store button.");
            }
        }
        
        protected override void CheckStoreWorkFlow()
        {
            // Select another random region except the current one you are on
            var storesLinks = Stores.LampsPlusStoreRegionLinks.Where(x => TextActions.NormalizeUrl(x.GetAttribute(HtmlTextWriterAttribute.Href.ToString())) != Urls.OregonStoreUrl );
            ElementActions.SelectRandom(storesLinks).Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(Stores.MakeThisMyStoreClass.ToCssClassSelector()));

            StoreResultDetails();

            //Select Random Store
            Stores.SelectedStoreElement = Stores.RandomStoreElement;

            var selectedStoreName = Stores.SelectedStoreDetailsName.Text.ToLower();

            Stores.SelectedStoreDetailsLink.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(Stores.MakeThisMyStoreClass.ToCssClassSelector()));

            // Verify page navigated to store
            Assert.StringContains(Stores.BreadcrumbText, selectedStoreName, "Didn't navigate to store page after clicking Store Details link on region page.");
            Assert.Displayed(Stores.MakeThisMyStoreButton, "Make This My Store button is not displayed on page.");

            Browser.Wait.ForDomReady();
            Stores.ClickMakeThisMyStoreButton();
            Browser.Wait.ForDisplayedElement(Stores.MyStoreButton, 2);

            Assert.Equals("my store", Stores.MakeThisMyStoreButton.Text.ToLower(), "Make This My Store Button text not changed to My Store after clicking it.");

            Browser.Wait.FiniteTime(3000);
            Browser.MouseOverOnElement(Stores.BopusSubmenu, Stores.BopusSubmenu);

            Browser.Wait.IsVisibleElement(By.ClassName(Stores.HeaderDropDownsMenuClass));
        
            var dropdownMyStoreName = Stores.DropdownMyStoreName.ToLower();
            Assert.Equals(dropdownMyStoreName, selectedStoreName, $"My Store name ({dropdownMyStoreName}) in dropdown doesn't match selected store ({selectedStoreName}).");

            Assert.StringContains(TextActions.RemoveWhitespace(Stores.StoreAddress.ToLower()), TextActions.RemoveWhitespace(Stores.DropdownMyStoreAddress.ToLower()), "My Store address in dropdown not the address of selected store.");
        }
    }


    /// <summary>
    /// Verify when user selects different region/ZIP to search, the page updates correctly.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5294
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T532
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5294"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T532")]
    public abstract class T532_MobileBase : T341_T532_Base
    {
        protected T532_MobileBase(ITestOutputHelper output) : base(output) { }
      
        protected override void StoreResultDetails()
        {
            foreach (var store in Stores.StoreResults)
            {
                Assert.True(Stores.GetDetailBtnStoreResult(store) == "DETAILS", "Store listing doesn't contain Store Details link.");
                Assert.True(Stores.GetMakeThisMyStoreResult(store).Contains("MY STORE"), "Store listing doesn't contain Make This My Store button.");
            }
        }
        
        protected override void CheckStoreWorkFlow()
        {
            //Allow iOS location usage once
            if (Browser.Device != null)
            {
                if (Browser.Device.IsIphone)
                {
                    ((IphoneBrowser)Browser).AllowLocationOnce();
                }
            }

            Browser.Wait.IsVisibleElement(By.CssSelector(Stores.LpIconDetailsClass.ToCssClassSelector()));
            var storesLinks = Stores.LampsPlusStoreRegionLinks.Where(x => TextActions.NormalizeUrl(x.GetAttribute(HtmlTextWriterAttribute.Href.ToString())) != Urls.OregonStoreUrl);
            Browser.ScrollIntoView(Stores.StorePickerSubmitElement);
            Browser.Wait.ForDomReady();
            Browser.ClickByJs(ElementActions.SelectRandom(storesLinks));

            Browser.Wait.ForDomReady();

            Browser.Wait.IsVisibleElement(By.CssSelector(Stores.MakeMyStoreClass.ToCssClassSelector()));

            StoreResultDetails();

            Browser.Wait.ForClickableElement(Stores.StoreDetailBtns[0]).Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(Stores.CallForAppointmentInStoreId.ToCssIdSelector()));

            Assert.Displayed(Stores.AddressLocalityField, "Location Name is not displayed on page.");
            Assert.Displayed(Stores.MakeThisMyStoreButton, "My store Button  is not displayed on page.");
            Assert.Displayed(Stores.LpIconCallElement, "Call Store Icon is not displayed on page.");
            Assert.Displayed(Stores.LpIconDirectionsElement, "Directions Icon is not displayed on page.");
            Assert.Displayed(Stores.LpIconCalendarElement, "Appointments Icon is not displayed on page.");
            Assert.Displayed(Stores.LpIconCouponElement, "Coupon Icon is not displayed on page.");

            Browser.Wait.IsVisibleElement(By.CssSelector(Stores.MakeThisMyStoreClass.ToCssClassSelector()));

            Stores.MakeThisMyStoreButton.Click();
            Browser.ClickOnButtonMultipleTimes(Stores.MakeThisMyStoreButton, 5, Stores.IsStoreSetToMyStore);

            Browser.Wait.ForDomReady();
            Assert.True(Stores.MakeThisMyStoreButton.Text == "My Store", "Make this my store did not change to my store after clicking.");

            Browser.Navigate(Urls.StoresPageUrl);

            Browser.Wait.IsVisibleElement(By.CssSelector(Stores.LpIconDetailsClass.ToCssClassSelector()));

            //Select Random Store
            var storeNearMeLink = Stores.RandomStoreNearMeElement;
            var storeNearMeUrl = storeNearMeLink.GetAttribute("href");

            if (!storeNearMeUrl.EndsWith("/"))
            {
                storeNearMeUrl = storeNearMeUrl + "/";
            }

            storeNearMeLink.Click();

            Browser.Wait.IsVisibleElement(By.Id(Stores.CallForAppointmentInStoreId));

            Assert.True(storeNearMeUrl == Browser.PageUrl, "Store did not navigate to the correct area.");
        }
    }


    public abstract class T341_T532_Base : StoresTestsBase
    {
        protected T341_T532_Base(ITestOutputHelper output) : base(output) { }
        
        protected void Validate(string config)
        {
            InitializeFramework(config);

            Browser.Navigate(Urls.StoresPageUrl);
            
            CheckStoreWorkFlow();
        }

        /// <summary>
        /// Check Store Results Details.
        /// </summary>
        protected abstract void StoreResultDetails();

        /// <summary>
        /// Check Store Workflow.
        /// </summary>
        protected abstract void CheckStoreWorkFlow();
    }
}
