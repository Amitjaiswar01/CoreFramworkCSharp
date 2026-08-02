using System;
using System.Collections.Generic;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.ProductDetail
{
    //[Collection(LpTraits.BatchGroup.Desktop.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T239_Windows_VerifyChatOptionAvailabilityTests : T239_DesktopBase
    {
        public T239_Windows_VerifyChatOptionAvailabilityTests(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void HeaderFooterLinks(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T239_Mac_VerifyChatOptionAvailabilityTests : T234_DesktopBase
    {
        public T239_Mac_VerifyChatOptionAvailabilityTests(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void HeaderFooterLinks(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T239_iPad_VerifyChatOptionAvailabilityTests : T234_DesktopBase
    {
        public T239_iPad_VerifyChatOptionAvailabilityTests(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void HeaderFooterLinks(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T239_TabletEmulator_VerifyChatOptionAvailabilityTests : T234_DesktopBase
    {
        public T239_TabletEmulator_VerifyChatOptionAvailabilityTests(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void HeaderFooterLinks(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the chat option is available for non-ESI roles.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5392
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T239
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5392"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T239")]

    public abstract class T239_DesktopBase : ProductDetailTestsBase
    {
        protected T239_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config);

            var shortSku = ProductActions.GetAnySkuWithProductDetailPage;

            Assert.DatabaseObject(shortSku, "ProductActions.GetAnySkuWithProductDetailPage()");

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            Browser.Wait.ForDomReady();

            var day = DateTime.Now.DayOfWeek.ToString();
            var nowTime = DateTime.Now.TimeOfDay;

            var start1 = new TimeSpan(04, 00, 00);
            var end1 = new TimeSpan(20, 00, 00);

            var start2 = new TimeSpan(07, 00, 00);
            var end2 = new TimeSpan(16, 30, 00);
                        
            nowTime = nowTime.Seconds > 45 ? new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute + 1, 00) : new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 00);
                
            if((day == "Saturday") || (day == "Sunday"))
            {
                if(DateTimeHelper.IsTimeInBetween(start2, end2, nowTime))
                {
                    CheckElements();
                }
                else
                {
                    Skip.IfNot(DateTime.Now.TimeOfDay > nowTime, "Chat is outside available hours.");
                }
            }
            else
            {
                if (DateTimeHelper.IsTimeInBetween(start1, end1, nowTime))
                {
                    CheckElements();
                }
                else
                {
                    Skip.IfNot(DateTime.Now.TimeOfDay > nowTime, "Chat is outside available hours.");
                }
            }
        }

        public void CheckElements()
        {
            var element = new KeyValuePair<IElement, string>(ProductDetail.ChatButtonLink, "Product Description");
            VerifyChatButton(element.Key, element.Value);

            Browser.Wait.ForClickableElement(ProductDetail.ProductHelp).Click();
            Browser.Wait.ForClickableElement(ProductDetail.NeedHelpChat).Click();

            element = new KeyValuePair<IElement, string>(ProductDetail.NeedHelpChat, "Need Help Chat");
            VerifyChatButton(element.Key, element.Value);

            Browser.Wait.ForClickableElement(ProductDetail.CloseNeedHelp).Click();

            element = new KeyValuePair<IElement, string>(ProductDetail.DesignChatLink, "Design Chat");
            VerifyChatButton(element.Key, element.Value);

            Browser.ScrollToBottomOfPage(ProductDetail.PdAddToCartStickyId);

            element = new KeyValuePair<IElement, string>(HeaderFooter.FooterChatLink, "Footer");
            VerifyChatButton(element.Key, element.Value);

        }

        private void VerifyChatButton(IElement element, string chatButtonName)
        {
            Browser.Wait.ForClickableElement(element).Click();

            Browser.Wait.IsVisibleElement(By.ClassName(HeaderFooter.WidgetFloatingWrapperClass));

            Assert.Displayed(ProductDetail.VirtualAssistantContainer, $"{chatButtonName} chat is not displayed.");

            ProductDetail.CloseVirtualAssistant();

            Assert.False(ProductDetail.IsVirtualAssistantCloseIconVisible, $"{RecurringDataIssue}BoldChat Close Icon should not be displayed.");
        }
    }
}
