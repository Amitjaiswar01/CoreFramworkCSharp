using System.Collections.Generic;
using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using Castle.Core.Internal;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;

namespace LampsPlus.VisualRegressionTests.Common.CartOverview
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7343_Windows_VerifyLayoutCartPromoCode : T7343_DesktopBase
    {
        public T7343_Windows_VerifyLayoutCartPromoCode(ITestOutputHelper output, T7343_T7348_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyLayoutCartPromoCode(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7343_Mac_VerifyLayoutCartPromoCode : T7343_DesktopBase
    {
        public T7343_Mac_VerifyLayoutCartPromoCode(ITestOutputHelper output, T7343_T7348_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutCartPromoCode(string config) => Validate(Validate, config);      
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7343_iPad_VerifyLayoutCartPromoCode : T7343_DesktopBase
    {
        public T7343_iPad_VerifyLayoutCartPromoCode(ITestOutputHelper output, T7343_T7348_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]

        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutCartPromoCode(string config) => Validate(Validate, config);
    }

    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7343_TabletEmulator_VerifyLayoutCartPromoCode : T7343_DesktopBase
    {
        public T7343_TabletEmulator_VerifyLayoutCartPromoCode(ITestOutputHelper output, T7343_T7348_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutCartPromoCode(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7348_iPhone_VerifyLayoutCartPromoCode : T7348_MobileBase
    {
        public T7348_iPhone_VerifyLayoutCartPromoCode(ITestOutputHelper output, T7343_T7348_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void VerifyLayoutCartPromoCode(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7348_AndroidPhone_VerifyLayoutCartPromoCode : T7348_MobileBase
    {
        public T7348_AndroidPhone_VerifyLayoutCartPromoCode(ITestOutputHelper output, T7343_T7348_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifyLayoutCartPromoCode(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7348_Emulator_VerifyLayoutCartPromoCode : T7348_MobileBase
    {
        public T7348_Emulator_VerifyLayoutCartPromoCode(ITestOutputHelper output, T7343_T7348_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyLayoutCartPromoCode(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Cart Overview page when using a Promo Code.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7452
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7343
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7452"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7343")]
    public abstract class T7343_DesktopBase : T7343_T7348_Base
    {
        protected T7343_DesktopBase(ITestOutputHelper output, T7343_T7348_SharedSku_Fixture fixture) : base(output, fixture) { }
    }


    /// <summary>
    /// Verify the layout of the Cart Overview page when using a Promo Code.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7452
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7348
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7452"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7348")]
    public abstract class T7348_MobileBase : T7343_T7348_Base
    {
        protected T7348_MobileBase(ITestOutputHelper output, T7343_T7348_SharedSku_Fixture fixture) : base(output, fixture) { }

        protected override void Validate(string config)
        {
            InitializeVisualTest(config);

            var firstSku = Fixture.ShortSku1;
            Assert.DatabaseObject(firstSku, "ProductActions.GetRandomComboKitSku");

            var secondSku = Fixture.ShortSku2;
            Assert.DatabaseObject(secondSku, "ProductActions.GetShortSkuThatMeetsMinimumOrder");

            ProductDetail.NavigateToProductDetailByShortSku(firstSku);

            Browser.ScrollToBottomOfPage(Browser.PageUrl);
            Browser.Wait.ForClickableElement(GlobalLocators.AddToCartButton);
            Browser.ClickByJs(GlobalLocators.AddToCartButton);
            Browser.Wait.IsVisibleElement(By.ClassName(CartOverview.CheckOutNowClass));

            Browser.Wait.ForDisplayedElement(CartOverview.CartPromotionalButton);
            CartOverview.CartPromotionalButton.Click();
            Browser.Wait.ForDisplayedElement(CartOverview.PromoInputField, 30);

            //Screenshot of Promo Code field.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { CartOverview.CartMoreYouMayLikeContainer, CartOverview.CartIdContainer }, true, true, CartOverview.CartIdContainer, maxDownOffset:10);

            CartOverview.PromoInputField.SendKeys("1234");
            CartOverview.PromoInputField.SendKeys(Keys.Return);
            Browser.Wait.IsVisibleElement(By.CssSelector("promoCodeInput-error".ToCssIdSelector()));

            //Screenshot of Promo Code error.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { CartOverview.CartMoreYouMayLikeContainer, CartOverview.CartIdContainer }, true, true, CartOverview.CartIdContainer, maxDownOffset: 10);

            CartOverview.PromoInputField.Clear();
            CartOverview.PromoInputField.SendKeys(PromoCodeList.AutoPromoCodeTest.Name);
            CartOverview.ApplyPromoCode();

            //Screenshot of visible screen with Promo Code applied.
            Browser.ScrollToTopOfWindow();

            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { CartOverview.CartIdElement }, true, true, CartOverview.CartIdElement, maxDownOffset: 10);

            Browser.ScrollToTopOfWindow();
            CartOverview.ClickCheckOutNowButton();
            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.ProceedPaymentId.ToCssIdSelector()));

            Browser.Wait.ForDomReady();
            CustomerAddressInformation.EnterShippingAddress(new IntAddress(), true);

            Browser.ScrollIntoView(CustomerAddressInformation.StateField);
            OrderSummaryBlock.ProceedToPaymentButton.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(Payment.PlaceYourIntlOrderButtonId.ToCssIdSelector()));

            Payment.PlaceInternationalOrder();

            Assert.False(OrderConfirmation.GetOrderId.IsNullOrEmpty(), "Order id was not found");

            //Screenshot of the entire page but ignore Order ID and email.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { OrderConfirmation.OrderIdHeading, OrderConfirmation.EmailUTagElement }, true,true, OrderConfirmation.OrderIdHeading, 150, 0, 10, 40);

            Browser.RefreshPage();
            Browser.Wait.ForDomReady();
            ProductDetail.NavigateToProductDetailByShortSku(secondSku);
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()), 30);

            Browser.Wait.ForDomReady();
            GlobalLocators.AddToCartButton.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.CheckOutNowClass.ToCssClassSelector()));

            //Screenshot of Cart page but ignore Cart ID.
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { CartOverview.CartMoreYouMayLikeContainer, CartOverview.CartIdContainer });//TODO Alternative capture
        }
    }


    public class T7343_T7348_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku1 { get; }
        public string ShortSku2 { get; }

        public T7343_T7348_SharedSku_Fixture()
        {
            ShortSku1 = ProductActions.GetRandomComboKitSku;
            ShortSku2 = ProductActions.GetShortSkuThatMeetsMinimumOrder;
        }
    }


    public abstract class T7343_T7348_Base : VisualTestsBase, IClassFixture<T7343_T7348_SharedSku_Fixture>
    {
        protected readonly T7343_T7348_SharedSku_Fixture Fixture;

        protected T7343_T7348_Base(ITestOutputHelper output, T7343_T7348_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            InitializeVisualTest(config);

            var secondSku = Fixture.ShortSku2;
            Assert.DatabaseObject(secondSku, "ProductActions.GetShortSkuThatMeetsMinimumOrder");

            ProductDetail.NavigateToProductDetailByShortSku(secondSku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));
            GlobalLocators.AddToCartButton.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.CheckOutNowClass.ToCssClassSelector()), 30);

            Browser.Wait.ForDisplayedElement(CartOverview.CartPromotionalButton);
            CartOverview.CartPromotionalButton.Click();

            //1 Screenshot of Promo Code Link.
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { CartOverview.CartMoreYouMayLikeContainer, CartOverview.CartIdContainer }, true, true);

            CartOverview.PromoInputField.SendKeys("1234");
            CartOverview.PromoInputField.SendKeys(Keys.Return);
            CartOverview.PromoCodeApplyButton.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector("promoCodeInput-error".ToCssIdSelector()));

            //2 Screenshot of Promo Code error.
            Browser.ScrollToTopOfWindow();
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { CartOverview.CartMoreYouMayLikeContainer, CartOverview.CartIdContainer },true, true);

            CartOverview.PromoInputField.Clear();
            CartOverview.PromoInputField.SendKeys(PromoCodeList.AutoPromoCodeTest.Name);
            CartOverview.ApplyPromoCode();

            //3 Screenshot of visible screen with Promo Code applied.
            Browser.ScrollToTopOfWindow();
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { CartOverview.CartMoreYouMayLikeContainer, CartOverview.CartIdContainer });
        }
    }
}
