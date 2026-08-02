using System.Collections.ObjectModel;
using System.Web.UI;
using Automation.Framework;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;
using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class HeaderFooterBase : Page, IHeaderFooter
    {
        #region Class Setup
        public string LpIconMenuClass => "lpIcon-menu";
        #endregion

        #region CSS Selector Strings
        public string BcTextClass { get; } = "bcText";
        public string ANavBtnClass { get; } = "aNavBtn";
        public string CategoryDropDownsClass { get; } = "categoryDropDowns";
        public string FtrSubscribeBtnId { get; } = "ftrSubscribeBtn";
        public string HeaderAccountClass { get; } = "lpmmLoginStatus__link";
        public string HrdSignOutId { get; } = "hdrSignOut";
        public string NavWrapperId { get; } = "lpHeader-navWrapper";
        public string SavedPortfolioTotalSavedIconId { get; } = "savedPortfolio-totalSaved--icon";
        public string TxtEmailUpdatesRequestId { get; } = "txtEmailUpdatesRequest";
        public string EmailAddressFtrId { get; } = "EmailAddressFtr";
        public string UserNameId { get; } = "userName";
        private string RateUsStarsContainerClass { get; } = "rating";
        public string SubmitRatingBtnId { get; } = "btnSubmitRating";
        public string LpModalContentId { get; } = "lpModalContent";
        public string RateUsCommentId { get; } = "Comments";
        public string FooterAboutUsId { get; } = "footer_about_us";
        public string FooterAdviceAndTipsId { get; } = "footer_advice_and_tips";
        public string FooterCareersId { get; } = "footer_careers";
        public string FooterCatalogsId { get; } = "footer_catalogs";
        public string FooterReturnPolicyId { get; } = "footer_return_policy";
        public string WidgetFloatingWrapperClass { get; } = "widget-floating__wrapper";
        public string AboutUsSplashCaptionClass { get; } = "aboutUsSplash__caption";

        public abstract string ToggleHelpMenuClass { get; }
        public abstract string OpenPositionsBtnClass { get; }
        public abstract string FooterContainerClass { get; }
        public abstract string FtrAboutUsId { get; }
        public abstract string FtrCatalogsId { get; }
        public abstract string FtrHelpId { get; }
        public abstract string FtrProsId { get; }
        public abstract string FtrReturnPolicyId { get; }
        public abstract string PixleeContainerClass { get; }
        public abstract string HdrSignOutId { get; }
        public abstract string LpCollapsibleHeaderClass { get; }
        public abstract string LpCollapsibleSubmenu { get; }
        public abstract string LpmmMenuContainerClass { get; }
        public abstract string MoreLikeThisClass { get; }
        public abstract string PortfolioItemCountSelector { get; }
        public abstract string SubmitRatingBtnClass { get; }
        public abstract string RateUsConfirmationPageId { get; }
        public abstract string FooterChatLinkXpath { get; }
        public abstract string InstagramFeedXpath { get; }
        public abstract string RateUsId { get; }
        public abstract string LpFooterRateUsId { get; }
        #endregion

        #region Page Elements
        public IElement RateUsComment => Browser.Locate.ElementById(RateUsCommentId);
        public virtual IElement InstagramFeed => Browser.Locate.ElementByClassName(PixleeContainerClass);
        public IElement HeaderAccountButton => Browser.Locate.ElementByClassName(HeaderAccountClass);
        public IElement RateUsStarsContainer => Browser.Locate.ElementByClassName(RateUsStarsContainerClass);
        public IElement RateUsStarsFifthStarElement => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Value, "5", RateUsStarsContainer, true);

        public abstract IElement FooterChatLink { get; }
        public abstract IElement SubmitRatingBtn { get; }
        public abstract IElement FooterAboutUsLink { get; }
        public abstract IElement FooterAdviceAndTipsLink { get; }
        public abstract IElement FooterCareersLink { get; }
        public abstract IElement FooterCatalogsLink { get; }
        public abstract IElement FooterContainer { get; }
        public abstract IElement FooterReturnPolicyLink { get; }
        public abstract IElement RateUsConfirmationPage { get; }
        public abstract IElement SavedIcon { get; }
        public abstract IElement RateUs { get; }
        public abstract IElement BodyElement { get; }
        public abstract IElement GetChandeliersNavElement(string config);
        public abstract IElement SignOutLink { get; }
        public abstract IElement SignUpForCouponsOffersAndSaleAlertsField { get; }
        public abstract IElement UserNameLink { get; }
        public abstract IElement FooterHelpLink { get; }
        public abstract IElement FooterLpProsLink { get; }
        public abstract IElement HamburgerMenu { get; }
        public abstract IElement HamburgerMenuContainer { get; }
        public abstract IElement SignUpForEmailUpdatesSubmitButton { get; }
        public abstract IElement Footer { get; }

        public abstract ReadOnlyCollection<IElement> NavElements { get; }
        #endregion

        /// <inheritdoc />
        protected HeaderFooterBase(IBrowser browser) : base(browser) { }

        /// <summary>
        /// Get nav link with "All " text within a displayed and hovered nav element.
        /// <param name="navElement">IElement of a nav element category in header nav.</param>
        /// </summary>
        /// <returns>IElement that matches the criteria</returns>
        public abstract IElement GetNavLinkWithAllText(IElement navElement, OperatingSystem operatingSystem);
    }
}