using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.AddingToCartAndWishList.T346_T537_VerifySignedUserCanSaveAWishList
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.AddingToCartAndWishList)]
    //[Collection(LpTraits.BatchGroup.Mobile.AddingToCartAndWishList)]
    public class T537_iPhone_VerifySignedUserCanSaveAWishList : T537_MobileBase
    {
        public T537_iPhone_VerifySignedUserCanSaveAWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void SignedUserCanSaveAWishList(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T537_Emulator_VerifySignedUserCanSaveAWishList : T537_MobileBase
    {
        public T537_Emulator_VerifySignedUserCanSaveAWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void SignedUserCanSaveAWishList(string config) => Validate(config);

    }


    /// <summary>
    /// Verify that a signed-in user can save a Wish List.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-6468
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T537
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-6468"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T537")]
    //[Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    public abstract class T537_MobileBase : TestsBaseMobile
    {
        protected T537_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFunctionalTest(config);

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