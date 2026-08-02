using Xunit;
using Xunit.Abstractions;
using OpenQA.Selenium;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.ProductDetail;
using xRetry;
using Skip = Xunit.Skip;

namespace LampsPlus.RegressionTests.Common.ProductDetail
{
    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7819_Windows_VerifySpecificationsTablesDisplayed : T7819_DesktopBase
    {
        public T7819_Windows_VerifySpecificationsTablesDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void SpecificationsTables(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7819_Mac_VerifySpecificationsTablesDisplayed : T7819_DesktopBase
    {
        public T7819_Mac_VerifySpecificationsTablesDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7819. Rework - ACD-10245")]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void SpecificationsTables(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7819_iPad_VerifySpecificationsTablesDisplayed : T7819_DesktopBase
    {
        public T7819_iPad_VerifySpecificationsTablesDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void SpecificationsTables(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7819_TabletEmulator_VerifySpecificationsTablesDisplayed : T7819_DesktopBase
    {
        public T7819_TabletEmulator_VerifySpecificationsTablesDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void SpecificationsTables(string config) => Validate(config);
    }

    
    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
    public class T7820_iPhone_VerifySpecificationsTablesDisplayed : T7820_MobileBase
    {
        public T7820_iPhone_VerifySpecificationsTablesDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void SpecificationsTables(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
    public class T7820_AndroidPhone_VerifySpecificationsTablesDisplayed : T7820_MobileBase
    {
        public T7820_AndroidPhone_VerifySpecificationsTablesDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void SpecificationsTables(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7820_Emulator_VerifySpecificationsTablesDisplaye : T7820_MobileBase
    {
        public T7820_Emulator_VerifySpecificationsTablesDisplaye(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void SpecificationsTables(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that Specifications Tables Displayed on PDP.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9473
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7819
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9473"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7819")]
    public abstract class T7819_DesktopBase : T7819_T7820_Base
    {
        protected T7819_DesktopBase(ITestOutputHelper output) : base(output) { }

        public override void VerifySpecificationsTables()
        {
           Assert.Displayed(ProductDetail.PdProdSpecificationsTables, "Specifications section should not displayed");

           var productAttribute = ProductDetail.PdProdSpecificationsTables.Text.Contains(ProductDetail.ProductAttributeString);
           var productSpecification = ProductDetail.PdProdSpecificationsTables.Text.Contains(ProductDetail.ProductSpecificationString);

           if (productAttribute && productSpecification)
           { 
                Assert.Displayed(ProductDetail.ProductAttributes, "Product Attributes Table does not displayed");
                Assert.Displayed(ProductDetail.ProductSpecificationsTables, "Product Specifications Tables does not displayed");
           }
           else if(productSpecification || productAttribute)
           {
               if (productSpecification)
               {
                   Assert.Displayed(ProductDetail.ProductSpecificationsTables, "Product Specifications Tables does not displayed");
               }
               else
               {
                   Assert.Displayed(ProductDetail.ProductAttributes, "Product Attributes Table does not displayed");
               }
           }
        }
    }


    /// <summary>
    /// Verify that Specifications Tables Displayed on PDP.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9473
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7820
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9473"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7820")]
    public abstract class T7820_MobileBase : T7819_T7820_Base
    {
        protected T7820_MobileBase(ITestOutputHelper output) : base(output) { }

        public override void VerifySpecificationsTables()
        {
            Browser.Wait.ForClickableElement(ProductDetail.PdProdSpecificationsTables).Click();

            Browser.Wait.ForDisplayedElement(ProductDetail.ProductAttributes);

            var productAttribute = ProductDetail.PdProdSpecificationsTables.Text.Contains(ProductDetail.ProductAttributeString);
            var productSpecification = ProductDetail.PdProdSpecificationsTables.Text.Contains(ProductDetail.ProductSpecificationString);

            if (productAttribute && productSpecification)
            {
                Assert.Displayed(ProductDetail.ProductAttributes, "Product Attributes Table does not displayed");
                Assert.Displayed(ProductDetail.ProductSpecificationsTables, "Product Specifications Tables does not displayed");
            }
            else if (productSpecification || productAttribute)
            {
                if (productSpecification)
                {
                    Assert.Displayed(ProductDetail.ProductSpecificationsTables, "Product Specifications Tables does not displayed");
                }
                else
                {
                    Assert.Displayed(ProductDetail.ProductAttributes, "Product Attributes Table does not displayed");
                }
            }
        }
    }


    public abstract class T7819_T7820_Base : ProductDetailTestsBase
    {
        protected T7819_T7820_Base(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config, Urls.HomePageUrl);
            var shortSku = ProductActions.GetSkuThatHasSpecificationsTables;

            Assert.DatabaseObject(shortSku, "ProductActions.GetSkuThatHasSpecificationsTables()");

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            Browser.Wait.IsVisibleElement(By.Id(ProductDetail.ProductPriceId));
            Browser.ScrollToElement(ProductDetail.PdProdSpecificationsTables);
            
            VerifySpecificationsTables();
        }

        public abstract void VerifySpecificationsTables();
    }
}
