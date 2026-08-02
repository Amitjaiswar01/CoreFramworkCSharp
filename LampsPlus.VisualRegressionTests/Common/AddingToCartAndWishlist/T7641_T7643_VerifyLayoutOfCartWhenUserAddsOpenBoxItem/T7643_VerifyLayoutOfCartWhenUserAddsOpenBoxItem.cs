using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.AddingToCartAndWishlist.T7641_T7643_VerifyLayoutOfCartWhenUserAddsOpenBoxItem
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7643_iPhone_VerifyLayoutOfCartWhenUserAddsOpenBoxItem : T7643_MobileBase
    {
        public T7643_iPhone_VerifyLayoutOfCartWhenUserAddsOpenBoxItem(ITestOutputHelper output, T7643_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfCartOpenBoxItemAddedToCart(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7643_Android_VerifyLayoutOfCartWhenUserAddsOpenBoxItem : T7643_MobileBase
    {
        public T7643_Android_VerifyLayoutOfCartWhenUserAddsOpenBoxItem(ITestOutputHelper output, T7643_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfCartOpenBoxItemAddedToCart(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7643_Emulator_VerifyLayoutOfCartWhenUserAddsOpenBoxItem : T7643_MobileBase
    {
        public T7643_Emulator_VerifyLayoutOfCartWhenUserAddsOpenBoxItem(ITestOutputHelper output, T7643_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfCartOpenBoxItemAddedToCart(string config) => Validate(Validate, config);
    }


    public class T7643_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7643_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetOpenBoxShortSku;
        }
    }


    /// <summary>
    /// Verify the layout of the Cart when a user adds an Open Box item.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9892
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7643
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9892"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7643")]
    public abstract class T7643_MobileBase : VisualTestsBaseMobile, IClassFixture<T7643_SharedSku_Fixture>
    {
        protected readonly T7643_SharedSku_Fixture Fixture;

        protected T7643_MobileBase(ITestOutputHelper output, T7643_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: User has identified an Open Box item using the query.
            InitializeVisualTest(config);
            var sku = Fixture.ShortSku;

            //Act: User has navigated to the PDP of the identified SKU.
            ProductDetail.NavigateToOpenBoxProductDetailByShortSku(sku);
            Assert.True(ProductDetail.IsCurrentPage, "user is not on Product Detail Page");

            //Act : User has captured the screenshot of the entire page.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.IgnoreStockCheckWrapper() });

            //Act : User has added the product to the cart.
            ProductDetail.AddToCart();
            Assert.True(Cart.IsCurrentPage, "User is not on cart page");

            //Act : User has captured the screenshot of the visible page.
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { Cart.IgnoreCartId() });
        }
    }
}