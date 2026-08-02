using Xunit;
using Xunit.Abstractions;
using OpenQA.Selenium;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using xRetry;

namespace LampsPlus.RegressionTests.Common.Shipping
{
	//[Collection(LpTraits.BatchGroup.Common.Shipping)]
	[Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
	public class T170_Windows_VerifyGoogleAutocomplete : T170_DesktopBase
	{
		public T170_Windows_VerifyGoogleAutocomplete(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
		[InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
		public void GoogleAutocomplete(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T170_Mac_VerifyGoogleAutocomplete : T170_DesktopBase
    {
        public T170_Mac_VerifyGoogleAutocomplete(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T170. Rework - CI-3235")]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void GoogleAutocomplete(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T170_iPad_VerifyGoogleAutocomplete : T170_DesktopBase
    {
        public T170_iPad_VerifyGoogleAutocomplete(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void GoogleAutocomplete(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T170_TabletEmulator_VerifyGoogleAutocomplete : T170_DesktopBase
    {
        public T170_TabletEmulator_VerifyGoogleAutocomplete(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void GoogleAutocomplete(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Shipping)]
	public class T420_iPhone_VerifyGoogleAutocomplete : T420_MobileBase
	{
		public T420_iPhone_VerifyGoogleAutocomplete(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void GoogleAutocomplete(string config) => Validate(config);
	}


	//[Collection(LpTraits.BatchGroup.Common.Shipping)]
	[Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T420_Emulator_VerifyGoogleAutocomplete : T420_MobileBase
	{
		public T420_Emulator_VerifyGoogleAutocomplete(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void GoogleAutocomplete(string config) => Validate(config);
	}


	/// <summary>
	/// Verify google autocomplete and fedex validation appears on shipping page for certain circumstances.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5379
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T170
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5379"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T170")]
	public abstract class T170_DesktopBase : T170_T420_Base
	{
		protected T170_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected override void WaitForShippingPageToLoad()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.ProceedPaymentId.ToCssIdSelector()));
        }

        protected override void StateZipVerification(string suggestedStateString)
        {
            Assert.Equals(suggestedStateString, CustomerAddressInformation.StateField.GetAttribute(GlobalLocators.ValueAttribute).ToLower(), "Shipping page state and Payment page state values do not match.");
        }
    }


	/// <summary>
	/// Verify google autocomplete and fedex validation appears on shipping page for certain circumstances.
	/// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5268
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T420
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5268"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T420")]
	public abstract class T420_MobileBase : T170_T420_Base
	{
		protected T420_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void WaitForShippingPageToLoad()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(CustomerAddressInformation.AddAnotherAddressFieldLinkClass));
        }

        protected override void StateZipVerification(string suggestedStateString)
        {
          var customerAddressInformationState = CustomerAddressInformation.StateField.Text.Equals("California");

          var suggestedState = suggestedStateString.Equals("ca");

          Assert.True(suggestedState && customerAddressInformationState, "Shipping page state and Payment page state values do not match.");
        }
    }


	public abstract class T170_T420_Base : ShippingInfoTestsBase
	{
		private const string Name = "Test-T170";

		protected T170_T420_Base(ITestOutputHelper output) : base(output) { }
		
		protected void Validate(string config)
		{
			InitializeFramework(config, Urls.HomePageUrl);

            ShoppingCartWorkflow.CheckoutWithSingleItem();

            ValidateAutoCompleteNotDisplayed();

            CustomerAddressInformation.AddAnotherAddressFieldLink.Click();

            Browser.Wait.IsVisibleElement(By.Id(Shipping.SingleShippingAddress2Id));

            ValidateAutoCompleteDisplayed();

            CustomerAddressInformation.StreetAddressField.SendKeys(Keys.ArrowDown);
            CustomerAddressInformation.StreetAddressField.SendKeys(Keys.Enter);

            Browser.Wait.IsInvisibleElement(By.ClassName(Shipping.PacContainerClass));

            CustomerAddressInformation.ClearAndEnterText(CustomerAddressInformation.ApartmentSuiteOtherField, CustomerAddressInformation.Address.AddressLine2);
            CustomerAddressInformation.ClearAndEnterText(CustomerAddressInformation.EmailField, CustomerAddressInformation.Address.Email);
            CustomerAddressInformation.ClearAndEnterText(CustomerAddressInformation.PhoneField, CustomerAddressInformation.Address.Phone);
            CustomerAddressInformation.ClearAndEnterText(CustomerAddressInformation.ZipPostalCodeField, CustomerAddressInformation.Address.ZipCode);

            Shipping.ProceedToPaymentButton.Click();

            Browser.Wait.IsVisibleElement(By.Id(Payment.PlaceYourOrderButtonId));

            var paymentAddress = Payment.StreetAddressString.ToLower();
            var paymentApartmentString = Payment.ApartmentString.ToLower();
            var paymentCity = Payment.CityStringWithApartmentFieldActive.ToLower();
            var paymentState = Payment.StateStringWithApartmentFieldActive.ToLower();
            var paymentZipCode = Payment.ZipCodeStringWithApartmentFieldActive;

            Browser.Navigate(Urls.ShippingPageUrl);
            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.ProceedPaymentId.ToCssIdSelector()));
            
            Assert.Equals(paymentAddress, CustomerAddressInformation.StreetAddressField.GetAttribute(GlobalLocators.ValueAttribute).ToLower(), "Shipping page street address and Payment page shipping street address values do not match.");
            Assert.Equals(paymentApartmentString, CustomerAddressInformation.ApartmentSuiteOtherField.GetAttribute(GlobalLocators.ValueAttribute).ToLower(), "Shipping page apartment field and Payment page apartment field values do no match.");
            Assert.Equals(paymentCity, CustomerAddressInformation.CityField.GetAttribute(GlobalLocators.ValueAttribute).ToLower(), "Shipping page city and Payment page shipping city values do not match.");

            StateZipVerification(paymentState);
            
            Assert.Equals(paymentZipCode, CustomerAddressInformation.ZipPostalCodeField.GetAttribute(GlobalLocators.ValueAttribute).ToLower(), "Shipping page zip code and Payment page shipping zip code values do not match.");
        }

		private void ValidateAutoCompleteNotDisplayed()
        {
            WaitForShippingPageToLoad();
            
            CustomerAddressInformation.FirstNameField.SendKeys(Name);
			CustomerAddressInformation.LastNameField.SendKeys(Name);
			CustomerAddressInformation.StreetAddressField.SendKeys("&%$");

            Browser.Wait.IsVisibleElement(By.CssSelector(Search.PacTargetInputClass.ToCssClassSelector()));

			Assert.NotDisplayed(CustomerAddressInformation.GoogleAutocompleteElement,"Google autocomplete should not be displayed.");
		}

		private void ValidateAutoCompleteDisplayed()
		{
            CustomerAddressInformation.FillStreetAddressFieldAndLetGoogleSuggestionAct("Americana Way");

			Assert.Displayed(CustomerAddressInformation.GoogleAutocompleteElement,
				"Google auto complete is not displayed.");
        }

        protected abstract void WaitForShippingPageToLoad();

        protected abstract void StateZipVerification(string suggestedStateString);
    }
}
