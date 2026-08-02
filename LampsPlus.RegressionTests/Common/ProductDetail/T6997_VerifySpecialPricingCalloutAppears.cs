using System;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using OpenQA.Selenium;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.ProductDetail;

namespace LampsPlus.RegressionTests.Common.ProductDetail
{
    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
    public class T6997_iPhone_VerifySpecialPricingCalloutAppears : T6997_MobileBase
    {
        public T6997_iPhone_VerifySpecialPricingCalloutAppears(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI)]
        public void VerifySpecialPricingCalloutAppears(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
    public class T6997_AndroidPhone_VerifySpecialPricingCalloutAppears : T6997_MobileBase
    {
        public T6997_AndroidPhone_VerifySpecialPricingCalloutAppears(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_PCSI)]
        public void VerifySpecialPricingCalloutAppears(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T6997_Emulator_VerifySpecialPricingCalloutAppears : T6997_MobileBase
    {
        public T6997_Emulator_VerifySpecialPricingCalloutAppears(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_PCSI)]
        public void VerifySpecialPricingCalloutAppears(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the 'Special' pricing callout appears where appropriate.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7728
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T6997
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7728"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T6997")]
    //[Collection(LpTraits.UserRole.Professional)]
    public abstract class T6997_MobileBase : ProductDetailTestsBase
    {
        protected T6997_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config);
            var sku = ProductActions.GetProMemberSpecialPriceDiscountCallOutShortSku;

            ProductDetail.NavigateToProductDetailByShortSku(sku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            Assert.True(ProductDetail.IsProductDetailPage, "The user is not brought to the PDP for the item.");

            var productName = ProductDetail.ProductName;
            var price = ProductDetail.GetProductPrice();
            var prosSpecialCallout = ProductDetail.ProsSpecialPriceCallout.Text.Trim();

            Assert.True(prosSpecialCallout.CaseInsensitiveContains("PROS SPECIAL PRICE"), "pros special price callout not displayed");

            var breadsCrumbs = ProductDetail.ListOfBreadCrumbLink();

            if (breadsCrumbs.Count >= 2)
            {
                breadsCrumbs[breadsCrumbs.Count - 2].Click();
            }
            else
            {
                Browser.Log.Message("Product page has less than 2 link in breadcrumb.");
            }

            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.SortFilterButtonTriggerClass.ToCssClassSelector()));

            var url = Browser.PageUrl;

            Sort.NavigateToPriceFilteredSortPage(url, Convert.ToDecimal(price));

            Assert.Displayed(Sort.GetSkuContainerElement(sku), "The user is not brought to the section of the page with the product.");

            Sort.SearchPageForProductName(productName);

            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.SortMoreLikeThisBtnClass.ToCssClassSelector()));
        }
    }
}