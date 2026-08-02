using System;
using System.Collections.ObjectModel;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// https://www.lampsplus.com/
    /// </summary>
    public class Home : HomeBase
    {
        /// <inheritdoc />
        public Home(IBrowser browser, IGlobalLocators globalLocators) : base(browser, globalLocators) { }

        #region CSS Selector Strings
        private string CareersLandingLinksId { get; } = "careersLanding-links";
        private string RatingBoxClass { get; } = "pdSummaryTeaser--link"; 
        private string ReadReviewsClass { get; } = "pdSummaryTeaser__reviewCount";

        public override string PlaViewDetailsLinkXpath { get; } = "//*[@id='qlViewDetails']";
        public override string PlaDetailsId => throw new NotImplementedException();
        public override string HomeHeaderClass => throw new NotImplementedException();

        #endregion

        #region Page Elements
        //Elements that exist in both Desktop and Mobile views but are located differently.
        public override IElement BodyElement => Browser.Locate.ElementById(BdHomePageId);
        public override IElement PlaViewDetailsLinkElement => Browser.Locate.ElementByXpath("//*[@id='qlViewDetails']");


        //Elements that exist in Desktop view and NOT Mobile view.
        public override IElement CareersLinks => Browser.Locate.ElementById(CareersLandingLinksId);
        public override IElement PlaReviews => Browser.Locate.ElementByClassName(ReadReviewsClass);
        public override IElement PlaReviewStars => Browser.Locate.ElementByClassName(RatingBoxClass);
        public override IElement StoreNumberField => Browser.Locate.ElementById(TxtStoreNumberId);
        #endregion
    }
}
