using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.WarmUpTests.HomepageWarmUpTest
{
    public class T7477_WarmUpElementsAndPagesRelatedToHomepage : T7477_DesktopBase
    {
        public T7477_WarmUpElementsAndPagesRelatedToHomepage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void WarmUpTestForHomepage(string config) => Validate(config);
    }


    /// <summary>
    /// Warm up elements and pages related to the Homepage.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8402
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7477
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8402"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7477")]
    public abstract class T7477_DesktopBase : TestsBase
    {
        protected T7477_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            var promoCode = PromoCodeList.AutoPromoCodeTest;

            var setup = new TestSetup(config, $"{Urls.SortPagePromoCodeUrl}{promoCode.Name}");
            InitializeFramework(config, setup: setup);

            Browser.Navigate(Urls.MyLampsPlusPageUrl);

            var shortSku = ProductActions.GetListableInStockShortSku();
            Assert.DatabaseObject(shortSku, "ProductActions.GetListableInStockShortSku()");
            Browser.Navigate($"{Urls.MoreLikeThisPageBaseUrl}{shortSku}");

            Browser.Navigate($"{Urls.ProductFullPageBaseUrl}{shortSku}");

            Browser.Navigate(Urls.StoresPageUrl);

            Browser.Wait.ForDomReady();

            Stores.LampsPlusStoreRegionLinks[0].Click();
            Browser.Wait.ForDomReady();

            Stores.StoreDetailsRegionLinks[0].Click();

            Browser.Navigate(Urls.EmailSubscribeChangeEmailPreferencesUrl);

            Browser.Navigate(Urls.CreateAccountPageUrl);
            Browser.Navigate(Urls.LampsPlusAccountVerificationUrlStart);
        }
    }
}
