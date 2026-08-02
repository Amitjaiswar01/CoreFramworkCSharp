using System.Collections.Generic;

namespace LampsPlus.AutomationFramework.Utilities
{
    public static class Urls
    {
        public static Dictionary<string, string> FooterLegalLinks => new Dictionary<string, string>
        {
            { "HomePageUrl", NormalizeUrl(HomePageUrl) },
            { "TermsOfUsePageUrl", NormalizeUrl(TermsOfUsePageUrl) },
            { "SiteMapPageUrl", NormalizeUrl(SiteMapPageUrl) },
            { "DoNotSellMyInfoUrl", NormalizeUrl(DoNotSellMyInfoUrl) },
        };

        public static Dictionary<string, string> FooterUrls => new Dictionary<string, string>
        {
            { "StoresPageUrl", NormalizeUrl(StoresPageUrl) },
            { "CareersPageUrl", NormalizeUrl(CareersPageUrl) },
            { "ProfessionalsInfoPolicyUrl", NormalizeUrl(ProfessionalsInfoPolicyUrl) },
            { "ManageAccountPageUrl", NormalizeUrl(ManageAccountPageUrl) },
            { "IdeasAdviceUrlProd", NormalizeUrl(IdeasAdviceUrlProd) },
            { "LpTwitterUrl", NormalizeUrl(LpTwitterUrl) },
            { "DoNotSellMyInfoUrl", NormalizeUrl(DoNotSellMyInfoUrl) },
        };

        public static Dictionary<string, string> FooterMobileUrls => new Dictionary<string, string>
        {
            { "DoNotSellMyInfoUrl", NormalizeUrl(DoNotSellMyInfoUrl) },
            { "HelpAndPoliciesPageUrl", NormalizeUrl(HelpAndPoliciesPageUrl) },
            { "CATransparencyPageUrl", NormalizeUrl(CaDisclosureTransparencyPageUrl) },
        };

        public static Dictionary<string, string> CommonFooterLegalUrls => new Dictionary<string, string>
        {
            { "SeeOurPolicyUrl", NormalizeUrl(SeeOurPolicyUrl) },
            { "CaDisclosureTransparencyPageUrl", NormalizeUrl(CaDisclosureTransparencyPageUrl) },
        };

        public static Dictionary<string, string> MobileFooterLegalUrls => new Dictionary<string, string>
        {
            { "CareersUrl", NormalizeUrl(CareersPageUrl) },
            { "RateUsUrl", NormalizeUrl(RateUsMobileUrl) },
            { "AccessibilityPageUrl", NormalizeUrl(AccessibilityPageUrl) },
            { "PrivacyPageUrl", NormalizeUrl(PrivacyPolicyPageUrl) },
            { "TermsOfUsePageUrl", NormalizeUrl(TermsOfUsePageUrl) },
            { "SitemapPageUrl", NormalizeUrl(SiteMapPageUrl) }
        };

        public static Dictionary<string, string> MobileProUserFooterLegalUrls => new Dictionary<string, string>
        {
            { "CareersUrl", NormalizeUrl(CareersPageUrl) },
            { "AccessibilityPageUrl", NormalizeUrl(AccessibilityPageUrl) },
            { "PrivacyPageUrl", NormalizeUrl(PrivacyPolicyPageUrl) },
            { "TermsOfUsePageUrl", NormalizeUrl(TermsOfUsePageUrl) },
            { "SitemapPageUrl", NormalizeUrl(SiteMapPageUrl) }
        };

        public static Dictionary<string, string> FooterProUserSocialUrls => new Dictionary<string, string>
        {
            { "LpPinterestUrl", NormalizeUrl(LpPinterestUrl) },
            { "LpInstagramUrl", NormalizeUrl(LpInstagramUrl) },
            { "LpFacebookUrl", NormalizeUrl(LpFacebookUrl) },
            { "LpYouTubeUrl", NormalizeUrl(LpYouTubeUrl) },
        };

        public static Dictionary<string, string> FooterProsUserSocialUrls => new Dictionary<string, string>
        {
            { "MyLampsPlusPageUrl", NormalizeUrl(MyLampsPlusPageUrl) },
            { "LpPinterestUrl", NormalizeUrl(LpPinterestUrl) },
            { "LpInstagramUrl", NormalizeUrl(LpInstagramUrl) },
            { "LpFacebookUrl", NormalizeUrl(LpFacebookUrl) },
            { "LpTwitterUrl", NormalizeUrl(LpTwitterUrl)},
            { "LpYouTubeUrl", NormalizeUrl(LpYouTubeUrl) }
        };

        public static Dictionary<string, string> FooterProsHelpCenterUrls => new Dictionary<string, string>
        {
            { "ProfessionalsInfoPolicyUrl", NormalizeUrl(ProfessionalsInfoPolicyUrl) },
            { "OrderHistoryPageUrl", NormalizeUrl(OrderHistoryPageUrl) },
            { "ReturnsPolicyPageUrl", NormalizeUrl(ReturnsPolicyPageUrl) }
        };

        public static Dictionary<string, string> FooterSocialUrls => new Dictionary<string, string>
        {
            { "LpPinterestUrl", NormalizeUrl(LpPinterestUrl) },
            { "LpInstagramUrl", NormalizeUrl(LpInstagramUrl) },
            { "LpFacebookUrl", NormalizeUrl(LpFacebookUrl) },
            { "LpYouTubeUrl", NormalizeUrl(LpYouTubeUrl) },
            { "LpTwitterUrl", NormalizeUrl(LpTwitterUrl)},
            { "LpTikTokUrl", NormalizeUrl(LpTiktokUrl)}
        };

