namespace LampsPlus.AutomationFramework.Databases.Queries.Shipping
{
    /// <summary>
    /// Query to verify the address information of Item 1 and Item 2 as entered on shipping page.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7317
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7318
    /// </summary>
    public class SavedAddressByCartId
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT shortsku, 
                                       shiptofirstname, 
                                       shiptolastname, 
                                       shiptoaddressline1, 
                                       shiptoaddressline2, 
                                       shiptocity, 
                                       shiptostate, 
                                       shiptozipcode,
                                       shiptocountry,
                                       shiptophonenumber
                                    FROM assets.dbo.tblcartshareditems 
                                    WHERE cartid = @cartId  
                                    ";
    }
}
