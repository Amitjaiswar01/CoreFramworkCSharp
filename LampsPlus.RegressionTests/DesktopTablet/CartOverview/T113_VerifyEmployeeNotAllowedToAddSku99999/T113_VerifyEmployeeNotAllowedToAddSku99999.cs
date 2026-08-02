using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview.T113_VerifyEmployeeNotAllowedToAddSku99999
{
    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T113_Windows_VerifyEmpNotAllowedToAddSku99999 : T113_DesktopBase
    {
        public T113_Windows_VerifyEmpNotAllowedToAddSku99999(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void VerifyEmpNotAllowedToAddSku99999(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that an employee is NOT allowed to add SKU 99999 through 'Add an Item'
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9919
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T113
    /// </summary>
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9919"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T113")]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]

    public abstract class T113_DesktopBase : TestsBaseDesktop
    {
        protected T113_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            /*Arrange
              Get any Random SKU
              Empty Cart
              Add Sku to Cart
            */
            InitializeFunctionalTest(config);
            ShoppingCartWorkflow.EmptyCart();
            var shortSku = ProductActions.GetAnySkuWithProductDetailPage;
            ProductDetail.AddSingleProductToCart(shortSku);

            /*Act
              Pass 99999 value in Add by Style# Container
              Get list of products in cart before and after adding invalid short sku
            */
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");
            var cartProductsBefore = Cart.GetListOfAllProductsOnCartPage();
            Cart.AddSkuToCartByStyleNumber(Cart.GetInvalidShortSku());
            var cartProductsAfter = Cart.GetListOfAllProductsOnCartPage();

            //Assert: The error modal is displayed and list of sku remains the same
            Assert.Equals(Messages.CartMessages.IncorrectSkuMessage, Cart.GetCartErrorModalText(), "User did not receive the message:Please specify a SKU other than 99999.");
            Assert.True(Cart.VerifySkusInCartRemainSame(cartProductsBefore, cartProductsAfter),"List are not equal");
        }
    }
}