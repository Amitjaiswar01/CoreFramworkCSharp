using System;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using OpenQA.Selenium;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.OtherPages
{
    //[Collection(LpTraits.BatchGroup.Common.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
    public class T7608_Windows_VerifyStoreLocatorQueryStringParameterForSfpPages : T7608_DesktopBase
    {
        public T7608_Windows_VerifyStoreLocatorQueryStringParameterForSfpPages(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void StoreLocatorQueryStringParameterForSfp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
    public class T7608_Mac_VerifyStoreLocatorQueryStringParameterForSfpPages : T7608_DesktopBase
    {
        public T7608_Mac_VerifyStoreLocatorQueryStringParameterForSfpPages(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void StoreLocatorQueryStringParameterForSfp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
    public class T7608_iPad_VerifyStoreLocatorQueryStringParameterForSfpPages : T7608_DesktopBase
    {
        public T7608_iPad_VerifyStoreLocatorQueryStringParameterForSfpPages(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void StoreLocatorQueryStringParameterForSfp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
    public class T7608_TabletEmulator_VerifyStoreLocatorQueryStringParameterForSfpPages : T7608_DesktopBase
    {
        public T7608_TabletEmulator_VerifyStoreLocatorQueryStringParameterForSfpPages(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void StoreLocatorQueryStringParameterForSfp(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Certona)]
    //[Collection(LpTraits.BatchGroup.Common.Certona)]
    public class T7609_IPhone_VerifyStoreLocatorQueryStringParameterForSfpPages : T7609_MobileBase
    {
        public T7609_IPhone_VerifyStoreLocatorQueryStringParameterForSfpPages(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void StoreLocatorQueryStringParameterForSfp(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
    //[Collection(LpTraits.BatchGroup.Common.Certona)]
    public class T7609_Emulator_VerifyStoreLocatorQueryStringParameterForSfpPages : T7609_MobileBase
    {
        public T7609_Emulator_VerifyStoreLocatorQueryStringParameterForSfpPages(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void StoreLocatorQueryStringParameterForSfp(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the store locator query string parameter for SFP pages.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8816
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7608
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8816"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7608")]
    public abstract class T7608_DesktopBase : T7608_T7609_Base
    {
        protected T7608_DesktopBase(ITestOutputHelper output) : base(output) { }
    }


    /// <summary>
    /// Verify the store locator query string parameter for SFP pages.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8816
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7609
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8816"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7609")]
    public abstract class T7609_MobileBase : T7608_T7609_Base
    {
        protected T7609_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void Validate(string config)
        {
            InitializeFramework(config);
            Browser.Wait.ForDomReady();

            var shortSku = ProductActions.GetAnySkuWithProductDetailPage;
            Assert.DatabaseObject(shortSku, "ProductActions.GetAnySkuWithProductDetailPage");

            var location = ProductActions.GetStoreLocation();
            Assert.DatabaseObject(location, "ProductActions.GetStoreLocation()");

            var url = Urls.HomePageUrl;
            Browser.Navigate($"{url}sfp/{shortSku}/?cm_mmc=GOO-SH-_-NA-_-NA-_-{shortSku}&store={location.LocationNumber}");
            Browser.Wait.ForDomReady();

            Assert.Equals(SortFullPageCertona.StoreName.Text, $"Available at our {location.LocationStoreName} Location", "Not correct Location City Displayed");

            Assert.Equals("Store Address & Hours", SortFullPageCertona.StoreAddressAndHours.Text, "Link Store Address & Hours is not displayed");

            var streetAddressDb = location.LocationAddress;
            var locationStateNameDb = location.LocationStoreName;
            var locationCityDb = location.LocationCity;
            var locationStateDb = location.LocationState;
            var locationZipCodeDb = location.LocationZip;

            Browser.Wait.ForClickableElement(SortFullPageCertona.StoreAddressAndHours).Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(Stores.StoreDetailsBtnClass.ToCssClassSelector()));

            Assert.Equals(SortFullPageCertona.AddressInformation.Text, $"Lamps Plus {locationStateNameDb}", "Line is not displayed");
            Assert.Equals(streetAddressDb.TrimEnd(), SortFullPageCertona.StreetAddressField.Text.TrimEnd(), "Street Address not matching");
            Assert.Equals(locationCityDb, SortFullPageCertona.AddressLocalityField.Text, "Address Locality not matching");
            Assert.Equals(locationStateDb, SortFullPageCertona.AddressRegionField.Text, "Address Region not matching");
            Assert.Equals(locationZipCodeDb, SortFullPageCertona.PostalCodeField.Text, "Postal Code not matching");
        }
    }

    public abstract class T7608_T7609_Base : TestsBase
    {
        protected T7608_T7609_Base(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            InitializeFramework(config);
            Browser.Wait.ForDomReady(1000);

            //Get shortSku from db
            var shortSku = ProductActions.GetAnySkuWithProductDetailPage;
            Assert.DatabaseObject(shortSku, "ProductActions.GetAnySkuWithProductDetailPage");

            //Get Store Location from db
            Browser.Wait.ForDomReady(1000);
            var location = ProductActions.GetStoreLocation();
            Assert.DatabaseObject(location, "ProductActions.GetStoreLocation()");

            //navigate to HomePage
            var url = Urls.HomePageUrl;
            Browser.Navigate($"{url}sfp/{shortSku}/?cm_mmc=GOO-SH-_-NA-_-NA-_-{shortSku}&store={location.LocationNumber}");
            Browser.Wait.ForDomReady();

            var actualLocationName = TextActions.RegexNoTabsAndNewLines(SortFullPageCertona.StoreName.Text.Trim());
            Assert.Equals($"Available at our {location.LocationStoreName} Location", actualLocationName, "Not correct Location City Displayed");

            Assert.Equals("Store Address & Hours", SortFullPageCertona.StoreAddressAndHours.Text, "Link Store Address & Hours is not displayed");

            //Get data from db
            var streetAddressDb = location.LocationAddress;
            var locationStateNameDb = location.LocationStoreName;
            var locationCityDb = location.LocationCity;
            var locationStateDb = location.LocationState;
            var locationZipCodeDb = location.LocationZip;
            var locationPhoneDb = location.LocationPhone;
            var locationTextDb = location.LocationSms;

            Browser.Wait.ForClickableElement(SortFullPageCertona.StoreAddressAndHours).Click();
            Browser.Wait.ForDisplayedElement(GlobalLocators.Iframe);

            var streetAddress = SortFullPageCertona.StreetAddressField.Text.Trim();
            var addressLocality = SortFullPageCertona.AddressLocalityField.Text.Replace(",", "").Trim();
            var addressRegion = SortFullPageCertona.AddressRegionField.Text.Replace(" ", "").Trim();
            var postalCode = SortFullPageCertona.PostalCodeField.Text.Replace(" ", "").Trim();
            var phoneNumber = SortFullPageCertona.PhoneAndTextNumbers(2).GetAttribute("content");
           
            //Verify db with lpsite
            Assert.Equals(SortFullPageCertona.AddressInformation.Text, $"Lamps Plus {locationStateNameDb}", "Line is not displayed");
            Assert.Equals(streetAddressDb.TrimEnd(), streetAddress, "Street Address not matching");
            Assert.Equals(locationCityDb, addressLocality, "Address Locality not matching");
            Assert.Equals(locationStateDb, addressRegion, "Address Region not matching");
            Assert.Equals(locationZipCodeDb, postalCode, "Postal Code not matching");
            Assert.Equals(locationPhoneDb, phoneNumber, "Phone Number not matching");

            string textDatabaseNumber = "TEXT : " + locationTextDb;
            var text = SortFullPageCertona.TextLabel.Replace(textDatabaseNumber, string.Empty).Trim();

            var weekDays = new TimeSpan(10, 00, 00);
            var weekDaysEnd = new TimeSpan(19, 00, 00);
            var saturdayEnd = new TimeSpan(18, 00, 00);
            var sundayStart = new TimeSpan(11, 00, 00);
            var thisDay = DateTime.Today.DayOfWeek.ToString();
            var nowOrig = DateTime.Now.TimeOfDay;
            {
                if (nowOrig.Seconds > 45)
                {
                    nowOrig = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute + 1, 00);
                }
                else
                {
                    nowOrig = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 00);
                }
                if (thisDay == "Sunday")
                {
                    
                    if (DateTimeHelper.IsTimeInBetween(sundayStart, saturdayEnd, nowOrig))
                    { 
                        //when store hours is open
                        var textNumber = SortFullPageCertona.PhoneAndTextNumbers(3).GetAttribute("content");
                        Assert.Equals(locationTextDb, textNumber, "Text number not matching");
                        Assert.Displayed(SortFullPageCertona.PhoneAndTextNumbers(3), "Text is not visible on site when store is open");
                    }
                    else
                    {
                        //when store hours is close 
                        var textOne = SortFullPageCertona.TextLabel;
                        Assert.Equals(text, textOne, "Text is visible on site when store is close.");
                    }
                }
                else if (thisDay == "Saturday")
                {

                    if (DateTimeHelper.IsTimeInBetween(weekDays, saturdayEnd, nowOrig))
                    {  
                        //when store hours is open
                        var textNumber = SortFullPageCertona.PhoneAndTextNumbers(3).GetAttribute("content");
                        Assert.Equals(locationTextDb, textNumber, "Text number not matching");
                        Assert.Displayed(SortFullPageCertona.PhoneAndTextNumbers(3), "Text is not visible on site when store is open");
                    }
                    else
                    {  
                        //when store hours is close
                        var textOne = SortFullPageCertona.TextLabel;
                        Assert.Equals(text, textOne, "Text is visible on site when store is close.");
                    }
                }
                else
                {
                    
                    if (DateTimeHelper.IsTimeInBetween(weekDays, weekDaysEnd, nowOrig))
                    {  
                        //when store hours is open on Weekdays
                        var textNumber = SortFullPageCertona.PhoneAndTextNumbers(3).GetAttribute("content");
                        Assert.Equals(locationTextDb, textNumber, "Text number not matching");
                        Assert.Displayed(SortFullPageCertona.PhoneAndTextNumbers(3), "Text is not visible on site when store is open");
                    }
                    else
                    {
                        //when store hours is close on Weekdays
                        var textOne = SortFullPageCertona.TextLabel;
                        Assert.Equals(text, textOne, "Text is visible on site when store is close.");
                    }
                }
            }
        }
    }
}
