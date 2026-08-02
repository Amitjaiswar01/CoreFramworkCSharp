using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.AddingToCartAndWishList.T346_T537_VerifySignedUserCanSaveAWishList
{
    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T346_Windows_VerifySignedUserCanSaveAWishList : T346_DesktopBase
    {
        public T346_Windows_VerifySignedUserCanSaveAWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void SignedUserCanSaveAWishList(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T346_Mac_VerifySignedUserCanSaveAWishList : T346_DesktopBase
    {
        public T346_Mac_VerifySignedUserCanSaveAWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void SignedUserCanSaveAWishList(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T346_iPad_VerifySignedUserCanSaveAWishList : T346_DesktopBase
    {
        public T346_iPad_VerifySignedUserCanSaveAWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void SignedUserCanSaveAWishList(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T346_TabletEmulator_VerifySignedUserCanSaveAWishList : T346_DesktopBase
    {
        public T346_TabletEmulator_VerifySignedUserCanSaveAWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void SignedUserCanSaveAWishList(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that a signed-in user can save a Wish List.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5487
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T346
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5487"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T346")]
    [Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    //[Collection(LpTraits.UserRole.Customer)]
    public abstract class T346_DesktopBase : TestsBaseDesktop
    {
        protected T346_DesktopBase(ITestOutputHelper output) : base(output) { }

        public virtual void Validate(string config)
        {
            InitializeFunctionalTest(config);

            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "P", "This test can only be executed against DBCLUST.");
            /*Arrangement
             User is signed in as a consumer.
             User has added an item to the Wish List.
             User is on the Wish List page.
             */
            WishListWorkflow.AddSingleItemToWishList();
            Assert.True(WishList.IsCurrentPage, "Current page is not WishList page");

            //Act
            WishList.RenameWishList(WishListTypes.WishListNames.NewWishList);

            //WishListAssertion
            Assert.Equals(WishListTypes.WishListNames.NewWishList, WishList.GetWishListHeaderText(), "Wish list names do not match");
        }
    }
}