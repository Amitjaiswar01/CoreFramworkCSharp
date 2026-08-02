using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.ProductDetail;
using OpenQA.Selenium;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Skip = Xunit.Skip;

namespace LampsPlus.RegressionTests.Common.ProductDetail
{
    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7786_Windows_VerifyReplacementPartLink : T7786_DesktopBase
    {
        public T7786_Windows_VerifyReplacementPartLink(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyReplacementPartLink(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7786_Mac_VerifyReplacementPartLink : T7786_DesktopBase
    {
        public T7786_Mac_VerifyReplacementPartLink(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7786. Rework - ACD-10303")]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyReplacementPartLink(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7786_iPad_VerifyReplacementPartLink : T7786_DesktopBase
    {
        public T7786_iPad_VerifyReplacementPartLink(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7786. Rework - ACD-10303")]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyReplacementPartLink(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7786_TabletEmulator_VerifyReplacementPartLink : T7786_DesktopBase
    {
        public T7786_TabletEmulator_VerifyReplacementPartLink(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7786. Rework - ACD-10303")]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyReplacementPartLink(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
    public class T7787_iPhone_VerifyReplacementPartLink : T7787_MobileBase
    {
        public T7787_iPhone_VerifyReplacementPartLink(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyReplacementPartLink (string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7787_Emulator_VerifyReplacementPartLink : T7787_MobileBase
    {
        public T7787_Emulator_VerifyReplacementPartLink (ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyReplacementPartLink(string config) => Validate(config);
    }


    /// <summary>
	/// Verify Replacement Link is displayed on the PDP for eligible products
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9326
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7786
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9326"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7786")]
    public abstract class T7786_DesktopBase : T7786_T7787_Base
    {
        protected T7786_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected override void ReplacementPart(string parentSku)
        {
            // Waiting for page load
            Browser.Wait.ForClickableElement(GlobalLocators.AddToCartButton);
            var replacementPartLinkText = ProductDetail.ReplacementPartLink.Text;

            //Verifying Replacement Part link is displayed or not
            Assert.Displayed(ProductDetail.ReplacementPartLink, "Replacement Part link is not displayed");
            Assert.StringContains(replacementPartLinkText, parentSku, "String doesnot contain SKU number");

            Browser.ScrollToElement(ProductDetail.ReplacementPartLink);
            Browser.Wait.IsVisibleElement(By.Id(ProductDetail.ReplacementPartLinkId));
            ProductDetail.ReplacementPartLink.Click();

            //Verifying the Replacement part modal is displayed or not
            Browser.Wait.IsVisibleElement(By.XPath(ProductDetail.ReplacementPartSkuXpath));
            Assert.True(ProductDetail.IsReplacementPartModalVisible, "Replacement Part modal is not displayed");
        }

        // Verify the part sku populated 
        protected override void VerifyModalData(string parentSku, int recommendedBulbCount, List<ProductModel> recommendedBulbSkuDb, int countDbProduct, List<ProductModel> childPartSkuDb)
        {
            var replacementPartLinkText = ProductDetail.ReplacementPartLink.Text.ToLower();

            if (replacementPartLinkText == "bulbs for style #" + parentSku.ToLower()) // Sku only have the Recommended Bulb attached to it
            {
                for (var partCount = 0; partCount < recommendedBulbCount; partCount++)
                {
                    var skuValueFromDb = recommendedBulbSkuDb[partCount].ShortSku;
                    var skuValueFromReplacementPartModal = ProductDetail.ReplacementPartSku[partCount].Text.Replace("Style #", string.Empty).TrimEnd();
                    Assert.Equals(skuValueFromDb, skuValueFromReplacementPartModal, "The Sku value does not match");
                }
            }
            else if (replacementPartLinkText == "replacement parts & accessories for style #" + parentSku) // sku only have Replacement part
            {
                for (var partCount = 1; partCount < countDbProduct; partCount++)
                {
                    var skuValueFromDb = childPartSkuDb[partCount].ShortSku;
                    var skuValueFromReplacementPartModal = ProductDetail.ReplacementPartSku[partCount].Text;
                    Assert.Equals(skuValueFromDb, skuValueFromReplacementPartModal, "The Sku value doesnot match");
                }
            }
            else  // sku with the recommneded bulb as well as Replacement part
            {
                var partCount = 0;
                var modalPartCount = 1;
                while (partCount <= countDbProduct && modalPartCount <= countDbProduct)
                {
                    var skuValueFromDb = childPartSkuDb[partCount].ShortSku;
                    var skuValueFromReplacementPartModal = ProductDetail.ReplacementPartSku[modalPartCount].Text.Replace("Style #", string.Empty).TrimEnd();
                    Assert.Equals(skuValueFromDb, skuValueFromReplacementPartModal, "The Sku value doesnot match");
                    partCount++;
                    modalPartCount++;
                }

                var bulbsFromReplacementPartModal = ProductDetail.ReplacementPartSku[0].Text.Replace("Style #", string.Empty).TrimEnd();
                var bulbSkuValueFromDb = recommendedBulbSkuDb[0].ShortSku;
                Assert.Equals(bulbSkuValueFromDb, bulbsFromReplacementPartModal, "Recommended Bulbs Sku value do not");
            }
        }
    }


    /// <summary>
    /// Verify Pricing Block Values for Residential Product on Regular Price.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9326
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7787
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9326"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7774")]
    public abstract class T7787_MobileBase : T7786_T7787_Base
    {
        protected T7787_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void ReplacementPart(string parentSku)
        {
            // Waiting for page load
            Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.PdAddToCartId));
            Browser.ScrollIntoView(ProductDetail.ProductDescDropDown);

            Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.ProductDescId.ToCssIdSelector()));
            Browser.Wait.ForClickableElement(ProductDetail.ProductDescDropDown).Click();

            var replacementPartLinkText = ProductDetail.ReplacementPartLink.Text;

            //Verifying Replacement Part link is displayed or not
            Assert.Displayed(ProductDetail.ReplacementPartLink, "Replacement Part link is not displayed");
            Assert.StringContains(replacementPartLinkText, parentSku , "String doesnot contain SKU number");
            
        }

        protected override void VerifyModalData(string parentSku, int recommendedBulbCount, List<ProductModel> recommendedBulbSkuDb, int countDbProduct, List<ProductModel> childPartSkuDb)
        {
            var replacementPartLinkText = ProductDetail.ReplacementPartLink.Text.ToLower();

            Browser.ClickByJs(ProductDetail.ReplacementPartLink);

            //Verifying the Replacement part modal is displayed or not
            Browser.Wait.IsVisibleElement(By.XPath(ProductDetail.ReplacementPartSkuXpath));
            Assert.True(ProductDetail.IsReplacementPartModalVisible, "Replacement Part modal is not displayed");

            if (replacementPartLinkText == "bulbs for style #"+ parentSku.ToLower()) // Sku only have the Recommended Bulb attached to it
            {
                for (var partCount = 0; partCount < recommendedBulbCount; partCount++)
                {
                    var skuValueFromDb = recommendedBulbSkuDb[partCount].ShortSku;
                    var skuValueFromReplacementPartModal = ProductDetail.ReplacementPartSku[partCount].Text.Replace("Style #", string.Empty).TrimEnd();
                    Assert.Equals(skuValueFromDb, skuValueFromReplacementPartModal, "The Sku value does not match");
                }
            }
            else if (replacementPartLinkText == "replacement parts & accessories for style #"+ parentSku.ToLower()) // sku only have Replacement part
            {
                for (var partCount = 0; partCount < countDbProduct; partCount++)
                {
                    var skuValueFromDb = childPartSkuDb[partCount].ShortSku;
                    var skuValueFromReplacementPartModal = ProductDetail.ReplacementPartSku[partCount].Text.Replace("Style #", string.Empty).TrimEnd();
                    Assert.Equals(skuValueFromDb, skuValueFromReplacementPartModal, "The Sku value doesnot match");
                }
            }
            else  // sku with the recommneded bulb as well as Replacement part
            {
                var partCount = 0;
                var modalPartCount = 1;
                while (partCount <= countDbProduct && modalPartCount <= countDbProduct)
                {
                    var skuValueFromDb = childPartSkuDb[partCount].ShortSku;
                    var skuValueFromReplacementPartModal = ProductDetail.ReplacementPartSku[modalPartCount].Text.Replace("Style #", string.Empty).TrimEnd();
                    Assert.Equals(skuValueFromDb, skuValueFromReplacementPartModal, "The Sku value doesnot match");
                    partCount++;
                    modalPartCount++;
                }

                var bulbsFromReplacementPartModal = ProductDetail.ReplacementPartSku[0].Text.Replace("Style #", string.Empty).TrimEnd();
                var bulbSkuValueFromDb = recommendedBulbSkuDb[0].ShortSku;
                Assert.Equals(bulbSkuValueFromDb, bulbsFromReplacementPartModal, "Recommended Bulbs Sku value do not");
            }
        }
    }


    public abstract class T7786_T7787_Base : ProductDetailTestsBase
    {
        protected T7786_T7787_Base(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config);

            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "P", "This test can only be executed against DBCLUST.");

            // Fetching the Parent Sku and Relation grp Id from the Database
            var shortSku = ProductActions.GetReplacementParentSku;
            var parentSku = shortSku.ParentSkuString;

            Assert.DatabaseObject(ProductActions.GetReplacementParentSku, "ProductActions.GetReplacementParentSku()");

            // Fetching the data of the child Sku available under Main Product by passing RelationshipGridId
            var childPartSkuDb = ProductActions.GetReplacementPartDetail(parentSku);
            var recommendedBulbSkuDb = ProductActions.GetReplacementBulbDetail(parentSku);

            Browser.NavigateToPdp(parentSku);

            // Navigate to Replacement Part sku
            ReplacementPart(parentSku);

            // Taking Count Of Replacement Part Available for the Parent Sku
            var countDbProduct = childPartSkuDb.Count;
            var recommendedBulbCount = recommendedBulbSkuDb.Count;

            VerifyModalData(parentSku, recommendedBulbCount, recommendedBulbSkuDb, countDbProduct, childPartSkuDb);
        }

        protected abstract void ReplacementPart(string parentSku);

        protected abstract void VerifyModalData(string parentSku, int recommendedBulbCount, List<ProductModel> recommendedBulbSkuDb, int countDbProduct, List<ProductModel> childPartSkuDb);
    }
}