        public static Dictionary<string, string> MobileFooterSocialUrls => new Dictionary<string, string>
        {
            { "LpPinterestUrl", NormalizeUrl(LpPinterestUrl) },
            { "LpInstagramUrl", NormalizeUrl(LpInstagramUrl) },
            { "LpFacebookUrl", NormalizeUrl(LpFacebookUrl) },
            { "LpYouTubeUrl", NormalizeUrl(LpYouTubeUrl) },
            { "LpTwitterUrl", NormalizeUrl(LpTwitterUrl)}
        };

        public static Dictionary<string, string> FooterLegalUrls => new Dictionary<string, string>
        {
            { "FooterTermsOfUseUrl", NormalizeUrl(TermsOfUsePageUrl) },
            { "FooterAccessibilityUrl", NormalizeUrl(AccessibilityPageUrl) },
            { "FooterPrivacyPolicyUrl", NormalizeUrl(PrivacyPolicyPageUrl) },
            { "FooterSiteMapUrl", NormalizeUrl(SiteMapPageUrl) },
            { "FooterCCPAPolicyUrl", NormalizeUrl(CcpaPolicyUrl)},
            { "FooterShippingPolicyUrl", NormalizeUrl(ShippingPolicyPageUrl)}
        };

        public static Dictionary<string, string> FooterHospitalityLegalUrls => new Dictionary<string, string>
        {
            { "FooterTermsOfUseUrl", NormalizeUrl(TermsOfUsePageUrl) },
            { "FooterSiteMapUrl", NormalizeUrl(SiteMapPageUrl) },
            { "FooterCCPAPolicyUrl", NormalizeUrl(CaDisclosureTransparencyPageUrl)}
        };

        public static Dictionary<string, string> StoreInSessionUrls => new Dictionary<string, string>
        {
            { "HeaderAccountUrl", NormalizeUrl(CreateAccountPageUrl) }
        };

        public static Dictionary<string, string> MobileHeaderUrls => new Dictionary<string, string>
        {
            { "MobileHeaderCartIconUrl", NormalizeUrl(CartOverviewPageUrl) },
            { "HamburgerLampsPlusLogoUrl", NormalizeUrl(HomePageUrl)}
        };

        public static Dictionary<string, string> ProsAboutUsFooterNavLinksUrls => new Dictionary<string, string>
        {
            { "FooterAboutLampsPlusUrl", NormalizeUrl(AboutUsPageUrl)},
            { "FooterStoreLocatorUrl", NormalizeUrl(StoresPageUrl)},
            { "FooterCareersUrl", NormalizeUrl(CareersPageUrl)},
            { "FooterCharitablePartnerships", NormalizeUrl(CharitablePartnershipsUrl)}
        };

        public static Dictionary<string, string> MobileProUserFooterNavLinksUrls => new Dictionary<string, string>
        {
            { "StoresPageUrl", NormalizeUrl(StoresPageUrl)},
            { "CatalogPageUrl", NormalizeUrl(CatalogsPageUrl) },
            { "AboutUsPageUrl", NormalizeUrl(AboutUsPageUrl) },
            { "OrderHistoryPageUrl", NormalizeUrl(OrderHistoryPageUrl) },
            { "ReturnsPolicyPageUrl", NormalizeUrl(ReturnsPolicyPageUrl) },
            { "HelpPageUrl", NormalizeUrl(HelpAndPoliciesPageUrl)}
        };

        public static Dictionary<string, string> MobileFooterNavLinksUrls => new Dictionary<string, string>
        {
            {"StoresPageUrl", NormalizeUrl(StoresPageUrl)},
            {"CatalogPageUrl", NormalizeUrl(CatalogsPageUrl)},
            {"AboutUsPageUrl", NormalizeUrl(AboutUsPageUrl)},
            {"ProsPageUrl", NormalizeUrl(ProfessionalsPageUrl)},
            {"OrderHistoryPageUrl", NormalizeUrl(OrderHistoryPageUrl)},
            {"ReturnsPolicyPageUrl", NormalizeUrl(ReturnsPolicyPageUrl)},
            {"HelpPageUrl", NormalizeUrl(HelpAndPoliciesPageUrl)},
            {"HospitalityPageUrl", NormalizeUrl(HospitalityPageUrl)},
        };

        public static Dictionary<string, string> GlobalNavMobileUrls => new Dictionary<string, string>
        {
            { "AllChandeliersSortPageUrl", NormalizeUrl(AllChandeliersSortPageUrl) },
            { "ChandeliersDiningLivingRoomUrl", NormalizeUrl(ChandeliersDiningLivingRoomUrl) },
            { "CeilingLightsFlushMountUrl", NormalizeUrl(CeilingLightsFlushMountUrl) },
            { "LampsAndShadesUrl", NormalizeUrl(TableLampsUrl) },
            { "WallLampsPageUrl", NormalizeUrl(WallLampsPageUrl) },
        };

        public static Dictionary<string, string> HeaderElementsUrls => new Dictionary<string, string>
        {
            { "LampsPlusOpenBoxUrl", NormalizeUrl(LampsPlusOpenBoxUrl) },
            { "ContactUsPageUrl", NormalizeUrl(ContactUsPageUrl) },
        };

        public static Dictionary<string, string> ProAccountHeaderElementsUrls => new Dictionary<string, string>
        {
            { "OrderHistoryPageUrl", NormalizeUrl(OrderHistoryPageUrl) },
            { "ManageAccountPageUrl", NormalizeUrl(ManageAccountPageUrl) },
            { "RecentlyViewedUrl", NormalizeUrl(RecentlyViewedUrl) },
            { "SignOutPageUrl", NormalizeUrl(SignOutPageUrl) },
        };

        public static Dictionary<string, string> HospitalityAccountHeaderElementsUrls => new Dictionary<string, string>
        {
            { "ManageAccountPageUrl", NormalizeUrl(ManageAccountPageUrl) },
            { "RecentlyViewedUrl", NormalizeUrl(RecentlyViewedUrl) },
            { "SignOutPageUrl", NormalizeUrl(SignOutPageUrl) },
        };

