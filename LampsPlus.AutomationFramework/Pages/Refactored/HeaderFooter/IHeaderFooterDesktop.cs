using System.Collections.Generic;
using System.Collections.ObjectModel;
using Automation.Framework;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.HeaderFooter
{
    public interface IHeaderFooterDesktop : IPageObjectModel
    {
        void NavigateToManageAccount();
        void ScrollToFooter();
        void HoverOverChandelierStickyNavigation();
        void SignOut();
        void NavigateToWishListThroughHeaderLink();
        void OpenMyAccountMenu();
        void OpenInspirationMenu();
        void OpenSavedMenu();
        void OpenSaleMenu();
        void CloseRateUsModal();
        void OpenSessionMenu();
        void OpenStoresMenu();
        void OpenSignInMenu();
        void LoadLightingCatalog();
        void HoverOverSignInLink();
        void OpenHeaderChatModal();
        void CloseChatModal();
        void SignUpForCouponsOffersAndSaleAlerts(Account account);
        void OpenChandelierMenu();
        void OpenCeilingLightsMenu();
        void OpenLampsMenu();
        void OpenWallLightsMenu();
        void OpenFooterChatModal();
        void OpenHotelProgramsMenu();
        void HoverOverAccountLinkWhileStoreInSession();
        void OpenAccountMenuForStoreInSession();
        void OpenHospitalityLampsMenu();
        void NavigateToEmailPageFromFooter(string Email);
        int CartItemCount { get; }
        int GetNumberOfWishListItems();
        bool IsStoreNumberFieldVisible();
        bool IsEmailSubscribeFieldVisible();
        bool IsChatModalWindowVisible();
        bool IsStoresLinkDropdownVisible();
        bool IsSignInButtonVisible();
        bool IsEmployeeSignedInWithStoreInSession();
        bool WaitForRecentlyViewedSection(string recentlyViewedSelector);
        bool IsOpenBoxLinkVisible();
        bool IsSignUpForCouponsOffersAndSaleAlertsLabelVisible();
        bool IsSignUpForCouponsOffersAndSaleAlertsMessageVisible();
        bool IsEmailSubscribeButtonVisible();
        bool IsSessionMenuVisible();
        bool IsSignInLinkVisible { get; }
        bool IsRateUsModalOpened();
        string FootLpProsPhoneNumber { get; }
        string DefaultProsNumber { get; }
        string GetLpLogoLink();
        string GetAllChandeliersLink();
        string GetDiningLivingLink();
        string GetFlushmountLink();
        string GetAllTableLampsLink();
        string GetWallLampsLink();
        string GetWishListLink();
        string GetSavedRoomLink();
        string GetCartIconLink();
        string GetProLampsLogoLink();
        string GetContactPhoneLink();
        string GetProContactUsPhoneNumber();
        string GetFooterLpProsPhoneNumber();
        string GetCartCountInHeader();
        string GetLampsPlusLogoLink();
        string GetLampsPlusContactUsPhoneNumber();
        string FreeShippingFreeReturnsDisclaimer();
        string FooterShippingTest();
        string GetRecentlyViewedSectionForStoreInSession();
        string GetStoreInSessionPhoneNumberLink();
        string GetFooterHomePageLink();
        string GetHospitalityContactPhoneLink();
        string GetHospitalityLampsLink();
        string GetHospitalityBestValueLink();
        string GetEmailSubscribeFieldText();
        Dictionary<string, string> GetHeaderElementsLinks();
        Dictionary<string, string> GetProAccountHeaderElementsLinks();
        Dictionary<string, string> GetInspirationHeaderElementsLinks();
        Dictionary<string, string> GetSavedHeaderElementsLinks();
        Dictionary<string, string> GetSaleHeaderElementLinksForPros();
        Dictionary<string, string> GetCommonFooterNavLinksLinks();
        Dictionary<string, string> GetFooterProUserSocialLinks();
        Dictionary<string, string> GetFooterProsUserSocialLinks();
        Dictionary<string, string> GetCommonFooterLegalLinks();
        Dictionary<string, string> GetFooterLinks();
        Dictionary<string, string> GetProsFooterLegalLinks();
        Dictionary<string, string> GetAccountHeaderElementsLinks();
        Dictionary<string, string> GetSaleHeaderElementLinks();
        Dictionary<string, string> GetChandelierMenuLinks();
        Dictionary<string, string> GetCeilingLightsMenuLink();
        Dictionary<string, string> GetLampsMenuLink();
        Dictionary<string, string> GetWallLightsMenuLink();
        Dictionary<string, string> GetFooterEmailIconLink();
        Dictionary<string, string> GetFooterB2BProgramsLinks();
        Dictionary<string, string> GetFooterAboutUsLinks();
        Dictionary<string, string> GetProsFooterAboutUsLinks();
        Dictionary<string, string> GetFooterCustomerServiceLinks();
        Dictionary<string, string> GetFooterResourcesLinks();
        Dictionary<string, string> GetFooterProsResourcesLinks();
        Dictionary<string, string> GetFooterSocialLinks();
        Dictionary<string, string> GetFooterProsHelpCenterLinks();
        Dictionary<string, string> GetFooterLegalLinks();
        Dictionary<string, string> GetStoreInSessionAccountHeaderLink();
        Dictionary<string, string> GetAccountHeaderElementsForStoreInSessionLinks();
        Dictionary<string, string> GetSaleHeaderElementLinksForStoreInSession();
        Dictionary<string, string> GetFooterStoreInSessionAboutUsLinks();
        Dictionary<string, string> GetHospitalityAccountHeaderElementsLinks();
        Dictionary<string, string> GetHospitalityOurCompanyElementsLinks();
        Dictionary<string, string> GetHospitalityHelpCenterElementsLinks();
        Dictionary<string, string> GetHospitalityResourcesElementsLinks();
        Dictionary<string, string> GetFooterHospitalityLegalLinks();
        ReadOnlyCollection<IElement> GetNavElements();
    }
}