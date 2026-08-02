using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;

using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// Common across all pages.
    /// </summary>
    public class MobileHeaderFooter : HeaderFooterBase
    {
        /// <inheritdoc />
        public MobileHeaderFooter(IBrowser browser) : base(browser) { }

        #region Class Setup

        #endregion  

        #region CSS Selector Strings
        private string FtrCareersId { get; } = "ftr-careers";
        private string _toggleMenuXpath { get; } = "//div[@class='toggleMenu']";



        public override string FooterContainerClass { get; } = "globalFooter";
        public override string FtrAboutUsId { get; } = "ftr-about-us";
        public override string FtrCatalogsId { get; } = "ftr-catalogs";
        public override string FtrHelpId { get; } = "ftr-help";
        public override string FtrProsId { get; } = "ftr-pros";
        public override string FtrReturnPolicyId { get; } = "ftr-return-policy";
        public override string HdrSignOutId { get; } = "hdrSignOut";
        public override string LpCollapsibleHeaderClass { get; } = "lpCollapsible__header";
        public override string LpCollapsibleSubmenu { get; } = "lpCollapsible__submenu";
        public override string LpmmMenuContainerClass { get; } = "lpmmMenuContainer";
        public override string MoreLikeThisClass { get; } = "moreLikeThis";
        public override string PortfolioItemCountSelector { get; } = "button .lpIcon-favoriteselected";
        public override string SubmitRatingBtnClass { get; } = "btnSubmitRating";
        public override string RateUsConfirmationPageId { get; } = "bdRateUsConfirmation";
        public override string FooterChatLinkXpath { get; } = "(//*[@class = 'bcText']/a)[2]";
        public override string InstagramFeedXpath { get; } = "//*[contains(@class, 'instagramFeed')]";
        public override string LpFooterRateUsId { get; } = "ftr-rate-us";
        public override string OpenPositionsBtnClass { get; } = "wide";
        public override string ToggleHelpMenuClass { get; } = "toggleHelpMenu";
        public override string PixleeContainerClass => throw new NotImplementedException();
        public override string RateUsId => throw new NotImplementedException();
        #endregion

        #region Page Elements
        //Elements that exist in both Desktop and Mobile views but are located differently.
        public override IElement RateUs => Browser.Locate.ElementById(LpFooterRateUsId);
        public override IElement SubmitRatingBtn => Browser.Locate.ElementByClassName(SubmitRatingBtnClass);
        public override IElement FooterChatLink => Browser.Locate.ElementByXpath(FooterChatLinkXpath);
        public override IElement FooterAboutUsLink => Browser.Locate.ElementById(FtrAboutUsId);
        public override IElement FooterCareersLink => Browser.Locate.ElementBySelector(FtrCareersId.ToCssIdSelector());
        public override IElement FooterCatalogsLink => Browser.Locate.ElementById(FtrCatalogsId);
        public override IElement FooterReturnPolicyLink => Browser.Locate.ElementById(FtrReturnPolicyId);
        public override IElement RateUsConfirmationPage => Browser.Locate.ElementBySelector(RateUsConfirmationPageId.ToCssIdSelector());
        public override IElement SignOutLink => Browser.Locate.ElementBySelector(HdrSignOutId.ToCssIdSelector());
        public override IElement UserNameLink => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.A, "data-nav-sale", "Manage Account");


        public override IElement Footer => Browser.Locate.ElementByClassName(FooterContainerClass);
        public override IElement FooterContainer => Browser.Locate.ElementByClassName(MoreLikeThisClass);
        public override IElement FooterHelpLink => Browser.Locate.ElementById(FtrHelpId);
        public override IElement FooterLpProsLink => Browser.Locate.ElementById(FtrProsId);
        public override IElement HamburgerMenu => Browser.Locate.ElementByXpath(_toggleMenuXpath);
        public override IElement HamburgerMenuContainer => Browser.Locate.ElementByClassName(LpmmMenuContainerClass);
        public override IElement SavedIcon => Browser.Locate.ElementBySelector(PortfolioItemCountSelector);

        //Elements that exist in Desktop view and NOT Mobile view.
        public override IElement InstagramFeed => Browser.Locate.ElementByXpath(InstagramFeedXpath);
        public override IElement BodyElement => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Body);
        public override IElement FooterAdviceAndTipsLink => throw new NotImplementedException();

        public override IElement SignUpForCouponsOffersAndSaleAlertsField => Browser.Locate.ElementById(EmailAddressFtrId);
        public override IElement SignUpForEmailUpdatesSubmitButton => throw new NotImplementedException();

        public override IElement GetChandeliersNavElement(string config)
        {
            HamburgerMenu.Click();
            Browser.Wait.ForElementToStopAnimating(HamburgerMenuContainer);

            if (config.Contains("UNSI"))
            {
                return NavElements[1];
            }

            return NavElements[2];
        }


        // List of elements that exist in both Desktop and Mobile view.
        public override ReadOnlyCollection<IElement> NavElements => Browser.Locate.ElementByClassName(LpmmMenuContainerClass).FindElements(By.CssSelector(LpCollapsibleHeaderClass.ToCssClassSelector()));

        public override IElement GetNavLinkWithAllText(IElement navElement, OperatingSystem operatingSystem)
        {
            navElement.Click();
            Browser.Wait.ForElementToStopAnimating(navElement); // wait for animation to finish
            var parent = Browser.Locate.ParentElement(navElement);
            var categoryPageLinks = parent.FindElement(By.CssSelector(LpCollapsibleSubmenu.ToCssClassSelector())).FindElements(By.TagName(HtmlTextWriterTag.A.ToString()));
            var sortPageLinks = categoryPageLinks.Where(link => link.GetAttribute("href").Contains("/products/")).Where(link => link.Text.Contains("All ")).ToList();
            return sortPageLinks[MathHelper.GetRandomNumber(sortPageLinks.Count)];
        }
    }
}
#endregion