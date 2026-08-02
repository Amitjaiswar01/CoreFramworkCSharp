using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium.Support.UI;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview
{
    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T122_Windows_VerifyRemoveLinkModifiesTotalPrice : T122_DesktopBase
    {
        public T122_Windows_VerifyRemoveLinkModifiesTotalPrice(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void RemoveLinkModifiesTotalPrice(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T122_Mac_VerifyRemoveLinkModifiesTotalPrice : T122_DesktopBase
    {
        public T122_Mac_VerifyRemoveLinkModifiesTotalPrice(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void RemoveLinkModifiesTotalPrice(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T122_iPad_VerifyRemoveLinkModifiesTotalPrice : T122_DesktopBase
    {
        public T122_iPad_VerifyRemoveLinkModifiesTotalPrice(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void RemoveLinkModifiesTotalPrice(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T122_TabletEmulator_VerifyRemoveLinkModifiesTotalPrice : T122_DesktopBase
    {
        public T122_TabletEmulator_VerifyRemoveLinkModifiesTotalPrice(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void RemoveLinkModifiesTotalPrice(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that clicking the 'Remove' link in the edit popup removes modified price.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5328
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T122
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5328"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T122")]
    [Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    public abstract class T122_DesktopBase : ShoppingCartTestsBase
    {
        protected T122_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            var setup = new TestSetup(config, useEmployeeManagerAccount:true) { AccountConfig = { UseEmployeeManagerAccount = true } };
            InitializeFramework(config, setup: setup);

            var shortSku = ProductActions.GetRandomComboKitSku;

            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = shortSku });

            var totalBeforeDiscount = OrderSummaryBlock.ProductTotalValue.Text;

            Browser.Wait.ForClickableElement(CartOverview.CartEditPriceElement).Click();
            Browser.Wait.ForClickableElement(CartOverview.TextPercentDiscountField).SendKeys("5");
            new SelectElement(CartOverview.SelDiscountReasonField.InternalElement).SelectByIndex(1);

            var cachedButton = CartOverview.ApplyDiscountButton;
            cachedButton.Click();
            Browser.Wait.UntilElementUnloads(cachedButton);

            CartOverview.CartEditPriceElement.Click();
            Browser.Wait.ForDisplayedElement(CartOverview.DiscountTooltipRemoveButton).Click();

            Assert.Equals(totalBeforeDiscount, OrderSummaryBlock.ProductTotalValue.Text, "Order total after applying and removing discount is not equal.");
            ShoppingCartWorkflow.EmptyCart();
        }
    }
}