        public static Dictionary<string, string> AccountHeaderElementsUrls => new Dictionary<string, string>
        {
            { "OrderHistoryPageUrl", NormalizeUrl(OrderHistoryPageUrl) },
            { "CreateAccountPageUrl", NormalizeUrl(CreateAccountPageUrl) },
            { "RecentlyViewedUrl", NormalizeUrl(RecentlyViewedUrl) }
        };

        public static Dictionary<string, string> AccountHeaderElementsForStoreInSessionUrls => new Dictionary<string, string>
        {
            { "EmployeeToolsUrl", NormalizeUrl(EmployeeToolsPageUrl) },
            { "MyOrders", NormalizeUrl(EmployeeOrderLookupPageUrl) },
            { "ManageAccountUrl", NormalizeUrl(ManageAccountPageUrl) },
            { "SignOutUrl", NormalizeUrl(SignOutPageUrl)}
        };

        public static Dictionary<string, string> InspirationHeaderElementsUrls => new Dictionary<string, string>
        {
            { "RoomInspirationUrl", NormalizeUrl(RoomInspirationUrl) },
            { "RoomInspirationLivingRoomPageUrl", NormalizeUrl(RoomInspirationLivingRoomUrl)},
            { "RoomInspirationBedroomPageUrl", NormalizeUrl(RoomInspirationBedroomUrl)},
            { "RoomInspirationKitchenPageUrl", NormalizeUrl(RoomInspirationKitchenUrl)},
            { "RoomInspirationAllRoomsPageUrl", NormalizeUrl(RoomInspirationUrl)},
            { "LightingCatalogUrl", NormalizeUrl(LightingCatalogUrl) },
            { "IdeasAdviceUrlProd", NormalizeUrl(IdeasAdviceUrlProd) },
            { "BuyingGuidesUrlProd", NormalizeUrl(BuyingGuidesUrlProd)},
            { "StyleAndTrendsUrlProd", NormalizeUrl(StyleAndTrendsUrlProd)},
            { "RoomsUrlProd", NormalizeUrl(RoomsUrlProd)},
            { "MoreArticlesUrlProd", NormalizeUrl(MoreArticlesUrlProd)}
        };

        public static Dictionary<string, string> SavedHeaderElementsUrls => new Dictionary<string, string>
        {
            { "RoomsPageUrl", NormalizeUrl(RoomsPageUrl) },
            { "WishListPageUrl", NormalizeUrl(WishListPageUrl) },
        };

        public static Dictionary<string, string> SaleHeaderElementsUrlsForPros => new Dictionary<string, string>
        {
            { "OnSaleUrl", NormalizeUrl(OnSaleUrl) },
            { "OnSaleUrl2", NormalizeUrl(OnSaleUrl) },
            { "ChandeliersOnSaleUrl", NormalizeUrl(ChandeliersOnSaleUrl) },
            { "CeilingLightsOnSaleUrl", NormalizeUrl(CeilingLightsOnSaleUrl) },
            { "OutdoorLightinsOnSaleUrl", NormalizeUrl(OutdoorLightinsOnSaleUrl) },
            { "TableLampssOnSaleUrl", NormalizeUrl(TableLampssOnSaleUrl) },
            { "BathroomLightingOnSaleUrl", NormalizeUrl(BathroomLightingOnSaleUrl) },
            { "FurnituresOnSaleUrl", NormalizeUrl(FurnituresOnSaleUrl) },
            { "FloorLampssOnSaleUrl", NormalizeUrl(FloorLampssOnSaleUrl) },
            { "CeilingFanOnSaleUrl", NormalizeUrl(CeilingFanOnSaleUrl) },
            { "MirrosOnSaleUrl", NormalizeUrl(MirrosOnSaleUrl) },
            { "ProsSpecialPageUrl", NormalizeUrl(ProsSpecialPageUrl) },
            { "LpDailySalesUrl", NormalizeUrl(LpDailySalesUrl) },
            { "ClearanceViewPageUrl", NormalizeUrl(ClearanceViewPageUrl) },
            { "LampsPlusOpenBoxLinkFromSaleMenuUrl", NormalizeUrl(LampsPlusOpenBoxLinkFromSaleMenuUrl) }
        };

        public static Dictionary<string, string> SaleHeaderElementsUrls => new Dictionary<string, string>
        {
            { "OnSaleUrl", NormalizeUrl(OnSaleUrl) },
            { "OnSaleUrl2", NormalizeUrl(OnSaleUrl) },
            { "ChandeliersOnSaleUrl", NormalizeUrl(ChandeliersOnSaleUrl) },
            { "CeilingLightsOnSaleUrl", NormalizeUrl(CeilingLightsOnSaleUrl) },
            { "OutdoorLightinsOnSaleUrl", NormalizeUrl(OutdoorLightinsOnSaleUrl) },
            { "TableLampssOnSaleUrl", NormalizeUrl(TableLampssOnSaleUrl) },
            { "BathroomLightingOnSaleUrl", NormalizeUrl(BathroomLightingOnSaleUrl) },
            { "FurnituresOnSaleUrl", NormalizeUrl(FurnituresOnSaleUrl) },
            { "FloorLampssOnSaleUrl", NormalizeUrl(FloorLampssOnSaleUrl) },
            { "CeilingFanOnSaleUrl", NormalizeUrl(CeilingFanOnSaleUrl) },
            { "MirrosOnSaleUrl", NormalizeUrl(MirrosOnSaleUrl) },
            { "LpDailySalesUrl", NormalizeUrl(LpDailySalesUrl) },
            { "ClearanceViewPageUrl", NormalizeUrl(ClearanceViewPageUrl) },
            { "LampsPlusOpenBoxLinkFromSaleMenuUrl", NormalizeUrl(LampsPlusOpenBoxLinkFromSaleMenuUrl) }
        };

