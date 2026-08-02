using System;
using System.Collections.ObjectModel;
using System.Web.UI;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// Common across all pages.
    /// </summary>
    public class HeaderFooter : HeaderFooterBase
    {
        /// <inheritdoc />
        public HeaderFooter(IBrowser browser) : base(browser) { }
        public override string FooterContainerClass { get; } = "lpFooter";
        public override string PixleeContainerClass { get; } = "pixleeContainer";
        public override string RateUsId { get; } = "footer_rate_us";
        public override string LpFooterRateUsId { get; } = "footer_rate_us";
        public override string FtrProsId { get; } = "footer_pros";
        public override string FtrHelpId { get; } = "footer_help";
        public override string OpenPositionsBtnClass { get; } = "openPositionsBtn";

        public override string FtrAboutUsId => throw new NotImplementedException();
        public override string FtrCatalogsId => throw new NotImplementedException();
        public override string FtrReturnPolicyId => throw new NotImplementedException();
        public override string HdrSignOutId => throw new NotImplementedException();
        public override string LpCollapsibleHeaderClass => throw new NotImplementedException();
        public override string LpCollapsibleSubmenu => throw new NotImplementedException();
        public override string LpmmMenuContainerClass => throw new NotImplementedException();
        public override string MoreLikeThisClass => throw new NotImplementedException();
        public override string PortfolioItemCountSelector => throw new NotImplementedException();
        public override string SubmitRatingBtnClass => throw new NotImplementedException();
        public override string RateUsConfirmationPageId => throw new NotImplementedException();
        public override string FooterChatLinkXpath => throw new NotImplementedException();
        public override string InstagramFeedXpath => throw new NotImplementedException();
        public override string ToggleHelpMenuClass => throw new NotImplementedException();


        #region Page Elements
        //Elements that exist in both Desktop and Mobile views but are located differently.
        public override IElement SubmitRatingBtn => Browser.Locate.ElementById(SubmitRatingBtnId);
        public override IElement FooterChatLink => Browser.Locate.ElementByClassName(FooterContainerClass).FindElement(By.ClassName(BcTextClass));
        public override IElement FooterAboutUsLink => Browser.Locate.ElementById(FooterAboutUsId);
        public override IElement FooterAdviceAndTipsLink => Browser.Locate.ElementById(FooterAdviceAndTipsId);
        public override IElement FooterCareersLink => Browser.Locate.ElementById(FooterCareersId);
        public override IElement FooterCatalogsLink => Browser.Locate.ElementById(FooterCatalogsId);
        public override IElement FooterContainer => Browser.Locate.ElementByClassName(FooterContainerClass);
        public override IElement FooterHelpLink => Browser.Locate.ElementById(FtrHelpId);
        public override IElement FooterReturnPolicyLink => Browser.Locate.ElementById(FooterReturnPolicyId);
        public override IElement RateUsConfirmationPage => Browser.Locate.ElementById(LpModalContentId);
        public override IElement SavedIcon => Browser.Locate.ElementById(SavedPortfolioTotalSavedIconId);
        public override IElement SignOutLink => Browser.Locate.ElementBySelector(HrdSignOutId.ToCssIdSelector());
        public override IElement SignUpForEmailUpdatesSubmitButton => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Button, HtmlTextWriterAttribute.Id, FtrSubscribeBtnId);
        public override IElement BodyElement => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Body);
        public override IElement GetChandeliersNavElement(string config) => Browser.Locate.ElementByXpath("//*[@id=\"aChandeliers\"]");
        public override IElement SignUpForCouponsOffersAndSaleAlertsField => Browser.Locate.ElementById(TxtEmailUpdatesRequestId);
        public override IElement UserNameLink => Browser.Locate.ElementBySelector(UserNameId.ToCssIdSelector());
        public override IElement RateUs => Browser.Locate.ElementById(RateUsId);
        public override IElement FooterLpProsLink => Browser.Locate.ElementById(FtrProsId);

        public override IElement HamburgerMenu => throw new NotImplementedException();
        public override IElement HamburgerMenuContainer => throw new NotImplementedException();
        public override IElement Footer => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> NavElements => Browser.Locate.ElementsByClassName(ANavBtnClass, Browser.Locate.ElementById(NavWrapperId));
        #endregion

        public override IElement GetNavLinkWithAllText(IElement navElement, OperatingSystem operatingSystem)
        {
            if (operatingSystem == OperatingSystem.iPad)
            {
                var xElementCoordinate = 0;
                var yElementCoordinate = 0;
                Browser.GetElementCoordinates(navElement, ref xElementCoordinate, ref yElementCoordinate, 110);
                Browser.ClickWithTapByCoordinates(xElementCoordinate, yElementCoordinate);
            }
            else
            {
                Browser.MouseOverOnElement(navElement);
            }

            Browser.Wait.ForDomReady(); // wait for animation to finish on hover
            var categoryDropdowns = Browser.Locate.DisplayedElements(Browser.Locate.ElementsByClassName(CategoryDropDownsClass))[0];
            var anchorTags = Browser.Locate.ElementsByTagName(HtmlTextWriterTag.A, categoryDropdowns);

            return Browser.Locate.ElementWithText(anchorTags, AttributeSelectorType.Contains, "All ");
        }
    }
}