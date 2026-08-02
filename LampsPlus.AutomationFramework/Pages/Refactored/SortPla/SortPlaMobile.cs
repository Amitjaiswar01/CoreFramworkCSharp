using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.SortPla
{
    public class SortPlaMobile : SortPlaDesktop, ISortPlaMobile
    {
        //Class members
        private string _shipsTodayQLClass = "shipsTodayQL";
        private string _productTitleClass = "productTitle";
        private string _gliderContainerClass = "glider-container";
        private string _notifyMeSubmitBtnId = "notifyMeSubmitBtn";
        private string _pdpStickyHeaderId = "pdpStickyHeader";
        private string _moreDetailsId = "moreDetails";
        private string _paypalLaterWidgetId = "paypalLaterWidget";
        private string _moreDetailsFieldSelector = ".field #moreDetails";

        private IElement PlaProductTitle => Browser.Locate.ElementByClassName(_productTitleClass);

        private bool IsCartPageLoaded(int timeToWait)
        {
            return Browser.Wait.IsVisibleElement(By.ClassName("checkOutNow"), 30);
        }

        protected IElement ShipsTodayQlElement => Browser.Locate.ElementBySelector(_shipsTodayQLClass.ToCssClassSelector());
        protected override IElement MoreDetailsLink => Browser.Locate.ElementBySelector(_moreDetailsFieldSelector);

        //Instances
        protected IBrowser Browser;
        public SortPlaMobile(IBrowser browser) : base(browser)
        {
            Browser = browser;
        }

        //Interface implementation
        public bool IsCurrentPage => Browser.Wait.IsVisibleElement(By.CssSelector("#sfpQuicklook.plaWrapper"));
        public bool IsStickyHeaderVisible => Browser.Wait.IsVisibleElement(By.ClassName(_pdpStickyHeaderId));

        public bool IsNotifyButtonVisible()
        {
            return Browser.Wait.IsVisibleElement(By.CssSelector(_notifyMeSubmitBtnId.ToCssIdSelector()));
        }

        public void NavigateToPdpThroughPlaProductName()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_productTitleClass));
            PlaProductTitle.Click();
        }

        public override void NavigateToPlaWithReviews(string url, string sku)
        {
            Browser.Navigate($"{url}?sfp={sku}");
            Browser.Wait.IsVisibleElement(By.ClassName(_gliderContainerClass));
        }

        public override void NavigateToPdpThroughMoreDetails()
        {
            Browser.Wait.IsVisibleElement(By.Id(_moreDetailsId));
            MoreDetailsLink.Click();
        }

        public override void PlaAddToCart()
        {
            Browser.Wait.IsVisibleElement(By.Id(_paypalLaterWidgetId));
            Browser.Wait.FiniteTime(5);
            Browser.Wait.ForCondition(() => AddToCartButton.IsInitialized, 10);
            Browser.Wait.ForElementToStopAnimating(AddToCartButton, 10);
            Browser.ClickOnButtonMultipleTimes(AddToCartButton, 5, IsCartPageLoaded);
        }
    }
}