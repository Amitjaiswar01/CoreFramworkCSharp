using System;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// Class to define all common elements on the Shipping and Billing pages.
    /// </summary>
    public class CustomerAddressInformation : CustomerAddressInformationBase
    {
        /// <inheritdoc />
        public CustomerAddressInformation(IBrowser browser, IOrderSummaryBlock orderSummaryBlock, IShipping shippingInstance, IGlobalLocators globalLocators, TestsBase testsBase) : base(browser, orderSummaryBlock, shippingInstance, globalLocators, testsBase) { }

        public override string AddressLabelClass { get; } = "addressLabel";
        public override string AddAnotherAddressFieldLinkXpath { get; } = "//button[@class='showAnotherAddressField anchorLink']";
        public override string NorthCarolinaString { get; } = "NC";

        public override string AddAnotherAddressFieldLinkClass => throw new NotImplementedException();
        public override string AddressFieldPairClass => throw new NotImplementedException();
        public override string CaliforniaString => throw new NotImplementedException();
        public override string FedExStateSelectorId => throw new NotImplementedException();
        public override string FedExShippingStateXpath => throw new NotImplementedException();
        public override string FieldCheckboxClass => throw new NotImplementedException();
        public override string ShowAddressLine2BtnClass => throw new NotImplementedException();
        public override string LpSelectMobileDrawerClass => throw new NotImplementedException();
        public override string PaymentInfoAddressFieldsetClass => throw new NotImplementedException();
        public override string PennsylvaniaString =>throw new NotImplementedException();
        public override string SingleShippingCountryId => throw new NotImplementedException();
        public override string UnitedStatesString => throw new NotImplementedException();
       

        /// <inheritdoc />
        public override IElement AddAnotherAddressFieldLink => Browser.Locate.ElementByXpath(AddAnotherAddressFieldLinkXpath);
        public override IElement IntAddAnotherAddressFieldLink => Browser.Locate.ElementBySelector($"{Shipping.PaymentInfoAddressFieldsetClass.ToCssClassSelector()} > {Shipping.ShowAnotherAddressFieldContainerClass.ToCssClassSelector()} > {HtmlTextWriterTag.Button}");
        public override IElement ChangeShippingApplyButton => Browser.Locate.ElementByXpath("//*[contains(@class,'updateAddr')]");
        public override IElement CountryField => Browser.Locate.ElementById(Shipping.SingleShippingCountryId);
        public override IElement FedExAddressValidationHeader => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.H1, FedExAddressValidationModal)[1];
        public override IElement SaveAddressCheckbox => Browser.Locate.ElementImmediately($"{Shipping.FieldCheckboxClass.ToCssClassSelector()} > {HtmlTextWriterTag.Label}");
        public override IElement SelectShippingAddressModal => Browser.Locate.ElementById(Shipping.SavedShippingAdressesModalId);
        public override IElement SaveAddressCheckboxInput => Browser.Locate.ElementByXpath(Shipping.EditShippingAddressSaveBtn);//TODO Experiment Implemented SaveAddress
        public override IElement ShippingAddressOption => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Label, HtmlTextWriterAttribute.For, "address1");
        public override IElement ShippingInformationModal => Browser.Locate.ElementById(Shipping.LpModalContentId);
        public override IElement ShipToDifferentAddressButton => Browser.Locate.ElementBySelector(Shipping.ShipToDifferentAddrClass.ToCssClassSelector());
        public override IElement StateField => Browser.Locate.ElementById(Shipping.SingleShippingStateId);

        public override IElement FedExApartmentStateSelection => throw new NotImplementedException();
        public override IElement CountrySelection => throw new NotImplementedException();
        public override IElement StateSelection => throw new NotImplementedException();
        
        public override void EnterDifferentCountryValueOnPaymentPage(IElement element, Address address)
        {
            FillFormSelectByValue(element, address.Country);
        }

        public override void SelectCountry(IElement element, Address address)
        {
            GlobalLocators.ClickDropdownByValue(element, address.Country);
        }

        public override void SelectState(IElement element, string state)
        {
            FillFormControlByText(element, state);
        }
        
        public override void SelectFedExState(IElement element, string state)
        {
            FillFormControlByText(element, state);
        }

        public override IElement GetCommonSaveAddressCheckbox(bool getMobileInput = false)
        {
            return SaveAddressCheckbox;
        }
    }
}
