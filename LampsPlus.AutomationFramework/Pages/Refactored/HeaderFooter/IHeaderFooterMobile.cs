using System.Collections.Generic;
using OpenQA.Selenium;
using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.HeaderFooter
{
    public interface IHeaderFooterMobile : IHeaderFooterDesktop
    {
        void HideSearchField();
        void ToggleHamburgerMenu();
        void ToggleSearchIcon();
        void OpenLpMenu();
        void SelectSignInButton();
        bool IsSearchFieldHidden();
        List<IElement> GetMyAccountElements();
        By GetHamburgerMenu();
        IElement GetHamburgerMenuSublist();
        IElement GetSearchField();
        IElement GetFooterEmailField();
        IElement WaitForEmailSubscribeElementToLoad();
        IElement GetChandeliersNavLink();
        IElement GetAllChandeliersLink();
        string GetFooterCallButton();
        string GetSignInText();
        string GetCreateAccountString();
        string GetCallButtonPhoneNumber();
        string GetExpectedEmailSubscribeString();
        string GetGlobalNavLink(IElement parentElement, IElement subElement);
        Dictionary<string, string> GetGlobalNavLinks();
        Dictionary<string, string> GetMobileProUserFooterNavLinksLinks();
        Dictionary<string, string> GetMobileProUserFooterLegalLinks();
    }
}