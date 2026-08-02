using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Skip = Xunit.Skip;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.HeaderFooter.T543_T6995_VerifyHeaderFooterForProfessional
{
    public class T6995_iPhone_VerifyLpHeaderFooterDisplaysTheCorrectElementsPcsi : T6995_MobileBase
    {
        public T6995_iPhone_VerifyLpHeaderFooterDisplaysTheCorrectElementsPcsi(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI)]
        public void HeaderElements(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.HeaderFooter)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.HeaderFooter)]
    public class T6995_Android_VerifyLpHeaderFooterDisplaysTheCorrectElementsPcsi : T6995_MobileBase
    {
        public T6995_Android_VerifyLpHeaderFooterDisplaysTheCorrectElementsPcsi(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_PCSI)]
        public void HeaderElements(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.HeaderFooter)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.HeaderFooter)]
    public class T6995_Emulator_VerifyLpHeaderFooterDisplaysTheCorrectElementsPcsi : T6995_MobileBase
    {
        public T6995_Emulator_VerifyLpHeaderFooterDisplaysTheCorrectElementsPcsi(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_PCSI)]
        public void HeaderElements(string config) => Validate(config);
    }

    /// <summary>
    /// Verify that the Lamps Plus site header displays the correct elements for PCSI user
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7724
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T6995
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7724"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T6995")]
    public abstract class T6995_MobileBase : TestsBaseMobile
    {
        protected T6995_MobileBase(ITestOutputHelper output) : base(output)
        {
        }

        public void Validate(string config)
        {
            /*Arrange
            User has navigated to the Lamps Plus homepage.
            */
            InitializeFunctionalTest(config);
            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "P", "This test can only be executed against DBCLUST.");

            //Act and Assert Verify Header links
            Assert.Equals(HeaderFooter.GetProLampsLogoLink(), Urls.NormalizeUrl(Urls.HomePageUrl), "Lamps Plus Pro Logo  Element not displayed");
            Assert.Equals(HeaderFooter.GetCartIconLink(), Urls.NormalizeUrl(Urls.CartOverviewPageUrl), "Shopping Cart Icon  Element not displayed");
            
            //Assert click on Search icon hides Search field.
            HeaderFooter.HideSearchField();
            Assert.True(HeaderFooter.IsSearchFieldHidden(),"Search filed was not hidden");

            //Act and Assert 'My Account' Hamburger links
            HeaderFooter.OpenMyAccountMenu();
            HeaderFooter.GetMyAccountElements().ForEach(x => Assert.Displayed(x, "Hamburger menu My Account element is not displayed."));

            //Act and Assert 'GlobalNavLinks' Hamburger links
            Assert.True(DoCollectionsMatch(Urls.GlobalNavMobileUrls, HeaderFooter.GetGlobalNavLinks()), "Hamburger menu Global Nav elements link(s) do not match");

            HeaderFooter.ToggleHamburgerMenu();

            //*Act and Assert Footer links
            Assert.True(DoCollectionsMatch(Urls.MobileProUserFooterNavLinksUrls, HeaderFooter.GetMobileProUserFooterNavLinksLinks()), "Common Footer Nav link(s) do not match");
            Assert.True(DoCollectionsMatch(Urls.FooterProUserSocialUrls, HeaderFooter.GetFooterProUserSocialLinks()), "Common Footer Social link(s) do not match");
            Assert.True(DoCollectionsMatch(Urls.MobileProUserFooterLegalUrls, HeaderFooter.GetMobileProUserFooterLegalLinks()), "Common Footer Legal link(s) do not match");
            Assert.True(DoCollectionsMatch(Urls.FooterMobileUrls, HeaderFooter.GetFooterLinks()), "Footer link(s) do not match");
        }

        private bool DoCollectionsMatch(Dictionary<string, string> dictionaryOne, Dictionary<string, string> dictionaryTwo)
        {
            var mismatch = dictionaryOne.Where(entry => dictionaryTwo[entry.Key] != entry.Value)
                .ToDictionary(entry => entry.Key, entry => entry.Value);

            if (mismatch.Count <= 0) return true;
            foreach (var kvp in mismatch)
            {
                Log.Message($"Mismatch: linkname = {kvp.Key}, link = {kvp.Value}");
            }
            return false;
        }
    }
}