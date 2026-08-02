using System.Linq;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.AutomationFramework;

namespace LampsPlus.RegressionTests.Common.Sort.T217_T451_VerifyH1Tag
{
    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T217_Windows_VerifyH1Tag : T217_DesktopBase
    {
        public T217_Windows_VerifyH1Tag(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void H1Tag(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T217_Mac_VerifyH1Tag : T217_DesktopBase
    {
        public T217_Mac_VerifyH1Tag(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void H1Tag(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T217_iPad_VerifyH1Tag : T217_DesktopBase
    {
        public T217_iPad_VerifyH1Tag(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void H1Tag(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T217_TabletEmulator_VerifyH1Tag : T217_DesktopBase
    {
        public T217_TabletEmulator_VerifyH1Tag(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void H1Tag(string config) => Validate(config);
    }

    /// <summary>
    /// Verify that h1 tags do not interfere with hybrid pages.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10080
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T217
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10080"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T217")]
    public abstract class T217_DesktopBase : TestsBaseDesktop
    {
        protected T217_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange: User is on the homepage. User has identified an available Search path.
            InitializeFunctionalTest(config);

            var searchPath = ProductActions.GetSearchPath();
            Assert.DatabaseObject(searchPath, "ProductActions.GetSearchPath()");

            /* Act: Navigate to the Search path. 
            Identify the h1 tag.
            Select a filter on the Sort page.
            */
            Sort.NavigateToSpecificSearchPath(searchPath);
            Assert.True(Sort.IsCurrentPage, "User is not on Sort page.");

            var initialH1TagText = Sort.GetH1TextBeforeAppliedFilters();
            var filterOptionText = Sort.ApplyFilters(1)[0].ElementAt(0).Value;
            var h1TagText = Sort.GetH1TagText();

            /* Assert: Verify that the h1 tag has changed from the original one.
            Verify that the h1 tag has changed to include whatever filter was added.
            */
            Assert.True(h1TagText.Contains(filterOptionText.ToLower()), $"{filterOptionText} Attribute Is Not Included In h1 Title");
            Assert.False(initialH1TagText == h1TagText, $"{initialH1TagText} is equal to {h1TagText} and it should not be");
        }
    }
}
