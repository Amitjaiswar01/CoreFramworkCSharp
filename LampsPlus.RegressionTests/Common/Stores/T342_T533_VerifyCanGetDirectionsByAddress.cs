using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.Stores;
using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Common.Stores
{
    //[Collection(LpTraits.BatchGroup.Common.Stores)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Stores)]
    public class T342_Windows_VerifyCanGetDirectionsByAddress : T342_DesktopBase
    {
        public T342_Windows_VerifyCanGetDirectionsByAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void CanGetDirectionsByAddress(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Stores)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Stores)]
    public class T342_Mac_VerifyCanGetDirectionsByAddress : T342_DesktopBase
    {
        public T342_Mac_VerifyCanGetDirectionsByAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void CanGetDirectionsByAddress(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Stores)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Stores)]
    public class T342_iPad_VerifyCanGetDirectionsByAddress : T341_DesktopBase
    {
        public T342_iPad_VerifyCanGetDirectionsByAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void CanGetDirectionsByAddress(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Stores)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Stores)]
    public class T342_TabletEmulator_VerifyCanGetDirectionsByAddress : T341_DesktopBase
    {
        public T342_TabletEmulator_VerifyCanGetDirectionsByAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void CanGetDirectionsByAddress(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.Stores)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Stores)]
    public class T533_iPhone_VerifyCanGetDirectionsByAddress : T533_MobileBase
    {
        public T533_iPhone_VerifyCanGetDirectionsByAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void CanGetDirectionsByAddress(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Stores)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Stores)]
    public class T533_Emulator_VerifyCanGetDirectionsByAddress : T533_MobileBase
    {
        public T533_Emulator_VerifyCanGetDirectionsByAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void CanGetDirectionsByAddress(string config) => Validate(config);
    }


    /// <summary>
    /// Verify user can get directions by entering an address below the map for a specific store.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5558
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T342
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5558"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T342")]
    public abstract class T342_DesktopBase : T342_T533_Base
    {
        protected T342_DesktopBase(ITestOutputHelper output) : base(output) { }
    }


    /// <summary>
    /// Verify user can get directions by entering an address below the map for a specific store.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5225
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T533
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5225"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T533")]
    public abstract class T533_MobileBase : T342_T533_Base
    {
        protected T533_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void Validate(string config)
        {
            InitializeFramework(config);

            Browser.Navigate(Urls.ScottsdaleStoreUrl);

            Browser.Wait.IsVisibleElement(By.XPath(Stores.DirectionsButtonXpath));

            var streetAddress = Stores.StreetAddressField.Text;
            var addressLocality = Stores.AddressLocalityField.Text;
            var addressRegion = Stores.AddressRegionField.Text;
            var postalCode = Stores.PostalCodeField.Text;

            var fullAddress1 = $"{streetAddress}, {addressLocality}, {addressRegion} {postalCode}".Replace(" ", "+");
            var fullAddress2 = $"{streetAddress},{addressLocality},{addressRegion},{postalCode}".Replace(" ", "+");

            if (OperatingSystem == OperatingSystem.iPhone)
            {
                var xElementCoordinate = 0;
                var yElementCoordinate = 0;
                Browser.GetElementCoordinates(Stores.GetDirectionsButton, ref xElementCoordinate, ref yElementCoordinate, 100);
                Browser.ClickWithTapByCoordinates(xElementCoordinate, yElementCoordinate);
            }
            else
            {
                Browser.Wait.ForClickableElement(Stores.GetDirectionsButton).Click();

                Browser.WaitForNewTab(10);

                Browser.SwitchToTabByIndex(1); // This switches test's context to tab 1, the physical tab is already opened by Maps
            }

            Browser.Wait.ForCondition(() => Browser.PageUrl.Contains("www.google.com/maps"));

            Assert.Condition(() => Browser.PageUrl.Contains(fullAddress1) || Browser.PageUrl.Contains(fullAddress2), $"URL does not contain store address {fullAddress2}");

            Browser.CloseCurrentTab();
        }
    }


    public abstract class T342_T533_Base : StoresTestsBase
    {
        protected T342_T533_Base(ITestOutputHelper output) : base(output) { }
        
        protected virtual void Validate(string config)
        {
            InitializeFramework(config);

            Browser.Navigate(Urls.ScottsdaleStoreUrl);

            var streetAddress = Stores.StreetAddressField.Text;
            var addressLocality = Stores.AddressLocalityField.Text;
            var addressRegion = Stores.AddressRegionField.Text;
            var postalCode = Stores.PostalCodeField.Text;

            var fullAddress1 = $"{streetAddress}, {addressLocality}, {addressRegion} {postalCode}".Replace(" ", "+"); 
            var fullAddress2 = $"{streetAddress},{addressLocality},{addressRegion},{postalCode}".Replace(" ", "+");

            Browser.Wait.ForClickableElement(Stores.GetDirectionsButton).Click();

            Browser.WaitForNewTab(10);

            Browser.SwitchToCurrentWindow();

            Assert.Condition(() => Browser.PageUrl.Contains(fullAddress1) || Browser.PageUrl.Contains(fullAddress2), $"URL does not contain store address {fullAddress2}");

            Browser.CloseCurrentTab();
        }
    }
}
