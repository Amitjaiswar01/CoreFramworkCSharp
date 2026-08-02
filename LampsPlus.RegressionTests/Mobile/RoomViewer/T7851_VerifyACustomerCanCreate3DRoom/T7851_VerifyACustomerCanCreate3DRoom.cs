using xRetry;
using Xunit;
using Xunit.Abstractions;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using Automation.Framework.Core;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Mobile.RoomViewer.T7851_VerifyACustomerCanCreate3DRoom
{
    public class T7851_VerifyUserCanCreate3DRoom
    {
        [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
        public class T7851_IPhone_VerifyUserCanCreate3DRoom : T7851_MobileBase
        {
            public T7851_IPhone_VerifyUserCanCreate3DRoom(ITestOutputHelper output) : base(output) { }

            [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
            [RetryTheory(3)]
            [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
            public void VerifyUserCanCreate3DRoom(string config) => Validate(config);
        }


        [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
        public class T7851_Emulator_VerifyUserCanCreate3DRoom : T7851_MobileBase
        {
            public T7851_Emulator_VerifyUserCanCreate3DRoom(ITestOutputHelper output) : base(output) { }

            [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
            [SkippableTheory]
            [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
            public void VerifyUserCanCreate3DRoom(string config) => Validate(config);
        }


        /// <summary>
        /// Verify a Customer can Create a 3D Room
        /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10144
        /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7851
        /// </summary>
        [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
        [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10144"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7851")]
        public abstract class T7851_MobileBase : TestsBaseMobile
        {
            protected T7851_MobileBase(ITestOutputHelper output) : base(output) { }

            protected void Validate(string config)
            {
                /*Arrangement
                User is signed in as a consumer
                User is having short sku with 3d Ar eligibility
                */
                InitializeFunctionalTest(config);
                var shortSku = ProductActions.GetAugmentedReality2DAnd3DSku;

                // Act: User has navigated to PDP that has Room Viewer option and clicked on the View in your room button and selected 3dAR
                ProductDetail.NavigateToProductDetailByShortSku(shortSku);
                Assert.True(ProductDetail.IsCurrentPage, "Current Page is Not Product Detail Page");
                ProductDetail.ClickOnViewInYourRoom();
                Assert.True(RoomViewer.IsArPageContentVisibleFor3d(), "Ar Page not loaded properly");
                RoomViewer.Open3DViewer();

                // Assert : Verify 3d Room Viewer is displayed or not 
                if (Browser.Device != null)
                {
                    if (Browser.Device.IsIphone)
                    {
                        ((IphoneBrowser)Browser).SwitchToNativeContext((AppiumDriver<AppiumWebElement>)Browser.Driver); //Switch to iOS Native context
                        bool roomViewerVisible = Browser.Wait.IsVisibleElement(By.XPath("//XCUIElementTypeWebView[@name='WebView']"),20);
                        ((IphoneBrowser)Browser).SwitchToWebViewContext((AppiumDriver<AppiumWebElement>)Browser.Driver); //Switch back to iOS WebView context

                        Assert.True(roomViewerVisible, "3d Room Viewer is not visible");
                    }
                    else
                    {
                        Log.Message("3D Room Viewer is Not Applicable for Emulator and Android Devices.");
                    }
                }
            }
        }
    }
}
