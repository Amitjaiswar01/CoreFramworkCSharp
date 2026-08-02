namespace LampsPlus.AutomationFramework.Utilities.Environment
{
    public class DatabaseConnectionStringsManager
    {
        private readonly string _databaseString;
        public DatabaseConnectionStringsManager(string databaseString)
        {
            _databaseString = databaseString;
        }
        
        public string CartEasyConnectionString => $"Server='prod1_db{_databaseString}'; Database='carteasy'; User ID='lpsqlrw1';Password='lp8!7E+m3'";
              
        public string AssetsConnectionString => $"Server='prod1_db{_databaseString}'; Database='assets'; User ID='lpsqlrw1';Password='lp8!7E+m3'";
                
        public string DomExportOrderConnectionString => $"Server='prod1_db{_databaseString}'; Database='domexportorder'; User ID='lpsqlrw1';Password='lp8!7E+m3'";

        public string ProductsConnectionString => $"Server='prod1_db{_databaseString}'; Database='Products'; User ID='lpsqlrw1'; Password='lp8!7E+m3'";

        public string UserProfileConnectionString => $"Server='prod1_db{_databaseString}'; Database='UserProfile'; User ID='lpsqlrw1'; Password='lp8!7E+m3'";
        public string ProdutMicroServicesConnectionString => $"Server='prod1_db{_databaseString}'; Database='ProductMicroservices'; User ID='lpsqlrw1'; Password='lp8!7E+m3'";

    }
}
