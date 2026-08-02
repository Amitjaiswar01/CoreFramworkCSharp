using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using OpenQA.Selenium;
using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    public abstract class ProductDetailFinishFamilyBase : Page, IProductDetailFinishFamily
    {
        protected ProductDetailFinishFamilyBase(IBrowser browser, IGlobalLocators globalLocators) : base(browser) { GlobalLocators = globalLocators; }

        #region Class Setup
        internal IGlobalLocators GlobalLocators { get; }

        public string MoreOptionsString => "MORE OPTIONS";
        public string MoreFinishesString => "MORE FINISHES";
        public string OtherOptionsString => "Other Options";
        #endregion

        #region CSS Selectors
        private string JsWidgetHeaderClass { get; } = "js-widget-header";
        public string JsWidgetOtherOptionsClass { get; } = "js-widget-other-options";
        public string PdScrollableContainerClass { get; } = "pdScrollableContainer";

        public abstract string OtherOptionsThumbClass { get; }
        #endregion

        #region Page Elements
        public IElement MoreOptionsCollapsableSectionHeader => Browser.Locate.ElementByClassName(JsWidgetHeaderClass, OtherOptionsAccordion);
        public IElement OtherOptionsAccordion => Browser.Locate.ElementByClassName(JsWidgetOtherOptionsClass);

        public abstract IElement MoreOptionsCollapsableSlider { get; }

        public abstract ReadOnlyCollection<IElement> ItemsList { get; }
        #endregion

        public List<string> GetOtherOptionsWidgetSkus()
        {
            var otherOptionsWidgetSkus = new List<string>();
            Browser.Wait.IsVisibleElement(By.CssSelector(JsWidgetOtherOptionsClass.ToCssClassSelector()));
            var itemsList = ItemsList;

            for (var i = 0; i < ItemsList.Count; i++)
            {
                otherOptionsWidgetSkus.Add(Browser.Locate.ElementByTagName(HtmlTextWriterTag.Img, itemsList[i]).GetAttribute(GlobalLocators.DataSkuAttribute));
            }
            return otherOptionsWidgetSkus;
        }
    }
}
