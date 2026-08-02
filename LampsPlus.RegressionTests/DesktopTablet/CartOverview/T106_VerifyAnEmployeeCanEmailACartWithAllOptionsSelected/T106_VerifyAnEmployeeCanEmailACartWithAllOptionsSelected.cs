using System;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview.T106_VerifyAnEmployeeCanEmailACartWithAllOptionsSelected
{
    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T106_Windows_VerifyAnEmployeeCanEmailACartWithAllOptionsSelected : T106_DesktopBase
    {
        public T106_Windows_VerifyAnEmployeeCanEmailACartWithAllOptionsSelected(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void EmailCart(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T106_Mac_VerifyAnEmployeeCanEmailACartWithAllOptionsSelected : T106_DesktopBase
    {
        public T106_Mac_VerifyAnEmployeeCanEmailACartWithAllOptionsSelected(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SIS_ESI)]
        public void EmailCart(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T106_iPad_VerifyAnEmployeeCanEmailACartWithAllOptionsSelected : T106_DesktopBase
    {
        public T106_iPad_VerifyAnEmployeeCanEmailACartWithAllOptionsSelected(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SIS_ESI)]
        public void EmailCart(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T106_TabletEmulator_VerifyAnEmployeeCanEmailACartWithAllOptionsSelected : T106_DesktopBase
    {
        public T106_TabletEmulator_VerifyAnEmployeeCanEmailACartWithAllOptionsSelected(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SIS_ESI)]
        public void EmailCart(string config) => Validate(config);
    }


    /// <summary>
    /// Verify a user can email a cart with all options selected
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9917
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T106
    /// </summary>
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9917"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T106")]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]

    public abstract class T106_DesktopBase : TestsBaseDesktop
    {
        protected T106_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            // Arrange: Add SKU to the Cart
            InitializeFunctionalTest(config);

            var shortSku = ProductActions.GetAnySkuWithProductDetailPage;

            Assert.DatabaseObject(shortSku, "ProductActions.GetAnySkuWithProductDetailPage()");

            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = shortSku });

            /* Act : Click on Email Link
            Provide emails and click on In-Home Consult Info Button
            Check all checkboxes for email cart options
            Click on Send Button
            */
            var emailAddresses = new[] { "testingLP1@mailinator.com", "testingLP2@mailinator.com", "testingLP3@mailinator.com" };
            Cart.EmailShoppingCartWithOptionsSelected(emailAddresses);

            // Assert : Verify the thank you message is visible correctly
            Assert.Equals($"{Messages.CartMessages.EmailSentMessage}{"\r\n"}{string.Join(Environment.NewLine, emailAddresses) }", Cart.GetSuccessfulEmailMessage(emailAddresses[2]), "Success message on email form displayed");

            // Assert : Check the emails are added to the database 
            Assert.True(Cart.AreEmailsFoundInDatabase(emailAddresses[0], emailAddresses[1], emailAddresses[2]), "Email addresses are not saved in the database");
        }
    }
}