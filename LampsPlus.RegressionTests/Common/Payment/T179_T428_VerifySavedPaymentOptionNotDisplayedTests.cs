using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.Payment;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Common.Payment
{
    //[Collection(LpTraits.BatchGroup.Common.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T179_Windows_VerifySavedPymtOptNotAvailForUsers : T179_DesktopBase
    {
        public T179_Windows_VerifySavedPymtOptNotAvailForUsers(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void SavedPymtOptNotAvailForUsers(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    public class T179_Windows_Employee_VerifySavedPymtOptNotAvailForUsers : T179_DesktopBase
    {
        public T179_Windows_Employee_VerifySavedPymtOptNotAvailForUsers(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void SavedPymtOptNotAvailForUsers(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T179_Mac_VerifySavedPymtOptNotAvailForUsers : T179_DesktopBase
    {
        public T179_Mac_VerifySavedPymtOptNotAvailForUsers(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void SavedPymtOptNotAvailForUsers(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T179_iPad_VerifySavedPymtOptNotAvailForUsers : T179_DesktopBase
    {
        public T179_iPad_VerifySavedPymtOptNotAvailForUsers(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void SavedPymtOptNotAvailForUsers(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T179_TabletEmulator_VerifySavedPymtOptNotAvailForUsers : T179_DesktopBase
    {
        public T179_TabletEmulator_VerifySavedPymtOptNotAvailForUsers(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void SavedPymtOptNotAvailForUsers(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Payment)]
    public class T428_iPhone_VerifySavedPymtOptNotAvailForUsers : T428_MobileBase
    {
        public T428_iPhone_VerifySavedPymtOptNotAvailForUsers(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void SavedPymtOptNotAvailForUsers(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T428_Emulator_VerifySavedPymtOptNotAvailForUsers : T428_MobileBase
    {
        public T428_Emulator_VerifySavedPymtOptNotAvailForUsers(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void SavedPymtOptNotAvailForUsers(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the saved payment option isn't available for certain user roles.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5513
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T179
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5513"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T179")]
    public abstract class T179_DesktopBase : T179_T428_Base
    {
        protected T179_DesktopBase(ITestOutputHelper output) : base(output) { }
    }


    /// <summary>
    /// Verify the saved payment option isn't available for certain user roles.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5480
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T428
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5480"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T428")]
    public abstract class T428_MobileBase : T179_T428_Base
    {
        protected T428_MobileBase(ITestOutputHelper output) : base(output) { }
    }


    public abstract class T179_T428_Base : PaymentTestsBase
    {
        protected T179_T428_Base(ITestOutputHelper output) : base(output) { }
        
        protected void Validate(string config)
        {
            InitializeFramework(config);

            ShoppingCartWorkflow.ProceedToPaymentWithSingleProduct();

            Assert.False(Payment.IsSavedPaymentsElementVisible, "Saved Payment Area should not be displayed for Anonymous users.");
        }
    }
}
