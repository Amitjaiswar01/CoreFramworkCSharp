using xRetry;
using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ProductDetail.T7970_T7973_VerifyOpenBoxProductOnRegularPrice
{
    namespace LampsPlus.RegressionTests.Common.ProductDetail.T7970_T7973_VerifyOpenBoxProductOnRegularPrice
    {
        [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
        public class T7973_iPhone_VerifyOpenBoxProductOnRegularPrice : T7973_MobileBase
        {
            public T7973_iPhone_VerifyOpenBoxProductOnRegularPrice(ITestOutputHelper output) : base(output) { }

            [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
            [RetryTheory(3)]
            [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
            public void OpenBoxProductOnRegularPrice(string config) => Validate(config);
        }


        [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
        public class T7973_iPhone_Pro_VerifyOpenBoxProductOnRegularPrice : T7973_MobileBase
        {
            public T7973_iPhone_Pro_VerifyOpenBoxProductOnRegularPrice(ITestOutputHelper output) : base(output) { }

            [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
            [RetryTheory(3)]
            [InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI)]
            public void OpenBoxProductOnRegularPrice(string config) => Validate(config);
        }


        [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
        public class T7973_Android_VerifyOpenBoxProductOnRegularPrice : T7973_MobileBase
        {
            public T7973_Android_VerifyOpenBoxProductOnRegularPrice(ITestOutputHelper output) : base(output) { }

            [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
            [SkippableTheory]
            [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
            public void OpenBoxProductOnRegularPrice(string config) => Validate(config);
        }


        [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
        public class T7973_Emulator_VerifyOpenBoxProductOnRegularPrice : T7973_MobileBase
        {
            public T7973_Emulator_VerifyOpenBoxProductOnRegularPrice(ITestOutputHelper output) : base(output) { }

            [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
            [SkippableTheory]
            [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
            public void OpenBoxProductOnRegularPrice(string config) => Validate(config);
        }


        /// <summary>
        /// Verify Open Box Product on Regular Price
        /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10815
        /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7973
        /// </summary>
        [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
        [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10815"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7973")]
        public abstract class T7973_MobileBase : TestsBaseMobile
        {
            protected T7973_MobileBase(ITestOutputHelper output) : base(output) { }

            protected void Validate(string config)
            {
                //Arrange : Using query get a OpenBox item with Saving Amount greater than $5
                InitializeFunctionalTest(config);

                var openBoxProduct = ProductActions.GetSkuWithSavingsGreaterThan5Dollar();
                Assert.DatabaseObject(openBoxProduct, "ProductActions.GetSkuWithSavingsGreaterThan5Dollar");

                var productPriceDb = TextActions.FormatToTwoDecimals(openBoxProduct.RetailPrice58);
                var strikeThroughPriceDb = TextActions.FormatToTwoDecimals(openBoxProduct.StrikeThroughPrice);
                var savingsValueDb = $"Save {TextActions.FormatPrice(openBoxProduct.Savings)}";

                //Act : Navigate to PDP of OpenBox item
                ProductDetail.NavigateToOpenBoxProductDetailByShortSku(openBoxProduct.ShortSku);

                var mainPricePdp = TextActions.FormatToTwoDecimals((decimal)ProductDetail.GetProductPrice());
                var strikeThroughPricePdp = TextActions.RemoveDollarSign(ProductDetail.GetStrikeThroughPriceOnPdp());
                var saveAmountPdp = ProductDetail.GetSaveAmountOnPdp();

                //Assert : Verify that "OPEN BOX OUTLET PRICE" Text is displays on Pdp
                Assert.Equals(ProductDetail.GetOpenBoxCallout(), "OPEN BOX OUTLET PRICE", "Open Box Outlet text is not displayed");

                //Assert : Verify Main Price, Strike Through Price and Savings Amount match on Pdp and database
                Assert.Equals(productPriceDb, mainPricePdp, "Main Price does not match with database");
                Assert.Equals(strikeThroughPriceDb, strikeThroughPricePdp, "Strike Through Price does not match with database");
                Assert.Equals(savingsValueDb, saveAmountPdp, "Savings Amount does not match with database");

                //Assert : Verify Ends Verbiage and Sale Verbiage does not display on Pdp
                Assert.False(ProductDetail.IsEndsVerbiageVisible, "Ends verbiage displays on Pdp");
                Assert.False(ProductDetail.IsSaleVerbiageVisible, "Sale verbiage displays on Pdp");

                //Act : Scroll down the page until the Sticky Filter section display
                Browser.ScrollToBottomOfWindow();

                var mainPriceStickyHeader = TextActions.FormatToTwoDecimals(ProductDetail.GetProductPriceOnStickyHeader());

                //Assert : Verify that "OPEN BOX" Text is displays on Sticky header
                Assert.True(ProductDetail.IsOpenBoxVerbiageVisibleOnStickyHeader, "Open Box verbiage is not displayed on Sticky header");

                //Assert : Verify Main Price on Sticky header and in database
                Assert.Equals(productPriceDb, mainPriceStickyHeader, "Main Price does not match with database");
            }
        }
    }
}