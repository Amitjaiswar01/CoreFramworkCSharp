namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to return store phone number based on the city.
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7660
    /// </summary>
    public class ReturnStorePhoneNumberByCity
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT locnumber,
                                    loccity,
                                    locsms
                                    FROM carteasy.dbo.tbllocations
                                    WHERE  loccity = @city  
                                    ";
    }
}
