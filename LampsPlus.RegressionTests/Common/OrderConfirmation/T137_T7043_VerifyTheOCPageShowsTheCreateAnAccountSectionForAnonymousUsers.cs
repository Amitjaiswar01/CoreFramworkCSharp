using System;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.OrderConfirmation;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Common.OrderConfirmation
{
    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T137_Windows_VerifyOcPageShowsCreateAccount : T137_DesktopBase
    {
        public T137_Windows_VerifyOcPageShowsCreateAccount(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void OcPageShowsCreateAccount(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T137_Mac_VerifyOcPageShowsCreateAccount : T137_DesktopBase
    {
        public T137_Mac_VerifyOcPageShowsCreateAccount(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void OcPageShowsCreateAccount(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T137_iPad_VerifyOcPageShowsCreateAccount : T137_DesktopBase
    {
        public T137_iPad_VerifyOcPageShowsCreateAccount(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void OcPageShowsCreateAccount(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T137_TabletEmulator_VerifyOcPageShowsCreateAccount : T137_DesktopBase
    {
        public T137_TabletEmulator_VerifyOcPageShowsCreateAccount(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void OcPageShowsCreateAccount(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.OrderConfirmation)]
    public class T7043_iPhone_VerifyOcPageShowsCreateAccount : T7043_MobileBase
    {
        public T7043_iPhone_VerifyOcPageShowsCreateAccount(ITestOutputHelper output) : base(output) { }
        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void OcPageShowsCreateAccount(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T7043_Emulator_VerifyOcPageShowsCreateAccount : T7043_MobileBase
    {
        public T7043_Emulator_VerifyOcPageShowsCreateAccount(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void OcPageShowsCreateAccount(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the OC page shows the 'Create an Account' section for anonymous users.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-6521
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T137
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-6521"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T137")]
    public abstract class T137_DesktopBase : T137_T7043_Base
    {
        protected T137_DesktopBase(ITestOutputHelper output) : base(output) { }
    }


    /// <summary>
    /// Verify the OC page shows the 'Create an Account' section for anonymous users.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5056
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7043
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5056"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7043")]
    public abstract class T7043_MobileBase : T137_T7043_Base
    {
        protected T7043_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void Validate(string config)
        {
            InitializeFramework(config);

            var getSkuGreaterThanTwoHundredDollars = ProductActions.GetSkuGreaterThanTwoHundredDollars;

            Assert.DatabaseObject(getSkuGreaterThanTwoHundredDollars, "ProductActions.GetSkuGreaterThanTwoHundredDollars()");

            //Step 1.1: Add item to the cart.
            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = getSkuGreaterThanTwoHundredDollars });

            //Step 1.2: Proceed to Shipping page.
            CartOverview.CheckOutNowButton.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.ProceedPaymentId.ToCssIdSelector()));

            string email = string.Format("{0}@mailinator.com", DateTime.Now.ToString("MMddyyyyHHMMssff"));

            //Step 1.3: Fill out Shipping form.
            CustomerAddressInformation.EnterShippingAddress(new IntAddress { Email = email }, isIntAddress:true);

            //Step 1.4: Proceed to Payment page and place order.
            OrderSummaryBlock.ProceedToPaymentButton.Click();
            Browser.Wait.IsVisibleElement(By.Id(Payment.PlaceYourIntlOrderButtonId));

            Payment.PlaceInternationalOrder();

            Browser.Wait.IsVisibleElement(By.ClassName(OrderConfirmation.CreateAccountButtonClass));

            OrderConfirmation.FillInCreateAccountFormOc();

            Browser.Wait.ForDisplayedElement(OrderConfirmation.CreateAccountConfirmationElement);

            Assert.True(OrderConfirmation.CreateAccountConfirmationElement.Displayed, "Create Account Confirmation button element is not displayed.");
        }
    }


    public abstract class T137_T7043_Base : OrderConfirmationTestsBase
    {
        protected T137_T7043_Base(ITestOutputHelper output) : base(output) { }
       
        protected virtual void Validate(string config)
        {
            InitializeFramework(config);

            var getSkuGreaterThanTwoHundredDollars = ProductActions.GetSkuGreaterThanTwoHundredDollars;

            Assert.DatabaseObject(getSkuGreaterThanTwoHundredDollars, "ProductActions.GetSkuGreaterThanTwoHundredDollars()");

            //Step 1.1: Add item to the cart.
            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = getSkuGreaterThanTwoHundredDollars });

            //Step 1.2: Proceed to Shipping page.
            CartOverview.CheckOutNowButton.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.ProceedPaymentId.ToCssIdSelector()));

            var email = $"{DateTime.Now.ToString("MMddyyyyHHMMssff")}@mailinator.com";

            //Step 1.3: Fill out Shipping form.
            CustomerAddressInformation.EnterShippingAddress(new IntAddress { Email = email }, isIntAddress:true);

            //Step 1.4: Proceed to Payment page and place order.
            ShoppingCartWorkflow.ProceedToPayment();

            //Step 1.5: Place the order.
            Payment.PlaceInternationalOrder();

            Browser.Wait.ForDomReady();
            Browser.Wait.IsVisibleElement(By.CssSelector(OrderConfirmation.OrderConfirmationHeadingClass.ToCssClassSelector()));

            Assert.True(OrderConfirmation.OrderIdHeadingElement(1).Displayed, "The Order Confirmation with the Order ID is not displayed.");
            Assert.True(OrderConfirmation.OrderConfirmationCreateAccount.Displayed, "The CREATE ACCOUNT button is not displayed on the Order Confirmation page.");

            //Step 2.1: Click on the Password field and Enter the password
            OrderConfirmation.OrderConfirmationEnterPwd(1).Click();
            OrderConfirmation.FillInCreateAccountFormOc();

            //Check Success Modal and Sucess Message
            Browser.Wait.ForDisplayedElement(OrderConfirmation.CreateAccountSuccessElement);
            Assert.True(OrderConfirmation.CreateAccountSuccessElement.Displayed, "The Success message modal not displayed.");
            Assert.StringContains(OrderConfirmation.CreateAccountSuccessElement.Text, "Success!", "The Success message is not displayed.");
        }
    }
}
