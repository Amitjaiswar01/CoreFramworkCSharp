using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.ProductDetail
{
    //[Collection(LpTraits.BatchGroup.Desktop.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T241_Windows_VerifyEmailProduct : T241_DesktopBase
    {
        public T241_Windows_VerifyEmailProduct(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void EmailProduct(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T241_Mac_VerifyEmailProduct : T234_DesktopBase
    {
        public T241_Mac_VerifyEmailProduct(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void EmailProduct(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T241_iPad_VerifyEmailProduct : T234_DesktopBase
    {
        public T241_iPad_VerifyEmailProduct(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void EmailProduct(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T241_TabletEmulator_VerifyEmailProduct : T234_DesktopBase
    {
        public T241_TabletEmulator_VerifyEmailProduct(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void EmailProduct(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that you are able to email the PDP using the 'Email a friend' form.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5164
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T241
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5164"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T241")]
    public abstract class T241_DesktopBase : ProductDetailTestsBase
    {
        protected T241_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config);

            const string emailItemSource = "EmailItem";
            var recipientEmail = $"{CurrentDateTime}@mailinator.com";
	        var email = recipientEmail.Replace(" ", string.Empty).Replace(":", string.Empty);
			var fromEmail = "test@test.com";
            var firstName = "fname";
            var lastName = "lname";
            var zipcode = "91311";
            var shortSku = ProductActions.GetAnySkuWithProductDetailPage;

            Assert.DatabaseObject(shortSku, ProductActions.GetAnySkuWithProductDetailPage);

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);
            ProductDetail.EmailLink.Click();
            Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.LpModalId));
            ProductDetail.EmailRecipientTextbox.SendKeys(email);
            ProductDetail.FirstNameTextbox.SendKeys(firstName);
            ProductDetail.LastNameTextbox.SendKeys(lastName);
            ProductDetail.FromEmailTextbox.SendKeys(fromEmail);
            ProductDetail.ZipcodeTextbox.SendKeys(zipcode);
            ProductDetail.SendEmailButton.Click();

            GlobalLocators.LpModalCloseElement.Click();

            Browser.Wait.UntilElementDoesntExist(GlobalLocators.LpModalId.ToCssIdSelector());

            Browser.Wait.ForCondition(() => ProductActions.GetEmailProductRecipient(email) > 0);

            var sourceId = ProductActions.GetEmailProductRecipient(email);
            var sourceName = ProductActions.GetEmailProductSource(sourceId);

            Assert.DatabaseObject(sourceId, "ProductActions.GetEmailProductRecipient(email)");
            Assert.DatabaseObject(sourceName, "ProductActions.GetEmailProductSource(sourceId)");

            Assert.True(sourceId > 0, "Recipient email not found in UserProfile.");
            Assert.True(sourceName == emailItemSource, "Recipient source name is not correct.");
        }
    }
}
