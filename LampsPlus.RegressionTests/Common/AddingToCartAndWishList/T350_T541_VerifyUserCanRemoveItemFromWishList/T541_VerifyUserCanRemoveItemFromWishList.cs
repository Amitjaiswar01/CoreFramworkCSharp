using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.AddingToCartAndWishList.T350_T541_VerifyUserCanRemoveItemFromWishList
{
    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T541_iPhone_VerifyUserCanRemoveItemFromWishList : T541_MobileBase
    {
        public T541_iPhone_VerifyUserCanRemoveItemFromWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void VerifyUserRemoveItemFromWishList(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T541_Emulator_VerifyUserCanRemoveItemFromWishList : T541_MobileBase
    {
        public T541_Emulator_VerifyUserCanRemoveItemFromWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void VerifyUserRemoveItemFromWishList(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that a user can remove an item from the Wish List
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10102
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T541
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10102"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T541")]
    public abstract class T541_MobileBase : TestsBaseMobile
    {
        protected T541_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /* Arrange :
           Sign in as a Customer 
           Add an item to a Wish List
           Navigate to WishList Page
           */
            InitializeFunctionalTest(config);
            WishListWorkflow.AddSingleItemToWishList();

            // Act : Remove Items From the Wish List
            WishList.RemoveAllWishListItems();

            // Assert : Verify The Product is Removed from the Wish List
            Assert.True(WishList.IsWishListEmpty(), "Products are not Removed from the WishList");
        }
    }
}