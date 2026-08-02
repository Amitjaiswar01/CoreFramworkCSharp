using Xunit;
using Xunit.Abstractions;
using xRetry;
using OpenQA.Selenium;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.ProductDetail;

namespace LampsPlus.RegressionTests.Common.ProductDetail
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7563_Windows_VerifyCalloutAndPhoneNumberToOrderOnThePdp : T7563_DesktopBase
    {
        public T7563_Windows_VerifyCalloutAndPhoneNumberToOrderOnThePdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void CalloutAndPhoneNumberToOrderOnThePdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7563_Mac_VerifyCalloutAndPhoneNumberToOrderOnThePdp : T7563_DesktopBase
    {
        public T7563_Mac_VerifyCalloutAndPhoneNumberToOrderOnThePdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void CalloutAndPhoneNumberToOrderOnThePdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7563_iPad_VerifyCalloutAndPhoneNumberToOrderOnThePdp : T7563_DesktopBase
    {
        public T7563_iPad_VerifyCalloutAndPhoneNumberToOrderOnThePdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void CalloutAndPhoneNumberToOrderOnThePdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7563_TabletEmulator_VerifyCalloutAndPhoneNumberToOrderOnThePdp : T7563_DesktopBase
    {
        public T7563_TabletEmulator_VerifyCalloutAndPhoneNumberToOrderOnThePdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void CalloutAndPhoneNumberToOrderOnThePdp(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
    public class T7565_iPhone_VerifyCalloutAndPhoneNumberToOrderOnThePdp : T7565_MobileBase
    {
        public T7565_iPhone_VerifyCalloutAndPhoneNumberToOrderOnThePdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void CalloutAndPhoneNumberToOrderOnThePdp(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7565_Emulator_VerifyCalloutAndPhoneNumberToOrderOnThePdp : T7565_MobileBase
    {
        public T7565_Emulator_VerifyCalloutAndPhoneNumberToOrderOnThePdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]   
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void CalloutAndPhoneNumberToOrderOnThePdp(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the callout and phone number for call to order products.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8711
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7563
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8711"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7563")]
    public abstract class T7563_DesktopBase : T7563_T7565_Base
    {
        protected T7563_DesktopBase(ITestOutputHelper output) : base(output) { }
    
        protected override void VerifyToOrderCallCalloutOnSfp()
        {            
            Assert.True(ProductDetail.ToOrderCallout.CaseInsensitiveContains(ProductDetail.ToOrderString), "To order callout is not correct");
        }        
    }


    /// <summary>
    /// Verify the callout and phone number for call to order products.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8711
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7565
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8711"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7565")]
    public abstract class T7565_MobileBase : T7563_T7565_Base
    {
        protected T7565_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void Validate(string config)
        {
            InitializeFramework(config);
            
            var shortSku = ProductActions.GetShortSkuWithPhoneNumberCallToOrderCallout;

            // Navigate to SFP Page and Verify Callout
            Browser.Navigate(Urls.ProductFullPageBaseUrl + shortSku);
            Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.ToOrderCalloutClass.ToCssClassSelector()));
            VerifyToOrderCallCalloutOnSfp();

            // Navigate to PLA Page and Verify Callout
            Browser.Navigate(Urls.PlaSortPageBaseUrl + shortSku);
            Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.ToOrderCalloutClass.ToCssClassSelector()));
            VerifyToOrderCallCalloutOnSfp();

            //Navigate to PDP Page and Verify Callout 
            Browser.Navigate(Urls.LampsPlusProductsUrl + shortSku);
            Browser.Wait.IsVisibleElement(By.ClassName(ProductDetail.MoreYouLikeBorderClass));

            var textChatActual = ProductDetail.ForAvailabilityText;
            var phoneActual = ProductDetail.ForAvailabilityPhone;
            var fullText = ProductDetail.ForAvailability.Replace(" or\r\nChat", string.Empty).TrimEnd();
            var availabilityTextActual = fullText.Replace("\r\n", " ").Trim();
            Assert.True(ProductDetail.TimeVerifyCheckMobile(ProductDetail.AvailabilityString, textChatActual, ProductDetail.AvailabilityPhoneNumberString, phoneActual, ProductDetail.AvailabilityTextString, availabilityTextActual), "To order callout is not correct");
        }

        protected override void VerifyToOrderCallCalloutOnSfp()
       {
            Browser.Wait.ForDomReady();
            var textChatActual = ProductDetail.ForAvailabilityCallText.Replace("888-739-0201", string.Empty).Trim();
            var phoneActual = ProductDetail.ForAvailabilityCallText.Substring(18).Replace("\r\nor\r\nChat", string.Empty).Trim();
            var availabilityTextActual = ProductDetail.ForAvailabilityCallText;

            Assert.True(ProductDetail.TimeVerifyCheckMobile(ProductDetail.AvailabilityString, textChatActual, ProductDetail.AvailabilityPhoneNumberString, phoneActual, ProductDetail.AvailabilityTextString, availabilityTextActual), "To order callout is not correct");
        }
    }


    public abstract class T7563_T7565_Base : ProductDetailTestsBase
    {
        protected T7563_T7565_Base(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            InitializeFramework(config);

            var shortSku = ProductActions.GetShortSkuWithPhoneNumberCallToOrderCallout;
                      
            // Navigate to SFP Page and Verify Callout
            Browser.Navigate(Urls.ProductFullPageBaseUrl + shortSku);
            Browser.Wait.ForDomReady();
            VerifyToOrderCallCalloutOnSfp();

            // Navigate to PLA Page and Verify Callout
            Browser.Navigate(Urls.PlaSortPageBaseUrl + shortSku);
            Browser.Wait.ForDomReady();
            VerifyToOrderCallCalloutOnSfp();

            //Navigate to PDP Page and Verify Callout 
            Browser.Navigate(Urls.LampsPlusProductsUrl + shortSku);
            Browser.Wait.ForDomReady();
            var actualResultOrderCallout = TextActions.RegexNoTabsAndNewLines(ProductDetail.ToOrderCalloutString.ToLower().Trim());

            var orderCallout = ProductDetail.ToOrderCallout.ToLower();

            Assert.True(actualResultOrderCallout.CaseInsensitiveContains(orderCallout), "To order callout is not correct");
        }

        protected abstract void VerifyToOrderCallCalloutOnSfp();       
    }
}