        public static Dictionary<string, string> SaleHeaderElementsUrlsForStoreInSession => new Dictionary<string, string>
        {
            { "OnSaleUrl", NormalizeUrl(OnSaleUrl) },
            { "OnSaleUrl2", NormalizeUrl(OnSaleUrl) },
            { "ChandeliersOnSaleUrl", NormalizeUrl(ChandeliersOnSaleUrl) },
            { "CeilingLightsOnSaleUrl", NormalizeUrl(CeilingLightsOnSaleUrl) },
            { "OutdoorLightinsOnSaleUrl", NormalizeUrl(OutdoorLightinsOnSaleUrl) },
            { "TableLampssOnSaleUrl", NormalizeUrl(TableLampssOnSaleUrl) },
            { "BathroomLightingOnSaleUrl", NormalizeUrl(BathroomLightingOnSaleUrl) },
            { "FurnituresOnSaleUrl", NormalizeUrl(FurnituresOnSaleUrl) },
            { "FloorLampssOnSaleUrl", NormalizeUrl(FloorLampssOnSaleUrl) },
            { "CeilingFanOnSaleUrl", NormalizeUrl(CeilingFanOnSaleUrl) },
            { "MirrosOnSaleUrl", NormalizeUrl(MirrosOnSaleUrl) },
            { "LpDailySalesUrl", NormalizeUrl(LpDailySalesUrl) },
            { "ClearanceViewPageUrl", NormalizeUrl(ClearanceViewPageUrl) },
        };

        public static Dictionary<string, string> ChandeliersMenuUrls => new Dictionary<string, string>
        {
            { "AllChandeliersUrl", NormalizeUrl(AllChandeliersSortPageUrl) },
            { "ChandeliersDiningLivingRoomUrl", NormalizeUrl(ChandeliersDiningLivingRoomUrl)}
        };

        public static Dictionary<string, string> CeilingLightsMenuUrl => new Dictionary<string, string>
        {
            { "CeilingLightsFlushMountUrl", NormalizeUrl(CeilingLightsFlushMountUrl) }
        };

        public static Dictionary<string, string> LampsMenuUrl => new Dictionary<string, string>
        {
            { "TableLampsUrl", NormalizeUrl(TableLampsSortPageUrl) }
        };

        public static Dictionary<string, string> WallLightsMenuUrl => new Dictionary<string, string>
        {
            { "WallLightsUrl", NormalizeUrl(WallLampsPageUrl) }
        };

        public static Dictionary<string, string> FooterEmailIconUrl => new Dictionary<string, string>
        {
            { "FooterEmailIconUrl", NormalizeUrl(ContactUsPageEmailUrl) }
        };

        public static Dictionary<string, string> FooterB2BProgramsUrls => new Dictionary<string, string>
        {
            { "FooterProsUrl", NormalizeUrl(ProfessionalsPageUrl) },
            { "FooterHospitalityUrl", NormalizeUrl(HospitalityPageUrl) }
        };

        public static Dictionary<string, string> FooterResourceUrls => new Dictionary<string, string>
        {
            { "FooterIdeasAndAdviceUrl", NormalizeUrl(IdeasAdviceUrlProd) },
            { "FooterCatalogsUrl", NormalizeUrl(CatalogsPageUrl) },
            { "FooterGiftCardsUrl", NormalizeUrl(GiftCardLandingPageUrl) },
            { "FooterManageAccountUrl", NormalizeUrl(ManageAccountPageUrl) },
            { "FooterNewHomeownerCouponUrl", NormalizeUrl(NewHomeOwnerPageUrl) }
        };

        public static Dictionary<string, string> FooterProsResourcesUrls => new Dictionary<string, string>
        {
            { "FooterIdeasAndAdviceUrl", NormalizeUrl(IdeasAdviceUrlProd) },
            { "FooterCatalogsUrl", NormalizeUrl(CatalogsPageUrl) },
            { "FooterGiftCardsUrl", NormalizeUrl(GiftCardLandingPageUrl) },
            { "FooterManageAccountUrl", NormalizeUrl(ManageAccountPageUrl) }
        };

        public static Dictionary<string, string> FooterSocialMediaUrls => new Dictionary<string, string>
        {
            { "FooterIdeasAndAdviceUrl", NormalizeUrl(IdeasAdviceUrlProd) },
            { "FooterCatalogsUrl", NormalizeUrl(CatalogsPageUrl) },
            { "FooterGiftCardsUrl", NormalizeUrl(GiftCardLandingPageUrl) },
            { "FooterManageAccountUrl", NormalizeUrl(ManageAccountPageUrl) },
            { "FooterNewHomeownerCouponUrl", NormalizeUrl(NewHomeOwnerPageUrl) }
        };

        public static Dictionary<string, string> FooterCustomerServiceUrls => new Dictionary<string, string>
        {
            { "FooterHelpUrl", NormalizeUrl(HelpAndPoliciesPageUrl) },
            { "FooterContactUsUrl", NormalizeUrl(ContactUsPageUrl) },
            { "FooterOrderStatusUrl", NormalizeUrl(OrderHistoryPageUrl) },
            { "FooterReturnPolicyUrl", NormalizeUrl(ReturnsPolicyPageUrl)}
        };

        public static Dictionary<string, string> FooterAboutUsProgramsUrls => new Dictionary<string, string>
        {
            { "FooterAboutLampsPlusUrl", NormalizeUrl(AboutUsPageUrl) },
            { "FooterStoreLocatorUrl", NormalizeUrl(StoresPageUrl) },
            { "FooterCareersUrl", NormalizeUrl(CareersPageUrl) },
            { "FooterLightingDesignServicesUrl", NormalizeUrl(LightingDesignServicesPageUrl) },
            { "FooterCharitablePartnerships", NormalizeUrl(CharitablePartnershipsUrl)}
        };

