using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Shipping.T166_T416_VerifyPoBoxValidationWarning
{
    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T166_Windows_VerifyPoBoxValidationWarning : T166_DesktopBase
    {
        public T166_Windows_VerifyPoBoxValidationWarning(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void PoBoxValidationWarning(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T166_Mac_VerifyPoBoxValidationWarning : T166_DesktopBase
    {
        public T166_Mac_VerifyPoBoxValidationWarning(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void PoBoxValidationWarning(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T166_iPad_VerifyPoBoxValidationWarning : T166_DesktopBase
    {
        public T166_iPad_VerifyPoBoxValidationWarning(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void PoBoxValidationWarning(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T166_TabletEmulator_VerifyPoBoxValidationWarning : T166_DesktopBase
    {
        public T166_TabletEmulator_VerifyPoBoxValidationWarning(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void PoBoxValidationWarning(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that a user receives a validation warning when using a P.O. Box for shipping.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5459
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T166 
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5459"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T166")]
    public abstract class T166_DesktopBase : TestsBaseDesktop
    {
        protected T166_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrange:
            1. Add any item to cart
            2. Click check out 
            */
            InitializeFunctionalTest(config);

            ShoppingCartWorkflow.AddMultipleItemsToCart(Urls.ContemporaryFloorLampsSortPageUrl, 1);
            Cart.CheckOut();
            Assert.True(Shipping.IsCurrentPage, "Current page is not a Shipping page");
            CustomerAddressInformation.AddAnotherAddressField();

            // Act:Enter "po box" in address line 1 and tab/click into Address Line 2.
            CustomerAddressInformation.FillFormControlByText(CustomerAddressInformation.ShippingElementsCollection["StreetAddressField"], "po box");
            CustomerAddressInformation.ShippingElementsCollection["CityField"].Click();

            //Assert: Field validation is shown for Address Line 1 that shipping to P.O. Boxes is not allowed.
            Assert.True(CustomerAddressInformation.GetValidationErrorMessage(CustomerAddressInformation.ShippingElementsCollection["StreetAddressField"])
                .Replace(".", string.Empty).Replace(" ", string.Empty).ToLower().Contains("pobox"), "Expected to have an error message containing P.O. Box.");
        }
    }
}
