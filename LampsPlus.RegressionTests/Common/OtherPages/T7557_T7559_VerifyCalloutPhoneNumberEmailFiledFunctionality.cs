using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.Common.Sort;
using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Common.OtherPages
{
    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7557_Windows_VerifyCalloutPhoneNumberEmailFieldFunctionality : T7557_DesktopBase
    {
        public T7557_Windows_VerifyCalloutPhoneNumberEmailFieldFunctionality(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void CalloutPhoneNumberEmailFiledFunctionality(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7557_Mac_VerifyCalloutPhoneNumberEmailFieldFunctionality : T7557_DesktopBase
    {
        public T7557_Mac_VerifyCalloutPhoneNumberEmailFieldFunctionality(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void CalloutPhoneNumberEmailFiledFunctionality(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7557_iPad_VerifyCalloutPhoneNumberEmailFieldFunctionality : T7557_DesktopBase
    {
        public T7557_iPad_VerifyCalloutPhoneNumberEmailFieldFunctionality(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void CalloutPhoneNumberEmailFiledFunctionality(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7557_TabletEmulator_VerifyCalloutPhoneNumberEmailFieldFunctionality : T7557_DesktopBase
    {
        public T7557_TabletEmulator_VerifyCalloutPhoneNumberEmailFieldFunctionality(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void CalloutPhoneNumberEmailFiledFunctionality(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Sort)]
    public class T7559_iPhone_VerifyCalloutPhoneNumberEmailFiledFunctionality : T7559_MobileBase
    {
        public T7559_iPhone_VerifyCalloutPhoneNumberEmailFiledFunctionality(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void CalloutPhoneNumberEmailFiledFunctionality(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7559_Emulator_VerifyCalloutPhoneNumberEmailFiledFunctionality : T7559_MobileBase
    {
        public T7559_Emulator_VerifyCalloutPhoneNumberEmailFiledFunctionality(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void CalloutPhoneNumberEmailFiledFunctionality(string config) => Validate(config);
    }


    /// <summary>
	/// Verify the callout, phone number, and functionality of the email field for products that are not available.
	/// JIRA Task ID: https://lampstrack.lampsplus.com:8443/browse/ACD-8709
	/// Test Case ID: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7557
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8709"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7557")]
    public abstract class T7557_DesktopBase : T7557_T7559_Base
    {
        protected T7557_DesktopBase(ITestOutputHelper output) : base(output) { }

        public override void VerifyCorrectPhoneNumberDisplayed()
        {
            Assert.Equals(GlobalLocators.NotifyMyPhoneNumberString, SortPla.PhoneNumber.Text, "Correct Phone number not displayed.");
            Assert.Displayed(SortPla.ProductNotAvailableCallout, "Product Not Available call-out is not displayed.");       
            Assert.Displayed(SortPla.PlaEmail, "Email Field is not displayed.");                                           
            Assert.Displayed(SortPla.PlaNotifyMeButton, "Notify Me Button is not displayed");
        }

        public override void VerifyCorrectPhoneNumberDisplayedOnPdp()
        {
            Assert.Equals(GlobalLocators.NotifyMyPhoneNumberString, ProductDetail.ToOrderCalloutString.Trim().Substring(17, 12), "Correct Phone number not displayed on PDP.");
        }

        protected override void WaitForImageToLoad() { }

        protected override void WaitForNotifyMeBtnDisplayedOnPdp()
        {
            Browser.Wait.ForClickableElement(SortPla.PlaNotifyMeButton);
        }
    }


    /// <summary>
	/// Verify the callout, phone number, and functionality of the email field for products that are not available.
	/// Jira Task ID: https://lampstrack.lampsplus.com:8443/browse/ACD-9165
	/// Test Case ID: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7559
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9165"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7559")]
    public abstract class T7559_MobileBase : T7557_T7559_Base
    {
        protected T7559_MobileBase(ITestOutputHelper output) : base(output) { }

        public override void VerifyCorrectPhoneNumberDisplayed()
        {
            var phoneNumber = SortPla.PhoneNumber.Text;

            Assert.Equals(GlobalLocators.NotifyMyPhoneNumberString, phoneNumber, "Correct Phone number not displayed.");
            Assert.Displayed(SortPla.ProductNotAvailableCalloutNew, "Product Not Available call-out is not displayed.");   
            Assert.Displayed(SortPla.PlaEmailNew, "Email Field is not displayed.");                                           
            Assert.Displayed(SortPla.PlaNotifyMeButton, "Notify Me Button is not displayed");
        }

        public override void VerifyCorrectPhoneNumberDisplayedOnPdp()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.PdPleaseCallClass.ToCssClassSelector()));
        }

        protected override void WaitForImageToLoad()
        {
            Browser.Wait.IsVisibleElement(By.XPath(SortPla.PlaMainImageLoadedXpath));
        }

        protected override void WaitForNotifyMeBtnDisplayedOnPdp()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.PdHeroSpotId.ToCssIdSelector()));
            Browser.ScrollIntoView(ProductDetailColorPlus.PdpMoreYouMayLikeElement, true);
            Browser.Wait.ForClickableElement(SortPla.PlaNotifyMeButton);
        }
    }

    public abstract class T7557_T7559_Base : SortTestsBase
    {
        protected T7557_T7559_Base(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config);

            var shortSku = ProductActions.GetProductNotAvailableShortSku ;
            Assert.DatabaseObject(shortSku, "ProductActions.GetProductNotAvailableShortSku()");

            Browser.Navigate(Urls.ProductFullPageBaseUrl + shortSku);

            Browser.Wait.IsVisibleElement(By.CssSelector(SortPla.NotifyMeSubmitBtnId.ToCssIdSelector()));
            Browser.SwitchFocusToIframe(SortPla.PlaFullCertonaElement);

            VerifyCorrectPhoneNumberDisplayed();

            Browser.Navigate(Urls.ChandeliersDiningLivingRoomUrl + "?sfp=" + shortSku);

            WaitForImageToLoad();

            Browser.Wait.IsVisibleElement(By.CssSelector(SortPla.NotifyMeSubmitBtnId.ToCssIdSelector()));
            Browser.SwitchFocusToIframe(SortPla.PlaFrameElement);

            VerifyCorrectPhoneNumberDisplayed();

            Browser.Navigate(Urls.LampsPlusProductsUrl + shortSku);

            WaitForNotifyMeBtnDisplayedOnPdp();

            VerifyCorrectPhoneNumberDisplayedOnPdp();

            Assert.Displayed(SortPla.ProductNotAvailableCallout, "ProductNotAvailable call out is not displayed");
            Assert.Displayed(SortPla.PlaEmail, "Email Field is not displayed");
            Assert.Displayed(SortPla.PlaNotifyMeButton, "NotifyMe Button is not displayed");

            var account = new Account();
            Browser.Wait.ForDomReady();

            var email = account.EmailAddress;

            SortPla.EmailField.SendKeys(email);
            Browser.Wait.ForDomReady();

            SortPla.PlaNotifyMeButton.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.NotifyMeMessageContainerSuccessClass.ToCssClassSelector()));
            Assert.StringContains(SortPla.NotifyMyThankYouText, Email.NotifyMeMessageElement.Text, "Text content is incorrect.");

            var emailAndShortSkuFromDb = AccountActions.GetEmailAndSku(email, shortSku);

            Assert.Equals(email, emailAndShortSkuFromDb.EmailAddress, "Email address not displayed in the database.");
            Assert.Equals(shortSku, emailAndShortSkuFromDb.ShortSku, "SKU in the database not matches the SKU on the PDP.");
        }

        public abstract void VerifyCorrectPhoneNumberDisplayed();

        public abstract void VerifyCorrectPhoneNumberDisplayedOnPdp();

        protected abstract void WaitForImageToLoad();

        protected abstract void WaitForNotifyMeBtnDisplayedOnPdp();
    }
}