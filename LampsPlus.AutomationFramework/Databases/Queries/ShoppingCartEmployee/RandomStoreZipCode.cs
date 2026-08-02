namespace LampsPlus.AutomationFramework.Databases.Queries.ShoppingCartEmployee
{
    /// <summary>
    /// Query to identify random store zipcode.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T139
    /// </summary>
    public class RandomStoreZipCode
    {
        public const string Query = @"
                                     SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									 
									 SELECT TOP 1 LocZip
                                     FROM [Carteasy].[dbo].[TblLocations]
                                     WHERE exclude = 0 and istemporarilyclosed = 0
                                     order by newid()
                                    ";
    }
}