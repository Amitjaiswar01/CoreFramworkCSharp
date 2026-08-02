using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.AddingToCartAndWishList.T350_T541_VerifyUserCanRemoveItemFromWishList
{
    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T350_Windows_VerifyUserCanRemoveItemFromWishList : T350_DesktopBase
    {
        public T350_Windows_VerifyUserCanRemoveItemFromWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void VerifyUserRemoveItemFromWishList(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T350_Mac_VerifyUserCanRemoveItemFromWishList : T350_DesktopBase
    {
        public T350_Mac_VerifyUserCanRemoveItemFromWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void VerifyUserRemoveItemFromWishList(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T350_iPad_VerifyUserCanRemoveItemFromWishList : T350_DesktopBase
    {
        public T350_iPad_VerifyUserCanRemoveItemFromWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void VerifyUserRemoveItemFromWishList(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T350_TabletEmulator_VerifyUserCanRemoveItemFromWishList : T350_DesktopBase
    {
        public T350_TabletEmulator_VerifyUserCanRemoveItemFromWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void VerifyUserRemoveItemFromWishList(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that a user can remove an item from the Wish List.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10102
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T350
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10102"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T350")]
    public abstract class T350_DesktopBase : TestsBaseDesktop
    {
        protected T350_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /* Arrange :
            Sign in as a Customer 
            Add an item to a Wish List
            Navigate to WishList Page
            */
            InitializeFunctionalTest(config);
            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "P", "This test can only be executed against DBCLUST."); //Wishlist icon is not appearing for CSI account types.
            WishListWorkflow.AddSingleItemToWishList();

            // Act : Remove Items From the Wish List
            WishList.RemoveAllWishListItems();

            // Assert : Verify The Product is Removed from the Wish List
            Assert.True(WishList.IsWishListEmpty(), "Products are not Removed from the WishList");
        }
    }
}