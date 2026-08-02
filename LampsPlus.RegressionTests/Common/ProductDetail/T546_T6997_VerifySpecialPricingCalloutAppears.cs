using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using System;
using LampsPlus.RegressionTests.DesktopTablet.ProductDetail;
using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Common.ProductDetail
{
    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T546_Windows_VerifySpecialPricingCalloutAppears : T546_DesktopBase
    {
        public T546_Windows_VerifySpecialPricingCalloutAppears(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_PCSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_PCSI_ElasticSearch)]
        public void VerifySpecialPricingCalloutAppears(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T546_Mac_VerifySpecialPricingCalloutAppears : T546_DesktopBase
    {
        public T546_Mac_VerifySpecialPricingCalloutAppears(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_PCSI)]
        public void VerifySpecialPricingCalloutAppears(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T546_iPad_VerifySpecialPricingCalloutAppears : T546_DesktopBase
    {
        public T546_iPad_VerifySpecialPricingCalloutAppears(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_PCSI)]
        public void VerifySpecialPricingCalloutAppears(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T546_TabletEmulator_VerifySpecialPricingCalloutAppears : T546_DesktopBase
    {
        public T546_TabletEmulator_VerifySpecialPricingCalloutAppears(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifySpecialPricingCalloutAppears(string config) => Validate(config);
    }


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
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7728
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T546
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7728"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T546")]
    //[Collection(LpTraits.UserRole.Professional)]
    public abstract class T546_DesktopBase : T546_T6997_Base
    {
        protected T546_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected override void VerifySortPageSaveAmountWithAmountOnPdp(string sku, decimal pdpSaveAmount)
        {
            var sortPageSaveAmount = Convert.ToDecimal(TextActions.RemoveDollarSign(Sort.GetSortResultSavePriceBySku(sku).Text)
                .Replace("Save", "").Trim());
            Assert.Equals(pdpSaveAmount, sortPageSaveAmount, "The Save amount does not matches what was on the PDP.");
        }

        protected override void WaitForFilters()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(Sort.SortFilterDisplaySetDropdownsClass));

        }
    }


    /// <summary>
    /// Verify that the 'Special' pricing callout appears where appropriate.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7728
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T6997
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7728"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T6997")]
    //[Collection(LpTraits.UserRole.Professional)]
    public abstract class T6997_MobileBase : T546_T6997_Base
    {
        protected T6997_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void VerifySortPageSaveAmountWithAmountOnPdp(string sku, decimal pdpSaveAmount) { }

        protected override void WaitForFilters()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.SortFilterButtonTriggerClass.ToCssClassSelector()));
        }
    }

    public abstract class T546_T6997_Base : ProductDetailTestsBase
    {
        protected T546_T6997_Base(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify that the 'Special' pricing callout appears where appropriate.
        /// </summary>
        /// <param name="config"></param>
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
            
            WaitForFilters();

            var url = Browser.PageUrl;

            Sort.NavigateToPriceFilteredSortPage(url, Convert.ToDecimal(price));

            Assert.Displayed(Sort.GetSkuContainerElement(sku), "The user is not brought to the section of the page with the product.");

            Sort.SearchPageForProductName(productName);

            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.SortMoreLikeThisBtnClass.ToCssClassSelector()));

            Browser.Wait.ForDomReady();
           
            Browser.Wait.ForDomReady();
        }

        protected abstract void VerifySortPageSaveAmountWithAmountOnPdp(string sku, decimal pdpSaveAmount);

        protected abstract void WaitForFilters();
    }
}
