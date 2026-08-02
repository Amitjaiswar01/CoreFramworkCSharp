using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.Payment.T7941_VerifyErrorMessageIntOrderUnderTwoHundredDollars
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Payment)]
    public class T7941_Windows_VerifyErrorMessageIntOrderUnderTwoHundredDollars : T7941_DesktopBase
    {
        public T7941_Windows_VerifyErrorMessageIntOrderUnderTwoHundredDollars(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void ErrorMessageForInternationalOrder(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Payment)]
    public class T7941_Mac_VerifyErrorMessageIntOrderUnderTwoHundredDollars : T7941_DesktopBase
    {
        public T7941_Mac_VerifyErrorMessageIntOrderUnderTwoHundredDollars(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void ErrorMessageForInternationalOrder(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Payment)]
    public class T7941_iPad_VerifyErrorMessageIntOrderUnderTwoHundredDollars : T7941_DesktopBase
    {
        public T7941_iPad_VerifyErrorMessageIntOrderUnderTwoHundredDollars(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void ErrorMessageForInternationalOrder(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Payment)]
    public class T7941_TabletEmulator_VerifyErrorMessageIntOrderUnderTwoHundredDollars : T7941_DesktopBase
    {
        public T7941_TabletEmulator_VerifyErrorMessageIntOrderUnderTwoHundredDollars(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void ErrorMessageForInternationalOrder(string config) => Validate(config);
    }


    /// <summary>
    /// Verify Error Message For International Order Under $200
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10653
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7941
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10653"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7941")]
    public abstract class T7941_DesktopBase : TestsBaseDesktop
    {
        protected T7941_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrange:
             Add an item with an amount less then $200 to the cart
             Proceed to the Payment page.
            */
            InitializeFunctionalTest(config);

            var getSkuLessThanTwoHundredDollars = ProductActions.GetSkuThatIsLessThanTwoHundredDollars;
            Assert.DatabaseObject(getSkuLessThanTwoHundredDollars, "ProductActions.GetSkuThatIsLessThanTwoHundredDollars()");

            ShoppingCartWorkflow.ProceedToPaymentWithSingleProduct(getSkuLessThanTwoHundredDollars);

            /*Act:
             Click on My Address Outside US
             Select the Country other than US & Canada
            */
            Payment.SelectSameAsShippingCheckbox();
            CustomerAddressInformation.EnterBillingAddress(Address);

            Address.Country = CountryCodeList.MX;
            CustomerAddressInformation.ChangeBillingCountry(Address);
            
            //Assert: Verify the Minimum Order Value Not Met error appears
            Assert.True(Payment.IsMinimumOrderMessageVisible, "Verify the Minimum Order Value Not Met error appears.");
        }
    }
}