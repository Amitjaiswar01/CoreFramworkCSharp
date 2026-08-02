using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.ProductDetail
{
    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T260_Windows_VerifyShowMarginAvailableForEsi : T260_DesktopBase
    {
        public T260_Windows_VerifyShowMarginAvailableForEsi(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void VeryShowMargin(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T260_Mac_VerifyShowMarginAvailableForEsi : T260_DesktopBase
    {
        public T260_Mac_VerifyShowMarginAvailableForEsi(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void VeryShowMargin(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T260_iPad_VerifyShowMarginAvailableForEsi : T260_DesktopBase
    {
        public T260_iPad_VerifyShowMarginAvailableForEsi(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void VeryShowMargin(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T260_TabletEmulator_VerifyShowMarginAvailableForEsi : T260_DesktopBase
    {
        public T260_TabletEmulator_VerifyShowMarginAvailableForEsi(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void VeryShowMargin(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the 'Show margin' feature is available to employees.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5366
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T260
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5366"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T260")]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    //[Collection(LpTraits.UserRole.Employee)]
    public abstract class T260_DesktopBase : ProductDetailTestsBase
    {
        protected T260_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            var setup = new TestSetup(config, Urls.ContemporaryFloorLampsSortPageUrl, useEmployeeManagerAccount: true);
            InitializeFramework(config, setup: setup);

            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.MoreFiltersBtnClass.ToCssClassSelector()));
            Sort.FirstDisplayedProductElement.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));
            Browser.ClickByJs(ProductDetail.MarginModalLink);

            Assert.Displayed(GlobalLocators.Iframe, "Failed - The Show Margin Modal Not Displayed");
        }
    }
}
