using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.CartOverview;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Common.CartOverview
{
    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T131_Windows_VerifyTotalChangeAccordingToShip : T131_DesktopBase
    {
        public T131_Windows_VerifyTotalChangeAccordingToShip(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void TotalChangeAccordingToShip(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T131_Mac_VerifyTotalChangeAccordingToShip : T131_DesktopBase
    {
        public T131_Mac_VerifyTotalChangeAccordingToShip(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void TotalChangeAccordingToShip(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T131_iPad_VerifyTotalChangeAccordingToShip : T131_DesktopBase
    {
        public T131_iPad_VerifyTotalChangeAccordingToShip(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void TotalChangeAccordingToShip(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T131_TabletEmulator_VerifyTotalChangeAccordingToShip : T131_DesktopBase
    {
        public T131_TabletEmulator_VerifyTotalChangeAccordingToShip(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void TotalChangeAccordingToShip(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.CartOverview)]
    public class T397_iPhone_VerifyTotalChangeAccordingToShip : T397_MobileBase
    {
        public T397_iPhone_VerifyTotalChangeAccordingToShip(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void TotalChangeAccordingToShip(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T397_Emulator_VerifyTotalChangeAccordingToShip : T397_MobileBase
    {
        public T397_Emulator_VerifyTotalChangeAccordingToShip(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void TotalChangeAccordingToShip(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that when the user tries to apply an invalid promo code the functionality is correct.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5241
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T131
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5241"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T131")]
    public abstract class T131_DesktopBase : T131_T397_Base
    {
        protected T131_DesktopBase(ITestOutputHelper output) : base(output) { }
    }


    /// <summary>
    /// Verify that when the user tries to apply an invalid promo code the functionality is correct.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5462
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T397
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5462"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T397")]
    public abstract class T397_MobileBase : T131_T397_Base
    {
        protected T397_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void Validate(string config)
        {
            InitializeFramework(config, Urls.HomePageUrl);

            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "P", "This test can only be executed against DBCLUST.");

            const string invalidPromo = "1234";
            ShoppingCartWorkflow.AddSingleItemToCart();

            Browser.Wait.IsVisibleElement(By.Id(CartOverview.CartPromotionalCodeId));
            Browser.ScrollIntoView(CartOverview.CartPromotionalButton,true);
            CartOverview.CartPromotionalButton.Click();
            CartOverview.PromoInputField.SendKeys(invalidPromo);
            CartOverview.PromoInputField.SendKeys(Keys.Return);

            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.PromoCodeInputErrorId.ToCssIdSelector()));

            Assert.Equals(CartOverview.NotAValidCodeString, CartOverview.PromoCodeErrorMessage.Text, "Promo code error message is not correct.");

            Browser.ScrollToTopOfWindow();
            CartOverview.CheckOutNowButton.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.ProceedPaymentId.ToCssIdSelector()));

            Assert.True(Shipping.ProceedToPaymentElement.IsInitialized, "Navigation did not bring the user to the Shipping page.");
        }
    }


    public abstract class T131_T397_Base : ShoppingCartTestsBase
    {
        protected T131_T397_Base(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            InitializeFramework(config);

            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "P", "This test can only be executed against DBCLUST.");

            const string invalidPromo = "1234";
            ShoppingCartWorkflow.AddSingleItemToCart();

            CartOverview.CartPromotionalButton.Click();
            CartOverview.PromoInputField.SendKeys(invalidPromo);
            CartOverview.PromoInputField.SendKeys(Keys.Return);

            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.PromoCodeInputErrorId.ToCssIdSelector()));

            Assert.Equals(CartOverview.NotAValidCodeString, CartOverview.PromoCodeErrorMessage.Text, "Promo code error message is not correct.");

            Browser.ScrollToTopOfWindow();
            CartOverview.CheckOutNowButton.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.ProceedPaymentId.ToCssIdSelector()));

            Assert.False(Shipping.PromotionDiscount.IsInitialized, "Promo code is displayed on Shipping page");
        }
    }
}
