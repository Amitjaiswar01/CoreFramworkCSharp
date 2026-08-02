using System.Collections.ObjectModel;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    class ProductDetailFinishFamily : ProductDetailFinishFamilyBase
    {
        public ProductDetailFinishFamily(IBrowser browser, IGlobalLocators globalLocators) : base(browser, globalLocators) { }

        #region CSS Selectors Strings
        public override string OtherOptionsThumbClass { get; } = "pdOtherOptionsThumb";
        #endregion

        #region Page Elements
        public override IElement MoreOptionsCollapsableSlider => Browser.Locate.ElementByClassName(PdScrollableContainerClass);

        public override ReadOnlyCollection<IElement> ItemsList => Browser.Locate.ElementsByClassName(OtherOptionsThumbClass);
        #endregion
    }
}
