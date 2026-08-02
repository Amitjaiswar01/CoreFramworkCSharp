using System;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.AddingToCartAndWishList.T348_T540_VerifyUserCanOpenSavedWishList
{
    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T348_Windows_VerifyUserCanOpenSavedWishList : T348_DesktopBase
    {
        public T348_Windows_VerifyUserCanOpenSavedWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void UserCanOpenSavedWishList(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T348_Windows_Employee_VerifyUserCanOpenSavedWishList : T348_DesktopBase
    {
        public T348_Windows_Employee_VerifyUserCanOpenSavedWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void UserCanOpenSavedWishList(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T348_Windows_Kiosk_VerifyUserCanOpenSavedWishList : T348_DesktopBase
    {
        public T348_Windows_Kiosk_VerifyUserCanOpenSavedWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_UNSI)]
        public void UserCanOpenSavedWishList(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T348_Mac_VerifyUserCanOpenSavedWishList : T348_DesktopBase
    {
        public T348_Mac_VerifyUserCanOpenSavedWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T348. Rework - ACD-10883")]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void UserCanOpenSavedWishList(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T348_iPad_VerifyUserCanOpenSavedWishList : T348_DesktopBase
    {
        public T348_iPad_VerifyUserCanOpenSavedWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void UserCanOpenSavedWishList(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T348_TabletEmulator_VerifyUserCanOpenSavedWishList : T348_DesktopBase
    {
        public T348_TabletEmulator_VerifyUserCanOpenSavedWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void UserCanOpenSavedWishList(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the user can open a saved Wish List.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5522
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T348
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5522"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T348")]
    public abstract class T348_DesktopBase : TestsBaseDesktop
    {
        protected T348_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            /*Arrangement
            User is signed in as a consumer
            User has previously saved a Wish List
            User is on the Wish List page
            */
            InitializeFunctionalTest(config, Urls.WishListPageUrl);
            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "P", "This test can only be executed against DBCLUST.");
            WishList.EmptyWishList();
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

            //Data clean up.
            WishList.EmptyWishList();
        }
    }
}