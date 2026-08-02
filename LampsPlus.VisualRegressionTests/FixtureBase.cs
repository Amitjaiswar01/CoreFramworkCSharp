using System;
using System.Collections.Generic;
using System.Configuration;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Enums;
using LampsPlus.AutomationFramework.Services;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.AutomationFramework.Workflow.Base;

namespace LampsPlus.VisualRegressionTests
{
    public class FixtureBase : IDisposable
    {
        private readonly Dictionary<string, LampsPlusAccount> _userAccountDict = new Dictionary<string, LampsPlusAccount>();

        protected ProductActions ProductActions { get; }
        protected AccountActions AccountActions { get; }
        protected ShoppingCartActions ShoppingCartActions { get; }
        protected OrderActions OrderActions { get; }
        protected SortActions SortActions { get; }

        public string TargetFixVersion;

        public string BaselineFixVersion;

        public int ThumbnailNumber;

        public bool IsBaselinePassed = false;
        public bool IsBaselineSkipped = false;
        public string SkipMessage;


        public FixtureBase()
        {
            //Get environments
            var environments = ConfigurationManager.AppSettings["TargetInstance"].Split('.');
            var baselineInstance = environments[0].Trim();
            var targetInstance = environments[1].Trim();
            DatabaseConnectionStringsManager databaseConnectionStrings;

            //Get fix version of website for Target
            if (targetInstance.Equals("C"))
            {
                DenvStringParser parserString = new DenvStringParser();
                var devEnvInformationTarget = parserString.Parse(EnvironmentUnderTest.Target, targetInstance);
                TargetFixVersion = devEnvInformationTarget.FixVersion;
                databaseConnectionStrings = new DatabaseConnectionStringsManager(devEnvInformationTarget.DatabaseString);
            }
            else
            {
                var environmentResolver = new EnvironmentResolver(EnvironmentUnderTest.Target, false);
                var parser = new DenvJsonParser(environmentResolver.VisualProxyIpAddress);
                var devEnvInformationTarget = new DevEnvInformation(EnvironmentUnderTest.Target, parser);
                TargetFixVersion = devEnvInformationTarget.FixVersion;
                databaseConnectionStrings = new DatabaseConnectionStringsManager(devEnvInformationTarget.DatabaseString);
            }

            //Get fix version of website for Baseline
            if (baselineInstance.Equals("C"))
            {
                DenvStringParser parserString = new DenvStringParser();
                var devEnvInformationBaseline = parserString.Parse(EnvironmentUnderTest.Baseline, baselineInstance);
                BaselineFixVersion = devEnvInformationBaseline.FixVersion;
            }
            else
            {
                var environmentResolverBaseline = new EnvironmentResolver(EnvironmentUnderTest.Baseline, false);
                var parserBaseline = new DenvJsonParser(environmentResolverBaseline.VisualProxyIpAddress);
                var devEnvInformationBaseline = new DevEnvInformation(EnvironmentUnderTest.Baseline, parserBaseline);
                BaselineFixVersion = devEnvInformationBaseline.FixVersion;
            }

            ProductActions = new ProductActions(databaseConnectionStrings.CartEasyConnectionString, databaseConnectionStrings.ProductsConnectionString, databaseConnectionStrings.ProdutMicroServicesConnectionString);
            AccountActions = new AccountActions(databaseConnectionStrings.CartEasyConnectionString);
            ShoppingCartActions = new ShoppingCartActions(databaseConnectionStrings.CartEasyConnectionString, databaseConnectionStrings.AssetsConnectionString);
            OrderActions = new OrderActions(databaseConnectionStrings.CartEasyConnectionString, databaseConnectionStrings.AssetsConnectionString,
                                            databaseConnectionStrings.DomExportOrderConnectionString, databaseConnectionStrings.UserProfileConnectionString);
            SortActions = new SortActions(databaseConnectionStrings.AssetsConnectionString, databaseConnectionStrings.ProductsConnectionString, databaseConnectionStrings.CartEasyConnectionString);
        }

        public LampsPlusAccount GetAccountUnderTest(string config, bool useEmployeeManagerAccount)
        {
            var userRole = new TestConfigurationModel(config).UserRole;
            var key = GetDictionaryKey(userRole, useEmployeeManagerAccount);

            if (!_userAccountDict.ContainsKey(key))
                _userAccountDict.Add(key, SignInWorkflowBase.GetDefaultLoginTypeByUserRole(userRole, useEmployeeManagerAccount));

            return _userAccountDict[key];
        }

        private string GetDictionaryKey(UserRole userRole, bool useEmployeeManagerAccount)
        {
            return $"{userRole.ToString()}_{useEmployeeManagerAccount.ToString()}";
        }

        public virtual void Dispose()
        {
            foreach (var key in _userAccountDict.Keys)
            {
                UserAccountManagerService.ReleaseUser(_userAccountDict[key].UserName);
            }
        }
    }
}