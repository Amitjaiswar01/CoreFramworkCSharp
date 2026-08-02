using System;
using Automation.Framework.Utilities;
using Xunit;
using Xunit.Abstractions;
using OpenQA.Selenium;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using xRetry;

namespace LampsPlus.RegressionTests.Common.Shipping
{
    //[Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T169_Windows_VerifyAUserCanSaveAShippingAddress : T169_DesktopBase
    {
        public T169_Windows_VerifyAUserCanSaveAShippingAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void UserCanSaveAShippingAddress(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Professional)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T169_Windows_Pro_VerifyAUserCanSaveAShippingAddress : T169_DesktopBase
    {
        public T169_Windows_Pro_VerifyAUserCanSaveAShippingAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_PCSI)]
        public void UserCanSaveAShippingAddress(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Customer)]
    //[Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)] TODO: Wrong trait. Need to have a new one for Mac tests specifically otherwise they will skip.
    public class T169_Mac_VerifyAUserCanSaveAShippingAddress : T169_DesktopBase
    {
        public T169_Mac_VerifyAUserCanSaveAShippingAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void UserCanSaveAShippingAddress(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T169_iPad_VerifyAUserCanSaveAShippingAddress : T169_DesktopBase
    {
        public T169_iPad_VerifyAUserCanSaveAShippingAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void UserCanSaveAShippingAddress(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T169_TabletEmulator_VerifyAUserCanSaveAShippingAddress : T169_DesktopBase
    {
        public T169_TabletEmulator_VerifyAUserCanSaveAShippingAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void UserCanSaveAShippingAddress(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Shipping)]
    public class T419_iPhone_VerifyAUserCanSaveAShippingAddress : T419_MobileBase
    {
        public T419_iPhone_VerifyAUserCanSaveAShippingAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void UserCanSaveAShippingAddress(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Professional)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Shipping)]
    public class T419_iPhone_Pro_VerifyAUserCanSaveAShippingAddress : T419_MobileBase
    {
        public T419_iPhone_Pro_VerifyAUserCanSaveAShippingAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI)]
        public void UserCanSaveAShippingAddress(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Shipping)]
    public class T419_AndroidPhone_VerifyAUserCanSaveAShippingAddress : T419_MobileBase
    {
        public T419_AndroidPhone_VerifyAUserCanSaveAShippingAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI)]
        public void UserCanSaveAShippingAddress(string config) => Validate(config);
    }
    

    //[Collection(LpTraits.UserRole.Professional)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Shipping)]
    public class T419_AndroidPhone_Pro_VerifyAUserCanSaveAShippingAddress : T419_MobileBase
    {
        public T419_AndroidPhone_Pro_VerifyAUserCanSaveAShippingAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_PCSI)]
        public void UserCanSaveAShippingAddress(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T419_Emulator_VerifyAUserCanSaveAShippingAddress : T419_MobileBase
    {
        public T419_Emulator_VerifyAUserCanSaveAShippingAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void UserCanSaveAShippingAddress(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Professional)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T419_Emulator_Pro_VerifyAUserCanSaveAShippingAddress : T419_MobileBase
    {
        public T419_Emulator_Pro_VerifyAUserCanSaveAShippingAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory] 
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_PCSI)]
        public void UserCanSaveAShippingAddress(string config) => Validate(config);
    }


    /// <summary>
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5368
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T169
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5368"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T169")]
    public abstract class T169_DesktopBase : T169_T419_Base
    {
        protected T169_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected override void SaveAddress()
        {
            var shippingAddress = new Address { AddressLine1 = "9201 Winnetka Ave" };

            Browser.Wait.IsVisibleElement(By.XPath(ManageAccount.ShippingAddressesLinkXpath));
            Browser.ClickByJs(ManageAccount.ManageShippingAddressesLinkForElement);
            Browser.Wait.ForClickableElement(ManageAccount.BtnAddShippingAddress);
            ManageAccount.BtnAddShippingAddress.Click();
            Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.LpModalId));
            ManageAccountWorkflow.AddNewShippingAddressToModal(shippingAddress);
            Browser.Wait.ForClickableElement(ManageAccount.BtnSaveShippingAddress).Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(ManageAccount.OptionWrapperClass.ToCssClassSelector()));
        }

        protected override void ProceedToPayment()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.ProceedPaymentId.ToCssIdSelector()));
            Shipping.ProceedToPaymentButton.Click();

            Browser.Wait.IsVisibleElement(By.Id(Payment.PlaceYourOrderButtonId));
        }

        protected override void WaitForShippingPage()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(Shipping.SavedAddressClass));
            Browser.Wait.IsVisibleElement(By.CssSelector((Shipping.ShipToDifferentAddrClass.ToCssClassSelector())));
        }
    }


    /// <summary>
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5554
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T419
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5554"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T419")]
    public abstract class T419_MobileBase : T169_T419_Base
    {
        protected T419_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void SaveAddress()
        {
            var shippingAddress = new Address { AddressLine1 = "9201 Winnetka Ave" };

            //Create saved addresses
            Browser.Wait.ForClickableElement(ManageAccount.ShippingAddressLink).Click();
            Browser.Wait.IsVisibleElement(By.Id(ManageAccount.BtnAddShippingAddressId));
            Browser.ClickOnButtonMultipleTimes(ManageAccount.BtnAddShippingAddress, 5, ManageAccount.IsManageAccountShippingFormVisible);

            ManageAccountWorkflow.AddNewShippingAddressToModal(shippingAddress);
            Browser.Wait.ForClickableElement(ManageAccount.BtnSaveShippingAddress).Click();

            Browser.Wait.IsVisibleElement(By.Id(ManageAccount.BtnAddShippingAddressId));
        }

        protected override void ProceedToPayment()
        {
            Browser.ClickOnButtonMultipleTimes(Shipping.ProceedToPaymentButton, 5, Payment.IsPaymentPageVisible);
        }

        protected override void WaitForShippingPage()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.SavedAddressClass.ToCssClassSelector()));
        }
    }


    [Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    public abstract class T169_T419_Base : ShippingInfoTestsBase
    {
        protected T169_T419_Base(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config, Urls.ManageAccountPageUrl);

            Browser.Wait.IsVisibleElement(By.ClassName(ManageAccount.ShippingAddressLinkClass));

            SaveAddress();

            var customerEmail = TestSetup.AccountConfig.AccountUnderTest.UserName;//TODO Fix 

            var shortSku = ProductActions.GetShortSkuThatMeetsMinimumOrder;

            Assert.DatabaseObject(shortSku, "ProductActions.GetShortSkuThatMeetsMinimumOrder()");

            var inputShippingAddress = new Address
            {
                State = StateCodeListUnitedStates.CA,
                AddressLine1 = $"{getRandomAddressNumber()} Plummer St.",
                SaveToProfile = true
            };

            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = shortSku });
            CartOverview.RemovePromoCode();
            Browser.Wait.ForClickableElement(CartOverview.CheckOutNowButton).Click();

            WaitForShippingPage();

            Browser.ClickByJs(CustomerAddressInformation.ShipToDifferentAddressButton);
            //CustomerAddressInformation.ShipToDifferentAddressButton.Click();

            Browser.Wait.IsVisibleElement(By.ClassName(Shipping.SelectShippingAddressClass));

            Assert.Equals(Shipping.SelectShippingAddress.Text, Shipping.SelectShippingAddressString, "Select a Shipping Address did not displayed.");
           
            Browser.Wait.IsVisibleElement(By.ClassName(Shipping.AddNewAddrClass));

            Browser.Wait.ForClickableElement(CustomerAddressInformation.AddNewAddressButton).Click();
            Browser.Wait.ForElementToStopAnimating(Shipping.ShippingInformationPageContainer);

            CustomerAddressInformation.EnterShippingAddress(inputShippingAddress, UserRole.SNIS_NPCSI);
            Browser.Wait.ForClickableElement(CustomerAddressInformation.SaveAddressFromModalButton).Click();
            Browser.Wait.IsVisibleElement(By.XPath(ManageAccount.UpdatedAddressXpath));

            ProceedToPayment();

            var savedAddress = ProductActions.GetLastSavedAddressByEmail(customerEmail);

            Assert.DatabaseObject(savedAddress, "ProductActions.GetLastSavedAddressByEmail(LoginType.CustomerLoginAccount.UserName)");

            Assert.Equals(inputShippingAddress.FirstName, savedAddress.FirstName, "First name does not match.");
            Assert.Equals(inputShippingAddress.LastName, savedAddress.LastName, "Last name does not match.");
            Assert.Equals(inputShippingAddress.AddressLine1, savedAddress.Address1, "AddressLine1 does not match.");
            Assert.Equals(inputShippingAddress.AddressLine2, savedAddress.Address2, "AddressLine2 does not match.");
            Assert.Equals(inputShippingAddress.City, savedAddress.City, "City does not match.");
            Assert.Equals(inputShippingAddress.State, savedAddress.State, "State does not match.");
            Assert.Equals(inputShippingAddress.ZipCode, savedAddress.Zip, "Zip Code does not match.");
            Assert.Equals(inputShippingAddress.Phone, savedAddress.Phone, "Phone number does not match.");
        }

        private string getRandomAddressNumber()
        {
            Random generator = new Random();
            return generator.Next(0, 9999).ToString("D6");
        }

        protected abstract void SaveAddress();
        protected abstract void ProceedToPayment();
        protected abstract void WaitForShippingPage();
    }
}
