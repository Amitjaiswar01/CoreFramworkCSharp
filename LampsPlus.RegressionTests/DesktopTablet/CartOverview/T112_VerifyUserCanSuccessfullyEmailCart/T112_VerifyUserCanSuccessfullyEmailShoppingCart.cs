using System;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview.T112_VerifyUserCanSuccessfullyEmailCart
{
    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T112_Windows_VerifyUserCanSuccessfullyEmailShoppingCart : T112_DesktopBase
    {
        public T112_Windows_VerifyUserCanSuccessfullyEmailShoppingCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void UserCanSuccessfullyEmailShoppingCart(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T112_Mac_VerifyUserCanSuccessfullyEmailShoppingCart : T112_DesktopBase
    {
        public T112_Mac_VerifyUserCanSuccessfullyEmailShoppingCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void UserCanSuccessfullyEmailShoppingCart(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T112_iPad_VerifyUserCanSuccessfullyEmailShoppingCart : T112_DesktopBase
    {
        public T112_iPad_VerifyUserCanSuccessfullyEmailShoppingCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void UserCanSuccessfullyEmailShoppingCart(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T112_TabletEmulator_VerifyUserCanSuccessfullyEmailShoppingCart : T112_DesktopBase
    {
        public T112_TabletEmulator_VerifyUserCanSuccessfullyEmailShoppingCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void UserCanSuccessfullyEmailShoppingCart(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that user can successfully email the shopping cart
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9935
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T112 
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9935"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T112")]
    public abstract class T112_DesktopBase : TestsBaseDesktop
    {
        protected T112_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: Add product to the cart
            InitializeFunctionalTest(config);
            ShoppingCartWorkflow.AddMultipleItemsToCart(Urls.ContemporaryFloorLampsSortPageUrl, 1);

            /*Act:
            Click on the Email cart link
            Enter up to 3 email addresses
            Click on send button
            */
            string[] emailAddresses = {"testingLP1@mailinator.com", "testingLP2@mailinator.com", "testingLP3@mailinator.com"};
            Cart.EmailShoppingCart(emailAddresses);

            /*Assert:
            Verify Success message on email form displayed
            Verify that email address has been added to the database
            */
            Assert.Equals($"{Messages.CartMessages.EmailSentMessage}{"\r\n"}{string.Join(Environment.NewLine, emailAddresses) }", Cart.GetSuccessfulEmailMessage(emailAddresses[2]), "Success message on email form displayed");
            Assert.True(Cart.AreEmailsFoundInDatabase(emailAddresses), "Email addresses are not saved in the database");
        }
    }
}
