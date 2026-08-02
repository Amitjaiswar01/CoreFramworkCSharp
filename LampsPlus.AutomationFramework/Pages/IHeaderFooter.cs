using System.Collections.ObjectModel;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.AutomationFramework.Pages
{
    /// <summary>
    /// Common across all pages.
    /// </summary>
    public interface IHeaderFooter
    {
        #region Class Setup
        string HrdSignOutId { get; }
        string PixleeContainerClass { get; }
        string LpIconMenuClass { get; }
        string FooterContainerClass { get; }
        string FtrAboutUsId { get; }
        string FtrCatalogsId { get; }
        string FtrHelpId { get; }
        string FtrProsId { get; }
        string FtrReturnPolicyId { get; }
        string HeaderAccountClass { get; }
        string HdrSignOutId { get; }
        string LpCollapsibleHeaderClass { get; }
        string LpCollapsibleSubmenu { get; }
        string LpmmMenuContainerClass { get; }
        string MoreLikeThisClass { get; }
        string PortfolioItemCountSelector { get; }
        string SubmitRatingBtnClass { get; }
        string RateUsConfirmationPageId { get; }
        string FooterChatLinkXpath { get; }
        string InstagramFeedXpath { get; }
        string UserNameId { get; }
        string CategoryDropDownsClass { get; }
        string LpFooterRateUsId { get; }
        string RateUsCommentId { get; }
        string WidgetFloatingWrapperClass { get; }
        string OpenPositionsBtnClass { get; }
        string AboutUsSplashCaptionClass { get; }
        string ToggleHelpMenuClass { get; }
        #endregion

        #region Page Elements
        IElement RateUsStarsFifthStarElement { get; }
        IElement SubmitRatingBtn { get; }
        IElement BodyElement { get; }
        IElement FooterAboutUsLink { get; }
        IElement FooterAdviceAndTipsLink { get; }
        IElement FooterCareersLink { get; }
        IElement FooterCatalogsLink { get; }
        IElement FooterContainer { get; }
        IElement FooterHelpLink { get; }
        IElement FooterLpProsLink { get; }
        IElement FooterReturnPolicyLink { get; }
        IElement GetChandeliersNavElement(string config);
        IElement HamburgerMenu { get; }
        IElement HamburgerMenuContainer { get; }
        IElement InstagramFeed { get; }
        IElement FooterChatLink { get; }
        IElement HeaderAccountButton { get; }
        IElement RateUsConfirmationPage { get; }
        IElement RateUsStarsContainer { get; }
        IElement SavedIcon { get; }
        IElement SignUpForEmailUpdatesSubmitButton { get; }
        IElement SignOutLink { get; }
        IElement SignUpForCouponsOffersAndSaleAlertsField { get; }
        IElement UserNameLink { get; }
        IElement Footer { get; }
        IElement RateUs { get; }
        IElement RateUsComment { get; }

        ReadOnlyCollection<IElement> NavElements { get; }
        #endregion

        /// <summary>
        /// Log class to update log messages.
        /// </summary>
        Log Log { get; }

        /// <summary>
        /// Instance of a Browser to enable browser specific UI testing.
        /// </summary>
        IBrowser Browser { get; }

        /// <summary>
        /// Get nav link with "All " text within a displayed and hovered nav element.
        /// <param name="navElement">IElement of a nav element category in header nav.</param>
        /// <param name="operatingSystem">Tests base operating System.</param>
        /// </summary>
        /// <returns>IElement that matches criteria</returns>
        IElement GetNavLinkWithAllText(IElement navElement, OperatingSystem operatingSystem);

        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        /// <param name="url">URL to navigate to. This must be begin with http:// or https://.</param>
        void Navigate(string url);
    }
}