        public static Dictionary<string, string> FooterHospitalityOurCompanyProgramsUrls => new Dictionary<string, string>
        {
            { "FooterAboutLampsPlusUrl", NormalizeUrl(AboutUsHospitalityPageUrl) },
            { "FooterContactUsUrl", NormalizeUrl(ContactUsPageUrl) },
            { "FooterHospitalityFaqsUrl", NormalizeUrl(HospitalityFaqsPageUrl )}

        };

        public static Dictionary<string, string> FooterHospitalityHelpCenterProgramsUrls => new Dictionary<string, string>
        {
            { "FooterPrivacyPolicyUrl", NormalizeUrl(PrivacyPolicyPageUrl) },
            { "FooterAccessibilityUrl", NormalizeUrl(AccessibilityPageUrl) },
            { "FooterTermsOfUseUrl", NormalizeUrl(TermsOfUsePageUrl) }
        };

        public static Dictionary<string, string> FooterHospitalityResourcesProgramsUrls => new Dictionary<string, string>
        {
            { "FooterManageAccountUrl", NormalizeUrl(ManageAccountPageUrl) },
            { "FooterWarrantyInformationUrl", NormalizeUrl(WarrantyPageUrl) }
        };

        public static Dictionary<string, string> FooterAboutUsStoreInSessionUrls => new Dictionary<string, string>
        {
            { "FooterAboutLampsPlusUrl", NormalizeUrl(AboutUsPageUrl) },
            { "FooterStoreLocatorUrl", NormalizeUrl(StoresPageUrl) },
            { "FooterCareersUrl", NormalizeUrl(CareersPageUrl) },
            { "FooterLightingDesignServicesUrl", NormalizeUrl(LightingDesignServicesPageUrl) },
            { "FooterCharitablePartnerships", NormalizeUrl(CharitablePartnershipsUrl)}
        };

        public static Dictionary<string, string> SubCategoryUrls => new Dictionary<string, string>
        {
                { "Area Rugs", "https://www.lampsplus.com/products/rugs/" },
                { "Bathroom Lighting", "https://www.lampsplus.com/products/bathroom-lighting/" },
                { "Beds", "https://www.lampsplus.com/products/beds/" },
                { "Cabinets &amp; Chests", "https://www.lampsplus.com/products/cabinets-and-storage/" },
                { "Ceiling Fans", "https://www.lampsplus.com/fans/" },
                { "Ceiling Lighting", "https://www.lampsplus.com/ceiling-lighting/" },
                { "Chandeliers", "https://www.lampsplus.com/chandeliers/" },
                { "Close to Ceiling Lights", "https://www.lampsplus.com/products/close-to-ceiling-lights/" },
                { "Desk Lamps", "https://www.lampsplus.com/products/desk-lamps/" },
                { "Dining &amp; Entertaining", "https://www.lampsplus.com/products/entertaining-and-dining/" },
                { "FirePlaces", "https://www.lampsplus.com/products/fireplaces/" },
                { "Floor Lamps", "https://www.lampsplus.com/floor-lamps/" },
                { "Fountains", "https://www.lampsplus.com/products/fountains/" },
                { "Furniture", "https://www.lampsplus.com/furniture/" },
                { "Home Accessories", "https://www.lampsplus.com/products/home-accessories/" },
                { "Home Decor", "https://www.lampsplus.com/home-decor/" },
                { "Home Textiles", "https://www.lampsplus.com/products/home-textiles/" },
                { "Kitchen Lighting", "https://www.lampsplus.com/kitchen-lighting/" },
                { "Landscape Lighting", "https://www.lampsplus.com/landscape-lighting/" },
                { "Lamps", "https://www.lampsplus.com/lamps/" },
                { "Lamp Shades", "https://www.lampsplus.com/lamp-shades/" },
                { "Lighting Fixtures", "https://www.lampsplus.com/lighting-fixtures/" },
                { "Mirrors", "https://www.lampsplus.com/mirrors/" },
                { "Outdoor Lighting", "https://www.lampsplus.com/outdoor/" },
                { "Outdoor Security Lighting", "https://www.lampsplus.com/products/outdoor-lighting/usage_security/" },
                { "Pendant Lighting", "https://www.lampsplus.com/pendant-lighting/" },
                { "Picture Lights", "https://www.lampsplus.com/products/picture-lights/" },
                { "Pillows", "https://www.lampsplus.com/products/home-textiles/type_decorative-pillows/" },
                { "Recessed Lighting", "https://www.lampsplus.com/products/recessed-lighting/" },
                { "Sconces", "https://www.lampsplus.com/products/sconces/" },
                { "Sculptures", "https://www.lampsplus.com/products/sculpture/" },
                { "Seating", "https://www.lampsplus.com/furniture/seating/" },
                { "Tables", "https://www.lampsplus.com/furniture/tables/" },
                { "Table Lamps", "https://www.lampsplus.com/table-lamps/" },
                { "Track Lighting", "https://www.lampsplus.com/products/track-lighting/" },
                { "Under Cabinet Lights", "https://www.lampsplus.com/products/under-cabinet-lights/" },
                { "Up Lights - Clip Lights", "https://www.lampsplus.com/products/up-lights-@-clip-lights/" },
                { "Wall Art - Wall Décor", "https://www.lampsplus.com/products/wall-art/" },
                { "Wall Lamps", "https://www.lampsplus.com/products/wall-lamps/" },
                { "Wall Lights", "https://www.lampsplus.com/wall-lights/" },
        };

