using System;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;
using Skip = Xunit.Skip;
using xRetry;

namespace LampsPlus.RegressionTests.Common.AddingToCartAndWishList.T348_T540_VerifyUserCanOpenSavedWishList
{
    public class T540_VerifyUserCanOpenSavedWishList
    {
        [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.AddingToCartAndWishList)]
        //[Collection(LpTraits.BatchGroup.Mobile.AddingToCartAndWishList)]
        public class T540_IPhone_VerifyUserCanOpenSavedWishList : T540_MobileBase
        {
            public T540_IPhone_VerifyUserCanOpenSavedWishList(ITestOutputHelper output) : base(output) { }

            [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
            [SkippableTheory]
            [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
            public void VerifyUserCanOpenSavedWishList(string config) => Validate(config);
        }


        [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
        //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
        public class T540_Emulator_VerifyUserCanOpenSavedWishList : T540_MobileBase
        {
            public T540_Emulator_VerifyUserCanOpenSavedWishList(ITestOutputHelper output) : base(output) { }

            [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
            [SkippableTheory]
            [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
            public void VerifyUserCanOpenSavedWishList(string config) => Validate(config);
        }


        /// <summary>
        /// Verify the user can open a saved Wish List.
        /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5522
        /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T348
        /// </summary>
        [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
        [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-6469"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T540")]
        public abstract class T540_MobileBase : TestsBaseMobile
        {
            protected T540_MobileBase(ITestOutputHelper output) : base(output) { }

            protected void Validate(string config)
            {
                /*Arrangement
                User is signed in as a consumer
                User has previously saved a Wish List
                User is on the Wish List page
                */
                InitializeFunctionalTest(config);
                Skip.IfNot(DevEnvInformation.DatabaseSymbol == "P", "This test can only be executed against DBCLUST.");
                WishListWorkflow.AddSingleItemToWishList();
                Assert.True(WishList.IsCurrentPage, "Current page is not WishList page");

                var originalWishListSku = WishList.GetWishListItemSku();
                var newWishListName = $"{WishListTypes.WishListNames.NewWishList}{DateTime.Now}";
                var createWishListName = $"{WishListTypes.WishListNames.CreateWishList}{DateTime.Now}";

                WishList.RenameWishList(newWishListName);
                WishList.CreateWishList(createWishListName);

                //Act. On the Wish List page, click on the "Open List" button.
                WishList.OpenWishList();
                
                WishList.SelectWishListItemByName(newWishListName);

                //Assert. The Wish List with the correct saved products is loaded.
                var openWishListSku = WishList.GetWishListItemSku();

                Assert.True(WishList.CompareWishListItems(originalWishListSku, openWishListSku), "Original Skus added to Wish List do not match Wish list that is open.");
            }
        }
    }
}
