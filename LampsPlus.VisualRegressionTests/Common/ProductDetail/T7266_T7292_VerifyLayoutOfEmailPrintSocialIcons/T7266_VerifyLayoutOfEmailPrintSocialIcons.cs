using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;


namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7266_T7292_VerifyLayoutOfEmailPrintSocialIcons
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    //[Collection(LpTraits.UserRole.CustomerKiosk)]
    public class T7266_Windows_VerifyLayoutOfEmailPrintSocialIconsForKiosk : T7266_DesktopBase
    {
        public T7266_Windows_VerifyLayoutOfEmailPrintSocialIconsForKiosk(ITestOutputHelper output, T7266_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_UNSI)]
        public void LayoutOfEmailPrintSocialIcons(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    //[Collection(LpTraits.UserRole.EmployeeKiosk)]
    public class T7266_Windows_VerifyLayoutOfEmailPrintSocialIconsForKioskEmp : T7266_DesktopBase
    {
        public T7266_Windows_VerifyLayoutOfEmailPrintSocialIconsForKioskEmp(ITestOutputHelper output, T7266_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI)]
        public void LayoutOfEmailPrintSocialIcons(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7266_Windows_VerifyLayoutOfEmailPrintSocialIcons : T7266_DesktopBase
    {
        public T7266_Windows_VerifyLayoutOfEmailPrintSocialIcons(ITestOutputHelper output, T7266_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfEmailPrintSocialIcons(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7266_Mac_VerifyLayoutOfEmailPrintSocialIcons : T7266_DesktopBase
    {
        public T7266_Mac_VerifyLayoutOfEmailPrintSocialIcons(ITestOutputHelper output, T7266_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfEmailPrintSocialIcons(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    //[Collection(LpTraits.UserRole.CustomerKiosk)]
    public class T7266_Mac_VerifyLayoutOfEmailPrintSocialIconsForKiosk : T7266_DesktopBase
    {
        public T7266_Mac_VerifyLayoutOfEmailPrintSocialIconsForKiosk(ITestOutputHelper output, T7266_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SIS_UNSI)]
        public void LayoutOfEmailPrintSocialIcons(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    //[Collection(LpTraits.UserRole.EmployeeKiosk)]
    public class T7266_Mac_VerifyLayoutOfEmailPrintSocialIconsForKioskEmp : T7266_DesktopBase
    {
        public T7266_Mac_VerifyLayoutOfEmailPrintSocialIconsForKioskEmp(ITestOutputHelper output, T7266_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SIS_ESI)]
        public void LayoutOfEmailPrintSocialIcons(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7266_iPad_VerifyLayoutOfEmailPrintSocialIcons : T7266_DesktopBase
    {
        public T7266_iPad_VerifyLayoutOfEmailPrintSocialIcons(ITestOutputHelper output, T7266_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfEmailPrintSocialIcons(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7266_TabletEmulator_VerifyLayoutOfEmailPrintSocialIcons : T7266_DesktopBase
    {
        public T7266_TabletEmulator_VerifyLayoutOfEmailPrintSocialIcons(ITestOutputHelper output, T7266_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfEmailPrintSocialIcons(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    //[Collection(LpTraits.UserRole.CustomerKiosk)]
    public class T7266_iPad_VerifyLayoutOfEmailPrintSocialIconsForKiosk : T7266_DesktopBase
    {
        public T7266_iPad_VerifyLayoutOfEmailPrintSocialIconsForKiosk(ITestOutputHelper output, T7266_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SIS_UNSI)]
        public void LayoutOfEmailPrintSocialIcons(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    //[Collection(LpTraits.UserRole.CustomerKiosk)]
    public class T7266_TabletEmulator_VerifyLayoutOfEmailPrintSocialIconsForKiosk : T7266_DesktopBase
    {
        public T7266_TabletEmulator_VerifyLayoutOfEmailPrintSocialIconsForKiosk(ITestOutputHelper output, T7266_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_UNSI)]
        public void LayoutOfEmailPrintSocialIcons(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    //[Collection(LpTraits.UserRole.EmployeeKiosk)]
    public class T7266_iPad_VerifyLayoutOfEmailPrintSocialIconsForKioskEmp : T7266_DesktopBase
    {
        public T7266_iPad_VerifyLayoutOfEmailPrintSocialIconsForKioskEmp(ITestOutputHelper output, T7266_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SIS_ESI)]
        public void LayoutOfEmailPrintSocialIcons(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    //[Collection(LpTraits.UserRole.EmployeeKiosk)]
    public class T7266_TabletEmulator_VerifyLayoutOfEmailPrintSocialIconsForKioskEmp : T7266_DesktopBase
    {
        public T7266_TabletEmulator_VerifyLayoutOfEmailPrintSocialIconsForKioskEmp(ITestOutputHelper output, T7266_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_ESI)]
        public void LayoutOfEmailPrintSocialIcons(string config) => Validate(Validate, config);
    }

    public class T7266_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7266_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetFreeShippingAndReturnShortSkus;
        }
    }

    /// <summary>
    /// Verify the layout of Email and Print icons, Social Media Icons, Free Shipping & Free Returns Callout, Check Stock / Check Availability link and Inventory Information on the PDP.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7360
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7266
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7360"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7266")]
    public abstract class T7266_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7266_SharedSku_Fixture>
    {
        protected readonly T7266_SharedSku_Fixture Fixture;

        protected T7266_DesktopBase(ITestOutputHelper output, T7266_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: User has identified a SKU.
            InitializeVisualTest(config);
            var shortSku = Fixture.ShortSku;
            Assert.DatabaseObject(shortSku, "ProductActions.GetFreeShippingAndReturnShortSkus()");

            //Act: Load the PDP page.
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            //Act: Capture a screenshot of the product image and information to just below the social media icons, but ignore the Ships Today verbiage.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.IgnoreStockCheckWrapper(),ProductDetail.IgnoreQuestionsAndAnswersSection(),ProductDetail.IgnoreReviewsSection() }, true, true, ProductDetail.IgnoreStockCheckWrapper(), 10, 0, 10);
        }
    }
}