using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.Sort.T7761_VerifyFunctionalityAvailableAtThisLocationCheckbox
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    public class T7761_Windows_VerifyFunctionalityAvailableAtThisLocationCheckboxForKiosk : T7761_DesktopBase
    {
        public T7761_Windows_VerifyFunctionalityAvailableAtThisLocationCheckboxForKiosk(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_UNSI_ElasticSearch)]
        public void FunctionalityAvailableAtThisLocationCheckboxForKiosk(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    public class T7761_Mac_VerifyFunctionalityAvailableAtThisLocationCheckboxForKiosk : T7761_DesktopBase
    {
        public T7761_Mac_VerifyFunctionalityAvailableAtThisLocationCheckboxForKiosk(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SIS_UNSI)]
        public void FunctionalityAvailableAtThisLocationCheckboxForKiosk(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    public class T7761_iPad_VerifyFunctionalityAvailableAtThisLocationCheckboxForKiosk : T7761_DesktopBase
    {
        public T7761_iPad_VerifyFunctionalityAvailableAtThisLocationCheckboxForKiosk(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SIS_UNSI)]
        public void FunctionalityAvailableAtThisLocationCheckboxForKiosk(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    public class T7761_TabletEmulator_VerifyFunctionalityAvailableAtThisLocationCheckboxForKiosk : T7761_DesktopBase
    {
        public T7761_TabletEmulator_VerifyFunctionalityAvailableAtThisLocationCheckboxForKiosk(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_UNSI)]
        public void FunctionalityAvailableAtThisLocationCheckboxForKiosk(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the "Available at this location" checkbox presence and functionality for Kiosk
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10091
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7761
    /// </summary>      
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10091"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7761")]
    public abstract class T7761_DesktopBase : TestsBaseDesktop
    {
        protected T7761_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFunctionalTest(config);

            // Arrange : Navigate to Sort Page
            Browser.Navigate(Urls.AllChandeliersSortPageUrl);
            Assert.True(Sort.IsCurrentPage, "Sort Page is not loaded");

            // Assert : Verify "Available at this location" checkbox is present
            Assert.True(Sort.IsAvailableAtThisLocationCheckboxPresent, "The 'Available at this location' check box is not present.");

            // Act : Note the number of Sort page results, the breadcrumbs, and the URL
            List<string> pageContentBeforeCheckbox = Sort.GetPageContents();

            // Act : Select "Available at this location" checkbox
            Sort.SelectAvailableAtThisLocationCheckbox();

            List<string> pageContentAfterCheckbox = Sort.GetPageContents();

            /* Assert
            Verify the number of results is reduced.
            The URL remains the same.
            The breadcrumbs remain the same.
            */
            Assert.Condition(() => int.Parse(pageContentBeforeCheckbox[0]) > int.Parse(pageContentAfterCheckbox[0]), "The number of results is not reduced.");
            Assert.Equals(pageContentBeforeCheckbox[1], pageContentAfterCheckbox[1], "The URL is not same.");
            Assert.Equals(pageContentBeforeCheckbox[2], pageContentAfterCheckbox[2], "The breadcrumb is not same.");
        }
    }
}