namespace LampsPlus.AutomationFramework.Databases.Queries.Certona
{
    /// <summary>
    /// Query to find a store location. 
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7608 
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7609
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7610
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7611
    /// </summary>

    public class StoreLocation
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 locnumber,
                                    locaddress, 
                                    loccity,
                                    StoreName, 
                                    locstate, 
                                    loczip, 
                                    locphone, 
                                    locsms 
                                    FROM carteasy.dbo.tbllocations
                                    WHERE exclude = 0
                                          AND istemporarilyclosed = 0 
                                          AND locsms IS NOT NULL -- (7/7/23) Added so any record with a NULL value on DBTEST is not selected.
                                    ORDER BY Newid()  
                                    ";
    }
}
