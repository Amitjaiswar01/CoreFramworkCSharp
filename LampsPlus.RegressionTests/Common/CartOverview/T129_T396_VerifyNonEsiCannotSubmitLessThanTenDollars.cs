using System;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.CartOverview;

namespace LampsPlus.RegressionTests.Common.CartOverview
{
    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
	public class T129_Windows_VerifyUserCannotSubmitLessTenDollars : T129_DesktopBase
	{
		public T129_Windows_VerifyUserCannotSubmitLessTenDollars(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
		public void UserCannotSubmitLessTenDollars(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T129_Mac_VerifyUserCannotSubmitLessTenDollars : T129_DesktopBase
    {
        public T129_Mac_VerifyUserCannotSubmitLessTenDollars(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void UserCannotSubmitLessTenDollars(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T129_iPad_VerifyUserCannotSubmitLessTenDollars : T129_DesktopBase
    {
        public T129_iPad_VerifyUserCannotSubmitLessTenDollars(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void UserCannotSubmitLessTenDollars(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T129_TabletEmulator_VerifyUserCannotSubmitLessTenDollars : T129_DesktopBase
    {
        public T129_TabletEmulator_VerifyUserCannotSubmitLessTenDollars(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void UserCannotSubmitLessTenDollars(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.CartOverview)]
    public class T396_iPhone_VerifyUserCannotSubmitLessTenDollars : T396_MobileBase
	{
		public T396_iPhone_VerifyUserCannotSubmitLessTenDollars(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
		public void UserCannotSubmitLessTenDollars(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
	public class T396_Emulator_VerifyUserCannotSubmitLessTenDollars : T396_MobileBase
	{
		public T396_Emulator_VerifyUserCannotSubmitLessTenDollars(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
		public void UserCannotSubmitLessTenDollars(string config) => Validate(config);
	}


	/// <summary>
	/// Verify that a non-ESI user cannot submit an order that is less than $10.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5209
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T129
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5209"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T129")]
    //[Collection(LpTraits.UserRole.Customer)]
    public abstract class T129_DesktopBase : T129_T396_Base
	{
		protected T129_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected override IElement GetPaypalButtonContainer()
	    {
	        return Browser.Wait.ForDisplayedElement(CartOverview.PayPalButtonContainer);
        }

        protected override void VerifyErrorMessages()
        {
            Browser.MouseOverOnElement(CartOverview.CheckOutNowButton);
            var firstTooltip = Browser.Wait.ForElement(CartOverview.ShowUpTooltip);

            Assert.Displayed(firstTooltip, Messages.PromoRelatedMessages.TooltipMsg);

            CheckForErrorMessage(firstTooltip.Text);

            // Hover out
            Browser.MouseOverOnElement(HeaderFooter.LampsLogo);
            Browser.Wait.ForCondition(() => !firstTooltip.Displayed);

            Browser.MouseOverOnElement(CartOverview.PayPalButton);
            var secondTooltip = CartOverview.ShowUpTooltip;

            Assert.Displayed(CartOverview.ShowUpTooltip, Messages.PromoRelatedMessages.TooltipMsg);

            CheckForErrorMessage(secondTooltip.Text);
        }
    }


	/// <summary>
	/// Verify that a non-ESI user cannot submit an order that is less than $10.
	/// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5053
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T396
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5053"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T396")]
    //[Collection(LpTraits.UserRole.Customer)]
    public abstract class T396_MobileBase : T129_T396_Base
	{
		protected T396_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override IElement GetPaypalButtonContainer()
	    {
            Browser.Wait.IsVisibleElement(By.ClassName(CartOverview.PaypalButtonContainerClass));
            return Browser.Wait.ForDisplayedElement(CartOverview.PayPalButtonContainer);
        }

        protected override void VerifyErrorMessages()
        {
            CheckForErrorMessage(CartOverview.CheckoutValidationMessage.Text);
        }
    }


	public abstract class T129_T396_Base : ShoppingCartTestsBase 
	{
		protected T129_T396_Base(ITestOutputHelper output) : base(output) { }
		
		protected void Validate(string config)
		{
		    InitializeFramework(config);

            var shortSku = ProductActions.GetLessThanTenDollarItem;

            Assert.DatabaseObject(shortSku, "ProductActions.GetLessThanTenDollarItem()");

		    ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = shortSku });

            CartOverview.RemovePromoCode();

            ShoppingCartWorkflow.EnterCartZipCodeForShippingOption(CountryCodeList.US, ZipCodeList.NorthSmithfield, 0);

            Browser.Wait.UntilElementDoesntExist(CartOverview.JsCloseShippingOptionsOverlayClass);

            var paypalButtonContainer = GetPaypalButtonContainer();

		    Assert.True(CartOverview.CheckOutNowButton.GetAttribute("aria-disabled") == "true", "Checkout Now button is enabled");
		    Assert.True(ElementActions.HasClass(paypalButtonContainer, "disabled"), "PayPalButton button is enabled");

            VerifyErrorMessages();
        }

        protected abstract void VerifyErrorMessages();
        protected abstract IElement GetPaypalButtonContainer();
        protected void CheckForErrorMessage(string msg)
        {
            var verifyErrorMsg = "Minimum $10 error message is not properly displayed.";
            var validationTooltipText = msg.Split('\r', '\n'); // Split the message by returns.
            Assert.True(string.Equals(Messages.PromoRelatedMessages.SorryMsg, validationTooltipText[0], StringComparison.OrdinalIgnoreCase), verifyErrorMsg);
            Assert.True(string.Equals(Messages.PromoRelatedMessages.TenPerOrderMsg, validationTooltipText[2], StringComparison.OrdinalIgnoreCase), verifyErrorMsg);
            Assert.True(string.Equals(Messages.PromoRelatedMessages.ReadOurPolicy, validationTooltipText[4], StringComparison.OrdinalIgnoreCase), verifyErrorMsg);
        }
    }
}
