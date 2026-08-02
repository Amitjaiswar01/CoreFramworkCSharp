using System;
using System.Collections.ObjectModel;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// https://www.lampsplus.com/
    /// </summary>
    public class MobileHome : HomeBase
    {
        /// <inheritdoc />
        public MobileHome(IBrowser browser, IGlobalLocators globalLocators) : base(browser, globalLocators) { }

        #region CSS Selectors
        public override string PlaDetailsId { get; } = "moreDetails";
        public override string HomeHeaderClass { get; } = "homeLighting__header";
        public override string PlaViewDetailsLinkXpath => throw new NotImplementedException();

        #endregion

        #region Page Elements
        //Elements that exist in both Desktop and Mobile views but are located differently.
        public override IElement BodyElement => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Body);
        public override IElement PlaViewDetailsLinkElement => Browser.Locate.ElementById(PlaDetailsId);

        //Elements that exist in Desktop view and NOT Mobile view.
        public override IElement CareersLinks => throw new NotImplementedException();
        public override IElement PlaReviews => throw new NotImplementedException();
        public override IElement PlaReviewStars => throw new NotImplementedException();
        public override IElement StoreNumberField => throw new NotImplementedException();
        #endregion
    }
}