        #region Page Urls
        public static string SixteenPlusMoreColorsUrl => "https://www.lampsplus.com/products/32_1404~o1512/";
        public static string HundredPlusMoreColorsUrl => "https://www.lampsplus.com/products/manufacturer_color-plus/ ";
        public static string AboutUsHospitalityPageUrl => "https://www.lampsplus.com/about-hospitality/";
        public static string AboutUsMobilePageUrl => "https://www.lampsplus.com/about-us/";
        public static string AboutUsPageUrl => "https://www.lampsplus.com/about-us/";
        public static string AccessibilityPageUrl => "https://www.lampsplus.com/help-and-policies/accessibility/";
        public static string ArtShadeWallLampsPageUrl =>"https://www.lampsplus.com/products/wall-lamps/type_art-shade/";
        public static string AugmentedRealityUrl => "https://www.lampsplus.com/viewer/";
        public static string AllChandeliersSortPageUrl => "https://www.lampsplus.com/products/chandeliers/";
        public static string BathroomLightingUrl => "https://www.lampsplus.com/products/bathroom-lighting/";
        public static string CaDisclosureTransparencyPageUrl => "https://www.lampsplus.com/help-and-policies/disclosure-on-transparency-in-supply-chains/";
        public static string CareersPageUrl => "https://www.lampsplus.com/careers/";
        public static string CatalogsPageUrl => "https://www.lampsplus.com/lighting-catalog/";
        public static string CeilingFansUrl => "https://www.lampsplus.com/products/ceiling-fans/";
        public static string CeilingLightsFlushMountUrl => "https://www.lampsplus.com/products/close-to-ceiling-lights/usage_flush-mount/";
        public static string ChandeliersDiningLivingRoomUrl => "https://www.lampsplus.com/products/chandeliers/usage_dining-@-living-room/";
        public static string ChangeEmailPreferencePageUrl => "https://www.lampsplus.com/account/email-preferences/";
        public static string ClearancePageUrl => "https://www.lampsplus.com/products/chandeliers/clearance_view-clearance-items/";
        public static string ClearanceViewPageUrl => "https://www.lampsplus.com/products/clearance_view-clearance-items/";
        public static string ClockSortPageUrl => "https://www.lampsplus.com/products/clocks/";
        public static string ColorPlusCallOutUrl => "https://www.lampsplus.com/products/table-lamps/type_art-shade/manufacturer_color-plus/";
        public static string ColorPlusPageUrl => "https://www.lampsplus.com/color-plus/";
        public static string ConfirmationExpiredUrl => "https://www.lampsplus.com/cart/confirmationexpired/";
        public static string ContactUsPageEmailUrl => "https://www.lampsplus.com/contact-us/#emailUs";
        public static string ContactUsPageUrl => "https://www.lampsplus.com/contact-us/";
        public static string ContemporaryFloorLampsSortPageUrl => "https://www.lampsplus.com/products/floor-lamps/style_contemporary/";
        public static string CouponsMobilePageUrl => "https://www.lampsplus.com/lampsplus-coupons";
        public static string CouponsPageUrl => "https://www.lampsplus.com/lampsplus-coupons/";
        public static string CreateAccountPageUrl => "https://www.lampsplus.com/account/create/";
        public static string CrystalChandeliersUrl => "https://www.lampsplus.com/products/chandeliers/style_crystal/";
        public static string DesignYourOwnTrackLightingSystemPageUrl => "https://www.lampsplus.com/htmls/byotracklighting/frameset.aspx";
        public static string DeskLampsSortPageUrl => "https://www.lampsplus.com/products/desk-lamps/";
        public static string DevEnvPageUrl => "https://www.lampsplus.com/denv.aspx?j=1";
        public static string DoNotSellMyInfoUrl => "https://www.lampsplus.com/ccpa/";
        public static string EmailCartUrl => "https://www.lampsplus.com/cart/emailcart/";
        public static string FloorLampsSortPageUrl => "https://www.lampsplus.com/products/floor-lamps/";
        public static string FreeShippingAndFreeReturnsUrl => "https://www.lampsplus.com/products/fr_free-shipping-free-returns/";
        public static string GiftCardLandingPageUrl => "https://www.lampsplus.com/gift-cards/";
        public static string GoogleUrl => "https://www.google.com";
        public static string HelpAndPoliciesPageUrl => "https://www.lampsplus.com/help-and-policies/";
        public static string HomePageUrl => "https://www.lampsplus.com/";
        public static string SavedRoomPageUrl => "https://www.lampsplus.com/viewer/rooms/";
        public const string SortPagePromoCodeUrl = "https://www.lampsplus.com/products/lpced_coupon-eligible-design/?a=";
		public static string HospitalityFaqsPageUrl => "https://www.lampsplus.com/help-and-policies/lamps-plus-hospitality-info/";
        public static string HospitalityPageUrl => "https://www.lampsplus.com/hospitality-lighting/";
        public static string HospitalityProducts => "https://www.lampsplus.com/products/lphitems_view-only-hospitality-items/";
        public static string IdeasAdviceUrlProd => "https://www.lampsplus.com/ideas-and-advice/";
        public static string BuyingGuidesUrlProd => "https://www.lampsplus.com/ideas-and-advice/category/buying-guides/";
        public static string StyleAndTrendsUrlProd => "https://www.lampsplus.com/ideas-and-advice/category/style-and-trends/";
        public static string RoomsUrlProd => "https://www.lampsplus.com/ideas-and-advice/category/rooms/";
        public static string MoreArticlesUrlProd => "https://www.lampsplus.com/ideas-and-advice/";
        public static string LightingDesignServicesPageUrl => "https://www.lampsplus.com/in-home-consultations/";
        public static string LampShadesSortPageUrl => "https://www.lampsplus.com/products/lamp-shades/";
        public static string LampsPlusProductsUrl => "https://www.lampsplus.com/products/";
        public static string LampsPlusAccountVerificationUrlStart => "https://www.lampsplus.com/account/activation";
        public static string LampsPlusOpenBoxUrl => "https://www.lampsplus.com/products/openbox_view-open-box-items/";
        public static string LampsPlusOpenBoxLinkFromSaleMenuUrl => "https://www.lampsplus.com/products/openbox_view-open-box-items/";
        public static string LetsLincUrl => "https://lampsplus.letslinc.com/";
        public static string LightingCatalogUrl => "https://www.lampsplus.com/lighting-catalog/";
        public static string LpDailySalesUrl => "https://www.lampsplus.com/products/ds_daily-savings/";
        public static string LpFacebookUrl => "https://www.facebook.com/lampsplus";
        public static string LpInstagramMobileUrl => "https://www.instagram.com/lampsplus/";
        public static string LpInstagramUrl => "https://www.instagram.com/lampsplus/";
        public static string LpOnSaleUrl => "https://www.lampsplus.com/products/onsale_view-on-sale-items/";
        public static string LpPinterestUrl => "https://www.pinterest.com/lampsplus/";
        public static string LpTiktokUrl => "https://www.tiktok.com/@lampsplus";
        public static string LpTwitterUrl => "https://twitter.com/lampsplus";
        public static string LpYouTubeUrl => "https://www.youtube.com/user/LampsPlus?sub_confirmation=1";
        public static string MaxSessionLimitPageUrl => "https://www.lampsplus.com/connectascustomer/getallusersessions/";
        public static string NewHomeOwnerPageUrl => "https://www.lampsplus.com/newhomeowner/";
        public static string NotFooSearchPageUrl => "https://www.lampsplus.com/products/s_not-foo/";
        public static string OnSaleUrl => "https://www.lampsplus.com/products/onsale_view-on-sale-items/";
        public static string OregonStoreUrl => "https://www.lampsplus.com/stores/oregon/";
        public static string OutdoorTableSortUrl => "https://www.lampsplus.com/products/outdoor-tables/";
        public static string OutdoorLightingSortUrl => "https://www.lampsplus.com/products/outdoor-lighting/";
        public static string PdpFreeShippingReturnsUrl => "https://www.lampsplus.com/products/ceiling-fans/price-range_@@50-@-@@99@@@99/fr_free-shipping-free-returns/";
        public static string PrivacyPolicyPageUrl => "https://www.lampsplus.com/help-and-policies/your-privacy-and-security/";
        public static string ProductDetailPageUrl => "https://www.lampsplus.com/products/feiss-monterey-9-and-one-quarter-inch-high-outdoor-wall-light__00044.html";
        public static string ProductsUrlDirectory => "products";
        public static string ProfessionalContactUsMobileUrl => "https://www.lampsplus.com/pros/contact-us";
        public static string ProfessionalsInfoPolicyUrl => "https://www.lampsplus.com/help-and-policies/lamps-plus-professionals-info/";
        public static string ProfessionalsPageUrl => "https://www.lampsplus.com/pros/";
        public static string ProsSpecialPageUrl => "https://www.lampsplus.com/products/prosspecials_pros-specials/";
        public static string RecentlyViewedUrl => "https://www.lampsplus.com/recently-viewed/";
        public static string ReturnsPolicyPageUrl => "https://www.lampsplus.com/help-and-policies/return-policy/";
        public static string RoomInspirationUrl => "https://www.lampsplus.com/shop-by-room/";
        public static string SconcesSortPageUrl => "https://www.lampsplus.com/products/sconces/";
        public static string SeatingSortPageUrl => "https://www.lampsplus.com/products/seating/";
        public static string BrittoManufacturerWith16ColorsCalloutUrl => "https://www.lampsplus.com/products/lamp-shades/type_art-shade/manufacturer_ragnar/";
        public static string ShippingPolicyPageUrl => "https://www.lampsplus.com/help-and-policies/shipping-and-delivery/";
        public static string ShopByTrendUrl => "https://www.lampsplus.com/shop-by-trend/";
        public static string SiteMapPageUrl => "https://www.lampsplus.com/sitemap/";
        public static string TableSortUrl => "https://www.lampsplus.com/products/tables/";
        public static string TableLampsUrl => "https://www.lampsplus.com/table-lamps/";
        public static string TableLampsSortPageUrl => "https://www.lampsplus.com/products/table-lamps/";
        public static string TermsOfUsePageUrl => "https://www.lampsplus.com/help-and-policies/terms-of-use/";
        public static string WarrantyPageUrl => "https://www.lampsplus.com/hospitality-warranty/";
        public static string PaymentPageUrl => "https://www.lampsplus.com/cart/billing/";
        public static string CartOverviewPageUrl => "https://www.lampsplus.com/cart/";
        public static string EmailSubscribeChangeEmailPreferencesUrl => "https://www.lampsplus.com/account/email/?isFromFooter=true";
        public static string EmployeeOrderLookupPageUrl => "https://www.lampsplus.com/employee-tools/EmployeeOrderLookup.aspx";
        public static string EmployeeToolsPageUrl => "https://www.lampsplus.com/employee-tools/default.aspx";
        public static string ManageAccountPageUrl => "https://www.lampsplus.com/account/profile/";
        public static string MyLampsPlusPageUrl => "https://www.lampsplus.com/social/instagram/";
        public static string ManagePaymentOptionsPageUrl => "https://www.lampsplus.com/account/profile/paymentoptions/";
        public static string ManageShippingAddressPageUrl => "https://www.lampsplus.com/account/profile/shipping-addresses/";
        public static string OpenBoxProductPageUrl => "https://www.lampsplus.com/products/open-box/";
        public static string OrderConfirmationPageUrl => "https://www.lampsplus.com/cart/order-confirmation/";
        public static string OrderHistoryMobilePageUrl => "https://www.lampsplus.com/account/order-history/";
        public static string OrderHistoryPageUrl => "https://www.lampsplus.com/account/order-history/";
        public static string RoomsPageUrl => "https://www.lampsplus.com/viewer/rooms/";
        public static string RateUsMobileUrl => "https://www.lampsplus.com/#";
        public static string RateUsUrl => "https://www.lampsplus.com/rate-us/";
        public static string ShippingPageUrl => "https://www.lampsplus.com/cart/shipping/";
        public static string ShippingNotificationPageUrl => "https://www.lampsplus.com/cart/deliverypolicyagreement/";
        public static string SignInPageUrl => "https://www.lampsplus.com/account/sign-in/";
        public static string SignOutPageUrl => "https://www.lampsplus.com/account/sign-out?ReturnUrl=/";
        public static string StoresMobilePageUrl => "https://www.lampsplus.com/stores/";
        public static string StoresPageUrl => "https://www.lampsplus.com/stores/";
        public static string StoreAvailabilityUrl => "https://www.lampsplus.com/storeavailability/";
        public static string ScottsdaleStoreUrl => "https://www.lampsplus.com/stores/arizona/scottsdale-85250/";
        public static string SeeOurPolicyUrl => "https://www.lampsplus.com/help-and-policies/ccpa/";
        public static string WishListPageUrl => "https://www.lampsplus.com/wish-list/";
        public static string WallLampsPageUrl => "https://www.lampsplus.com/products/wall-lamps/";
        public static string SecureEmployeeToolsPageUrl => "https://www.lampsplus.com/secure/employee-tools/default.aspx";
        public static string HospitalityNightstandLampsPageUrl => "https://www.lampsplus.com/products/type_nightstand-lamps/";
        public static string HotelBrandProgramsBestValuePageUrl => "https://www.lampsplus.com/products/hotelbrand_any-hotel-@-best-value/";
        public static string ChandeliersOnSaleUrl => "https://www.lampsplus.com/products/chandeliers/onsale_view-on-sale-items/";
        public static string TableLampssOnSaleUrl => "https://www.lampsplus.com/products/table-lamps/onsale_view-on-sale-items/";
        public static string FloorLampssOnSaleUrl => "https://www.lampsplus.com/products/floor-lamps/onsale_view-on-sale-items/";
        public static string CeilingLightsOnSaleUrl => "https://www.lampsplus.com/products/close-to-ceiling-lights/onsale_view-on-sale-items/";
        public static string BathroomLightingOnSaleUrl => "https://www.lampsplus.com/products/bathroom-lighting/onsale_view-on-sale-items/";
        public static string CeilingFanOnSaleUrl => "https://www.lampsplus.com/products/ceiling-fans/onsale_view-on-sale-items/";
        public static string OutdoorLightinsOnSaleUrl => "https://www.lampsplus.com/products/outdoor-lighting/onsale_view-on-sale-items/";
        public static string FurnituresOnSaleUrl => "https://www.lampsplus.com/products/furniture/onsale_view-on-sale-items/"; 
        public static string MirrosOnSaleUrl => "https://www.lampsplus.com/products/mirrors/onsale_view-on-sale-items/";
        public static string ShopByFinishBronzeUrl => "https://www.lampsplus.com/products/finish_bronze/";
        public static string LightingCatalogSaleUrl => "https://www.lampsplus.com/lighting-catalog/sale-main/";
        public static string CcpaPolicyUrl => "https://www.lampsplus.com/help-and-policies/ccpa/";
        public static string CharitablePartnershipsUrl => "https://www.lampsplus.com/giving/";
        public static string RoomInspirationLivingRoomUrl => "https://www.lampsplus.com/shop-by-room/room_living-room!great-room/";
        public static string RoomInspirationBedroomUrl => "https://www.lampsplus.com/shop-by-room/room_bedroom/";
        public static string RoomInspirationKitchenUrl => "https://www.lampsplus.com/shop-by-room/room_kitchen/";
        #endregion

