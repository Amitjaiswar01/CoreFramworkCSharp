using System.Collections.Generic;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Enums;
using LampsPlus.AutomationFramework.Services;

namespace LampsPlus.AutomationFramework.Utilities.Environment
{
    /// <summary>
    /// Helper to get application information from the https://www.lampsplus.com/denv.aspx?j=1 page such as the instance name, database,
    /// and version information of the software under test.
    /// </summary>
	public class DevEnvInformation
    {        
        private static readonly object Lock = new object();
        private readonly EnvironmentUnderTest _currentEnvironment;
        private readonly IDenvParser _denvParser;
        private static readonly Dictionary<EnvironmentUnderTest, EnvironmentInformation> EnvironmentInfoDict = new Dictionary<EnvironmentUnderTest, EnvironmentInformation>();

        /// <summary>
        /// Symbol of the database (T, P, T2).
        /// </summary>
        public string DatabaseSymbol => EnvironmentInfoDict[_currentEnvironment].DatabaseSymbol;

        /// <summary>
        /// Instance designator for example A, B, C,...
        /// </summary>
        public string InstanceName => EnvironmentInfoDict[_currentEnvironment].InstanceName;

        /// <summary>
        /// Version of the PSS being used by the website.
        /// </summary>
        public string PssVersion => EnvironmentInfoDict[_currentEnvironment].PssVersion;

        /// <summary>
        /// Version of the search provider being used by the website.
        /// </summary>
        public string SearchProviderVersion => EnvironmentInfoDict[_currentEnvironment].SearchProviderVersion;

        /// <summary>
        /// Version of the website.
        /// </summary>
        public string FixVersion => EnvironmentInfoDict[_currentEnvironment].FixVersion;
       

        public bool IsProductionInstance => EnvironmentInfoDict[_currentEnvironment].IsProductionInstance;

        /// <summary>
        /// Return the prod database string when true. Otherwise return the test database string.
        /// </summary>
        public string DatabaseString => EnvironmentInfoDict[_currentEnvironment].DatabaseString;

        /// <summary>
        /// Helper to get application information from the https://www.lampsplus.com/denv.aspx?j=1 page such as the instance name, database,
        /// and version information of the software under test.
        /// </summary>
        /// TODO: how to pass log object from the fixture
        public DevEnvInformation(EnvironmentUnderTest currentEnvironment, IDenvParser denvParser)
        {
            _currentEnvironment = currentEnvironment;
            _denvParser = denvParser;           

            Initialize();
        }

        private void Initialize()
        {
            if (CheckInitialized()) return;
           
            lock (Lock)
            {
                if (CheckInitialized()) return;
               
                try
                {
                    var environmentInformation = _denvParser.Parse(Urls.DevEnvPageUrl);
                    EnvironmentInfoDict.Add(_currentEnvironment, environmentInformation);                    
                }
                catch
                {
                    throw;                    
                }
            }
        }

        private bool CheckInitialized()
        {
            return EnvironmentInfoDict.ContainsKey(_currentEnvironment);
        }
        
        public void LogInformation(Log log)
        {
            log.Message($"Log Message from {GetType().Name}");
            log.Message($"Log Page {Urls.DevEnvPageUrl}");
            log.Message($"Instance name: {InstanceName}");
            log.Message($"Database: {DatabaseSymbol}");
            log.Message($"PSS Version: {PssVersion}");
            log.Message($"Search Provider Version: {SearchProviderVersion}");
            log.Message($"LP FixVersion: {FixVersion}");
            log.Message($"Is Production Instance: {IsProductionInstance}");
        }
    }
}

