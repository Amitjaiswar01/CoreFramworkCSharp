using System;
using System.Collections.Generic;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.Common.Sort;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using xRetry;

namespace LampsPlus.RegressionTests.Mobile.OtherPages
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7802_iPhone_VerifyFunctionalityOfSfp4sOnSortPage : T7802_MobileBase
    {
        public T7802_iPhone_VerifyFunctionalityOfSfp4sOnSortPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Zephyr: T7802. Rework - ACD-10904")]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyFunctionalityOfSfp4sOnSortPage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7802_Emulator_VerifyFunctionalityOfSfp4OnSortPage : T7802_MobileBase
    {
        public T7802_Emulator_VerifyFunctionalityOfSfp4OnSortPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyFunctionalityOfSfp4sOnSortPage(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the functionality of SFP4s on the sort page
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9396
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7802
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9396"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-7802")]
    public abstract class T7802_MobileBase : T7802_Base
    {
        protected T7802_MobileBase(ITestOutputHelper output) : base(output) { }
    }


    public abstract class T7802_Base : SortTestsBase
    {
        protected T7802_Base(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config);

            var firstProduct = 1;
            var expectedSku = Sfp4Sku();
            var expectedUrl = Urls.Sfp4PageBaseUrl + expectedSku;
            Browser.Navigate(expectedUrl);

            //Verify PDP loads on tapping on Product Name
            Browser.Wait.IsVisibleElement(By.XPath(SortPla.PlaCertonaImageLoadedXpath));
            SortPla.ClickOnPlaProduct();
            Assert.True(Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.LpMobileAccordionClass.ToCssClassSelector())), "Clicking on PLA does not navigate to PDP");

            //Verify PDP loads on tapping on the 'MORE DETAILS' link
            Browser.Navigate(expectedUrl);
            Browser.Wait.IsVisibleElement(By.CssSelector(SortPla.MoreDetailsId.ToCssIdSelector()));
            Browser.Wait.ForClickableElement(SortPla.PlaMoreDetailsLinkElement);
            Browser.ExecuteJs("arguments[0].click()", SortPla.PlaMoreDetailsLinkElement.InternalElement);
            Assert.True(Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.LpMobileAccordionClass.ToCssClassSelector())), "Clicking on Details does not navigate to PDP");

            //Verify PDP loads on tapping 'SIMILAR DESIGNS' section.
            Browser.Navigate(expectedUrl);
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));
            Browser.Wait.ForClickableElement(SortPla.PlaMoreDetailsLinkElement);
            Browser.ScrollToBottomOfPage(expectedUrl);
            Browser.ScrollToTopOfWindow();
            Browser.ScrollToElement(GlobalLocators.AddToCartButton);
            Sort.FirstImageOnSort(firstProduct).Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            //Verify item is added to the cart when tapped on Add To Cart button
            Browser.Navigate(expectedUrl);
            Browser.ScrollToBottomOfPage(expectedUrl);
            Browser.ScrollToTopOfWindow();
            Browser.Wait.ForClickableElement(GlobalLocators.AddToCartButton).Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.CheckOutNowClass.ToCssClassSelector()));
            Assert.Equals(expectedSku, CartOverview.ProductSkuCart, $"SFP4 SKU: {expectedSku} is not added to the cart.");
        }

        protected string Sfp4Sku()
        {
            var random = new Random();
            var plaSku = new List<string> { "69794", "5Y584", "22087", "2G189" };
            var index = random.Next(plaSku.Count);
            return (plaSku[index]);
        }
    }
}
