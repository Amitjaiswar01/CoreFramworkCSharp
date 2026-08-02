using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Utilities.Environment;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Automation.Framework;
using Automation.Framework.Core;
using Automation.Framework.Core.ScreenCapturer;
using Automation.Framework.Exceptions;
using LampsPlus.AutomationFramework.Services;
using LampsPlus.AutomationFramework.Utilities;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.VisualRegressionTests
{
    public class VisualTestsBase : TestsBase
    {
        private readonly FixtureBase _fixtureBase;

        public VisualTestsBase(ITestOutputHelper output, FixtureBase fixtureBase) : base(output)
        {
            _fixtureBase = fixtureBase;
        }

        protected void IsBaselineTestPassed(bool status)
        {
            if (TestSetup.TestConfiguration.IsBaseLine)
            {
                _fixtureBase.IsBaselinePassed = status;
            }
        }

        protected void Validate(Action<string> validate, string config)
        {
            validate(config);
            IsBaselineTestPassed(true);
        }

        public static IEnumerable<object[]> RepeatVisualTest(string baselineConfig, string targetConfig) => Enumerable.Range(1, 10).Select(x => new List<object[]>{ new object[] { baselineConfig }, new object[] { targetConfig } }).SelectMany(i => i).ToArray();

        public void VisualAccountSetup(string config, bool useEmployeeManagerAccount)
        {
            TestSetup.AccountConfig.AccountUnderTest = _fixtureBase.GetAccountUnderTest(config, useEmployeeManagerAccount);
        }

        public void InitializeVisualTest(string config, string initialUrl = "", bool disposeBrowser = true, bool skipGlobalSetup = false, bool useEmployeeManagerAccount = false, AccountConfiguration accountConfiguration = null, bool skipHomePageNav = false, bool emptyCart = false, bool isVisualInstanceSwitchTest = false)
        {

            LampsPlusAccount accountUnderTest = null;
            TestSetup = new TestSetup(config, initialUrl, useEmployeeManagerAccount, accountUnderTest, false);

            //If Baseline test fails but not Skipped, the Target test will be failed. If Baseline Skipped, Target will be Skipped as well with the same Skip message.
            if (!TestSetup.TestConfiguration.IsBaseLine)
            {
                Log.Message($"Is Baseline test passed:  {_fixtureBase.IsBaselinePassed}");

                if (_fixtureBase.IsBaselineSkipped)
                {
                    Skip.If(_fixtureBase.IsBaselineSkipped, _fixtureBase.SkipMessage);
                }
                else if (!_fixtureBase.IsBaselinePassed && !_fixtureBase.IsBaselineSkipped)
                {
                    throw new FrameworkVisualTestsException("Baseline visual test failed and comparison test shouldn't be executed");
                }
            }

            if (accountConfiguration != null)
            {
                TestSetup.AccountConfig.KeepMeLoggedIn = accountConfiguration.KeepMeLoggedIn;
                TestSetup.AccountConfig.StoreInSessionStoreNumber = accountConfiguration.StoreInSessionStoreNumber;
                TestSetup.AccountConfig.ClearStoreInSessionOnSetup = accountConfiguration.ClearStoreInSessionOnSetup;
                TestSetup.AccountConfig.ClearStoreInSessionOnTearDown = accountConfiguration.ClearStoreInSessionOnTearDown;
                TestSetup.AccountConfig.ClearSavedPaymentOptionsOnSetup = accountConfiguration.ClearSavedPaymentOptionsOnSetup;
                TestSetup.AccountConfig.ClearSavedShippingAddressOnSetup = accountConfiguration.ClearSavedShippingAddressOnSetup;
            }

            //Get fix versions of baseline and target for visual Batch name
            BaselineFixVersion = _fixtureBase.BaselineFixVersion;
            TargetFixVersion = _fixtureBase.TargetFixVersion;

            base.InitializeFramework(config, disposeBrowserAfterTest: disposeBrowser, visualTestAccount: true, setup: TestSetup, isInstanceSwitchMobile: isVisualInstanceSwitchTest);

            if (base.IsLpInstanceSwitchForMobileTest) return; //Exit method if LP instance switch mobile test.

            //Initialize Screen capturer
            ScreenCapturer = TestSetup.TestConfiguration.UseAppiumDriver ? (IScreenCapturer) new ApplitoolsScreenCapturerAppium((Browser)Browser, Log, BaselineFixVersion, TargetFixVersion, Settings) :
                new ApplitoolsScreenCapturer((Browser)Browser, Log, BaselineFixVersion, TargetFixVersion, Settings);

            VisualAccountSetup(config, useEmployeeManagerAccount);//TODO Visual Account setup based on 'IsDbClust': DBclust or DBtest

            if (!TestSetup.TestConfiguration.IsBaseLine)
            {
                UserAccountManagerService.ClearUserAssets(TestSetup.AccountConfig.AccountUnderTest.UserName);
            }

            if (!skipGlobalSetup)
            {
                GlobalSetupWorkflow.Setup(skipHomePageNav);
            }
        }

        //Visual test Dispose
        public override void Dispose()
        {
            if (base.IsLpInstanceSwitchForMobileTest) return; //Exit method if LP instance switch mobile test.
            if (TestSetup.TestConfiguration.IsBaseLine)
            {
                Log.Message($"FixtureBase.IsBaselinePassed: {_fixtureBase.IsBaselinePassed}");
                DisposeMethod(ScreenCapturer.IsCaptureFailed);
            }
            else
            {
                DisposeMethod();
            }
        }

    }
}
