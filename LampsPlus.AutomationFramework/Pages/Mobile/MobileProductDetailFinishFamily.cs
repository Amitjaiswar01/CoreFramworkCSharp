using System.Collections.ObjectModel;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    class MobileProductDetailFinishFamily : ProductDetailFinishFamilyBase
    {
        public MobileProductDetailFinishFamily(IBrowser browser, IGlobalLocators globalLocators) : base(browser, globalLocators) { }

        #region CSS Selectors
        public override string OtherOptionsThumbClass => throw new System.NotImplementedException();
        #endregion

        #region Page Elements
        public override IElement MoreOptionsCollapsableSlider => OtherOptionsAccordion;

        public override ReadOnlyCollection<IElement> ItemsList => Browser.Locate.ElementsByClassName(JsWidgetOtherOptionsClass);
        #endregion
    }
}
