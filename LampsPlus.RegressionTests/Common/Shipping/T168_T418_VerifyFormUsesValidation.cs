using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.AutomationFramework.Workflow.Mobile;
using xRetry;

namespace LampsPlus.RegressionTests.Common.Shipping
{
    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T168_Windows_VerifyShippingInfoFormUsesValidation : T168_DesktopBase
	{
        public T168_Windows_VerifyShippingInfoFormUsesValidation(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void ShippingInfoFormUsesValidation(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T168_Mac_VerifyShippingInfoFormUsesValidation : T168_DesktopBase
	{
		public T168_Mac_VerifyShippingInfoFormUsesValidation(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
		public void ShippingInfoFormUsesValidation(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T168_iPad_VerifyShippingInfoFormUsesValidation : T168_DesktopBase
    {
        public T168_iPad_VerifyShippingInfoFormUsesValidation(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void ShippingInfoFormUsesValidation(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T168_TabletEmulator_VerifyShippingInfoFormUsesValidation : T168_DesktopBase
    {
        public T168_TabletEmulator_VerifyShippingInfoFormUsesValidation(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void ShippingInfoFormUsesValidation(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Shipping)]
	public class T418_iPhone_VerifyShippingInfoFormUsesValidation : T418_MobileBase
	{
        public T418_iPhone_VerifyShippingInfoFormUsesValidation(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void ShippingInfoFormUsesValidation(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Shipping)]
    public class T418_AndroidPhone_VerifyShippingInfoFormUsesValidation : T418_MobileBase
	{
        public T418_AndroidPhone_VerifyShippingInfoFormUsesValidation(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
		public void ShippingInfoFormUsesValidation(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T418_Emulator_VerifyShippingInfoFormUsesValidation : T418_MobileBase
	{
        public T418_Emulator_VerifyShippingInfoFormUsesValidation(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
		public void ShippingInfoFormUsesValidation(string config) => Validate(config);
	}


	/// <summary>
	/// Verify the validation for all required fields on the Shipping Page.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5190
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T168
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5190"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T168")]
	public abstract class T168_DesktopBase : T168_T418_Base
	{
		protected T168_DesktopBase(ITestOutputHelper output) : base(output) { }
        protected override void HandleSelectElement(IElement element, string key)
        {
            CustomerAddressInformation.PhoneField.Click();
            CustomerAddressInformation.FillFormSelectByValue(element, key);
            CustomerAddressInformation.PhoneField.Click();
        }

        protected override void VerifyAllRequiredShippingFormControls()
        {
            //click proceed to payment to trigger all the errors.
            CustomerAddressInformation.ProceedToPayment();


            CustomerAddressInformation.PhoneField.Click();
            CustomerAddressInformation.PhoneField.SendKeys(CustomerAddressInformation.Address.Phone);

            Assert.False(CustomerAddressInformation.FormControlValidationErrorMessageDisplayed(CustomerAddressInformation.PhoneField), "Phone Field has a validation error message. Expected No Validation Message");

            foreach (var formControl in CustomerAddressInformation.RequiredFormControls())
            {
                var element = formControl.Value;
                CustomerAddressInformation.ClearFormControl(element);

                Assert.True(CustomerAddressInformation.FormControlValidationErrorMessageDisplayed(element), $"Element #{element.GetAttribute("Id")} did not have a validation error message. Expected Validation Message");

                Assert.PageUrl(Urls.ShippingPageUrl, Browser.PageUrl, $"Expected to be on the Shipping Info page but on {Browser.PageUrl}");

                var elementTag = element.TagName;

                if (elementTag.CaseInsensitiveContains("select"))
                {
                    HandleSelectElement(element, formControl.Key);
                }
                else
                {
                    CustomerAddressInformation.FillFormControlByText(element, formControl.Key);
                }

                Assert.False(CustomerAddressInformation.FormControlValidationErrorMessageDisplayed(element), $"Element #{element.GetAttribute("Id")} has a validation error message. Expected No Validation Message");
            }
        }

        protected override void ClickAddAnotherAddressFieldLink()
        { 
            Browser.Wait.ForDomReady();
            Browser.Navigate(Urls.ShippingPageUrl);
            Browser.Wait.IsVisibleElement(By.XPath(CustomerAddressInformation.AddAnotherAddressFieldLinkXpath));
            Browser.ClickByJs(CustomerAddressInformation.AddAnotherAddressFieldLink);
        }
    }


	/// <summary>
	/// Verify the validation for all required fields on the Shipping Page.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5524
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T418
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5524"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T418")]
	public abstract class T418_MobileBase : T168_T418_Base
	{
		protected T418_MobileBase(ITestOutputHelper output) : base(output) { }
        protected override void HandleSelectElement(IElement element, string key)
        {
            element.Click();
            GlobalLocators.ClickDropdownByValue(Browser.Locate.ElementBySelector(MobileManageAccountWorkflow.NewStateShippingParentSelector), key);
        }

        protected override void VerifyAllRequiredShippingFormControls()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.ProceedPaymentId.ToCssIdSelector()),30);
            //click proceed to payment to trigger all the errors.
            CustomerAddressInformation.ProceedToPayment();

            CustomerAddressInformation.CheckShippingFormIsLoaded();

            Browser.Wait.ForCondition(() => Shipping.ErrorMessage.Text == Payment.ErrorMessageString);
            //Validate phone field first otherwise all 10 digits are not entered correctly
            Assert.True(CustomerAddressInformation.FormControlValidationErrorMessageDisplayed(CustomerAddressInformation.PhoneField), "Phone Field did not have a validation error message. Expected Validation Message");

            CustomerAddressInformation.PhoneField.Click();
            CustomerAddressInformation.PhoneField.SendKeys(CustomerAddressInformation.Address.Phone);

            Assert.False(CustomerAddressInformation.FormControlValidationErrorMessageDisplayed(CustomerAddressInformation.PhoneField), "Phone Field has a validation error message. Expected No Validation Message");

            IElement StateValidationMessage = Browser.Locate.ElementBySelector("#singleShippingState-error");

            foreach (var formControl in CustomerAddressInformation.RequiredFormControls())
            {
                if (formControl.Key == "CA")
                {
                    //Steps to verify mobile state dropdown
                    StateValidationMessage = Browser.Locate.ElementBySelector("#singleShippingState-error");
                    Assert.True(StateValidationMessage.Displayed,
                        "State Field did not have a validation error message. Expected Validation Message");
                    HandleSelectElement(CustomerAddressInformation.StateField, formControl.Key);
                }
                if (formControl.Key != "CA")
                {
                    var element = formControl.Value;
                    CustomerAddressInformation.ClearFormControl(element);

                    Assert.True(CustomerAddressInformation.FormControlValidationErrorMessageDisplayed(element),
                        $"Element #{element.GetAttribute("Id")} did not have a validation error message. Expected Validation Message");

                    Assert.PageUrl(Urls.ShippingPageUrl, Browser.PageUrl,
                        $"Expected to be on the Shipping Info page but on {Browser.PageUrl}");

                    if (element.TagName == "select")
                    {
                        HandleSelectElement(element, formControl.Key);
                    }
                    else
                    {
                        CustomerAddressInformation.FillFormControlByText(element, formControl.Key);
                    }

                    Assert.False(CustomerAddressInformation.FormControlValidationErrorMessageDisplayed(element),
                        $"Element #{element.GetAttribute("Id")} has a validation error message. Expected No Validation Message");
                }
            }

            if (StateValidationMessage.Displayed)
            {
                CustomerAddressInformation.StateField.SendKeys(Keys.Tab); //Added for an Automation only issue: The error message doesn't always disappear after selecting a state.
            }

            Assert.False(StateValidationMessage.Displayed, "State Field has a validation error message. Expected No Validation Message");
        }

        protected override void ClickAddAnotherAddressFieldLink()
        {
            Browser.Navigate(Urls.ShippingPageUrl);

            Browser.Wait.IsVisibleElement(By.CssSelector(CustomerAddressInformation
                .AddAnotherAddressFieldLinkClass.ToCssClassSelector()));

            Browser.ClickByJs(CustomerAddressInformation.AddAnotherAddressFieldLink);
        }
    }


    public abstract class T168_T418_Base : ShippingInfoTestsBase
    {
        protected T168_T418_Base(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config);

            ShoppingCartWorkflow.CheckoutWithSingleItem();

            Browser.SwitchToCurrentWindow();

            VerifyAllRequiredShippingFormControls();

            var paymentButton = Browser.Locate.ElementById(Shipping.ProceedPaymentId);

            paymentButton.Click();

            Browser.Wait.ForPage(Urls.PaymentPageUrl, 20);

            Assert.False(Browser.PageUrl.Equals(Urls.ShippingPageUrl), $"Browser's Url was still {Urls.ShippingPageUrl}. Expected to be redirected after Proceed to Payment was clicked.");

            ClickAddAnotherAddressFieldLink();

            CustomerAddressInformation.ApartmentSuiteOtherField.SendKeys("lptest");

            Browser.Wait.ForClickableElement(Shipping.ProceedToPaymentButton);

            CustomerAddressInformation.ProceedToPayment();

            Browser.Wait.ForPage(Urls.PaymentPageUrl, 15);
        }

        protected abstract void HandleSelectElement(IElement element, string key);

        /// <summary>
        /// Verify the behavior of all shipping info form controls.
        /// </summary>
        protected abstract void VerifyAllRequiredShippingFormControls();

        /// <summary>
        /// Click AddAnotherAddressField link
        /// </summary>
        protected abstract void ClickAddAnotherAddressFieldLink();
    }
}