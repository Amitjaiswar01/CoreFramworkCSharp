namespace LampsPlus.AutomationFramework.Utilities.Environment
{
    public class EnvironmentInformation
    {
        public string DatabaseSymbol { get; set; }
        public string InstanceName { get; set; }
        public string PssVersion { get; set; }
        public string SearchProviderVersion { get; set; }
        public bool IsProductionInstance { get; set; }
        public string FixVersion { get; set; }

        public string DatabaseString
        {
            get
            {
                switch (DatabaseSymbol)
                {
                    case "P":
                        return "clust";
                    case "T":
                        return "test";
                    case "T2":
                    case "unknown":
                        return "test2";
                    default:
                        return string.Empty;
                }
            }
        }
    }
}