        #region Fragments
        public static string ReadReviewsUrlFragment => "#turntoReviewsSection";
	    #endregion

		public static string[] SortPageUrls => new []
        {
            AllChandeliersSortPageUrl,
            ClockSortPageUrl,
            ContemporaryFloorLampsSortPageUrl,
            DeskLampsSortPageUrl,
            FloorLampsSortPageUrl,
            OutdoorTableSortUrl,
            SconcesSortPageUrl,
            SeatingSortPageUrl,
            TableLampsSortPageUrl,
            TableSortUrl,
        };

        #region Certona Urls
        /// <summary>
        /// Appendable Base Url: https://lampsplus.com/sfp/
        /// </summary>
        public static string ProductFullPageBaseUrl => "https://lampsplus.com/sfp/";

        /// <summary>
        /// Appendable Base Url: https://www.lampsplus.com/sfp4/
        /// </summary>
        public static string Sfp4PageBaseUrl => "https://www.lampsplus.com/sfp4/";

        /// <summary>
        /// Appendable Base Url: https://www.lampsplus.com/products/floor-lamps/style_transitional/finish_black/color_black/type_torchiere/?sfp=>
        /// </summary>
        public static string PlaSortPageBaseUrl => "https://www.lampsplus.com/products/floor-lamps/style_transitional/finish_black/color_black/type_torchiere/?sfp=";

        /// <summary>
        /// Appendable Base Url: https://www.lampsplus.com/products/table-lamps/?sfp4=
        /// </summary>
        public static string Pla4SortPageBaseUrl => "https://www.lampsplus.com/products/table-lamps/?sfp4=";

        public static string PlaTableLampsSfpUrl => "https://www.lampsplus.com/products/table-lamps/?sfp=";

        /// <summary>
        /// Appendable Base Url: https://www.lampsplus.com/more-like-this/
        /// </summary>
        public static string MoreLikeThisPageBaseUrl => "https://www.lampsplus.com/more-like-this/";

        public static string NormalizeUrl(string url)
        {
            url = url.EndsWith("/") ? url.Substring(0, url.Length - 1) : url;
            url = url.Replace("https://", string.Empty).Replace("http://", string.Empty).Replace("www.", string.Empty);

            return url;
        }
        #endregion
    }
}
