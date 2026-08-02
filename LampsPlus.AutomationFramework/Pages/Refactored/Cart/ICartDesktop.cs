using System.Collections.Generic;
using System.Collections.ObjectModel;
using Automation.Framework;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Cart
{
    public interface ICartDesktop : IPageObjectModel
    {
        IBrowser Navigate();
        IElement GetAdditionalDiscount();
        IElement GetCheckOutNowButton();
        IElement GetPaypalButton();
        IElement GetToolTip();
        IElement GetTextMarginField();
        IElement GetCartEmailModal();
        IElement GetDiscountVendorApprovalComment();
        IElement GetDiscountPriceField();
        IElement GetPercentDiscountPriceField();
        IElement GetMoreYouMayLike();
        void CheckOut();
        void ScrollToPayPalLaterWidget();
        void OpenEditPriceModal();
        void CheckPosBox();
        void RemoveCartItems();
        void CheckPosBoxForAllCartSkus(int cartItemsQty);
        void OpenShippingOptions();
        void OpenPromoCodeEntryField();
        void IsPromoCodeTextFieldVisible();
        void ClearPromoCode();
        void UpdatePromoCode(string promocode);
        void ApplyZipCode(string zipCode);
        void ShippingUpdate();
        void SelectLargeImageOnPrintModal();
        void OpenMoreDetailsDrawer();
        void VerifyShippingVerbiage();
        void OpenAndFocusEmailModal();
        void InputEmailRecipientsInForm(string[] recipientEmails);
        void SendCartEmail();
        void VerifySecondDayShippingVerbiage();
        void HoverOverCheckOutNowButton();
        void ChangeItemQuantity(string quantity);
        void OpenCartLinkModal();
        void OpenPrintModal();
        void ClosePrintModal();
        void SelectLargeImagesOption();
        void EmailShoppingCartWithOptionsSelected(params string[] recipientEmails);
        void EmailShoppingCart(params string[] recipientEmails);
        void SelectPrintButton();
        void EnterDiscountValue(decimal discountValue);
        void SelectAddDiscountButton();
        void ApplyDiscount(decimal discount);
        void ApplyDiscountForProduct(string discountApplied, string discountReasonApplied);
        void ApplyDiscountForProductOnIpad(string discountApplied, string discountReasonApplied);
        void OpenEditPriceModalOnIpad();
        void NavigateToPdpViaProductImageInCart();
        void NavigateToPdpViaProductNameInCart();
        void OpenDiscountTooltip();
        void RemoveAppliedDiscount();
        void RemoveSingleItemFromCart();
        void FillFormSelectByValue(IElement selectControl, string value);
        void SelectNoneLinkUnderPrintYourCartModal();
        void SmallImagesRadioButton();
        void SelectModalPrintCartBtn();
        void EnterSkuInAddByStyle(string shortSku);
        void DeleteCart();
        void OpenDeleteCartModal();
        void AddSkuToCartByStyleNumber(string sku);
        void RemovePromoCode();
        void CartElementDelete();
        void WaitForUndoLinkToDisappear();
        void SelectPrintInStoreButton();
        void SelectEditCartLink();
        decimal GetProductTotal();
        decimal GetSubTotal();
        decimal GetTaxAmount(string page = null);
        decimal GetSaleTaxAmount();
        decimal GetShippingTotal();
        decimal GetOrderTotalCost();
        decimal GetOrderTotalWithoutDiscount();
        decimal GetAdditionalDiscounts();
        decimal GetShippingCost();
        decimal GetSubTotalCost();
        decimal GetDiscountTotalCost();
        decimal GetCalculatedPromoDiscount(int discountRate, bool withoutDiscount = false, bool employeeDiscount = false);
        decimal GetCalculateSubTotal(int discountRate, bool withoutDiscount = false, bool employeeDiscount = false);
        decimal EnterCartZipCodeForShippingOption(string countryCode, string zipCode, int index);
        decimal GetOrderSummaryValuesByIndex(int index);
        decimal GetProductTotalPriceWithoutDiscount(int index = 0);
        string EstimatedTaxLabel { get; }
        string GetProductNameOnCart();
        string GetSaleDiscountValue();
        string GetSaleEndsInCallOut();
        string GetShippingOptionsErrorText();
        string GetAdditionalDiscountLabel();
        string GetCartErrorModalText();
        string GetMarginTextValue();
        string GetAdditionalDiscountsLabel();
        string GetInvalidPromoCodeValue();
        string GetPromoCodeStatusMessage();
        string GetInvalidPromoCodeErrorMessage();
        string GetEmailThankYouMessageOnModal();
        string GetOrderSummaryBlockText();
        string GetSuccessfulEmailMessage(string lastEmail = null);
        string GetInvalidShortSku();
        string GetProductInventoryDetails();
        string GetPayPalCallout();
        string GetCartId();
        string GetTaxLabel();
        string GetLimitedQuantityCallout();
        string GetProductTitleOnCart();
        string UndoMessageProductName();
        int GetShippingChargeCost();
        int GetAllCartProductsPosLinkCount();
        int GetCountOfAllProductsInCart();
        int GetNumberOfShippingOptions();
        bool AreEmailsFoundInDatabase(params string[] recipientEmails);
        bool AreShippingZoneFieldsRemoved();
        bool IsCheckOutNowButtonDisabled { get; }
        bool IsMarginDisplayedOnEditPriceModal { get; }
        bool IsAllPosCheckboxesUnchecked { get; }
        bool IsAllPosLinkVisible { get; }
        bool IsPosLabelVisible { get; }
        bool IsShippingAndProcessingDisabled { get; }
        bool IsPaypalButtonDisabled { get; }
        bool IsPromoCodePrefixVisible { get; }
        bool VerifySkusInCartRemainSame(List<ProductModel> list1, List<ProductModel> list2);
        bool IsTextMarginFieldEmpty();
        bool IsAdditionalDiscountDisplayed();
        bool IsCartEmpty();
        bool AfterUndoMessageToDisappear();
        bool IsPaypalWidgetDisplayed();
        bool DoesCartMatchAddedProducts(Dictionary<string, int> addedProducts);
        List<ProductModel> GetListOfAllProductsOnCartPage();
        List<ProductModel> GetProductDetailsInCart(string shortSku);
        List<string> GetListOfCartSkus(string url, int cartItemsQty);
        List<string> GetCartLinkDetails();
        List<ShippingOptionItem> GetAvailableShippingOptions();
        ReadOnlyCollection<IElement> GetShippingTypeRadios();
        ReadOnlyCollection<IElement> GetShippingCostLabels();
        ReadOnlyCollection<IElement> GetShippingDaysLabels();
    }
}